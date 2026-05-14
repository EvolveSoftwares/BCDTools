using System.Runtime.InteropServices;
using System.Text;

namespace BCDTools;
#pragma warning disable
internal class Program
{
    const string TEMPLATE_PART_GUID_STR = "85c434af-7860-49b3-85c3-b70316081b7b";
    const string TEMPLATE_DISK_GUID_STR = "20ac3077-f7cd-4c47-95d5-53fbf8b752b5";
    static readonly byte[] TEMPLATE_PART_GUID_BYTES = Guid.Parse(TEMPLATE_PART_GUID_STR).ToByteArray();
    static readonly byte[] TEMPLATE_DISK_GUID_BYTES = Guid.Parse(TEMPLATE_DISK_GUID_STR).ToByteArray();
    const string BCD_EFI_TEMPLATE_B64 = "cmVnZgQAAAAEAAAAN/mSRuLT1wEBAAAAAwAAAAAAAAABAAAAIAAAAAAgAAABAAAAXAA/AD8AXABDADoAXABiAGMAZAAuAGUAZgBpAC4AbABvAGMAYQB0AGUAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAKG4nLvQP+wRi1NSVACjum2huJy70D/sEYtTUlQAo7ptAAAAAKK4nLvQP+wRi1NSVACjum1ybXRtJlOVRuLT1wEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAANj1pJgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAGhiaW4AAAAAABAAAAAAAAAAAAAAN/mSRuLT1wEAAAAAoP///25rLABIuZdG4tPXAQIAAABoDAAAAgAAAAAAAABoAgAA/////wAAAAD/////eAEAAP////8WAAAAAAAAAAAAAAAAAAAAAAAAAAwAAABOZXdTdG9yZVJvb3QAAAAAkAAAAHNrAAB4AQAAeAEAAAEAAAB0AAAAAQAEgEgAAABYAAAAAAAAABQAAAACADQAAgAAAAAAGAA/AA8AAQIAAAAAAAUgAAAAIAIAAAAAFAA/AA8AAQEAAAAAAAUSAAAAAQIAAAAAAAUgAAAAIAIAAAEFAAAAAAAFFQAAAGhdvneBFqWIYQFoAgECAAAAAAAAqP///25rIABIuZdG4tPXAQIAAAAgAAAAAwAAAAAAAADIDgAA/////wAAAAD/////eAEAAP////9MAAAAAAAAAAAAAAAAAAAAAAAAAAcAAABPYmplY3RzAPj///9YBwAA+P///zAGAABw////c2sAAHgBAAB4AQAAIAAAAHQAAAABAASASAAAAFgAAAAAAAAAFAAAAAIANAACAAAAAAAYABkABgABAgAAAAAABSAAAAAgAgAAAAAUAD8ADwABAQAAAAAABRIAAAABAgAAAAAABSAAAAAgAgAAAQUAAAAAAAUVAAAAaF2+d4EWpYhhAWgCAQIAAAAAAACg////bmsgAEi5l0bi09cBAgAAACAAAAAAAAAAAAAAAP//////////AwAAANgGAAB4AQAA/////wAAAAAAAAAAIAAAABgAAAAAAAAACwAAAERlc2NyaXB0aW9uAAAAAADo////bGYCAAgCAABEZXNjEAEAAE9iamXg////dmsHABgAAACgAgAAAQAAAAEAAABLZXlOYW1lAOD///9CAEMARAAwADAAMAAwADAAMAAwADEAAAAAAAAAiP///25rIABIuZdG4tPXAQIAAAAQAQAAAgAAAAAAAAAwBAAA/////wAAAAD/////eAEAAP////8WAAAAAAAAAAAAAAAAAAAAAAAAACYAAAB7OWRlYTg2MmMtNWNkZC00ZTcwLWFjYzEtZjMyYjM0NGQ0Nzk1fQAA8P///wAAAAAAAAAAMjQwMKD///9uayAASLmXRuLT1wECAAAAwAIAAAAAAAAAAAAA//////////8CAAAAqAMAAHgBAAD/////AAAAAAAAAAAgAAAAUAEAAAAAAAALAAAARGVzY3JpcHRpb24AAAAAAPD///+4AwAASAQAAERlc2Pg////dmsEAAQAAIACABAQBAAAAAEAAABUeXBlAAAAAKj///9uayAASLmXRuLT1wECAAAAwAIAAAcAAAAAAAAAYAkAAP////8AAAAA/////3gBAAD/////EAAAAAAAAAAAAAAAAAAAAAAAAAAIAAAARWxlbWVudHPo////bGYCAEgDAABEZXNj2AMAAEVsZW3Y////dmsQAFABAABwBAAAAwAAAAEAAABGaXJtd2FyZVZhcmlhYmxlqP7//wEAAABQAQAAAAAAAAUAAACkAAAA0AAAAIgAAABXSU5ET1dTAAEAAACIAAAAeAAAAEIAQwBEAE8AQgBKAEUAQwBUAD0AewA5AGQAZQBhADgANgAyAGMALQA1AGMAZABkAC0ANABlADcAMAAtAGEAYwBjADEALQBmADMAMgBiADMANAA0AGQANAA3ADkANQB9AAAAMAABAAAAEAAAAAQAAAB//wQAVwBpAG4AZABvAHcAcwAgAEIAbwBvAHQAIABNAGEAbgBhAGcAZQByAAAAAAABAAAAgAAAAAQAAAAEASoAAgAAAACY9gkAAAAAAGAJAAAAAACvNMSFYHizSYXDtwMWCBt7AgIEBEYAXABFAEYASQBcAE0AaQBjAHIAbwBzAG8AZgB0AFwAQgBvAG8AdABcAGIAbwBvAHQAbQBnAGYAdwAuAGUAZgBpAAAAf/8EAAAAAACo////bmsgAEi5l0bi09cBAgAAANgDAAAAAAAAAAAAAP//////////AQAAAHABAAB4AQAA/////wAAAAAAAAAADgAAAFgAAAAAAAAACAAAADExMDAwMDAx+P///0AIAAD4////6AgAAOD///92awcAWAAAAFAGAAADAAAAAQAAAEVsZW1lbnQAoP///wAAAAAAAAAAAAAAAAAAAAAGAAAAAAAAAEgAAAAAAAAArzTEhWB4s0mFw7cDFggbewAAAAAAAAAAdzCsIM33R0yV1VP7+LdStQAAAAAAAAAAAAAAAAAAAAAAAAAA2P///3ZrEAAEAACAAQAAAAQAAAABAAAARmlybXdhcmVNb2RpZmllZPD///+AAgAAsAYAAKAXAACo////bmsgAEi5l0bi09cBAgAAANgDAAAAAAAAAAAAAP//////////AQAAAGgBAAB4AQAA/////wAAAAAAAAAADgAAAEQAAAAAAAAACAAAADEyMDAwMDAy8P///2MAcwAtAEMAWgAAAPj///8YCAAA4P///3ZrBwBEAAAAeAcAAAEAAAABAAAARWxlbWVudAC4////XABFAEYASQBcAE0AaQBjAHIAbwBzAG8AZgB0AFwAQgBvAG8AdABcAGIAbwBvAHQAbQBnAGYAdwAuAGUAZgBpAAAAAACo////bmsgAEi5l0bi09cBAgAAANgDAAAAAAAAAAAAAP//////////AQAAACAGAAB4AQAA/////wAAAAAAAAAADgAAACoAAAAAAAAACAAAADEyMDAwMDA04P///3ZrBwBOAAAAoAkAAAEAAAABADAwRWxlbWVudDD4////UAoAAOD///92awcAKgAAAGAIAAABAAAAAQAAAEVsZW1lbnQA0P///1cAaQBuAGQAbwB3AHMAIABCAG8AbwB0ACAATQBhAG4AYQBnAGUAcgAAAAAAqP///25rIABIuZdG4tPXAQIAAADYAwAAAAAAAAAAAAD//////////wEAAAAoBgAAeAEAAP////8AAAAAAAAAAA4AAAAMAAAAAAAAAAgAAAAxMjAwMDAwNeD///92awcADAAAAEAHAAABAAAAAQAAAEVsZW1lbnQAqP///25rIABIuZdG4tPXAQIAAADYAwAAAAAAAAAAAAD//////////wEAAABQBwAAeAEAAP////8AAAAAAAAAAA4AAABOAAAAAAAAAAgAAAAyMzAwMDAwM8D///9sZgcAyAUAADExMDDoBgAAMTIwMMAHAAAxMjAwkAgAADEyMDAICQAAMjMwMPgJAAAyNDAwyAoAADI1MDCo////ewBkADYANQAzADYANwBjADcALQAzAGYAYgA3AC0AMQAxAGUAYwAtADgAMgA4ADgALQBlAGEAYgA3ADYAZAA3ADYANAAwADUAYQB9AAAAAAAAAAAAqP///25rIABIuZdG4tPXAQIAAADYAwAAAAAAAAAAAAD//////////wEAAAA4CAAAeAEAAP////8AAAAAAAAAAA4AAABQAAAAAAAAAAgAAAAyNDAwMDAwMeD///92awcAUAAAAHAKAAAHAAAAAQAAAEVsZW1lbnQAqP///3sAZAA2ADUAMwA2ADcAYwA3AC0AMwBmAGIANwAtADEAMQBlAGMALQA4ADIAOAA4AC0AZQBhAGIANwA2AGQANwA2ADQAMAA1AGEAfQAAAAAAAAAAAKj///9uayAASLmXRuLT1wECAAAA2AMAAAAAAAAAAAAA//////////8BAAAAUAsAAHgBAAD/////AAAAAAAAAAAOAAAACAAAAAAAAAAIAAAAMjUwMDAwMDTg////dmsHAAgAAABACwAAAwAAAAEAAABFbGVtZW50APD///8eAAAAAAAAAAAAAAD4////IAsAAIj///9uayAASLmXRuLT1wECAAAAEAEAAAIAAAAAAAAAyAwAAP////8AAAAA/////3gBAAD/////FgAAAAAAAAAAAAAAAAAAAAAAAAAmAAAAe2E1YTMwZmEyLTNkMDYtNGU5Zi1iNWY0LWEwMWRmOWQxZmNiYX0AAPj///8oEQAA+P///wgSAAD4////UA8AAKD///9uayAASLmXRuLT1wECAAAAWAsAAAAAAAAAAAAA//////////8BAAAAaAwAAHgBAAD/////AAAAAAAAAAAIAAAABAAAAAAAAAALAAAARGVzY3JpcHRpb24AAAAAAOD///92awQABAAAgAEAEBAEAAAAAQAAAFR5cGUAAAAA+P///0gMAACo////bmsgAEi5l0bi09cBAgAAAFgLAAACAAAAAAAAABAOAAD/////AAAAAP////94AQAA/////xAAAAAAAAAAAAAAAAAAAAAAAAAACAAAAEVsZW1lbnRz6P///2xmAgDoCwAARGVzY3AMAABFbGVtqP///25rIABIuZdG4tPXAQIAAABwDAAAAAAAAAAAAAD//////////wEAAACwDQAAeAEAAP////8AAAAAAAAAAA4AAABQAAAAAAAAAAgAAAAyNDAwMDAwMeD///92awcAUAAAAFgNAAAHAAAAAQAAAEVsZW1lbnQAqP///3sAOQBkAGUAYQA4ADYAMgBjAC0ANQBjAGQAZAAtADQAZQA3ADAALQBhAGMAYwAxAC0AZgAzADIAYgAzADQANABkADQANwA5ADUAfQAAAAAAAAAAAPj///84DQAAqP///25rIABIuZdG4tPXAQIAAABwDAAAAAAAAAAAAAD//////////wEAAABIDgAAeAEAAP////8AAAAAAAAAAA4AAAAIAAAAAAAAAAgAAAAyNTAwMDAwNOj///9sZgIA4AwAADI0MDC4DQAAMjUwMOD///92awcACAAAADgDAAADAAAAAQAAAEVsZW1lbnQA+P///ygOAACI////bmsgAEi5l0bi09cBAgAAABABAAACAAAAAAAAAMgPAAD/////AAAAAP////94AQAA/////xYAAAAAAAAAAAAAAAAAAAAAAAAAJgAAAHtkNjUzNjdjNy0zZmI3LTExZWMtODI4OC1lYWI3NmQ3NjQwNWF9AADY////bGYDAMACAAB7OWRlWAsAAHthNWFQDgAAe2Q2NQAAAAAAAAAAoP///25rIABIuZdG4tPXAQIAAABQDgAAAAAAAAAAAAD//////////wEAAADgCwAAeAEAAP////8AAAAAAAAAAAgAAAAEAAAAAAAAAAsAAABEZXNjcmlwdGlvbgAAAAAA4P///3ZrBAAEAACAAwAgEAQAAAABAAAAVHlwZQAAAACo////bmsgAEi5l0bi09cBAgAAAFAOAAALAAAAAAAAACAVAAD/////AAAAAP////94AQAA/////xAAAAAAAAAAAAAAAAAAAAAAAAAACAAAAEVsZW1lbnRz6P///2xmAgDwDgAARGVzY3APAABFbGVt4P///3ZrBwAuAAAAeBAAAAMAAAABAAAARWxlbWVudABoYmluABAAAAAQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAKj///9uayAASLmXRuLT1wECAAAAcA8AAAAAAAAAAAAA//////////8BAAAAsBAAAHgBAAD/////AAAAAAAAAAAOAAAALgAAAAAAAAAIAAAAMTEwMDAwMDHI////AAAAAAAAAAAAAAAAAAAAAAgAAAAAAAAAHgAAAAAAAAAAAAAAAgAAEgAAAAAAAAAAAAAAAPj////gDwAAqP///25rIABIuZdG4tPXAQIAAABwDwAAAAAAAAAAAAD//////////wEAAADQCwAAeAEAAP////8AAAAAAAAAAA4AAAA8AAAAAAAAAAgAAAAxMjAwMDAwMvD///9jAHMALQBDAFoAAAD4////qBIAAOD///92awcAPAAAAEgRAAABAAAAAQAAAEVsZW1lbnQAwP///1wAVwBpAG4AZABvAHcAcwBcAHMAeQBzAHQAZQBtADMAMgBcAHcAaQBuAGwAbwBhAGQALgBlAGYAaQAAAKj///9uayAASLmXRuLT1wECAAAAcA8AAAAAAAAAAAAA//////////8BAAAA2AsAAHgBAAD/////AAAAAAAAAAAOAAAAHgAAAAAAAAAIAAAAMTIwMDAwMDTg////dmsHAFAAAABgEwAABwAAAAEAMDBFbGVtZW50MPj////gEQAA4P///3ZrBwAeAAAAKBIAAAEAAAABAAAARWxlbWVudADY////VwBpAG4AZABvAHcAcwAgADEAMAAgAFAAcgBvAAAAAAAAAAAAqP///25rIABIuZdG4tPXAQIAAABwDwAAAAAAAAAAAAD//////////wEAAAAgEQAAeAEAAP////8AAAAAAAAAAA4AAAAMAAAAAAAAAAgAAAAxMjAwMDAwNeD///92awcADAAAABARAAABAAAAAQAAAEVsZW1lbnQAqP///25rIABIuZdG4tPXAQIAAABwDwAAAAAAAAAAAAD//////////wEAAAAAEgAAeAEAAP////8AAAAAAAAAAA4AAABQAAAAAAAAAAgAAAAxNDAwMDAwOOD///92awcALgAAAIAVAAADAAAAAQAwMEVsZW1lbnQw+P///yATAADo////XABXAGkAbgBkAG8AdwBzAAAAMDCo////ewBkADYANQAzADYANwBjADgALQAzAGYAYgA3AC0AMQAxAGUAYwAtADgAMgA4ADgALQBlAGEAYgA3ADYAZAA3ADYANAAwADUAYQB9AAAAAAAAAAAAqP///25rIABIuZdG4tPXAQIAAABwDwAAAAAAAAAAAAD//////////wEAAABAFAAAeAEAAP////8AAAAAAAAAAA4AAAAIAAAAAAAAAAgAAAAxNTAwMDA2NuD///92awcACAAAADAUAAADAAAAAQAAAEVsZW1lbnQA8P///wMAAAAAAAAAAAAAAPj///8QFAAAqP///25rIABIuZdG4tPXAQIAAABwDwAAAAAAAAAAAAD//////////wEAAADAFAAAeAEAAP////8AAAAAAAAAAA4AAAABAAAAAAAAAAgAAAAxNjAwMDAwOeD///92awcAAQAAgAEAAAADAAAAAQAAAEVsZW1lbnQA+P///6AUAACo////bmsgAEi5l0bi09cBAgAAAHAPAAAAAAAAAAAAAP//////////AQAAAEATAAB4AQAA/////wAAAAAAAAAADgAAAC4AAAAAAAAACAAAADIxMDAwMDAxoP///2xmCwAgEAAAMTEwMLgQAAAxMjAwiBEAADEyMDBQEgAAMTIwMMgSAAAxNDAwuBMAADE1MDBIFAAAMTYwMMgUAAAyMTAwuBUAADIyMDA4FgAAMjMwMBAXAAAyNTAwyP///wAAAAAAAAAAAAAAAAAAAAAIAAAAAAAAAB4AAAAAAAAAAAAAAAIAACIAAAAAAAAAAAAAAACo////bmsgAEi5l0bi09cBAgAAAHAPAAAAAAAAAAAAAP//////////AQAAADAWAAB4AQAA/////wAAAAAAAAAADgAAABIAAAAAAAAACAAAADIyMDAwMDAy4P///3ZrBwASAAAASBMAAAEAAAABAAAARWxlbWVudAD4////EBYAAKj///9uayAASLmXRuLT1wECAAAAcA8AAAAAAAAAAAAA//////////8BAAAACBcAAHgBAAD/////AAAAAAAAAAAOAAAATgAAAAAAAAAIAAAAMjMwMDAwMDPg////dmsHAE4AAACwFgAAAQAAAAEAAABFbGVtZW50AKj///97AGIAYgA5AGMAYgA2AGUANwAtADMAZgBkADAALQAxADEAZQBjAC0AOABiADUAMwAtADgAMAA2AGUANgBmADYAZQA2ADkANgAzAH0AAAAAAAAAAAD4////kBYAAKj///9uayAASLmXRuLT1wECAAAAcA8AAAAAAAAAAAAA//////////8BAAAAmBcAAHgBAAD/////AAAAAAAAAAAOAAAACAAAAAAAAAAIAAAAMjUwMDAwYzLg////dmsHAAgAAACIFwAAAwAAAAEAAABFbGVtZW50APD///8BAAAAAAAAAAAAAAD4////aBcAAOD///92awYABAAAgAEAAAAEAAAAAQAAAFN5c3RlbQAAQAgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";

