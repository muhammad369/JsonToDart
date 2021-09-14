using JsonToDart;
using Selim.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace J2dConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("DartToJson version 1.1.0");
            //
            if(args.Length == 0)
            {
                writeHelp();
            }
            else if(args[0] == "-f")
            {
                runFileMode(args);
            }
            else if (args[0] == "-c")
            {
                await runClipboardModeAsync(args);
            }
            else if(args[0] == "-i")
            {
                runInteractiveMode();
            }
            else writeHelp();
            //
            Console.WriteLine("Press any key to exit");
            Console.Read();

        }

        private static void runInteractiveMode()
        {
            throw new NotImplementedException();
        }

        private static async Task runClipboardModeAsync(string[] args)
        {
            string input, className = null;
            try
            {
                input = await Clipboard.GetTextAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
            //
            className = args.Length > 1 ? args[1] : "RootClass";

            var dart = createDartClass(input, className);

            if (dart == null) return;

            try
            {
                await Clipboard.SetTextAsync(dart);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            Console.WriteLine("Done");
        }

        private static void runFileMode(string[] args)
        {
            string input, className = null;
            try
            {
                input = File.ReadAllText(args[1]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            className = args.Length > 3 ? args[3] : "RootClass";

            var dart = createDartClass(input, className);

            if (dart == null) return;

            try
            {
                 File.WriteAllText(args[2], dart);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            Console.WriteLine("Done");

        }

        private static void writeHelp()
        {
            Console.WriteLine("\r\n -f [input file] [(optional) output file] [(optional) class name]\r\n"
                +" to take json input file and write output dart file \r\n");

            Console.WriteLine(" -c [class name]\r\n "
                +"to take input from the clipboard and write to the clipbord\r\n");

            Console.WriteLine(" -i \r\n for the interactive mode\r\n");
        }
        
        static string createDartClass(string jsonText, string className = "RootClass")
        {
            if (String.IsNullOrWhiteSpace(jsonText)) return null;
            //
            try
            {
                var jp = new JsonParser();
                var json = jp.Parse(jsonText);

                if (!(json is JsonObject))
                {
                    Console.WriteLine("must be json object");
                    return null;
                }
                //
                var jsonObject = json as JsonObject;

                
                var generator = new DartClassGenerator();

                return generator.generateDartClass(jsonObject, className);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
