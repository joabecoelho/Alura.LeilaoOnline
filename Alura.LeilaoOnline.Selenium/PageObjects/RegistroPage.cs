using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alura.LeilaoOnline.Selenium.PageObjects
{
    class RegistroPage
    {
        private IWebDriver driver;

        private By formRegistro = By.TagName("form");
        private By inputNome = By.Id("Nome");
        private By inputEmail = By.Id("Email");
        private By inputSenha = By.Id("Password");
        private By inputConfirmPassword = By.Id("ConfirmPassword");
        private By buttonRegistro = By.Id("btnRegistro");
        private By spanErroNome = By.Id("Nome-error");
        private By spanErroEmail = By.Id("Email-error");

        public RegistroPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void Visitar()
        {
            driver.Navigate().GoToUrl("http://localhost:5000");
        }

        private string GetText(By locator)
        {
            string texto = driver.FindElement(locator).Text;
            return texto;
        }

        private void PreencherInput(By locator, string texto)
        {
            driver.FindElement(locator).SendKeys(texto);
        }
        private void ClicarBotao(By locator)
        {
            driver.FindElement(locator).Click();
        }

        public void PreencherNome(string texto)
        {
            PreencherInput(inputNome, texto);
        }

        public void PreencherEmail(string texto)
        {
            PreencherInput(inputEmail, texto);
        }

        public void PreencherSenha(string texto)
        {
            PreencherInput(inputSenha, texto);
        }

        public void PreencherConfirmSenha(string texto)
        {
            PreencherInput(inputConfirmPassword, texto);
        }

        public void ClicarRegistro()
        {
            ClicarBotao(buttonRegistro);
        }

        public void PreencheFormulario(
            string nome,
            string email,
            string senha,
            string confirmsenha)
        {
            driver.FindElement(inputNome).SendKeys(nome);
            driver.FindElement(inputEmail).SendKeys(email);
            driver.FindElement(inputSenha).SendKeys(senha);
            driver.FindElement(inputConfirmPassword).SendKeys(confirmsenha);

        }

        public string PegarTextoMensagemErroNome()
        {
            return GetText(spanErroNome);
        }

        public string PegarTextoMensagemErroEmail()
        {
            return GetText(spanErroEmail);
        }
    }

}
