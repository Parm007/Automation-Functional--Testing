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
            baseUrl = "http://localhost:5501/getQuote.html"; // Replace with the actual URL of the web application
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void test1_ValidData_NoAccidents_Age25_Experience3()
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
        public void test2_ValidData_4Accidents_Age25_Experience3()
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


        [Test]
        public void test3_ValidData_2Accidents_Age35_Experience10()
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
            driver.FindElement(By.Id("age")).SendKeys("35");
            driver.FindElement(By.Id("experience")).SendKeys("10");
            driver.FindElement(By.Id("accidents")).SendKeys("2");


            // Click on the submit button
            driver.FindElement(By.Id("btnSubmit")).Click();


            // Verify that the insurance rate is refused as per business requirement
            IWebElement insuranceRateInput = driver.FindElement(By.Id("finalQuote"));
            string insuranceRate = insuranceRateInput.GetAttribute("value");
            Assert.AreEqual("$1600", insuranceRate);
        }


        [Test]
        public void test4_InValidPhone_NoAccidents_Age27_Experience3()
        {
            // Navigate to the web application
            driver.Navigate().GoToUrl(baseUrl);

            // Enter valid data for all mandatory input fields except phone number
            driver.FindElement(By.Id("firstName")).SendKeys("John");
            driver.FindElement(By.Id("lastName")).SendKeys("Doe");
            driver.FindElement(By.Id("address")).SendKeys("123 Main St");
            driver.FindElement(By.Id("city")).SendKeys("Anytown");
            driver.FindElement(By.Id("province")).SendKeys("Ontario");
            driver.FindElement(By.Id("postalCode")).SendKeys("N2L 3G1");
            driver.FindElement(By.Id("phone")).SendKeys("12345"); // invalid phone number
            driver.FindElement(By.Id("email")).SendKeys("johndoe@mail.com");
            driver.FindElement(By.Id("age")).SendKeys("27");
            driver.FindElement(By.Id("experience")).SendKeys("3");
            driver.FindElement(By.Id("accidents")).SendKeys("0");

            // Click on the submit button
            driver.FindElement(By.Id("btnSubmit")).Click();

            // Verify that the error message is displayed for the phone number field
            IWebElement phoneErrorElement = driver.FindElement(By.Id("phone-error"));
            string phoneErrorText = phoneErrorElement.Text;
            Assert.AreEqual("Phone Number must follow the patterns 111-111-1111 or (111)111-1111", phoneErrorText);

            // Verify that the insurance rate provided is correct (should be empty)
            IWebElement insuranceRateInput = driver.FindElement(By.Id("finalQuote"));
            string insuranceRate = insuranceRateInput.GetAttribute("value");
            Assert.AreEqual("", insuranceRate);
        }

        [Test]
        public void test5_InValidEmail_NoAccidents_Age28_Experience3()
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

            // Enter invalid email address
            driver.FindElement(By.Id("email")).SendKeys("johndoemail.com");

            // Enter valid age, driving experience, and accidents data
            driver.FindElement(By.Id("age")).SendKeys("28");
            driver.FindElement(By.Id("experience")).SendKeys("3");
            driver.FindElement(By.Id("accidents")).SendKeys("0");

            // Click on the submit button
            driver.FindElement(By.Id("btnSubmit")).Click();

            // Verify that an error message is displayed for invalid email address
            IWebElement emailError = driver.FindElement(By.Id("email-error"));
            Assert.IsTrue(emailError.Displayed);
            Assert.AreEqual("Must be a valid email address", emailError.Text);

            // Verify that the insurance rate provided is correct (should be 0 since email is invalid)
            IWebElement insuranceRateInput = driver.FindElement(By.Id("finalQuote"));
            string insuranceRate = insuranceRateInput.GetAttribute("value");
            Assert.AreEqual("", insuranceRate);
        }



        [Test]
        public void test6_InValidPostalCode_1Accidents_Age35_Experience17()
        {
            // Navigate to the web application
            driver.Navigate().GoToUrl(baseUrl);

            // Enter valid data for all mandatory input fields except postal code
            driver.FindElement(By.Id("firstName")).SendKeys("John");
            driver.FindElement(By.Id("lastName")).SendKeys("Doe");
            driver.FindElement(By.Id("address")).SendKeys("123 Main St");
            driver.FindElement(By.Id("city")).SendKeys("Anytown");
            driver.FindElement(By.Id("province")).SendKeys("Ontario");
            driver.FindElement(By.Id("postalCode")).SendKeys("INVALID");
            driver.FindElement(By.Id("phone")).SendKeys("(123)123-1234");
            driver.FindElement(By.Id("email")).SendKeys("johndoe@mail.com");
            driver.FindElement(By.Id("age")).SendKeys("35");
            driver.FindElement(By.Id("experience")).SendKeys("17");
            driver.FindElement(By.Id("accidents")).SendKeys("1");

            // Click on the submit button
            driver.FindElement(By.Id("btnSubmit")).Click();

            // Verify that the error message is displayed for invalid postal code
            IWebElement errorMessageElement = driver.FindElement(By.Id("postalCode-error"));
            string errorMessage = errorMessageElement.Text;
            Assert.AreEqual("Postal Code must follow the pattern A1A 1A1", errorMessage);

            // Verify that the insurance rate provided is correct
            IWebElement insuranceRateInput = driver.FindElement(By.Id("finalQuote"));
            string insuranceRate = insuranceRateInput.GetAttribute("value");
            Assert.AreEqual("", insuranceRate);
        }

        [Test]
        public void test7_OmmittedAge_NoAccidents_Experience5()
        {
            // Navigate to the web application
            driver.Navigate().GoToUrl(baseUrl);

            // Enter valid data for all mandatory input fields except age
            driver.FindElement(By.Id("firstName")).SendKeys("John");
            driver.FindElement(By.Id("lastName")).SendKeys("Doe");
            driver.FindElement(By.Id("address")).SendKeys("123 Main St");
            driver.FindElement(By.Id("city")).SendKeys("Anytown");
            driver.FindElement(By.Id("province")).SendKeys("Ontario");
            driver.FindElement(By.Id("postalCode")).SendKeys("N2L 3G1");
            driver.FindElement(By.Id("phone")).SendKeys("(123)123-1234");
            driver.FindElement(By.Id("email")).SendKeys("johndoe@mail.com");
            driver.FindElement(By.Id("experience")).SendKeys("5");
            driver.FindElement(By.Id("accidents")).SendKeys("0");

            // Click on the submit button
            driver.FindElement(By.Id("btnSubmit")).Click();

            // Verify that the age validation error message is displayed
            IWebElement ageValidationMessage = driver.FindElement(By.Id("age-error"));
            Assert.AreEqual("Age (>=16) is required", ageValidationMessage.Text);



            // Click on the submit button again
            driver.FindElement(By.Id("btnSubmit")).Click();

            // Verify that the insurance rate provided is correct
            IWebElement insuranceRateInput = driver.FindElement(By.Id("finalQuote"));
            string insuranceRate = insuranceRateInput.GetAttribute("value");
            Assert.AreEqual("", insuranceRate);
        }


        [Test]
        public void test8_OmmittedAccidents_Age37_Experience8()
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
            driver.FindElement(By.Id("age")).SendKeys("37");
            driver.FindElement(By.Id("experience")).SendKeys("8");

            // Click on the submit button
            driver.FindElement(By.Id("btnSubmit")).Click();

            // Verify that the error message is displayed for the number of at-fault accidents field
            IWebElement accidentsErrorMessage = driver.FindElement(By.Id("accidents-error"));
            string errorMessageText = accidentsErrorMessage.Text;
            Assert.AreEqual("Number of accidents is required", errorMessageText);

            // Enter valid data for the number of at-fault accidents field 

            // Click on the submit button again
            driver.FindElement(By.Id("btnSubmit")).Click();

            // Verify that the insurance rate provided is correct
            IWebElement insuranceRateInput = driver.FindElement(By.Id("finalQuote"));
            string insuranceRate = insuranceRateInput.GetAttribute("value");
            Assert.AreEqual("", insuranceRate);
        }


        [Test]
        public void test9_OmmittedExperience_Age45_NoAccidents()
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
            driver.FindElement(By.Id("age")).SendKeys("45");
            driver.FindElement(By.Id("accidents")).SendKeys("0");

            // Click on the submit button
            driver.FindElement(By.Id("btnSubmit")).Click();

            // Verify that the error message is displayed for the number of at-fault accidents field
            IWebElement accidentsErrorMessage = driver.FindElement(By.Id("experience-error"));
            string errorMessageText = accidentsErrorMessage.Text;
            Assert.AreEqual("Years of experience is required", errorMessageText);

            // Enter valid data for the number of at-fault accidents field 

            // Click on the submit button again
            driver.FindElement(By.Id("btnSubmit")).Click();

            // Verify that the insurance rate provided is correct
            IWebElement insuranceRateInput = driver.FindElement(By.Id("finalQuote"));
            string insuranceRate = insuranceRateInput.GetAttribute("value");
            Assert.AreEqual("", insuranceRate);
        }

        [Test]
        public void test10_OmmittedFirstName_Age45_NoAccidents_Experience5()
        {
            // Navigate to the web application
            driver.Navigate().GoToUrl(baseUrl);

            // Enter valid data for all mandatory input fields
            // driver.FindElement(By.Id("firstName")).SendKeys("John");
            driver.FindElement(By.Id("lastName")).SendKeys("Doe");
            driver.FindElement(By.Id("address")).SendKeys("123 Main St");
            driver.FindElement(By.Id("city")).SendKeys("Anytown");
            driver.FindElement(By.Id("province")).SendKeys("Ontario");
            driver.FindElement(By.Id("postalCode")).SendKeys("N2L 3G1");
            driver.FindElement(By.Id("phone")).SendKeys("(123)123-1234");
            driver.FindElement(By.Id("email")).SendKeys("johndoe@mail.com");
            driver.FindElement(By.Id("age")).SendKeys("45");
            driver.FindElement(By.Id("accidents")).SendKeys("0");
            driver.FindElement(By.Id("experience")).SendKeys("5");


            // Click on the submit button
            driver.FindElement(By.Id("btnSubmit")).Click();

            // Verify that the error message is displayed for the number of at-fault accidents field
            IWebElement accidentsErrorMessage = driver.FindElement(By.Id("firstName-error"));
            string errorMessageText = accidentsErrorMessage.Text;
            Assert.AreEqual("First Name is required", errorMessageText);

            // Enter valid data for the number of at-fault accidents field 

            // Click on the submit button again
            driver.FindElement(By.Id("btnSubmit")).Click();

            // Verify that the insurance rate provided is correct
            IWebElement insuranceRateInput = driver.FindElement(By.Id("finalQuote"));
            string insuranceRate = insuranceRateInput.GetAttribute("value");
            Assert.AreEqual("", insuranceRate);
        }

        [Test]
        public void test11_OmmittedLastName_Age45_NoAccidents_Experience5()
        {
            // Navigate to the web application
            driver.Navigate().GoToUrl(baseUrl);

            // Enter valid data for all mandatory input fields
            driver.FindElement(By.Id("firstName")).SendKeys("John");
            // driver.FindElement(By.Id("lastName")).SendKeys("Doe");
            driver.FindElement(By.Id("address")).SendKeys("123 Main St");
            driver.FindElement(By.Id("city")).SendKeys("Anytown");
            driver.FindElement(By.Id("province")).SendKeys("Ontario");
            driver.FindElement(By.Id("postalCode")).SendKeys("N2L 3G1");
            driver.FindElement(By.Id("phone")).SendKeys("(123)123-1234");
            driver.FindElement(By.Id("email")).SendKeys("johndoe@mail.com");
            driver.FindElement(By.Id("age")).SendKeys("45");
            driver.FindElement(By.Id("accidents")).SendKeys("0");
            driver.FindElement(By.Id("experience")).SendKeys("5");


            // Click on the submit button
            driver.FindElement(By.Id("btnSubmit")).Click();

            // Verify that the error message is displayed for the number of at-fault accidents field
            IWebElement accidentsErrorMessage = driver.FindElement(By.Id("lastname-error"));
            string errorMessageText = accidentsErrorMessage.Text;
            Assert.AreEqual("Last Name is required", errorMessageText);

            // Enter valid data for the number of at-fault accidents field 

            // Click on the submit button again
            driver.FindElement(By.Id("btnSubmit")).Click();

            // Verify that the insurance rate provided is correct
            IWebElement insuranceRateInput = driver.FindElement(By.Id("finalQuote"));
            string insuranceRate = insuranceRateInput.GetAttribute("value");
            Assert.AreEqual("", insuranceRate);
        }


        [Test]
        public void test12_OmmittedAddress_Age45_NoAccidents_Experience5()
        {
            // Navigate to the web application
            driver.Navigate().GoToUrl(baseUrl);

            // Enter valid data for all mandatory input fields
            driver.FindElement(By.Id("firstName")).SendKeys("John");
            driver.FindElement(By.Id("lastName")).SendKeys("Doe");
            // driver.FindElement(By.Id("address")).SendKeys("123 Main St");
            driver.FindElement(By.Id("city")).SendKeys("Anytown");
            driver.FindElement(By.Id("province")).SendKeys("Ontario");
            driver.FindElement(By.Id("postalCode")).SendKeys("N2L 3G1");
            driver.FindElement(By.Id("phone")).SendKeys("(123)123-1234");
            driver.FindElement(By.Id("email")).SendKeys("johndoe@mail.com");
            driver.FindElement(By.Id("age")).SendKeys("45");
            driver.FindElement(By.Id("accidents")).SendKeys("0");
            driver.FindElement(By.Id("experience")).SendKeys("5");


            // Click on the submit button
            driver.FindElement(By.Id("btnSubmit")).Click();

            // Verify that the error message is displayed for the number of at-fault accidents field
            IWebElement accidentsErrorMessage = driver.FindElement(By.Id("address-error"));
            string errorMessageText = accidentsErrorMessage.Text;
            Assert.AreEqual("Address is required", errorMessageText);

            // Enter valid data for the number of at-fault accidents field 

            // Click on the submit button again
            driver.FindElement(By.Id("btnSubmit")).Click();

            // Verify that the insurance rate provided is correct
            IWebElement insuranceRateInput = driver.FindElement(By.Id("finalQuote"));
            string insuranceRate = insuranceRateInput.GetAttribute("value");
            Assert.AreEqual("", insuranceRate);
        }

        [Test]
        public void test13_OmmittedCity_Age45_NoAccidents_Experience5()
        {
            // Navigate to the web application
            driver.Navigate().GoToUrl(baseUrl);

            // Enter valid data for all mandatory input fields
            driver.FindElement(By.Id("firstName")).SendKeys("John");
            driver.FindElement(By.Id("lastName")).SendKeys("Doe");
            driver.FindElement(By.Id("address")).SendKeys("123 Main St");
            // driver.FindElement(By.Id("city")).SendKeys("Anytown");
            driver.FindElement(By.Id("province")).SendKeys("Ontario");
            driver.FindElement(By.Id("postalCode")).SendKeys("N2L 3G1");
            driver.FindElement(By.Id("phone")).SendKeys("(123)123-1234");
            driver.FindElement(By.Id("email")).SendKeys("johndoe@mail.com");
            driver.FindElement(By.Id("age")).SendKeys("45");
            driver.FindElement(By.Id("accidents")).SendKeys("0");
            driver.FindElement(By.Id("experience")).SendKeys("5");


            // Click on the submit button
            driver.FindElement(By.Id("btnSubmit")).Click();

            // Verify that the error message is displayed for the number of at-fault accidents field
            IWebElement accidentsErrorMessage = driver.FindElement(By.Id("city-error"));
            string errorMessageText = accidentsErrorMessage.Text;
            Assert.AreEqual("City is required", errorMessageText);

            // Enter valid data for the number of at-fault accidents field 

            // Click on the submit button again
            driver.FindElement(By.Id("btnSubmit")).Click();

            // Verify that the insurance rate provided is correct
            IWebElement insuranceRateInput = driver.FindElement(By.Id("finalQuote"));
            string insuranceRate = insuranceRateInput.GetAttribute("value");
            Assert.AreEqual("", insuranceRate);
        }

        [Test]
        public void test14_InvalidAge_NoAccidents_Experience5()
        {
            // Navigate to the web application
            driver.Navigate().GoToUrl(baseUrl);

            // Enter valid data for all mandatory input fields except age
            driver.FindElement(By.Id("firstName")).SendKeys("John");
            driver.FindElement(By.Id("lastName")).SendKeys("Doe");
            driver.FindElement(By.Id("address")).SendKeys("123 Main St");
            driver.FindElement(By.Id("age")).SendKeys("12");
            driver.FindElement(By.Id("city")).SendKeys("Anytown");
            driver.FindElement(By.Id("province")).SendKeys("Ontario");
            driver.FindElement(By.Id("postalCode")).SendKeys("N2L 3G1");
            driver.FindElement(By.Id("phone")).SendKeys("(123)123-1234");
            driver.FindElement(By.Id("email")).SendKeys("johndoe@mail.com");
            driver.FindElement(By.Id("experience")).SendKeys("5");
            driver.FindElement(By.Id("accidents")).SendKeys("0");

            // Click on the submit button
            driver.FindElement(By.Id("btnSubmit")).Click();

            // Verify that the age validation error message is displayed
            IWebElement ageValidationMessage = driver.FindElement(By.Id("age-error"));
            Assert.AreEqual("Please enter a value greater than or equal to 16.", ageValidationMessage.Text);



            // Click on the submit button again
            driver.FindElement(By.Id("btnSubmit")).Click();

            // Verify that the insurance rate provided is correct
            IWebElement insuranceRateInput = driver.FindElement(By.Id("finalQuote"));
            string insuranceRate = insuranceRateInput.GetAttribute("value");
            Assert.AreEqual("", insuranceRate);
        }

        [Test]
        public void test15_InvalidExperience_Age27_NoAccidents()
        {
            // Navigate to the web application
            driver.Navigate().GoToUrl(baseUrl);

            // Enter valid data for all mandatory input fields except age
            driver.FindElement(By.Id("firstName")).SendKeys("John");
            driver.FindElement(By.Id("lastName")).SendKeys("Doe");
            driver.FindElement(By.Id("address")).SendKeys("123 Main St");
            driver.FindElement(By.Id("age")).SendKeys("27");
            driver.FindElement(By.Id("city")).SendKeys("Anytown");
            driver.FindElement(By.Id("province")).SendKeys("Ontario");
            driver.FindElement(By.Id("postalCode")).SendKeys("N2L 3G1");
            driver.FindElement(By.Id("phone")).SendKeys("(123)123-1234");
            driver.FindElement(By.Id("email")).SendKeys("johndoe@mail.com");
            driver.FindElement(By.Id("experience")).SendKeys("-10");
            driver.FindElement(By.Id("accidents")).SendKeys("0");

            // Click on the submit button
            driver.FindElement(By.Id("btnSubmit")).Click();

            // Verify that the age validation error message is displayed
            IWebElement ageValidationMessage = driver.FindElement(By.Id("experience-error"));
            Assert.AreEqual("Please enter a value greater than or equal to 0.", ageValidationMessage.Text);



            // Click on the submit button again
            driver.FindElement(By.Id("btnSubmit")).Click();

            // Verify that the insurance rate provided is correct
            IWebElement insuranceRateInput = driver.FindElement(By.Id("finalQuote"));
            string insuranceRate = insuranceRateInput.GetAttribute("value");
            Assert.AreEqual("", insuranceRate);
        }








        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
