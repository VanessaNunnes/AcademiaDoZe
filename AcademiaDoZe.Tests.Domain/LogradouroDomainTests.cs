//Vanessa Furtado Nunes
using AcademiaDoZe.Domain.Entities;
using AcademiaDoZe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDoZe.Tests.Domain
{
	public class LogradouroDomainTests
	{
		[Fact]
		public void CriarLogradouro_Valido_NaoDeveLancarExcecao()
		{
			var logradouro = Logradouro.Criar("Casa", "12345678", "Brasil", "SP", "SP", "Centro");
			Assert.NotNull(logradouro); 
		}

		[Fact]
		public void CriarLogradouro_Valido_DeveLancarExcecao()
		{
			Assert.Throws<DomainException>(() => Logradouro.Criar("Casa", "1234567", "Brasil", "SP", "SP", "Centro"));
		}

		[Fact]
		public void CriarLogradouro_Valido_VerificarNormalizado()
		{
			var logradouro = Logradouro.Criar("Casa  ", "1234./567-8", "  Brasil  ", "S P", " SP ", "  Centro");
			Assert.Equal("12345678", logradouro.Cep); 
			Assert.Equal("Casa", logradouro.Nome);
			Assert.Equal("Centro", logradouro.Bairro);
			Assert.Equal("SP", logradouro.Cidade);
			Assert.Equal("SP", logradouro.Estado);
			Assert.Equal("Brasil", logradouro.Pais);
		}

		[Fact]
		public void CriarLogradouro_Invalido_VerificarMessageExcecao()
		{
			var exception = Assert.Throws<DomainException>(() => Logradouro.Criar("", "12345678", "Brasil", "SP", "SP", "Centro"));
			Assert.Equal("Nome do logradouro não pode ser vazio.", exception.Message); // validando a mensagem de exceção
		}
	}
}
