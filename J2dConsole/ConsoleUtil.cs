using System;
using System.Text;

namespace J2dConsole
{
    public static class ConsoleUtil
    {
        public static string readConsoleMultiline()
        {
            StringBuilder output = new StringBuilder();
            
            while(true)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                if (!(key.Key == ConsoleKey.Enter && key.Modifiers == ConsoleModifiers.Control))
                {
                    break;
                }
                //
                output.Append(key.KeyChar);
            }

            //
            return output.ToString();
        }
    }
}