using Microsoft.VisualStudio.TestTools.UnitTesting;
using J2dConsole;
using System.Threading.Tasks;

namespace j2dTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task clipboard()
        {
            var sample = $@"{{""a"":1}}";

            await ClipboardUtil.setTextAsync(sample);

            Assert.AreEqual(sample, await ClipboardUtil.getTextAsync());

        }

        [TestMethod]
        public async Task clipboardMode()
        {
            var sample = $@"{{""a"":1}}";

            await ClipboardUtil.setTextAsync(sample);

            await Program.runClipboardModeAsync(new string[] { "-c" , "class1"});

            var dart = await ClipboardUtil.getTextAsync();

            Assert.AreEqual(true, dart.StartsWith("class"));

        }


    }
}
