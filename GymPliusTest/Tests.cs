using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Xml.Linq;

namespace ImdbTest
{
    public class Tests
    {
        public static IWebDriver driver;
        [Test]
        public static void BornToday()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Url = "https://www.imdb.com/?ref_=nv_home";
            driver.Manage().Window.Maximize();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            By untilPageIsMaximized = By.XPath("//*[@id='iconContext-menu']");
            IWebElement ClickBurgerMenu = wait.Until(ExpectedConditions.ElementIsVisible(untilPageIsMaximized));
            ClickBurgerMenu.Click();

            driver.ExecuteJavaScript("window.scrollBy(0, 300)");

            string xpath1 = "//*[@id='imdbHeader']/div[2]/aside/div/div[2]/div/div[4]/span/div/div/ul/a[1]/span";
            string xpath2 = "//*[@id='imdbHeader']/div[2]/aside/div/div[2]/div/div[5]/span/div/div/ul/a[1]/span";
            string xpath3 = "//*[@id='imdbHeader']/div[2]/aside/div/div[2]/div/div[5]/span/div/div/ul/a[1]";
            try
            {
                IWebElement ClickBornToday = driver.FindElement(By.XPath(xpath1));
                ClickBornToday.Click();
            }
            catch (NoSuchElementException)
            {
                try
                {
                    IWebElement ClickBornToday = driver.FindElement(By.XPath(xpath2));
                    ClickBornToday.Click();
                }
                catch (NoSuchElementException)
                {
                    IWebElement ClickBornToday = driver.FindElement(By.XPath(xpath3));
                    ClickBornToday.Click();
                }
            }

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
            driver.Manage().Window.Maximize();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            By untilPageIsMaximized = By.XPath("//*[@id='iconContext-menu']");
            IWebElement ClickBurgerMenu = wait.Until(ExpectedConditions.ElementIsVisible(untilPageIsMaximized));
            ClickBurgerMenu.Click();

            string xpath1 = "//*[@id='imdbHeader']/div[2]/aside/div/div[2]/div/div[1]/span/div/div/ul/a[2]/span";
            //string xpath2 = "//*[@id='imdbHeader']/div[2]/aside/div/div[2]/div/div[1]/div[2]/span/div/div/ul/a[2]/span";
            string xpath3 = "//*[@id='imdbHeader']/div[2]/aside/div/div[2]/div/div[1]/div[2]/span/div/div/ul/a[2]";
            string xpath4 = "//*[@id='imdbHeader']/div[2]/aside/div/div[2]/div/div[1]/span/div/div/ul/a[2]";
            try
            {
                IWebElement ClickTop250Movies = driver.FindElement(By.XPath(xpath1));
                ClickTop250Movies.Click();
            }
            catch (NoSuchElementException)
            {
                try
                {
                    IWebElement ClickTop250Movies = driver.FindElement(By.XPath(xpath4));
                    ClickTop250Movies.Click();
                }
                catch(NoSuchElementException)
                {
                    IWebElement ClickTop250Movies = driver.FindElement(By.XPath(xpath3));
                    ClickTop250Movies.Click();
                }
            }

            IWebElement ClickRankingDropDown = driver.FindElement(By.XPath("//*[@id='lister-sort-by-options']"));
            ClickRankingDropDown.Click();

            WebDriverWait wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            By releaseDateButtonLocator = By.XPath("//*[@id='lister-sort-by-options']/option[3]");
            IWebElement ClickByReleaseDate = wait.Until(ExpectedConditions.ElementIsVisible(releaseDateButtonLocator));
            ClickByReleaseDate.Click();

            string expectedResult = "113. Top Gun: Maverick (2022)";

            IWebElement firstMovieAfterClickingSortByReleaseDate = driver.FindElement(By.XPath("//*[@id='main']/div/span/div/div/div[3]/table/tbody/tr[1]/td[2]"));

            string actualResult = firstMovieAfterClickingSortByReleaseDate.Text;
            Assert.AreEqual(expectedResult, actualResult);

            driver.Quit();
        }
        [Test]
        public static void EnteringValidInformationInCreatingAccountFieldGetsYouToNextStep()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Url = "https://www.imdb.com/?ref_=nv_home";
            driver.Manage().Window.Maximize();


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

            string expectedResult = ("Authentication required");
            string actualResult = driver.Title;
            Assert.AreEqual(expectedResult, actualResult);

