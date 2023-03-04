using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
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

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            By releaseDateButtonLocator = By.XPath("//*[@id='lister-sort-by-options']/option[4]");
            IWebElement ClickByReleaseDate = wait.Until(ExpectedConditions.ElementIsVisible(releaseDateButtonLocator));
            ClickByReleaseDate.Click();

            string expectedResult = "109. Top Gun: Maverick (2022)";

            IWebElement firstMovieAfterClickingSortByReleaseDate = driver.FindElement(By.XPath("//*[@id='main']/div/span/div/div/div[3]/table/tbody/tr[1]/td[2]"));

            string actualResult = firstMovieAfterClickingSortByReleaseDate.Text;
            Assert.AreEqual(expectedResult, actualResult);
        }
        [Test]
        public static void EnteringValindInformationInCreatingAccountGetsYouToNextStep()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Url = "https://www.imdb.com/?ref_=nv_home";

            IWebElement ClickSignIn = driver.FindElement(By.XPath("//*[@id='imdbHeader']/div[2]/div[5]/a/span"));
            ClickSignIn.Click();

            IWebElement ClickCreateANewAccount = driver.FindElement(By.XPath("//*[@id='signin-options']/div/div[2]/a"));
            ClickCreateANewAccount.Click();

            string yourName = "Test";
            string email = "123@mailslurp.com";
            string password = "password";

            IWebElement inputYourName = driver.FindElement(By.XPath("//*[@id='ap_customer_name']"));
            inputYourName.SendKeys(yourName);
            IWebElement inputEmail = driver.FindElement(By.XPath("//*[@id='ap_email']"));
            inputEmail.SendKeys(email);
            IWebElement inputPassword = driver.FindElement(By.XPath("//*[@id='ap_password']"));
            inputPassword.SendKeys(password);
            IWebElement inputReEnterPassword = driver.FindElement(By.XPath("//*[@id='ap_password_check']"));
            inputReEnterPassword.SendKeys(password);

            IWebElement ClickCreateYourIMDBAccountButton = driver.FindElement(By.XPath("//*[@id='continue']"));
            ClickCreateYourIMDBAccountButton.Click();

            //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(7));
            //By solvePuzzleButtonLocator = By.XPath("//*[@id='home_children_button']");
            //IWebElement ClickSolvePuzzleButton = wait.Until(ExpectedConditions.ElementIsVisible(solvePuzzleButtonLocator));
            //ClickSolvePuzzleButton.Click();
            

            //IWebElement ClickOnPicture4 = driver.FindElement(By.XPath("//*[@id='image4']/a"));
            //ClickOnPicture4.Click();
        }

        [Test]
        public static void SearchBarIsFunctioning()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Url = "https://www.imdb.com/?ref_=nv_home";

            string[] words = { "inception", "tom cruise", "leonardo di caprio", "interstelar", "the boys", "now you see me", "john wick 4", "keanu reeves", "django unchained" };
            Random random = new Random();
            // get a random index from the array
            int randomIndex = random.Next(0, words.Length);
            // get the word at the random index
            string randomWord = words[randomIndex];

            IWebElement inputRandomWord = driver.FindElement(By.XPath("//*[@id='suggestion-search']"));
            inputRandomWord.SendKeys(randomWord);

            IWebElement ClickSearch = driver.FindElement(By.XPath("//*[@id='iconContext-magnify']"));
            ClickSearch.Click();

            string expectedResult = ($"Search {randomWord}");

            IWebElement searchAnswer = driver.FindElement(By.XPath("//*[@id='__next']/main/div[2]/div[3]/section/div/div[1]/section[1]/h1"));
            string actualResult = searchAnswer.Text;
            Assert.AreEqual(expectedResult, actualResult);

        }
        [Test]
        public void LinkActivityGetToQuotes()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Url = "https://www.imdb.com/?ref_=nv_home";

            string[] words = { "inception", "eurotrip", "borat", "interstelar", "the boys", "now you see me", "john wick 4", "anastasia", "django unchained" };
            Random random = new Random();
            int randomIndex = random.Next(0, words.Length);
            string randomWord = words[randomIndex];
            IWebElement inputRandomWord = driver.FindElement(By.XPath("//*[@id='suggestion-search']"));
            inputRandomWord.SendKeys(randomWord);
            IWebElement ClickSearch = driver.FindElement(By.XPath("//*[@id='iconContext-magnify']"));
            ClickSearch.Click();

            IWebElement ClickFirstLink = driver.FindElement(By.XPath("//*[@id='__next']/main/div[2]/div[3]/section/div/div[1]/section[2]/div[2]/ul/li[1]/div[2]/div/a"));
            ClickFirstLink.Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(7));
            By TriviaButtonLocator = By.XPath("//*[@id='__next']/main/div/section[1]/section/div[3]/section/section/div[1]/div/div[2]/ul/li[3]/a");
            IWebElement ClickTriviaButton = wait.Until(ExpectedConditions.ElementIsVisible(TriviaButtonLocator));
            ClickTriviaButton.Click();

            WebDriverWait wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(7));
            By PopUpRateThisMovieLocator = By.XPath("//*[@id='iconContext-clear']");
            IWebElement ClickClosePopUpRateThisMovie = wait.Until(ExpectedConditions.ElementIsVisible(PopUpRateThisMovieLocator));
            ClickClosePopUpRateThisMovie.Click();

            IWebElement ClickQuotesButton = driver.FindElement(By.XPath("//*[@id='__next']/main/div[2]/div[3]/section/div/div[1]/section[2]/div[2]/ul/li[1]/div[2]/div/a"));
            ClickQuotesButton.Click();

            string expectedResult = "Quotes";
            IWebElement movieQuotes = driver.FindElement(By.XPath("//*[@id='main']/section/div[1]/div/h1"));
            string actualResult = movieQuotes.Text;
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void RatingAMovieWithoutLoggingInIsNotPossible()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Url = "https://www.imdb.com/?ref_=nv_home";

            string[] words = { "inception", "eurotrip", "borat", "interstelar", "the boys", "now you see me", "john wick 4", "anastasia", "django unchained" };
            Random random = new Random();
            int randomIndex = random.Next(0, words.Length);
            string randomWord = words[randomIndex];
            IWebElement inputRandomWord = driver.FindElement(By.XPath("//*[@id='suggestion-search']"));
            inputRandomWord.SendKeys(randomWord);
            IWebElement ClickSearch = driver.FindElement(By.XPath("//*[@id='iconContext-magnify']"));
            ClickSearch.Click();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(7));
            By YourRatingLocator = By.XPath("//*[@id='__next']/main/div/section[1]/section/div[3]/section/section/div[2]/div[2]/div/div[2]/button/span/div");
            IWebElement ClickRateButton = wait.Until(ExpectedConditions.ElementIsVisible(YourRatingLocator));
            ClickRateButton.Click();
            // IWebElement ClickRatingStar = driver.FindElement(By.XPath("/html/body/div[5]/div[2]/div/div[2]/div/div[2]/div[2]/div/div[2]/button[7]"));
            // ClickRatingStar.Click();
            for ( var i=1; i<11; i++)
            {
                IWebElement ClickRatingStar = driver.FindElement(By.XPath("/html/body/div[5]/div[2]/div/div[2]/div/div[2]/div[2]/div/div[2]/button[1]"));
                ClickRatingStar.Click();
                IWebElement ClickRate = driver.FindElement(By.XPath("/html/body/div[5]/div[2]/div/div[2]/div/div[2]/div[2]/button/span"));
                ClickRate.Click();
            }

        }
        //[TearDown]
        //public static void Quit()
        //{
        //    driver.Quit();
        //}


    }
}
