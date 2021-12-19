using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alura.LeilaoOnline.Selenium.PageObjects
{
    public class LoginPage
    {
        private IWebDriver driver;

        private By inputLogin = By.Id("Login");
        private By inputSenha = By.Id("Password");
        private By buttonLogin = By.Id("btnLogin");

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void Visitar()
        {
            driver.Navigate().GoToUrl("http://localhost:5000/Autenticacao/Login");
        }

        public void PreencherFormulario(string login, string senha)
        {
            driver.FindElement(inputLogin).SendKeys(login);
            driver.FindElement(inputSenha).SendKeys(senha);
        }

        public void SubmeteFormulario()
        {
            driver.FindElement(buttonLogin).Click();
        }

    }
}
