using System.Runtime.InteropServices;
using System.Text;

namespace BCDTools;
#pragma warning disable
internal class Program
{
    const string BOOTMGR_GUID = "{9dea862c-5cdd-4e70-acc1-f32b344d4795}";
    const string BOOTLOADER_SETTINGS_GUID = "{6efb52bf-1766-41db-a6b3-0ee5eff72bd7}";
    const string RESUME_LOADER_SETTINGS_GUID = "{1afa9c49-16ab-4a5c-901b-212802da9460}";
    const string MEMTEST_GUID = "{b2721d73-1db4-4c62-bf78-c548a880142d}";
    const int HIVEX_OPEN_WRITE = 4;

    const int REG_SZ = 1;
    const int REG_BINARY = 3;
    const int REG_DWORD = 4;
    const int REG_MULTI_SZ = 7;

    [DllImport("libhivex.so.0")] static extern IntPtr hivex_open(string filename, int flags);
    [DllImport("libhivex.so.0")] static extern int hivex_close(IntPtr h);
    [DllImport("libhivex.so.0")] static extern long hivex_root(IntPtr h);
    [DllImport("libhivex.so.0")] static extern long hivex_node_get_child(IntPtr h, long node, string name);
    [DllImport("libhivex.so.0")] static extern long hivex_node_add_child(IntPtr h, long parent, string name);
    [DllImport("libhivex.so.0")] static extern int hivex_commit(IntPtr h, string filename, int flags);
    [DllImport("libhivex.so.0")] static extern int hivex_node_set_value(IntPtr h, long node, ref HivexValue val, int flags);

    [StructLayout(LayoutKind.Sequential)]
    struct HivexValue
    {
        public IntPtr key;
        public int type;
        public UIntPtr len;
        public IntPtr value;
    }

    static void Main(string[] args)
    {
        Console.WriteLine("\n[BCDTools]> by Dominik Erdinger alias Evolve");

        if (args.Length < 5)
        {
            Console.WriteLine("[HELP]> Usage: BCDTools <bcd-template> <output-bcd> <win-partuuid> <win-diskguid> <win-product-name>");
            Console.WriteLine("[HELP]> Example: BCDTools BCD-NEW /boot/efi/EFI/Microsoft/Boot/BCD 181ff8d7-9047-4769-9ce5-8751737eff58 8749DFBD-42A2-418D-A562-82D8FEC35FD2 \"Windows 11\"");
            return;
        }

        string template_path = args[0], output_path = args[1], win_part_uuid = args[2], win_disk_guid = args[3], product_name = args[4], locale = args.Length > 5 ? args[5] : "cs-CZ";
        string win_loader_guid = $"{{{Guid.NewGuid().ToString()}}}", win_resume_guid = $"{{{Guid.NewGuid().ToString()}}}";

        Console.WriteLine($"[BCDTools]> Loader GUID:  {win_loader_guid}");
        Console.WriteLine($"[BCDTools]> Resume GUID:  {win_resume_guid}");
        Console.WriteLine($"[BCDTools]> Win PARTUUID: {win_part_uuid}");
        Console.WriteLine($"[BCDTools]> Win DISKGUID: {win_disk_guid}");

        Console.WriteLine($"[BCDTools]> Copying template: {template_path} -> {output_path}");
        File.Copy(template_path, output_path, true);

        Console.WriteLine("[BCDTools]> Applying winload.reg template...");
        ApplyWinloadReg(output_path);

        Console.WriteLine($"[BCDTools]> Opening BCD: {output_path}");
        var h = hivex_open(output_path, HIVEX_OPEN_WRITE);
        if (h == IntPtr.Zero) { Console.WriteLine("[ERROR]> Failed to open BCD!"); return; }

        var root = hivex_root(h);
        var objects = hivex_node_get_child(h, root, "Objects");

        byte[] device_data = CreatePartitionDescriptor(win_part_uuid, win_disk_guid);

        Console.WriteLine($"[BCDTools]> Creating Resume Loader...");
        CreateResumeLoader(h, objects, win_resume_guid, device_data, locale);

        Console.WriteLine($"[BCDTools]> Creating Windows Boot Loader...");
        CreateWindowsLoader(h, objects, win_loader_guid, win_resume_guid, device_data, product_name, locale);

        Console.WriteLine("[BCDTools]> Updating Boot Manager...");
        UpdateBootManager(h, objects, win_loader_guid, win_resume_guid, device_data, locale);

        Console.WriteLine("[BCDTools]> Updating Memory Tester...");
        UpdateMemoryTester(h, objects, device_data, locale);

        SetDword(h, root, "System", 1);
        SetString(h, root, "KeyName", "BCD00000000");

        int commit_result = hivex_commit(h, output_path, 0);
        hivex_close(h);

        Console.WriteLine(commit_result == 0 ? $"[BCDTools]> BCD updated successfully!" : $"[ERROR]> Commit failed: {commit_result}");
    }