    static void Main(string[] args)
    {
        Console.WriteLine("\n[BCDT]> by Dominik Erdinger alias Evolve");
        Console.WriteLine("[BCDT]> Universal BCD template approach\n");

        if (args.Length < 1)
        {
            Help();
            return;
        }

        switch (args[0])
        {
            case "-d":
                if (args.Length < 3) { Help(); return; }
                RunDiagnostics(args[1], args[2]);
                break;

            case "-cefi":
                if (args.Length < 3) { Help(); return; }
                CreateEfiStructure(args[1], args[2]);
                break;

            case "-bcd":
                if (args.Length < 4) { Help(); return; }
                BuildBCD(args[1], args[2], args[3]);
                break;

            case "-deploy":
                if (args.Length < 5) { Help(); return; }
                Deploy(args[1], args[2], args[3], args[4]);
                break;

            default:
                Help();
                break;
        }
    }

    static void Help()
    {
        Console.WriteLine("[HELP]> Usage:");
        Console.WriteLine("  BCDTools -d <efi-path> <windows-path>");
        Console.WriteLine("      Diagnostics - check BCD, files, registry");
        Console.WriteLine();
        Console.WriteLine("  BCDTools -cefi <efi-path> <windows-path>");
        Console.WriteLine("      Copy EFI files (bootmgfw, Fonts, Resources) to EFI partition");
        Console.WriteLine();
        Console.WriteLine("  BCDTools -bcd <output-bcd> <efi-partuuid> <disk-guid>");
        Console.WriteLine("      Create BCD from universal template, patch GUIDs");
        Console.WriteLine();
        Console.WriteLine("  BCDTools -deploy <efi-path> <windows-path> <efi-partuuid> <disk-guid>");
        Console.WriteLine("      Full deployment: copy EFI files + create BCD + create Recovery BCD");
    }

