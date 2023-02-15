using NerdStore.BDD.Tests.Config;
using System;
using TechTalk.SpecFlow;
using Xunit;

namespace NerdStore.BDD.Tests.Usuario
{
    [Binding]
    [CollectionDefinition(nameof(AutomacaoWebTestsFixtureCollection))]
    public class Usuario_LoginSteps
    {
        private readonly LoginUsuarioTela _loginUsuarioTela;
        private readonly AutomacaoWebTestsFixture _testsFixture;

        public Usuario_LoginSteps(AutomacaoWebTestsFixture testsFixture)
        {
            _testsFixture = testsFixture;
            _loginUsuarioTela = new LoginUsuarioTela(_testsFixture.BrowserHelper);
        }

        [When(@"Ele clicar em login")]
        public void QuandoEleClicarEmLogin()
        {
            //act
            _loginUsuarioTela.ClicarNoLinkLogin();

            //assert
            Assert.Contains(_testsFixture.Configuration.LoginUrl, _loginUsuarioTela.ObterUrl());

        }
        
        [When(@"Preencher os dados do formulario de login")]
        public void QuandoPreencherOsDadosDoFormularioDeLogin(Table table)
        {
            //Arrange
            var usuario = new Usuario
            {
                Email = "teste@teste.com",
                Senha = "Teste@123"
            };
            _testsFixture.Usuario = usuario;
            
            //act
            _loginUsuarioTela.PreencherFormularioLogin(usuario);


            //Assert

            Assert.True(_loginUsuarioTela.ValidarPreenchimentoFormularioLogin(usuario));
        }
        
        [When(@"Clicar no botão login")]
        public void QuandoClicarNoBotaoLogin()
        {
            _loginUsuarioTela.ClicarNoBotaoLogin();
        }
    }
}
