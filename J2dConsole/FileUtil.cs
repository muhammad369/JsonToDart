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
                ConsoleUtil.writeError($"Failed to read file: {path}");
                ConsoleUtil.writeError(ex.Message);
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
                ConsoleUtil.writeError($"Failed to write file: {path}");
                ConsoleUtil.writeError(ex.Message);
                return false;
            }
        }
    }
}
