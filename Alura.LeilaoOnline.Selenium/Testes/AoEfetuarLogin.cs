using Alura.LeilaoOnline.Selenium.Fixtures;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LeilaoOnline.Testes
{
    [Collection("Chrome Driver")]
    public class AoEfetuarLogin
    {
        private IWebDriver driver;

        public AoEfetuarLogin(TestFixture fixture)
        {
            driver = fixture.Driver;
        }

        [Theory]
        [InlineData("Joabe", "")]
        [InlineData("", "123")]
        [InlineData("", "")]
        public void DadoInfoInvalidasDevePermancenerNaTelaDeLogin(
            string usuario,
            string senha)
        {
            //arrange
            driver.Navigate().GoToUrl("http://localhost:5000/Autenticacao/Login");

            var inputUsuario = driver.FindElement(By.Id("Login"));
            var inputSenha = driver.FindElement(By.Id("Password"));
            var buttonLogin = driver.FindElement(By.Id("btnLogin"));

            //act
            inputUsuario.SendKeys(usuario);
            inputSenha.SendKeys(senha);

            buttonLogin.Click();

            //assert
            Assert.Contains("Ainda não possui cadastro? ",
                driver.FindElement(By.XPath("//p[text()='Ainda não possui cadastro? ']")).Text);
        }

    }
}
