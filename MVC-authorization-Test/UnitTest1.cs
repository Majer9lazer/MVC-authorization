using System;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace MVC_authorization_Test
{
    [TestFixture]
    public class UnitTest1
    {
        private Random _rnd = new Random();
        private IWebDriver _driver;
        private WebDriverWait _wait;

        [SetUp]
        public void Start()
        {
            _driver = new ChromeDriver();
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(15));
        }

        [Test]
        public void SearchGoogle()
        {
            _driver.Navigate().GoToUrl("https://www.google.com");
            _driver.FindElement(By.Name("q")).SendKeys("Егор лучший?!");
            _driver.FindElement(By.Name("btnK")).Click();
            _wait.Until(ExpectedConditions.TitleContains("Егор лучший?! - Поиск в Google"));
        }

        [Test]
        public void RegisterUser()
        {
            _driver.Navigate().GoToUrl("https://temp-mail.org/ru/");
            //Thread.Sleep(2000);
            var element = _wait.Until(f => f.FindElement(By.Id("mail")));

            string value = element.GetAttribute("value");
            _driver.Navigate().GoToUrl("http://new.avislogistics.kz/Account/RegistrationForm");
            _driver.FindElement(By.Name("Login")).SendKeys("egor.best_" + _rnd.Next());
            _driver.FindElement(By.Name("Password")).SendKeys("1Rj,kfcnm2$");
            _driver.FindElement(By.Name("Password2")).SendKeys("1Rj,kfcnm2$");
            SelectElement dropdown = new SelectElement(_driver.FindElement(By.Id("IsLegalEntity")));


            dropdown.SelectByIndex(1);

            _driver.FindElement(By.Name("Surname"))
                .SendKeys("Сидоренко");

            _driver.FindElement(By.Name("Name"))
                .SendKeys("Егор");
            _driver.FindElement(By.Name("Bin"))
                .SendKeys("99" + _rnd.Next(1000000000, int.MaxValue));
            _driver.FindElement(By.Name("Email"))
                .SendKeys(value);

            //$"sidorenkoegor1999@mail_{_rnd.Next(2500)}.ru"

            SelectElement addressPhysicalContryId =
                new SelectElement(_wait.Until(f => f.FindElement(By.Id("AddressPhysical_ContryId"))));
            addressPhysicalContryId.SelectByIndex(3);

            // _wait.Timeout.Add(TimeSpan.FromSeconds(10));


            SelectElement addressPhysicalCityId =
                new SelectElement(_wait.Until(f => _driver.FindElement(By.Id("AddressPhysical_CityId"))));
            //_wait.Timeout.Add(TimeSpan.FromSeconds(10));

            addressPhysicalCityId.SelectByText("Алматы");


            _driver.FindElement(By.Name("AddressPhysical.PostalCode"))
                .SendKeys("050030");
            _driver.FindElement(By.Name("AddressPhysical.Street"))
                .SendKeys("Красногорская");
            _driver.FindElement(By.Name("AddressPhysical.House"))
                .SendKeys("35");

            _driver.FindElement(By.Name("ContactNumbers[0].СountryСode"))
                .SendKeys("7");
            _driver.FindElement(By.Name("ContactNumbers[0].PhoneCode"))
                .SendKeys("705");
            _driver.FindElement(By.Name("ContactNumbers[0].PhoneNumber"))
                .SendKeys("1648233");


            //for (int i = 1; i <= _rnd.Next(1, 5); i++)
            //{
            //    _driver.FindElement(By.ClassName("addline_phone_profile")).Click();

            //    _driver.FindElement(By.Name($"ContactNumbers[{i}].СountryСode"))
            //        .SendKeys("7");
            //    _driver.FindElement(By.Name($"ContactNumbers[{i}].PhoneCode"))
            //        .SendKeys(_rnd.Next(700, 800).ToString());
            //    _driver.FindElement(By.Name($"ContactNumbers[{i}].PhoneNumber"))
            //        .SendKeys(_rnd.Next(1000000, 9999999).ToString());
            //    Thread.Sleep(1000);
            //}

            _driver.FindElement(By.Id("btnAddUser")).Click();



            Assert.AreEqual(_wait.Until(f => f.Title), "Регистрация прошла успешно! | Avis Logistics");
            _driver.Navigate().GoToUrl("https://temp-mail.org/ru/");
            _driver.Navigate().Refresh();

            var urlMail = _wait.Until(f => f.FindElement(By.ClassName("title-subject"))).GetAttribute("href");
            _driver.Navigate().GoToUrl(urlMail);
            _wait.Until(d => d.Title == "Просмотр сообщения");
            var url = _driver.FindElements(By.TagName("a")).FirstOrDefault(f =>
                    f.GetAttribute("href").Contains("http://new.avislogistics.kz//Account/ActivateAccount"))
                .GetAttribute("href");
            _driver.Navigate().GoToUrl(url);

        }
        [TearDown]
        public void Stop()
        {
            _driver.Quit();
            _driver = null;
        }
    }
}
