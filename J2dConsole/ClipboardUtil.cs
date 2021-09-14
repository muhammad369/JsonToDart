using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextCopy;

namespace J2dConsole
{
    public static class ClipboardUtil
    {
        public static async Task<string> getTextAsync()
        {
            try
            {
                return await ClipboardService.GetTextAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


        public static async Task<bool> setTextAsync(string text)
        {
            try
            {
                await ClipboardService.SetTextAsync(text);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
