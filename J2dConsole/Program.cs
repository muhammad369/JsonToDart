using JsonToDart;
using Selim.Json;
using System;

namespace J2dConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("DartToJson version 1.1.0");
            //
            if(args.Length == 0)
            {
                writeHelp();
            }
            else
            {
                //var input = 
            }

            //
            Console.Read();
        }

        private static void writeHelp()
        {
            
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
