//Vanessa Furtado Nunes

using AcademiaDoZe.Domain.Entities;
using AcademiaDoZe.Domain.Enums;
using AcademiaDoZe.Domain.Exceptions;
using AcademiaDoZe.Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDoZe.Tests.Domain
{
	public class AcessoDomainTests
	{
		private Aluno GetValidAluno()
		{
			var logradouro = Logradouro.Criar("Rua A", "12345678", "Brasil", "SP", "cidade", "bairro");

			var foto = Arquivo.Criar(new byte[1], ".jpg");

			return Aluno.Criar(
	"12345678901",                        
	"Vanessa Nunes",                       
	DateOnly.FromDateTime(DateTime.Today.AddYears(-20)), 
	"vanessa@email.com",                  
	"99999999999",                        
	"password",                           
	foto,                                 
	logradouro,                           
	"321",                                
	"Apto 1"                              
);
		}

		[Fact]
		public void CriarAcesso_ComDadosValidos_DeveCriarObjeto()
		{
			var aluno = GetValidAluno();
			var tipo = EPessoaTipo.Aluno;
			var dataHora = DateTime.Now.AddMinutes(1);
			if (dataHora.TimeOfDay < new TimeSpan(6, 0, 0))
				dataHora = DateTime.Today.AddHours(6);
			else if (dataHora.TimeOfDay > new TimeSpan(22, 0, 0))
				dataHora = DateTime.Today.AddDays(1).AddHours(6);

			var acesso = Acesso.Criar(tipo, aluno, dataHora);

			Assert.NotNull(acesso);
		}

		[Fact]
		public void CriarAcesso_ComTipoInvalido_DeveLancarExcecao()
		{
			var aluno = GetValidAluno();
			var tipo = (EPessoaTipo)999;
			var dataHora = DateTime.Today.AddHours(8);
			var ex = Assert.Throws<DomainException>(() =>
				Acesso.Criar(tipo, aluno, dataHora)
			);
			Assert.Equal("TIPO_OBRIGATORIO", ex.Message);
		}
	}
}