    static void Deploy(string efi_path, string windows_path, string efi_partuuid, string disk_guid)
    {
        Console.WriteLine("[DEPLOY]> ====== FULL DEPLOYMENT ======\n");

        Console.WriteLine("[DEPLOY]> Step 1: Creating EFI structure...");
        CreateEfiStructure(efi_path, windows_path);
        Console.WriteLine();

        Console.WriteLine("[DEPLOY]> Step 2: Creating main BCD...");
        string main_bcd = $"{efi_path}/EFI/Microsoft/Boot/BCD";
        BuildBCD(main_bcd, efi_partuuid, disk_guid);
        Console.WriteLine();

        Console.WriteLine("[DEPLOY]> Step 3: Copying BCD to Recovery...");
        string recovery_bcd = $"{efi_path}/EFI/Microsoft/Recovery/BCD";
        Directory.CreateDirectory(Path.GetDirectoryName(recovery_bcd)!);
        File.Copy(main_bcd, recovery_bcd, true);
        Console.WriteLine($"[OK]   Recovery BCD: {recovery_bcd}");
        Console.WriteLine();

        Console.WriteLine("[DEPLOY]> ====== DEPLOYMENT COMPLETE ======");
        Console.WriteLine($"[DEPLOY]> Run: BCDTools -d {efi_path} {windows_path}");
        Console.WriteLine($"[DEPLOY]> for verification.");
    }

