using Alura.LeilaoOnline.Selenium.Fixtures;
using Alura.LeilaoOnline.Selenium.Helpers;
using Alura.LeilaoOnline.Selenium.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using System.Reflection;
using Xunit;

namespace LeilaoOnline.Testes
{
    [Collection("Chrome Driver")]
    public class AoNavegarParaHome
    {

        private IWebDriver driver;

        RegistroPage registroPage;

        //Setup
        public AoNavegarParaHome(TestFixture fixture)
        {
            driver = fixture.Driver;
        }

        [Fact]
        public void DadoChromeAbertoDeveMostrarLeilosNoTitulo()
        {
            //arange 
            registroPage = new RegistroPage(driver);
            registroPage.Visitar();

            //act

            //assert
            Assert.Contains("Leilões", driver.Title);
        }

        [Fact]
        public void DadoChromeAbertoDeveMostrarProximosLeiloesNaPagina()
        {
            //arange
            registroPage = new RegistroPage(driver);
            registroPage.Visitar();

            //act

            //assert
            Assert.Contains("Próximos Leilões", driver.PageSource);
        }

        [Fact]
        public void DadoChromeAbertoFormNaoDeveMensagensDeErro()
        {
            //arange 
            registroPage = new RegistroPage(driver);
            registroPage.Visitar();

            //act

            //assert
            var form = driver.FindElement(By.TagName("form"));
            var spans = form.FindElements(By.TagName("span"));
            foreach (var span in spans)
            {
                Assert.True(string.IsNullOrEmpty(span.Text));
            }
        }
    }
}
