using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace ImdbTest
{
    public class Tests
    {
        private static IWebDriver driver;
        private static WebDriverWait wait;
        private static By locatorMenu = By.Id("imdbHeader-navDrawerOpen");
        private static By locatorMenuBornToday = By.XPath("//*[contains(@href,'/feature/bornondate')]");
        private static By locatorHeadingBornToday = By.XPath("//*[@id='main']/div/h1");
        private static By locatorMenuTop250Movies = By.XPath("//*[contains(@href,'/chart/top/')]");
        private static By locatorDropdownSorting = By.XPath("//*[@id='lister-sort-by-options']");
        private static By locatorOptionSortingByReleaseDate = By.XPath("//*[@id='lister-sort-by-options']/option[3]");
        private static By locatorTextFirstMovie = By.XPath("//*[@class='titleColumn'][1]");
        private static By locatorLinkSignIn = By.XPath("//*[contains(@class,'imdb-header__signin-text')]");
        private static By locatorButtonCreateNewAccount = By.XPath("//*[contains(@class,'create-account')]");
        private static By locatorInputYourName = By.Id("ap_customer_name");
        private static By locatorInputEmail = By.Id("ap_email");
        private static By locatorInputPassword = By.Id("ap_password");
        private static By locatorInputReEnterPassword = By.Id("ap_password_check");
        private static By locatorButtonCreateYourIMDBAccount = By.Id("continue");
        private static By locatorInputSearchIMDB = By.Id("suggestion-search");
        private static By locatorButtonSearch = By.Id("suggestion-search-button");
        private static By locatorHeadingSearchResults = By.XPath("//main//h1");
        private static By locatorLinkFirstSearchResult = By.XPath("(//main//ul)[1]/li[1]//a[1]");
        private static By locatorLinkTrivia = By.XPath("(//main//ul)[1]/li[3]//a");
        private static By locatorButtonPopUpRateThisMovie = By.XPath("//*[@id='styleguide-v2']/div[7]/div[3]/div/div[1]/button");
        private static By locatorLinkQuotes = By.XPath("//*[@id='sidebar']/div[3]/ul/li[4]/a");
        private static By locatorHeadingMovieQuotes = By.XPath("//*[@id='main']/section/div[1]/div/h1");
        private static By locatorButtonRate = By.XPath("(//*[@data-testid='hero-rating-bar__user-rating'])[1]/button");
        private static By locatorButtonRate10 = By.XPath("//*[@aria-label='Rate 10']");
        private static By locatorButtonFinishRate = By.XPath("/html/body/div[4]/div[2]/div/div[2]/div/div[2]/div[2]/button");

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            driver.Url = "https://www.imdb.com/?ref_=nv_home";
            driver.Manage().Window.Maximize();

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        [Test]
        public static void BornToday()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(locatorMenu)).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(locatorMenuBornToday)).Click();

            DateTime now = DateTime.Now;
            string expectedResult = $"Birth Month Day of {now.ToString("MM-dd")} (Sorted by Popularity Ascending)";
            Assert.AreEqual(expectedResult, driver.FindElement(locatorHeadingBornToday).Text);
        }

        [Test]
        public static void ImdbTop250Movies()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(locatorMenu)).Click();
            wait.Until(ExpectedConditions.ElementToBeClickable(locatorMenuTop250Movies)).Click();
            driver.FindElement(locatorDropdownSorting).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(locatorOptionSortingByReleaseDate)).Click();

            string expectedResult = "114. Top Gun: Maverick (2022)";
            Assert.AreEqual(expectedResult, driver.FindElement(locatorTextFirstMovie).Text);
        }

        [Test]
        public static void EnteringValidInformationInCreatingAccountFieldGetsYouToNextStep()
        {
            string yourName = "Test";
            string email = "123@mailslurp.com";
            string password = "password";

            driver.FindElement(locatorLinkSignIn).Click();
            driver.FindElement(locatorButtonCreateNewAccount).Click();

            driver.FindElement(locatorInputYourName).SendKeys(yourName);
            driver.FindElement(locatorInputEmail).SendKeys(email);
            driver.FindElement(locatorInputPassword).SendKeys(password);
            driver.FindElement(locatorInputReEnterPassword).SendKeys(password);
            driver.FindElement(locatorButtonCreateYourIMDBAccount).Click();

            Assert.AreEqual("Authentication required", driver.Title);
        }

        [Test]
        public static void SearchBarIsFunctioning()
        {
            string[] words = { "inception", "tom cruise", "leonardo di caprio", "interstellar", "the boys", "now you see me", "john wick 4", "keanu reeves", "django unchained" };
            Random random = new Random();
            int randomIndex = random.Next(0, words.Length);
            string randomWord = words[randomIndex];

            driver.FindElement(locatorInputSearchIMDB).SendKeys(randomWord);
            driver.FindElement(locatorButtonSearch).Click();

            string expectedResult = ($"Search \"{randomWord}\"");
            Assert.AreEqual(expectedResult, driver.FindElement(locatorHeadingSearchResults).Text);
        }

        [Test]
        public void LinkActivityGetToQuotes()
        {
            string[] words = { "inception", "eurotrip", "borat", "interstellar", "the boys", "now you see me", "john wick 4", "anastasia", "django unchained" };
            Random random = new Random();
            int randomIndex = random.Next(0, words.Length);
            string randomWord = words[randomIndex];

            driver.FindElement(locatorInputSearchIMDB).SendKeys(randomWord);
            driver.FindElement(locatorButtonSearch).Click();
            driver.FindElement(locatorLinkFirstSearchResult).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(locatorLinkTrivia)).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(locatorButtonPopUpRateThisMovie)).Click();
            driver.FindElement(locatorLinkQuotes).Click();

            string expectedResult = "Quotes";
            Assert.AreEqual(expectedResult, driver.FindElement(locatorHeadingMovieQuotes).Text);
        }

        [Test]
        public static void RatingAMovieWithoutLoggingInIsNotPossible()
        {
            string[] words = { "inception", "eurotrip", "borat", "interstellar", "the boys", "now you see me", "8 mile", "anastasia", "django unchained" };
            Random random = new Random();
            int randomIndex = random.Next(0, words.Length);
            string randomWord = words[randomIndex];

            driver.FindElement(locatorInputSearchIMDB).SendKeys(randomWord);
            driver.FindElement(locatorButtonSearch).Click();
            driver.FindElement(locatorLinkFirstSearchResult).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(locatorButtonRate)).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(locatorButtonRate10));
            Actions actions = new Actions(driver);
            actions.DoubleClick(driver.FindElement(locatorButtonRate10));
            actions.Perform();
            driver.FindElement(locatorButtonFinishRate).Click();

            string expectedResult = "Sign in with IMDb - IMDb";
            Assert.AreEqual(expectedResult, driver.Title);
        }
    }
}
