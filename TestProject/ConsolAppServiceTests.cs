using Services;

namespace TestProject
{
    [TestClass]
    public class ConsolAppServiceTests
    {
        [TestMethod]
        [DataRow("Hello world", "Hello world")]
        [DataRow("Hello שלום Hello", "Hello םולש Hello", DisplayName = "heb in between eng")]
        [DataRow("Hello שלום עולם hello", "Hello םלוע םולש hello", DisplayName = "2 heb words  in between eng")]
        [DataRow("e של  e", "e לש  e", DisplayName = "extra spaces between words")]
        [DataRow("e של,e", "e לש,e", DisplayName = "comma instead of space")]
        public void BasicTests(string input, string expected)
        {
            var service = new ConsolAppService();

            var actual = service.FixStringToSupportHebrew(input);

            Assert.AreEqual(expected, actual);
        }
    }
}