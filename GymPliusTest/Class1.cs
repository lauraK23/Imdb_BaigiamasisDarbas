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

           // IWebElement ClosePopUp = driver.FindElement(By.XPath("//*[@id=\"iconContext-clear\"]/path[2]"));
           // ClosePopUp.Click();

            IWebDriver driver = new ChromeDriver();
            driver.Url = "https://www.imdb.com/?ref_=nv_home";

            IWebElement ClickBurgerMenu = driver.FindElement(By.XPath("//*[@id='imdbHeader-navDrawerOpen']/span"));
            ClickBurgerMenu.Click();

            //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            //wait.Until(driver => driver.FindElement(By.XPath("//*[@id='imdbHeader-navDrawerOpen']/span")).Text=="Celebs"));
            driver.ExecuteJavaScript("window.scrollBy(0, 500)");

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

        [Test]
        public static void ImdbTop250Movies()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Url = "https://www.imdb.com/?ref_=nv_home";

            IWebElement ClickBurgerMenu = driver.FindElement(By.XPath("//*[@id='imdbHeader-navDrawerOpen']/span"));
            ClickBurgerMenu.Click();

            IWebElement ClickTop250Movies = driver.FindElement(By.XPath("//*[@id='imdbHeader']/div[2]/aside/div/div[2]/div/div[1]/div[1]/span/div/div/ul/a[1]/span"));
            ClickTop250Movies.Click();

            IWebElement ClickRankingDropDown = driver.FindElement(By.XPath("//*[@id='lister-sort-by-options']"));
            ClickRankingDropDown.Click();



        }
        //[TearDown]
        //public static void Quit()
        //{
        //    driver.Quit();
        //}


    }
}