    static void ApplyWinloadReg(string bcd_path)
    {
        var h = hivex_open(bcd_path, HIVEX_OPEN_WRITE);
        if (h == IntPtr.Zero) { Console.WriteLine("[ERROR]> Failed to open BCD for template!"); return; }

        var root = hivex_root(h);
        var objects = hivex_node_get_child(h, root, "Objects");

        var bm = CreateObject(h, objects, BOOTMGR_GUID, 0x10100002);
        var bm_elem = hivex_node_add_child(h, bm, "Elements");
        hivex_node_add_child(h, bm_elem, "11000001");
        SetStringElement(h, bm_elem, "12000004", "Windows Boot Manager");
        hivex_node_add_child(h, bm_elem, "12000005");
        SetMultiSzElement(h, bm_elem, "14000006", new[] { "{7ea2e1ac-2e61-4728-aaa3-896d9d0a9f0e}" });
        hivex_node_add_child(h, bm_elem, "23000003");
        hivex_node_add_child(h, bm_elem, "23000006");
        hivex_node_add_child(h, bm_elem, "24000001");
        SetMultiSzElement(h, bm_elem, "24000010", new[] { MEMTEST_GUID });
        SetBinaryElement(h, bm_elem, "25000004", new byte[] { 0x1e, 0, 0, 0, 0, 0, 0, 0 });

        var mt = CreateObject(h, objects, MEMTEST_GUID, 0x10200005);
        var mt_elem = hivex_node_add_child(h, mt, "Elements");
        hivex_node_add_child(h, mt_elem, "11000001");
        hivex_node_add_child(h, mt_elem, "12000002");
        SetStringElement(h, mt_elem, "12000004", "Windows Memory Diagnostic");
        hivex_node_add_child(h, mt_elem, "12000005");
        SetMultiSzElement(h, mt_elem, "14000006", new[] { "{7ea2e1ac-2e61-4728-aaa3-896d9d0a9f0e}" });
        SetBinaryElement(h, mt_elem, "1600000b", new byte[] { 0x01 });

        var gs = CreateObject(h, objects, "{0ce4991b-e6b3-4b16-b23c-5e0d9250e5d9}", 0x20100000);
        var gs_elem = hivex_node_add_child(h, gs, "Elements");
        SetBinaryElement(h, gs_elem, "16000020", new byte[] { 0x00 });

        var rs = CreateObject(h, objects, RESUME_LOADER_SETTINGS_GUID, 0x20200004);
        var rs_elem = hivex_node_add_child(h, rs, "Elements");
        SetMultiSzElement(h, rs_elem, "14000006", new[] { "{7ea2e1ac-2e61-4728-aaa3-896d9d0a9f0e}" });

        var bd = CreateObject(h, objects, "{4636856e-540f-4170-a130-a84776f4c654}", 0x20100000);
        var bd_elem = hivex_node_add_child(h, bd, "Elements");
        SetBinaryElement(h, bd_elem, "15000011", new byte[] { 0x04, 0, 0, 0, 0, 0, 0, 0 });

        var ems = CreateObject(h, objects, "{5189b25c-5558-4bf2-bca4-289b11bd29e2}", 0x20100000);
        hivex_node_add_child(h, ems, "Elements");

        var bls = CreateObject(h, objects, BOOTLOADER_SETTINGS_GUID, 0x20200003);
        var bls_elem = hivex_node_add_child(h, bls, "Elements");
        SetMultiSzElement(h, bls_elem, "14000006",
            new[] { "{7ea2e1ac-2e61-4728-aaa3-896d9d0a9f0e}", "{7ff607e0-4395-11db-b0de-0800200c9a66}" });

        var hs = CreateObject(h, objects, "{7ea2e1ac-2e61-4728-aaa3-896d9d0a9f0e}", 0x20100000);
        var hs_elem = hivex_node_add_child(h, hs, "Elements");
        SetMultiSzElement(h, hs_elem, "14000006", new[] {
            "{4636856e-540f-4170-a130-a84776f4c654}",
            "{0ce4991b-e6b3-4b16-b23c-5e0d9250e5d9}",
            "{5189b25c-5558-4bf2-bca4-289b11bd29e2}"
        });

        var bmem = CreateObject(h, objects, "{7ff607e0-4395-11db-b0de-0800200c9a66}", 0x20200003);
        var bmem_elem = hivex_node_add_child(h, bmem, "Elements");
        SetBinaryElement(h, bmem_elem, "250000f3", new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 });
        SetBinaryElement(h, bmem_elem, "250000f4", new byte[] { 0x01, 0, 0, 0, 0, 0, 0, 0 });
        SetBinaryElement(h, bmem_elem, "250000f5", new byte[] { 0x00, 0xc2, 0x01, 0, 0, 0, 0, 0 });

