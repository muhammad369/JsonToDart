using System;
using System.Text;

namespace J2dConsole
{
    public static class ConsoleUtil
    {
        public static string readConsoleMultiline()
        {
            StringBuilder output = new StringBuilder();

            Console.BackgroundColor = ConsoleColor.DarkBlue;
            
            while(true)
            {
                string line = Console.ReadLine();
                if (line == "end")
                {
                    Console.ResetColor();
                    break;
                }
                //
                output.AppendLine(line);
            }

            //
            return output.ToString();
        }

        public static void writeText(string text)
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            //
            Console.Write(text);
            //
            Console.ResetColor();
            Console.WriteLine();
        }

        public static void writeError(string text)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            //
            Console.Write(text);
            //
            //
            Console.ResetColor();
            Console.WriteLine();
        }

    }
}