    static void BuildBCD(string output_path, string efi_partuuid, string disk_guid)
    {
        Console.WriteLine($"[BCD]> Decoding universal BCD template...");

        byte[] template = Convert.FromBase64String(BCD_EFI_TEMPLATE_B64);

        Console.WriteLine($"[BCD]> Template size: {template.Length} bytes");
        Console.WriteLine($"[BCD]> Target Partition GUID: {efi_partuuid}");
        Console.WriteLine($"[BCD]> Target Disk GUID:      {disk_guid}");

        byte[] new_part_bytes, new_disk_bytes;
        try
        {
            new_part_bytes = Guid.Parse(efi_partuuid.Trim('{', '}')).ToByteArray();
            new_disk_bytes = Guid.Parse(disk_guid.Trim('{', '}')).ToByteArray();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERROR]> Invalid GUID: {ex.Message}");
            return;
        }

        int part_replacements = ReplaceBytes(template, TEMPLATE_PART_GUID_BYTES, new_part_bytes);
        int disk_replacements = ReplaceBytes(template, TEMPLATE_DISK_GUID_BYTES, new_disk_bytes);

        Console.WriteLine($"[BCD]> Patched {part_replacements}x partition GUID, {disk_replacements}x disk GUID");

        if (part_replacements == 0)
            Console.WriteLine("[WARN]> No partition GUID replacements - template may be corrupted!");
        if (disk_replacements == 0)
            Console.WriteLine("[WARN]> No disk GUID replacements - template may be corrupted!");