        hivex_commit(h, bcd_path, 0);
        hivex_close(h);
    }

    static long CreateObject(IntPtr h, long objects, string guid, int type)
    {
        var node = hivex_node_add_child(h, objects, guid);
        var desc = hivex_node_add_child(h, node, "Description");
        SetDword(h, desc, "Type", type);
        return node;
    }

    static void CreateResumeLoader(IntPtr h, long objects, string guid, byte[] device_data, string locale)
    {
        var node = hivex_node_add_child(h, objects, guid);
        var desc = hivex_node_add_child(h, node, "Description");
        SetDword(h, desc, "Type", 0x10200004);
        var elem = hivex_node_add_child(h, node, "Elements");

        SetBinaryElement(h, elem, "11000001", device_data);
        SetStringElement(h, elem, "12000002", @"\Windows\system32\winresume.efi");
        SetStringElement(h, elem, "12000004", "Windows Resume Application");
        SetStringElement(h, elem, "12000005", locale);
        SetMultiSzElement(h, elem, "14000006", new[] { RESUME_LOADER_SETTINGS_GUID });
        SetBinaryElement(h, elem, "16000060", new byte[] { 0x01 });
        SetBinaryElement(h, elem, "17000077", new byte[] { 0x75, 0x00, 0x00, 0x15, 0x00, 0x00, 0x00, 0x00 });
        SetBinaryElement(h, elem, "21000001", device_data);
        SetStringElement(h, elem, "22000002", @"\hiberfil.sys");
        SetBinaryElement(h, elem, "25000008", new byte[] { 0x01, 0, 0, 0, 0, 0, 0, 0 });
    }

    static void CreateWindowsLoader(IntPtr h, long objects, string guid, string resume_guid, byte[] device_data, string product_name, string locale)
    {
        var node = hivex_node_add_child(h, objects, guid);
        var desc = hivex_node_add_child(h, node, "Description");
        SetDword(h, desc, "Type", 0x10200003);
        var elem = hivex_node_add_child(h, node, "Elements");

        SetBinaryElement(h, elem, "11000001", device_data);
        SetStringElement(h, elem, "12000002", @"\Windows\system32\winload.efi");
        SetStringElement(h, elem, "12000004", product_name);
        SetStringElement(h, elem, "12000005", locale);
        SetMultiSzElement(h, elem, "14000006", new[] { BOOTLOADER_SETTINGS_GUID });
        SetBinaryElement(h, elem, "16000060", new byte[] { 0x01 });
        SetBinaryElement(h, elem, "17000077", new byte[] { 0x75, 0x00, 0x00, 0x15, 0x00, 0x00, 0x00, 0x00 });
        SetBinaryElement(h, elem, "21000001", device_data);
        SetStringElement(h, elem, "22000002", @"\Windows");
        SetStringElement(h, elem, "23000003", resume_guid);
        SetBinaryElement(h, elem, "25000020", new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 });
        SetBinaryElement(h, elem, "250000c2", new byte[] { 0x01, 0, 0, 0, 0, 0, 0, 0 });
    }

    static void UpdateBootManager(IntPtr h, long objects, string loader_guid, string resume_guid, byte[] device_data, string locale)
    {
        var bm = hivex_node_get_child(h, objects, BOOTMGR_GUID);
        if (bm == 0) return;
        var elem = hivex_node_get_child(h, bm, "Elements");

        var dev = hivex_node_get_child(h, elem, "11000001");
        SetBinary(h, dev, "Element", device_data);

        var path = hivex_node_get_child(h, elem, "12000002");
        if (path == 0) path = hivex_node_add_child(h, elem, "12000002");
        SetString(h, path, "Element", @"\EFI\Microsoft\Boot\bootmgfw.efi");

        var loc = hivex_node_get_child(h, elem, "12000005");
        SetString(h, loc, "Element", locale);

        var def = hivex_node_get_child(h, elem, "23000003");
        SetString(h, def, "Element", loader_guid);

        var res = hivex_node_get_child(h, elem, "23000006");
        SetString(h, res, "Element", resume_guid);

        var dsp = hivex_node_get_child(h, elem, "24000001");
        SetMultiSz(h, dsp, "Element", new[] { loader_guid });
    }

    static void UpdateMemoryTester(IntPtr h, long objects, byte[] device_data, string locale)
    {
        var mt = hivex_node_get_child(h, objects, MEMTEST_GUID);
        if (mt == 0) return;
        var elem = hivex_node_get_child(h, mt, "Elements");

        var dev = hivex_node_get_child(h, elem, "11000001");
        SetBinary(h, dev, "Element", device_data);

        var path = hivex_node_get_child(h, elem, "12000002");
        SetString(h, path, "Element", @"\EFI\Microsoft\Boot\memtest.efi");

        var loc = hivex_node_get_child(h, elem, "12000005");
        SetString(h, loc, "Element", locale);
    }

    static void SetStringElement(IntPtr h, long parent, string name, string value)
    {
        var n = hivex_node_add_child(h, parent, name);
        SetString(h, n, "Element", value);
    }

    static void SetBinaryElement(IntPtr h, long parent, string name, byte[] value)
    {
        var n = hivex_node_add_child(h, parent, name);
        SetBinary(h, n, "Element", value);
    }

    static void SetMultiSzElement(IntPtr h, long parent, string name, string[] values)
    {
        var n = hivex_node_add_child(h, parent, name);
        SetMultiSz(h, n, "Element", values);
    }

    static void SetDword(IntPtr h, long node, string key, int value)
    {
        var key_ptr = Marshal.StringToHGlobalAnsi(key);
        var val_ptr = Marshal.AllocHGlobal(4);
        Marshal.WriteInt32(val_ptr, value);
        var val = new HivexValue { key = key_ptr, type = REG_DWORD, len = (UIntPtr)4, value = val_ptr };
        hivex_node_set_value(h, node, ref val, 0);
        Marshal.FreeHGlobal(key_ptr); Marshal.FreeHGlobal(val_ptr);
    }

    static void SetString(IntPtr h, long node, string key, string value)
    {
        var key_ptr = Marshal.StringToHGlobalAnsi(key);
        var bytes = Encoding.Unicode.GetBytes(value + "\0");
        var val_ptr = Marshal.AllocHGlobal(bytes.Length);
        Marshal.Copy(bytes, 0, val_ptr, bytes.Length);
        var val = new HivexValue { key = key_ptr, type = REG_SZ, len = (UIntPtr)bytes.Length, value = val_ptr };
        hivex_node_set_value(h, node, ref val, 0);
        Marshal.FreeHGlobal(key_ptr); Marshal.FreeHGlobal(val_ptr);
    }

    static void SetMultiSz(IntPtr h, long node, string key, string[] values)
    {
        var sb = new StringBuilder();
        foreach (var v in values) { sb.Append(v); sb.Append('\0'); }
        sb.Append('\0');
        var key_ptr = Marshal.StringToHGlobalAnsi(key);
        var bytes = Encoding.Unicode.GetBytes(sb.ToString());
        var val_ptr = Marshal.AllocHGlobal(bytes.Length);
        Marshal.Copy(bytes, 0, val_ptr, bytes.Length);
        var val = new HivexValue { key = key_ptr, type = REG_MULTI_SZ, len = (UIntPtr)bytes.Length, value = val_ptr };
        hivex_node_set_value(h, node, ref val, 0);
        Marshal.FreeHGlobal(key_ptr); Marshal.FreeHGlobal(val_ptr);
    }

    static void SetBinary(IntPtr h, long node, string key, byte[] data)
    {
        var key_ptr = Marshal.StringToHGlobalAnsi(key);
        var val_ptr = Marshal.AllocHGlobal(data.Length);
        Marshal.Copy(data, 0, val_ptr, data.Length);
        var val = new HivexValue { key = key_ptr, type = REG_BINARY, len = (UIntPtr)data.Length, value = val_ptr };
        hivex_node_set_value(h, node, ref val, 0);
        Marshal.FreeHGlobal(key_ptr); Marshal.FreeHGlobal(val_ptr);
    }

    static byte[] CreatePartitionDescriptor(string part_uuid, string disk_guid)
    {
        var part_bytes = Guid.Parse(part_uuid.Trim('{', '}')).ToByteArray();
        var disk_bytes = Guid.Parse(disk_guid.Trim('{', '}')).ToByteArray();
        var data = new byte[0x58];
        data[0x10] = 0x06;
        data[0x18] = 0x48;

        Buffer.BlockCopy(part_bytes, 0, data, 0x20, 16);
        Buffer.BlockCopy(disk_bytes, 0, data, 0x38, 16);

        return data;
    }
}