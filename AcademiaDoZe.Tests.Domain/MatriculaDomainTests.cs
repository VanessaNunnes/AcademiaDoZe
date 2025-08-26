//Vanessa Furtado Nunes
using AcademiaDoZe.Domain.Entities;
using AcademiaDoZe.Domain.Enums;
using AcademiaDoZe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDoZe.Tests.Domain
{
	public class MatriculaDomainTests
	{
		[Fact]
		public void CriarMatricula_Valida_NaoDeveLancarExcecao()
		{
			var logradouro = Logradouro.Criar("Rua A", "12345678", "Brasil", "SP", "SP", "Centro");
			var aluno = Aluno.Criar("João", "123456789", new DateOnly(2000, 1, 1),
										"joao@email.com", "48999999999", logradouro, "senha123",
										null, "100", null);

			var matricula = Matricula.Criar(aluno, EMatriculaPlano.Mensal,
										   DateOnly.FromDateTime(DateTime.Now),
										   DateOnly.FromDateTime(DateTime.Now.AddMonths(1)),
										   "Musculação", EMatriculaRestricoes.None, null);

			Assert.NotNull(matricula);
		}

		[Fact]
		public void CriarMatricula_AlunoMenor12_DeveLancarExcecao()
		{
			var logradouro = Logradouro.Criar("Rua A", "12345678", "Brasil", "SP", "SP", "Centro");
			var aluno = Aluno.Criar("João", "123456789", DateOnly.FromDateTime(DateTime.Now.AddYears(-10)),
										"joao@email.com", "48999999999", logradouro, "senha123",
										null, "100", null);

			Assert.Throws<DomainException>(() =>
				Matricula.Criar(aluno, EMatriculaPlano.Mensal,
								DateOnly.FromDateTime(DateTime.Now),
								DateOnly.FromDateTime(DateTime.Now.AddMonths(1)),
								"Musculação", EMatriculaRestricoes.None, null)
			);
		}

		[Fact]
		public void CriarMatricula_DataInicioMaiorQueDataFim_DeveLancarExcecao()
		{
			var logradouro = Logradouro.Criar("Rua A", "12345678", "Brasil", "SP", "SP", "Centro");
			var aluno = Aluno.Criar("João", "123456789", new DateOnly(2000, 1, 1),
										"joao@email.com", "48999999999", logradouro, "senha123",
										null, "100", null);

			Assert.Throws<DomainException>(() =>
				Matricula.Criar(aluno, EMatriculaPlano.Mensal,
								DateOnly.FromDateTime(DateTime.Now.AddMonths(1)),
								DateOnly.FromDateTime(DateTime.Now),
								"Musculação", EMatriculaRestricoes.None, null)
			);
		}
	}
}
