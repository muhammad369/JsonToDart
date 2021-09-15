﻿using JsonToDart;
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
            args = new string[] {"-i" };
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
                await runInteractiveModeAsync();
            }
            else writeHelp();
            //
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

        
        }

        private static async Task runInteractiveModeAsync()
        {
            string input = null, className = null;

            // take the input

            do
            {
                Console.WriteLine("How do you want to input your json ?");
                Console.WriteLine("[f] file [c] clipboard [h] here in the console [x] exit");
                var option = Console.ReadLine();
                if(option == "f")
                {
                    Console.WriteLine("Write the file path and hit Enter:");
                    input = FileUtil.readFile(Console.ReadLine());
                }
                else if(option == "c")
                {
                    input = await ClipboardUtil.getTextAsync();
                }
                else if(option == "h")
                {
                    input = ConsoleUtil.readConsoleMultiline();
                }
                else if(option == "x")
                {
                    return;
                }
            }
            while (input == null);

            // take the class name

            Console.WriteLine("What is the name you prefer for your generated class? Leave empty for default (RootClass)");
            var cn = Console.ReadLine();
            className = string.IsNullOrWhiteSpace(cn)? "RootClass" : cn;

            // process
            var dart = createDartClass(input, className);

            if (dart == null) return;

            // write the output

            do
            {
                Console.WriteLine("How do you want to take the generated dart code?");
                Console.WriteLine("[f] file [c] clipboard [h] here in the console [x] exit");
                var option = Console.ReadLine();
                if (option == "f")
                {
                    Console.WriteLine("Write the file path and hit Enter:");
                    FileUtil.writeFile(Console.ReadLine(), dart);
                }
                else if (option == "c")
                {
                    await ClipboardUtil.setTextAsync(dart);
                }
                else if (option == "h")
                {
                    Console.WriteLine(dart);
                }
                else if (option == "x")
                {
                    return;
                }
            }
            while (true);

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