            driver.Quit();
        }

        [Test]
        public static void SearchBarIsFunctioning()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Url = "https://www.imdb.com/?ref_=nv_home";
            driver.Manage().Window.Maximize();


            string[] words = { "inception", "tom cruise", "leonardo di caprio", "interstellar", "the boys", "now you see me", "john wick 4", "keanu reeves", "django unchained" };
            Random random = new Random();
            int randomIndex = random.Next(0, words.Length);
            string randomWord = words[randomIndex];

            IWebElement inputRandomWord = driver.FindElement(By.XPath("//*[@id='suggestion-search']"));
            inputRandomWord.SendKeys(randomWord);

            IWebElement ClickSearch = driver.FindElement(By.XPath("//*[@id='iconContext-magnify']"));
            ClickSearch.Click();

            string expectedResult = ($"Search \"{randomWord}\"");

            IWebElement searchAnswer = driver.FindElement(By.XPath("//*[@id='__next']/main/div[2]/div[3]/section/div/div[1]/section[1]/h1"));
            string actualResult = searchAnswer.Text;
            Assert.AreEqual(expectedResult, actualResult);
            driver.Quit();

        }
        [Test]
        public void LinkActivityGetToQuotes()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Url = "https://www.imdb.com/?ref_=nv_home";
            driver.Manage().Window.Maximize();

            string[] words = { "inception", "eurotrip", "borat", "interstellar", "the boys", "now you see me", "john wick 4", "anastasia", "django unchained" };
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
            By PopUpRateThisMovieLocator = By.XPath("//*[@id='styleguide-v2']/div[7]/div[3]/div/div[1]/button");
            IWebElement ClickClosePopUpRateThisMovie = wait.Until(ExpectedConditions.ElementIsVisible(PopUpRateThisMovieLocator));
            ClickClosePopUpRateThisMovie.Click();

            IWebElement ClickQuotesButton = driver.FindElement(By.XPath("//*[@id='sidebar']/div[3]/ul/li[4]/a"));
            ClickQuotesButton.Click();

            string expectedResult = "Quotes";
            IWebElement movieQuotes = driver.FindElement(By.XPath("//*[@id='main']/section/div[1]/div/h1"));
            string actualResult = movieQuotes.Text;
            Assert.AreEqual(expectedResult, actualResult);
            driver.Quit();
        }

        [Test]
        public static void RatingAMovieWithoutLoggingInIsNotPossible()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Url = "https://www.imdb.com/?ref_=nv_home";
            driver.Manage().Window.Maximize();

            string[] words = { "inception", "eurotrip", "borat", "interstellar", "the boys", "now you see me", "8 mile", "anastasia", "django unchained" };
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
            //By untilYourRatingIsClickable = By.XPath("//*[@id='__next']/main/div/section[1]/section/div[3]/section/section/div[2]/div[2]/div/div[2]/button/span/div");
            //IWebElement ClickRateButton = wait.Until(ExpectedConditions.ElementIsVisible(untilYourRatingIsClickable));
            //ClickRateButton.Click();

            By RatingStarsLocator = By.XPath("//*[@id='iconContext-star-border']");
            IWebElement RatingStars = wait.Until(ExpectedConditions.ElementIsVisible(RatingStarsLocator));
            //*[@id="iconContext-star-border"]
            RatingStars.Click();


            //IList<IWebElement> StarElements = RatingStars.FindElements(By.TagName("button"));

            //foreach (IWebElement star in StarElements)
            //{
            //    star.Click();
            //    Thread.Sleep(3000); 
            //}


            //WebDriverWait wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            //By UntilPageIsLoaded = By.XPath("/html/body/div[4]/div[2]/div/div[2]/div/div[2]/div[2]/div/div[2]/button[5]");
            //IWebElement ClickStarButton = wait.Until(ExpectedConditions.ElementIsVisible(UntilPageIsLoaded));
            //ClickStarButton.Click();

            
            // By UntilThisPageIsLoaded = By.XPath("/html/body/div[4]/div[2]/div/div[2]/div/div[2]/div[2]/div/div[2]/button[8]");
            //  IWebElement ClickRatingStar = wait.Until(ExpectedConditions.ElementIsVisible(UntilThisPageIsLoaded));
            IWebElement ClickRatingStar = driver.FindElement(By.XPath("/html/body/div[4]/div[2]/div/div[2]/div/div[2]/div[2]/div/div[2]/button[8]"));
            Actions action = new Actions(driver);
            action.DoubleClick(ClickRatingStar).Perform();
            //ClickRatingStar.Click();
            //body/div[6]/div[2]/div[1]/div[2]/div[1]/div[2]/div[2]/div[1]/div[1]
            // html / body / div[4] / div[2] / div / div[2] / div / div[2] / div[2] / div / div[2] / button[1]

            //By RateButtonLocator = By.XPath("//*[@id='__next']/main/div/section[1]/section/div[3]/section/section/div[2]/div[2]/div/div[2]/button/span/div");
            //IWebElement untilRateButtonIsClicable = wait.Until(ExpectedConditions.ElementIsVisible(RateButtonLocator));
            //untilRateButtonIsClicable.Click();
            // ClickRateButton = driver.FindElement(By.XPath("//*[@id='__next']/main/div/section[1]/section/div[3]/section/section/div[2]/div[2]/div/div[2]/button/span/div"));

            IWebElement ClickRateButton = driver.FindElement(By.XPath("/html/body/div[4]/div[2]/div/div[2]/div/div[2]/div[2]/button/span"));
            ClickRateButton.Click();
            // html / body / div[4] / div[2] / div / div[2] / div / div[2] / div[2] / button / span

            string expectedResult = "Sign in with IMDb - IMDb";
            string actualResult = driver.Title; 

            Assert.AreEqual(expectedResult, actualResult);

            driver.Quit();
        }
    }
}
