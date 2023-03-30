using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace InsuranceRateCalculatorTests
{
    [TestFixture]
    public class InsuranceRateCalculatorSystemTests
    {
        private IWebDriver driver;
        private string baseUrl;

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            baseUrl = "http://localhost:5500/getQuote.html"; // Replace with the actual URL of the web application
            driver.Manage().Window.Maximize(); 
        } 

        [Test]
        public void TestValidDataWithNoAccidentsAndAge25AndExperience3()
        {
            // Navigate to the web application
            driver.Navigate().GoToUrl(baseUrl);

            // Enter valid data for all mandatory input fields
            driver.FindElement(By.Id("firstName")).SendKeys("John");
            driver.FindElement(By.Id("lastName")).SendKeys("Doe");
            driver.FindElement(By.Id("address")).SendKeys("123 Main St");
            driver.FindElement(By.Id("city")).SendKeys("Anytown");
            driver.FindElement(By.Id("province")).SendKeys("Ontario");
            driver.FindElement(By.Id("postalCode")).SendKeys("N2L 3G1");
            driver.FindElement(By.Id("phone")).SendKeys("(123)123-1234");
            driver.FindElement(By.Id("email")).SendKeys("johndoe@mail.com");
            driver.FindElement(By.Id("age")).SendKeys("25");
            driver.FindElement(By.Id("experience")).SendKeys("3");
            driver.FindElement(By.Id("accidents")).SendKeys("0");

            // Click on the submit button
            driver.FindElement(By.Id("btnSubmit")).Click();

            // Verify that the insurance rate provided is correct
            IWebElement insuranceRateInput = driver.FindElement(By.Id("finalQuote"));
            string insuranceRate = insuranceRateInput.GetAttribute("value");
            Assert.AreEqual("$3500", insuranceRate);


        }

        [Test]
        public void TestValidDataWith4AccidentsAndAge25AndExperience3()
        {
            // Navigate to the web application
            driver.Navigate().GoToUrl(baseUrl);

            driver.FindElement(By.Id("firstName")).SendKeys("John");
            driver.FindElement(By.Id("lastName")).SendKeys("Doe");
            driver.FindElement(By.Id("address")).SendKeys("123 Main St");
            driver.FindElement(By.Id("city")).SendKeys("Anytown");
            driver.FindElement(By.Id("province")).SendKeys("Ontario");
            driver.FindElement(By.Id("postalCode")).SendKeys("N2L 3G1");
            driver.FindElement(By.Id("phone")).SendKeys("(123)123-1234");
            driver.FindElement(By.Id("email")).SendKeys("johndoe@mail.com");
            driver.FindElement(By.Id("age")).SendKeys("25");
            driver.FindElement(By.Id("experience")).SendKeys("3");
            driver.FindElement(By.Id("accidents")).SendKeys("4");


            // Click on the submit button
            driver.FindElement(By.Id("btnSubmit")).Click();


            // Verify that the insurance rate is refused as per business requirement
             IWebElement insuranceRateInput = driver.FindElement(By.Id("finalQuote"));
            string insuranceRate = insuranceRateInput.GetAttribute("value");
            Assert.AreEqual("No Insurance for you!!  Too many accidents - go take a course!", insuranceRate);
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
