using NerdStore.BDD.Tests.Config;
using System;
using TechTalk.SpecFlow;
using Xunit;

namespace NerdStore.BDD.Tests.Usuario
{
    [Binding]
    [CollectionDefinition(nameof(AutomacaoWebTestsFixtureCollection))]
    public class Usuario_CadastroSteps
    {
        private readonly AutomacaoWebTestsFixture _testsFixture;
        private readonly CadastroDeUsuarioTela _cadastroUsuarioTela;

        public Usuario_CadastroSteps(AutomacaoWebTestsFixture fixture)
        {
            _testsFixture = fixture;
            _cadastroUsuarioTela = new CadastroDeUsuarioTela(_testsFixture.BrowserHelper);
        }

        
        [When(@"Ele clicar em registrar")]
        public void QuandoEleClicarEmRegistrar()
        {
            //Act
            _cadastroUsuarioTela.ClicarNoLinkRegistrar();

            //Assert
            Assert.Contains(_testsFixture.Configuration.RegisterUrl, _cadastroUsuarioTela.ObterUrl());

        }
        
        [When(@"Preencher os dados do formulario")]
        public void QuandoPreencherOsDadosDoFormulario(Table table)
        {
            //Arrange
            _testsFixture.GerarDadosUsuario();
            var usuario = _testsFixture.Usuario;

            //act
            _cadastroUsuarioTela.PreencherFormularioRegistro(usuario);

            Assert.True(_cadastroUsuarioTela.ValidarPreenchimentoFormularioRegistro(usuario));

        }
        
        [When(@"Clicar no botão registrar")]
        public void QuandoClicarNoBotaoRegistrar()
        {
            //Act
            _cadastroUsuarioTela.ClicarNoBotaoRegistrar();
        }
        
        [When(@"Preencher os dados do formulario com uma senha sem maiusculas")]
        public void QuandoPreencherOsDadosDoFormularioComUmaSenhaSemMaiusculas(Table table)
        {
            //Arrange
            _testsFixture.GerarDadosUsuario();
            var usuario = _testsFixture.Usuario;
            usuario.Senha = "teste@123";

            //act
            _cadastroUsuarioTela.PreencherFormularioRegistro(usuario);

            Assert.True(_cadastroUsuarioTela.ValidarPreenchimentoFormularioRegistro(usuario));
        }
        
        [When(@"Preencher os dados do formulario com uma senha sem caractere especial")]
        public void QuandoPreencherOsDadosDoFormularioComUmaSenhaSemCaractereEspecial(Table table)
        {
            //Arrange
            _testsFixture.GerarDadosUsuario();
            var usuario = _testsFixture.Usuario;
            usuario.Senha = "Teste123";

            //act
            _cadastroUsuarioTela.PreencherFormularioRegistro(usuario);

            Assert.True(_cadastroUsuarioTela.ValidarPreenchimentoFormularioRegistro(usuario));
        }
        

        

        
        [Then(@"Ele receberá uma mensagem de erro que a senha precisa conter uma letra maiuscula")]
        public void EntaoEleReceberaUmaMensagemDeErroQueASenhaPrecisaConterUmaLetraMaiuscula()
        {
            string mensagemErro = "Passwords must have at least one uppercase ('A'-'Z').";
            Assert.True(_cadastroUsuarioTela.ValidarMensagemDeErroFormulario(mensagemErro));
        }
        
        [Then(@"Ele receberá uma mensagem de erro que a senha precisa conter um caractere especial")]
        public void EntaoEleReceberaUmaMensagemDeErroQueASenhaPrecisaConterUmCaractereEspecial()
        {
            string mensagemErro = "Passwords must have at least one non alphanumeric character.";
            Assert.True(_cadastroUsuarioTela.ValidarMensagemDeErroFormulario(mensagemErro));
        }
    }
}
