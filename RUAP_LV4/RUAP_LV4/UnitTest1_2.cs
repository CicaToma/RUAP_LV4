﻿using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    [TestFixture]
    public class InvalidLogin
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;

        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            baseURL = "http://demowebshop.tricentis.com/";
            verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }

        [Test]
        public void TheInvalidLoginTest()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
          

            driver.Navigate().GoToUrl(baseURL + "/");
            wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText("Log in")));
            driver.FindElement(By.LinkText("Log in")).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("img[alt=\"Tricentis Demo Web Shop\"]")));
            driver.FindElement(By.CssSelector("img[alt=\"Tricentis Demo Web Shop\"]")).Click();
            Assert.AreEqual("Demo Web Shop", driver.Title);
            wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText("Log in")));
            driver.FindElement(By.LinkText("Log in")).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("Email")));
            driver.FindElement(By.Id("Email")).Clear();
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("Email")));
            driver.FindElement(By.Id("Email")).SendKeys("marko_perkovic.thompson@gmail.com");
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("Password")));
            driver.FindElement(By.Id("Password")).Clear();
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("Password")));
            driver.FindElement(By.Id("Password")).SendKeys("EMojNarode");
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("input.button-1.login-button")));
            driver.FindElement(By.CssSelector("input.button-1.login-button")).Click();
        }
        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
    }
}
