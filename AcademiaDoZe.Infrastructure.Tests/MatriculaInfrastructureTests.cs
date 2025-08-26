using AcademiaDoZe.Domain.Entities;
using AcademiaDoZe.Domain.Enums;
using AcademiaDoZe.Infrastructure_.Data;
using AcademiaDoZe.Infrastructure_.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDoZe.Infrastructure.Tests
{
	public class MatriculaInfrastructureTests : TestBase
	{
		[Fact]
		public async Task Matricula_Adicionar_ObterPorAluno_ObterPorId()
		{
			var alunoRepo = new AlunoRepository(ConnectionString, DatabaseType);
			var aluno = await alunoRepo.ObterPorCpf("98765432100");
			Assert.NotNull(aluno);

			var matricula = Matricula.Criar(
				aluno: aluno!,
				plano: EMatriculaPlano.Mensal,
				dataInicio: DateOnly.FromDateTime(DateTime.Today),
				dataFim: DateOnly.FromDateTime(DateTime.Today.AddMonths(1)),
				objetivo: "Condicionamento físico",
				restricoes: null,
				laudo: null
			);

			var repo = new MatriculaRepository(ConnectionString, DatabaseType);
			var matriculaInserida = await repo.Adicionar(matricula);

			Assert.NotNull(matriculaInserida);
			Assert.True(matriculaInserida.Id > 0);

			var matriculasAluno = await repo.ObterPorAluno(aluno!.Id);
			Assert.Contains(matriculasAluno, m => m.Id == matriculaInserida.Id);

			var matriculaPorId = await repo.ObterPorId(matriculaInserida.Id);
			Assert.NotNull(matriculaPorId);
		}

		[Fact]
		public async Task Matricula_Atualizar()
		{
			var alunoRepo = new AlunoRepository(ConnectionString, DatabaseType);
			var aluno = await alunoRepo.ObterPorCpf("98765432100");
			Assert.NotNull(aluno);

			var repo = new MatriculaRepository(ConnectionString, DatabaseType);
			var matricula = (await repo.ObterPorAluno(aluno!.Id)).FirstOrDefault();
			Assert.NotNull(matricula);

			var matriculaAtualizada = Matricula.Criar(
				aluno: matricula.AlunoMatricula,
				plano: matricula.Plano,
				dataInicio: matricula.DataInicio,
				dataFim: matricula.DataFim,
				objetivo: "Perda de peso",
				restricoes: matricula.RestricoesMedicas,
				laudo: matricula.LaudoMedico
			);

			// Usar reflection para manter o mesmo ID
			var idProperty = typeof(Entity).GetProperty("Id");
			idProperty?.SetValue(matriculaAtualizada, matricula.Id);

			var resultado = await repo.Atualizar(matriculaAtualizada);
			Assert.Equal("Perda de peso", resultado.Objetivo);
		}

		[Fact]
		public async Task Matricula_ObterAtivas()
		{
			var repo = new MatriculaRepository(ConnectionString, DatabaseType);
			var ativas = await repo.ObterAtivas();
			Assert.NotNull(ativas);
			Assert.All(ativas, m => Assert.True(m.DataFim >= DateOnly.FromDateTime(DateTime.Today)));
		}

		[Fact]
		public async Task Matricula_ObterVencendoEmDias()
		{
			var repo = new MatriculaRepository(ConnectionString, DatabaseType);
			var proximas = await repo.ObterVencendoEmDias(7);

			Assert.NotNull(proximas);

			Assert.All(proximas, m =>
			{
				Assert.True(m.DataFim.HasValue, "DataFim não pode ser nula para este teste.");
				var dataFim = m.DataFim.Value.ToDateTime(TimeOnly.MinValue);
				var diasRestantes = (dataFim - DateTime.Today).Days;
				Assert.InRange(diasRestantes, 0, 7);
			});
		}


		[Fact]
		public async Task Matricula_ObterTodos()
		{
			var repo = new MatriculaRepository(ConnectionString, DatabaseType);
			var todas = await repo.ObterTodos();
			Assert.NotNull(todas);
		}

		[Fact]
		public async Task Matricula_Remover()
		{
			var alunoRepo = new AlunoRepository(ConnectionString, DatabaseType);
			var aluno = await alunoRepo.ObterPorCpf("98765432100");
			Assert.NotNull(aluno);

			var repo = new MatriculaRepository(ConnectionString, DatabaseType);
			var matricula = (await repo.ObterPorAluno(aluno!.Id)).FirstOrDefault();
			Assert.NotNull(matricula);

			var resultado = await repo.Remover(matricula!.Id);
			Assert.True(resultado);

			var removida = await repo.ObterPorId(matricula.Id);
			Assert.Null(removida);
		}
	}
}
