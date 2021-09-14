using JsonToDart;
using Selim.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using TextCopy;

namespace J2dConsole
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            
            Console.WriteLine("DartToJson version 1.0.0");
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
            Console.ReadKey();

        
        }

        private static void runInteractiveMode()
        {
            throw new NotImplementedException();
        }

        public static async Task runClipboardModeAsync(string[] args)
        {
            string input, className = null;

            input = await ClipboardUtil.getTextAsync();
            if (input == null) return;
            //
            className = args.Length > 1 ? args[1] : "RootClass";

            var dart = createDartClass(input, className);

            if (dart == null) return;

            if (!(await ClipboardUtil.setTextAsync(dart))) return;

            Console.WriteLine("Done");
        }

        public static void runFileMode(string[] args)
        {
            string input, className = null;

            input = FileUtil.readFile(args[1]);
            if (input == null) return;

            className = args.Length > 3 ? args[3] : "RootClass";

            var dart = createDartClass(input, className);

            if (dart == null) return;

            if(! FileUtil.writeFile(args[2], dart)) return;

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
