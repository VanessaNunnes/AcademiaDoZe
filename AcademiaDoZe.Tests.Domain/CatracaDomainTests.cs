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
	public class CatracaDomainTests
	{
		[Fact]
		public void CriarCatraca_Valida_NaoDeveLancarExcecao()
		{
			var logradouro = Logradouro.Criar("Rua A", "12345678", "Brasil", "SP", "SP", "Centro");
			var aluno = Aluno.Criar("João", "123456789", new DateOnly(2000, 1, 1),
										"joao@email.com", "48999999999", logradouro, "senha123",
										null, "100", null);

			var catraca = Catraca.Criar(aluno, DateTime.Now, AcademiaDoZe.Domain.Enums.EPessoaTipo.Aluno);

			Assert.NotNull(catraca);
		}

		[Fact]
		public void CriarCatraca_PessoaNula_DeveLancarExcecao()
		{
			Assert.Throws<DomainException>(() =>
				Catraca.Criar(null!, DateTime.Now, AcademiaDoZe.Domain.Enums.EPessoaTipo.Aluno)
			);
		}

		[Fact]
		public void CriarCatraca_DataHoraInvalida_DeveLancarExcecao()
		{
			var logradouro = Logradouro.Criar("Rua A", "12345678", "Brasil", "SP", "SP", "Centro");
			var aluno = Aluno.Criar("João", "123456789", new DateOnly(2000, 1, 1),
										"joao@email.com", "48999999999", logradouro, "senha123",
										null, "100", null);

			Assert.Throws<DomainException>(() =>
				Catraca.Criar(aluno, default, AcademiaDoZe.Domain.Enums.EPessoaTipo.Aluno)
			);
		}
	}
}
