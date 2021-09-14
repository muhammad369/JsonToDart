using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace J2dConsole
{
    public static class FileUtil
    {
        public static string readFile(string path)
        {
            try
            {
                return File.ReadAllText(path);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to read file: {path}");
                Console.WriteLine(ex.Message);
                return null;
            }
        }


        public static bool writeFile(string path, string content)
        {
            try
            {
                File.WriteAllText(path, content);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write file: {path}");
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
