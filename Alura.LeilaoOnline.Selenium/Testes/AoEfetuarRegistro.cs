using System;
using OpenQA.Selenium;
using Xunit;
using System.Collections.Generic;
using System.Text;
using Alura.LeilaoOnline.Selenium.Fixtures;
using System.Threading;

namespace LeilaoOnline.Testes
{
    [Collection("Chrome Driver")]
    public class AoEfetuarRegistro
    {
        private IWebDriver driver;

        public AoEfetuarRegistro(TestFixture fixture)
        {
            driver = fixture.Driver;
        }

        [Fact]
        public void DadoInfoValidasDeveIrParaPaginaDeAgradecimento()
        {
            //arrange - dado chrome aberto, página inicial do sist. de leilões,
            //dados de registro válidos informados
            driver.Navigate().GoToUrl("http://localhost:5000");

            var inputNome = driver.FindElement(By.Id("Nome"));

            var inputEmail = driver.FindElement(By.Id("Email"));

            var inputSenha = driver.FindElement(By.Id("Password"));

            var inputConfirmaSenha = driver.FindElement(By.Id("ConfirmPassword"));

            var buttonRegistrar = driver.FindElement(By.Id("btnRegistro"));

            //act - efetuo o 
            inputNome.SendKeys("Joabe");
            inputEmail.SendKeys("joabe@gmail.com");
            inputSenha.SendKeys("123");
            inputConfirmaSenha.SendKeys("123");

            buttonRegistrar.Click();

            //assert - devo ser direcionado para uma página de agradecimento
            Assert.Contains("Obrigado", driver.FindElement(By.XPath("//h4[contains(text(), 'Obrigado')]")).Text);
        }

        [Theory]
        [InlineData("", "joabe@gmail.com", "123", "123")]
        [InlineData("Joabe Coelho", "joabe", "123", "123")]
        [InlineData("Joabe Coelho", "joabe@gmail.com", "123", "431")]
        [InlineData("Joabe Coelho", "joabe@gmail.com", "123", "")]
        public void DadoInfoInvalidasDeveContinuarNaHome(
            string nome,
            string email,
            string senha,
            string confirmesenha)
        {
            //arrange - dado chrome aberto, página inicial do sist. de leilões,
            //dados de registro válidos informados
            driver.Navigate().GoToUrl("http://localhost:5000");

            var inputNome = driver.FindElement(By.Id("Nome"));

            var inputEmail = driver.FindElement(By.Id("Email"));

            var inputSenha = driver.FindElement(By.Id("Password"));

            var inputConfirmaSenha = driver.FindElement(By.Id("ConfirmPassword"));

            var buttonRegistrar = driver.FindElement(By.Id("btnRegistro"));

            //act - efetuo o 
            inputNome.SendKeys(nome);
            inputEmail.SendKeys(email);
            inputSenha.SendKeys(senha);
            inputConfirmaSenha.SendKeys(confirmesenha);

            buttonRegistrar.Click();

            //assert - devo ser direcionado para uma página de agradecimento
            Assert.Contains("Registre-se para participar dos leilões!", 
                driver.FindElement(By.XPath("//h4[text()='Registre-se para participar dos leilões!']")).Text);
        }

        [Fact]
        public void DadoNomeEmBrancoDeveMostrarMensagemDeErro()
        {
            //arrange
            driver.Navigate().GoToUrl("http://localhost:5000");

            var buttonRegistrar = driver.FindElement(By.Id("btnRegistro"));

            //act
            buttonRegistrar.Click();

            //assert
            IWebElement elemento = driver.FindElement(By.Id("Nome-error"));
            Assert.Equal("The Nome field is required.", elemento.Text);
        }

        [Fact]
        public void DadoEmailInvalidoDeveMostrarMensagemDeErro()
        {
            //arrange
            driver.Navigate().GoToUrl("http://localhost:5000");

            var buttonRegistrar = driver.FindElement(By.Id("btnRegistro"));

            var inputEmail = driver.FindElement(By.Id("Email"));

            //act
            inputEmail.SendKeys("joabe");
            buttonRegistrar.Click();

            //assert
            IWebElement elemento = driver.FindElement(By.Id("Email-error"));
            Assert.True(elemento.Displayed);
        }

    }
}
