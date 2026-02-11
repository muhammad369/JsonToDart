using System;
using System.Text;

namespace J2dConsole
{
    public static class ConsoleUtil
    {
        public static string readConsoleMultiline()
        {
            StringBuilder output = new StringBuilder();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.BackgroundColor = ConsoleColor.DarkCyan;
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
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            //
            Console.Write(text);
            //
            Console.ResetColor();
            Console.WriteLine();
        }

        public static void writeError(string text)
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.BackgroundColor = ConsoleColor.Gray;
            //
            Console.Write(text);
            //
            //
            Console.ResetColor();
            Console.WriteLine();
        }

    }
}