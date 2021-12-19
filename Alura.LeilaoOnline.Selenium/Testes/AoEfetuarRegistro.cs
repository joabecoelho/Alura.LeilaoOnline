using System;
using OpenQA.Selenium;
using Xunit;
using System.Collections.Generic;
using System.Text;
using Alura.LeilaoOnline.Selenium.Fixtures;
using System.Threading;
using Alura.LeilaoOnline.Selenium.PageObjects;

namespace LeilaoOnline.Testes
{
    [Collection("Chrome Driver")]
    public class AoEfetuarRegistro
    {
        private IWebDriver driver;

        RegistroPage registroPage;

        public AoEfetuarRegistro(TestFixture fixture)
        {
            driver = fixture.Driver;
        }

        [Fact]
        public void DadoInfoValidasDeveIrParaPaginaDeAgradecimento()
        {
            //arrange - dado chrome aberto, página inicial do sist. de leilão
            registroPage = new RegistroPage(driver);
            registroPage.Visitar();

            //act
            //dados de registro válidos informados
            registroPage.PreencherNome("Joabe");
            registroPage.PreencherEmail("joabe@gmail.com");
            registroPage.PreencherSenha("123");
            registroPage.PreencherConfirmSenha("123");

            registroPage.ClicarRegistro();

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
            //arrange
            registroPage = new RegistroPage(driver);
            registroPage.Visitar();

            //act
            registroPage.PreencherNome(nome);
            registroPage.PreencherEmail(email);
            registroPage.PreencherSenha(senha);
            registroPage.PreencherConfirmSenha(confirmesenha);

            registroPage.ClicarRegistro();

            //assert
            Assert.Contains("Registre-se para participar dos leilões!",
                driver.FindElement(By.XPath("//h4[text()='Registre-se para participar dos leilões!']")).Text);
        }

        [Fact]
        public void DadoNomeEmBrancoDeveMostrarMensagemDeErro()
        {
            //arrange
            registroPage = new RegistroPage(driver);
            registroPage.Visitar();

            //act
            registroPage.ClicarRegistro();

            //assert
            Assert.Equal("The Nome field is required.", registroPage.PegarTextoMensagemErroNome());
        }

        [Fact]
        public void DadoEmailInvalidoDeveMostrarMensagemDeErro()
        {
            //arrange
            registroPage = new RegistroPage(driver);
            registroPage.Visitar();

            registroPage.PreencheFormulario(
                nome: "",
                email: "daniel",
                senha: "",
                confirmsenha: "");

            //act
            registroPage.ClicarRegistro();

            //assert
            Assert.Equal("Please enter a valid email address.",
                registroPage.PegarTextoMensagemErroEmail());
        }

    }
}
