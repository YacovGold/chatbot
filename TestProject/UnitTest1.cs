namespace TestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var x = new DateConversionToJewish.DateConversionToJewishPlugin();
            var t = x.GetHebrewJewishDateString(new DateTime(2022, 09, 19));
            Assert.AreEqual("יום שני כ\"ג אלול תשפ\"ב", t);
        }
    }
}