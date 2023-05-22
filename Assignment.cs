using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumExtras.PageObjects;
using OpenQA.Selenium.Support;
using System.Reflection;
using OpenQA.Selenium.Support.UI;

namespace CaseStudyAssignment
{
    public class Assignment
    {
        IWebDriver driver;

        [SetUp]
        public void Initialization()
        {
            driver = new ChromeDriver();
            PageFactory.InitElements(driver, this);
            string path = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
            driver.Navigate().GoToUrl(path + "/WebPage/QE-index.html");
            

        }


        [FindsBy(How = How.XPath, Using="//input[@id='inputEmail']")]
        private IWebElement Email;

        [FindsBy(How = How.XPath, Using = "//input[@id='inputPassword']")]
        private IWebElement Password;

        [FindsBy(How = How.XPath, Using = "//button[text()='Sign in']")]
        private IWebElement SignIn;

        [FindsBy(How = How.XPath, Using = "//ul[@class='list-group']/li")]
        private IList<IWebElement> ListItems;

        [FindsBy(How = How.XPath, Using = "//button[@data-toggle='dropdown']")]
        private IWebElement SelectOptions;

        [FindsBy(How = How.LinkText, Using = "Option 3")]
        private IWebElement Option3;

        [FindsBy(How = How.XPath, Using = "//button[text()='Button']")]
        private IList<IWebElement> SelectButton;

        [FindsBy(How = How.XPath, Using = "//button[@id='test5-button']")]
        private IWebElement SelectButton1;

        [FindsBy(How = How.XPath, Using = "//div[@id='test5-alert']")]
        private IWebElement AlertMessage;
        
        [FindsBy(How = How.XPath, Using = "//tr/td")]
        private IList<IWebElement> Grid;


        [Test]
        public void Test1()
        {
            Assert.IsTrue(Email.Displayed, "Email field is not available!!");
            Assert.IsTrue(Password.Displayed, "Password field is not available");
            Assert.IsTrue(SignIn.Displayed, "SignIn button is not available");

            Email.SendKeys("something@gmail.com");
            Password.SendKeys("Password");
            SignIn.Click();
        }

        [Test]
        public void Test2()
        {
            Assert.IsTrue(ListItems.Count == 3, "List items not found");
            // Approach 1
            // Removing child text from parent text
            string parentText = ListItems[1].Text;
            string childText = ListItems[1].FindElement(By.TagName("span")).Text;
            Console.WriteLine(childText);
            parentText = parentText.Replace(childText, "").Trim();
            Console.WriteLine(parentText + "H");

            Assert.IsTrue(parentText == "List Item 2", "List Item 2 not found");
            
            // Approach 2
            // Using Contains method
            Assert.IsTrue(ListItems[1].Text.Contains("List Item 2"), "List Item 2 not found");
            Assert.IsTrue(ListItems[1].FindElement(By.TagName("span")).Text == "6", "Badge value not found");

        }

        [Test]
        public void Test3()
        {
            Assert.IsTrue(SelectOptions.Text.Contains("Option 1"), "Option 1 is not available");
            SelectOptions.Click();
            Option3.Click();
        }

        [Test]
        public void Test4()
        {
            Assert.IsTrue(SelectButton[0].Enabled, "Button is not enabled");
            Assert.IsFalse(SelectButton[1].Enabled, "Button is not disabled");
        }

        [Test]
        public void Test5()
        {
            
            // Used explicit wait because we are not sure the exact wait time of that particular element to be displayed
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until((dr) => SelectButton1.Displayed);
            SelectButton1.Click();
            Assert.IsTrue(AlertMessage.Text.Contains("You clicked a button!"), "Alert is not displayed");
            Assert.IsFalse(SelectButton1.Enabled, "Button is still in enabled state");


        }

        public string getGridValue(int x, int y)
        {
            if(x == 2)
            {
                return Grid[x + 4 + y].Text;
            }else if(x == 1)
            {
                return Grid[x + 2 + y].Text;
            }

            return Grid[x+y].Text;
        }

        [Test]
        public void Test6()
        {
            string value = getGridValue(2, 2);

            Assert.IsTrue(value == "Ventosanzap", "Cell does not contain 'Ventosanzap'");

            


        }
    }
}