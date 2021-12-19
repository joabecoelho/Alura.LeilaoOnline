using Alura.LeilaoOnline.Selenium.Fixtures;
using Alura.LeilaoOnline.Selenium.PageObjects;
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
        LoginPage loginPage;

        public AoEfetuarLogin(TestFixture fixture)
        {
            driver = fixture.Driver;
        }

        [Fact]
        public void DadoCredenciaisValidasDeveIrParaDashboard()
        {
            //arrange
            loginPage = new LoginPage(driver);
            loginPage.Visitar();

            //act
            loginPage.PreencherFormulario("fulano@example.org", "123");
            loginPage.SubmeteFormulario();

            //assert
            Assert.Contains("Dashboard", driver.Title);
        }

        [Theory]
        [InlineData("","")]
        [InlineData("fulano@example.org", "")]
        [InlineData("", "123")]
        public void DadoCredenciaisInvalidasDevePermanecerEmLogin(
            string login,
            string senha
            )
        {
            //arrange
            loginPage = new LoginPage(driver);
            loginPage.Visitar();

            //act
            loginPage.PreencherFormulario(login, senha);
            loginPage.SubmeteFormulario();

            //assert
            Assert.Contains("Login", driver.PageSource);
        }

    }
}
