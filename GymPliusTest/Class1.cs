using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System;

namespace ImdbTest
{
    public class Class1
    {
        public static IWebDriver driver;

        //[SetUp]
        //public static void SetUp()
        //{
        //    IWebDriver driver = new ChromeDriver();
        //    driver.Url = "https://www.imdb.com/?ref_=nv_home";
        //}

        [Test]

        public static void BornToday()
        {
            //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            //driver.ExecuteJavaScript("window.scrollBy(0, 200)");

           // IWebElement ClosePopUp = driver.FindElement(By.XPath("//*[@id=\"iconContext-clear\"]/path[2]"));
           // ClosePopUp.Click();

            IWebDriver driver = new ChromeDriver();
            driver.Url = "https://www.imdb.com/?ref_=nv_home";

            IWebElement ClickBurgerMenu = driver.FindElement(By.XPath("//*[@id='imdbHeader-navDrawerOpen']/span"));
            ClickBurgerMenu.Click();

            //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            //wait.Until(driver => driver.FindElement(By.XPath("//*[@id='imdbHeader-navDrawerOpen']/span")).Text=="Celebs"));
            driver.ExecuteJavaScript("window.scrollBy(0, 500)");

            //IWebElement ClickCelebs = driver.FindElement(By.XPath("//*[@id=\"imdbHeader\"]/div[2]/aside/div/div[2]/div/div[5]/span/label/span[2]"));
            //ClickCelebs.Click();

            IWebElement ClickBornToday = driver.FindElement(By.XPath("//*[@id='imdbHeader']/div[2]/aside/div/div[2]/div/div[5]/span/div/div/ul/a[1]/span"));
            driver.ExecuteJavaScript("window.scrollBy(0, 500)");
            ClickBornToday.Click();

            DateTime aDate = DateTime.Now;

            string expectedResult = ($"Birth Month Day of {aDate.ToString("MM-dd")} (Sorted by Popularity Ascending)");
            IWebElement bornTodayPageHeader = driver.FindElement(By.XPath("//*[@id='main']/div/h1"));
            string actualResult = bornTodayPageHeader.Text;
            Assert.AreEqual(expectedResult, actualResult);

            driver.Quit();
        }
        //[TearDown]
        //public static void Quit()
        //{
        //    driver.Quit();
        //}


    }
}
