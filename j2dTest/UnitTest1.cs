using Microsoft.VisualStudio.TestTools.UnitTesting;
using J2dConsole;
using System.Threading.Tasks;
using Selim.Json;

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

        [TestMethod]
        public async Task ConflictingNames()
        {
            var sample = new JsonObject()
                .add("ads", new JsonObject().add("data", new JsonObject()
                    .add("a",3))
                )
                .add("posts", new JsonObject().add("data", new JsonObject()
                    .add("b",5)
                    )
                );

            await ClipboardUtil.setTextAsync(sample.ToString());

            await Program.runClipboardModeAsync(new string[] { "-c" , "class1"});

            var dart = await ClipboardUtil.getTextAsync();

            //Assert.AreEqual(true, dart.StartsWith("class"));

        }

    }
}