        Directory.CreateDirectory(Path.GetDirectoryName(output_path)!);
        File.WriteAllBytes(output_path, template);
        Console.WriteLine($"[OK]   BCD written to: {output_path} ({template.Length} bytes)");
    }

    static int ReplaceBytes(byte[] data, byte[] needle, byte[] replacement)
    {
        if (needle.Length != replacement.Length)
            throw new ArgumentException("Needle and replacement must be same length");

        int count = 0;
        for (int i = 0; i <= data.Length - needle.Length; i++)
        {
            bool match = true;
            for (int j = 0; j < needle.Length; j++)
            {
                if (data[i + j] != needle[j]) { match = false; break; }
            }
            if (match)
            {
                Buffer.BlockCopy(replacement, 0, data, i, replacement.Length);
                count++;
                i += needle.Length - 1;
            }
        }
        return count;
    }
    static void CreateEfiStructure(string efi_path, string windows_path)
    {
        Console.WriteLine($"[CEFI]> Creating EFI structure at {efi_path}");

        string efi_root = ResolveOrCreatePath(efi_path, "EFI");
        Directory.CreateDirectory(efi_root);

        string efi_ms = ResolveOrCreatePath(efi_root, "Microsoft");
        Directory.CreateDirectory(efi_ms);

        string efi_boot = ResolveOrCreatePath(efi_ms, "Boot");
        string efi_recovery = ResolveOrCreatePath(efi_ms, "Recovery");
        string efi_default = ResolveOrCreatePath(efi_root, "Boot");

        string win_boot_efi = $"{windows_path}/Windows/Boot/EFI";
        string win_boot_fonts = $"{windows_path}/Windows/Boot/Fonts";
        string win_boot_resources = $"{windows_path}/Windows/Boot/Resources";

        Console.WriteLine("[CEFI]> Cleaning paths...");
        SafeDeletePath(efi_boot);
        SafeDeletePath(efi_recovery);

        Console.WriteLine("[CEFI]> Creating directories...");
        Directory.CreateDirectory(efi_boot);
        Directory.CreateDirectory(efi_recovery);
        Directory.CreateDirectory(efi_default);

        Console.WriteLine("[CEFI]> Copying EFI boot files...");
        if (Directory.Exists(win_boot_efi))
            CopyDirectory(win_boot_efi, efi_boot);

        if (Directory.Exists(win_boot_fonts))
        {
            Console.WriteLine("[CEFI]> Copying Fonts...");
            CopyDirectory(win_boot_fonts, $"{efi_boot}/Fonts");
        }

        if (Directory.Exists(win_boot_resources))
        {
            Console.WriteLine("[CEFI]> Copying Resources...");
            CopyDirectory(win_boot_resources, $"{efi_boot}/Resources");
        }

        Console.WriteLine("[CEFI]> Creating default EFI bootloader...");
        string? bootmgfw_src = FindFileCaseInsensitive(win_boot_efi, "bootmgfw.efi");
        if (bootmgfw_src != null)
        {
            string bootx64_dst = Path.Combine(efi_default, "bootx64.efi");
            File.Copy(bootmgfw_src, bootx64_dst, true);
            Console.WriteLine($"[OK]   {bootx64_dst}");
        }

        Console.WriteLine("[CEFI]> EFI structure created!");
    }
    static string ResolveOrCreatePath(string parent, string name)
    {
        if (Directory.Exists(parent))
        {
            var match = Directory.GetFileSystemEntries(parent)
                .FirstOrDefault(e => string.Equals(Path.GetFileName(e), name, StringComparison.OrdinalIgnoreCase));
            if (match != null) return match;
        }
        return Path.Combine(parent, name);
    }
    static void SafeDeletePath(string path)
    {
        try
        {
            if (File.Exists(path))
            {
                File.SetAttributes(path, FileAttributes.Normal);
                File.Delete(path);
                Console.WriteLine($"[CEFI]> Deleted file: {path}");
            }
            else if (Directory.Exists(path))
            {
                foreach (var file in Directory.GetFiles(path, "*", SearchOption.AllDirectories))
                {
                    try { File.SetAttributes(file, FileAttributes.Normal); } catch { }
                }
                Directory.Delete(path, recursive: true);
                Console.WriteLine($"[CEFI]> Deleted directory: {path}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[WARN]> Failed to delete {path}: {ex.Message}");
        }
    }
    static string? FindFileCaseInsensitive(string dir, string fileName)
    {
        if (!Directory.Exists(dir)) return null;
        return Directory.GetFiles(dir)
            .FirstOrDefault(f => string.Equals(Path.GetFileName(f), fileName, StringComparison.OrdinalIgnoreCase));
    }

    static void CopyDirectory(string source, string dest)
    {
        Directory.CreateDirectory(dest);
        foreach (var file in Directory.GetFiles(source))
        {
            string fileName = Path.GetFileName(file);
            string destFile = Path.Combine(dest, fileName);
            File.Copy(file, destFile, true);
        }
        foreach (var dir in Directory.GetDirectories(source))
        {
            string dirName = Path.GetFileName(dir);
            string destDir = Path.Combine(dest, dirName);
            CopyDirectory(dir, destDir);
        }
    }

    [DllImport("libhivex.so.0")] static extern IntPtr hivex_open(string filename, int flags);
    [DllImport("libhivex.so.0")] static extern int hivex_close(IntPtr h);
    [DllImport("libhivex.so.0")] static extern long hivex_root(IntPtr h);
    [DllImport("libhivex.so.0")] static extern long hivex_node_get_child(IntPtr h, long node, string name);
    [DllImport("libhivex.so.0")] static extern IntPtr hivex_node_children(IntPtr h, long node);
    [DllImport("libhivex.so.0")] static extern IntPtr hivex_node_name(IntPtr h, long node);
    [DllImport("libhivex.so.0")] static extern IntPtr hivex_value_value(IntPtr h, long val, out int t, out IntPtr len);
    [DllImport("libhivex.so.0")] static extern long hivex_node_get_value(IntPtr h, long node, string key);

    const int HIVEX_OPEN_READ = 0;

    static void RunDiagnostics(string efi_path, string windows_path)
    {
        Console.WriteLine("[DIAG]> ====== DIAGNOSTICS ======\n");

        Console.WriteLine("[DIAG]> --- Files ---");
        CheckFile(efi_path, "EFI/Microsoft/Boot/bootmgfw.efi", "EFI bootmgfw.efi");
        CheckFile(efi_path, "EFI/Microsoft/Boot/BCD", "EFI BCD");
        CheckFile(efi_path, "EFI/Microsoft/Recovery/BCD", "Recovery BCD");
        CheckFile(efi_path, "EFI/Boot/bootx64.efi", "Default EFI bootx64.efi");
        CheckDir(efi_path, "EFI/Microsoft/Boot/Fonts", "EFI Fonts");
        CheckDir(efi_path, "EFI/Microsoft/Boot/Resources", "EFI Resources");
        CheckFile(windows_path, "Windows/System32/winload.efi", "Windows winload.efi");
        CheckFile(windows_path, "Windows/System32/ntoskrnl.exe", "Windows ntoskrnl.exe");
        CheckFile(windows_path, "Windows/System32/config/SYSTEM", "SYSTEM hive");
        Console.WriteLine();

        string? bcdPath = ResolveCaseInsensitive(efi_path, "EFI/Microsoft/Boot/BCD");
        string? sysHivePath = ResolveCaseInsensitive(windows_path, "Windows/System32/config/SYSTEM");

        Console.WriteLine("[DIAG]> --- BCD analysis ---");
        if (bcdPath != null) AnalyzeBCD(bcdPath);
        else Console.WriteLine("[ERROR] BCD not found");
        Console.WriteLine();

        Console.WriteLine("[DIAG]> --- Windows registry ---");
        if (sysHivePath != null) AnalyzeSystemHive(sysHivePath);
        else Console.WriteLine("[ERROR] SYSTEM hive not found");
        Console.WriteLine();

        Console.WriteLine("[DIAG]> --- Done ---");
    }

    static string? ResolveCaseInsensitive(string root, string relPath)
    {
        if (!Directory.Exists(root)) return null;
        var parts = relPath.Replace('\\', '/').Split('/', StringSplitOptions.RemoveEmptyEntries);
        string current = root;
        foreach (var part in parts)
        {
            if (!Directory.Exists(current)) return null;
            var match = Directory.GetFileSystemEntries(current)
                .FirstOrDefault(e => string.Equals(Path.GetFileName(e), part, StringComparison.OrdinalIgnoreCase));
            if (match == null) return null;
            current = match;
        }
        return current;
    }

    static void CheckFile(string root, string relPath, string desc)
    {
        string? path = ResolveCaseInsensitive(root, relPath);
        if (path != null && File.Exists(path))
            Console.WriteLine($"[OK]   {desc,-35} {new FileInfo(path).Length,12:N0} bytes  {path}");
        else Console.WriteLine($"[MISS] {desc,-35} ---");
    }

    static void CheckDir(string root, string relPath, string desc)
    {
        string? path = ResolveCaseInsensitive(root, relPath);
        if (path != null && Directory.Exists(path))
            Console.WriteLine($"[OK]   {desc,-35} {Directory.GetFiles(path).Length} files  {path}");
        else Console.WriteLine($"[MISS] {desc,-35} ---");

    }

    static void AnalyzeBCD(string bcd_path)
    {
        if (!File.Exists(bcd_path)) { Console.WriteLine("[ERROR] BCD not found"); return; }

        byte[] data = File.ReadAllBytes(bcd_path), type6 = new byte[] { 0x06, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x48, 0x00, 0x00, 0x00 };
        
        int found = 0;
        for (int i = 16; i <= data.Length - 88; i++)
        {
            bool match = true;
            for (int j = 0; j < type6.Length; j++)
            {
                if (data[i + j] != type6[j]) { match = false; break; }
            }
            if (match)
            {
                found++;
                byte[] part = new byte[16], disk = new byte[16];
                Buffer.BlockCopy(data, i + 16, part, 0, 16);
                Buffer.BlockCopy(data, i + 40, disk, 0, 16);
                Guid pg = new Guid(part), dg = new Guid(disk);
                Console.WriteLine($"[BCD]  Partition descriptor #{found}:");
                Console.WriteLine($"         Partition GUID: {pg}");
                Console.WriteLine($"         Disk GUID:      {dg}");
            }
        }

        byte[] type8 = new byte[] { 0x08, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x1e, 0x00, 0x00, 0x00 };
        int found8 = 0;
        for (int i = 16; i <= data.Length - 46; i++)
        {
            bool match = true;
            for (int j = 0; j < type8.Length; j++)
            {
                if (data[i + j] != type8[j]) { match = false; break; }
            }
            if (match)
            {
                found8++;
                Console.WriteLine($"[BCD]  Locate descriptor #{found8}: element_id=0x{BitConverter.ToUInt32(data, i + 20):x8}");
            }
        }

        Console.WriteLine($"[BCD]  Total: {found} Partition descriptors, {found8} Locate descriptors");
    }

    static void AnalyzeSystemHive(string path)
    {
        if (!File.Exists(path)) { Console.WriteLine("[ERROR] SYSTEM hive not found"); return; }

        var h = hivex_open(path, HIVEX_OPEN_READ);
        if (h == IntPtr.Zero) { Console.WriteLine("[ERROR] Cannot open SYSTEM"); return; }

        var root = hivex_root(h);
        var setup = hivex_node_get_child(h, root, "Setup");
        if (setup != 0)
        {
            int sys = ReadDword(h, hivex_node_get_value(h, setup, "SystemSetupInProgress"));
            Console.WriteLine($"[REG]  SystemSetupInProgress: {sys}");
            var status = hivex_node_get_child(h, setup, "Status");
            if (status != 0)
            {
                var sysprep = hivex_node_get_child(h, status, "SysprepStatus");
                if (sysprep != 0)
                {
                    int gen = ReadDword(h, hivex_node_get_value(h, sysprep, "GeneralizationState"));
                    Console.WriteLine($"[REG]  GeneralizationState: {gen} (4 = generalized, ready for setup)");
                }
            }
        }

        hivex_close(h);
    }

    static int ReadDword(IntPtr h, long val)
    {
        if (val == 0) return 0;
        var ptr = hivex_value_value(h, val, out int type, out IntPtr len);
        if (ptr == IntPtr.Zero || (int)len < 4) return 0;
        return Marshal.ReadInt32(ptr);
    }
}