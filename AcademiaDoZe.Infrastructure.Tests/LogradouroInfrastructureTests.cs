using AcademiaDoZe.Domain.Entities;
using AcademiaDoZe.Infrastructure_.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDoZe.Infrastructure.Tests
{
	public class LogradouroInfrastructureTests : TestBase
	{
		[Fact]
		public async Task Logradouro_Adicionar()
		{
			var _cep = "12345679";
			// Adicionar

			var logradouro = Logradouro.Criar("Rua dos Testes", _cep, "Bairro Teste", "SC", "TS", "Pais teste");

			var repoLogradouroAdd = new LogradouroRepository(ConnectionString, DatabaseType);
			var logradouroInserido = await repoLogradouroAdd.Adicionar(logradouro);
			Assert.NotNull(logradouroInserido);
			Assert.True(logradouroInserido.Id > 0);
		}

		[Fact]
		public async Task Logradouro_ObterPorCep_Atualizar()
		{
			var _cep = "12345678";
			// ObterPorCep - existente para edição

			var repoLogradouroBuscaCep = new LogradouroRepository(ConnectionString, DatabaseType);

			var logradouroPorCep = await repoLogradouroBuscaCep.ObterPorCep(_cep);
			Assert.NotNull(logradouroPorCep);

			// Atualizar
			var logradouroAtualizado = Logradouro.Criar("Rua Atualizada", _cep, "Bairro Teste Att", "AT", "TS", "Pais testeAtt");

			// reflexão para definir o ID

			var idProperty = typeof(Entity).GetProperty("Id");

			idProperty?.SetValue(logradouroAtualizado, logradouroPorCep.Id);
			var repoLogradouroEdit = new LogradouroRepository(ConnectionString, DatabaseType);
			var resultadoAtualizacao = await repoLogradouroEdit.Atualizar(logradouroAtualizado);
			Assert.NotNull(resultadoAtualizacao);

			Assert.Equal("AT", resultadoAtualizacao.Estado);
			Assert.Equal("Rua Atualizada", resultadoAtualizacao.Nome);
		}

		[Fact]
		public async Task Logradouro_ObterPorCep_Remover_ObterPorId()
		{
			var _cep = "99999998"; // CEP único para o teste

			// Criar logradouro
			var logradouroParaAdicionar = Logradouro.Criar(
				"Rua Teste Remover",
				_cep,
				"Bairro Teste",
				"SC",
				"TS",
				"Pais Teste"
			);

			var repoAdd = new LogradouroRepository(ConnectionString, DatabaseType);
			var logradouroInserido = await repoAdd.Adicionar(logradouroParaAdicionar);
			Assert.NotNull(logradouroInserido);

			// ObterPorCep
			var repoBuscaCep = new LogradouroRepository(ConnectionString, DatabaseType);
			var logradouroPorCep = await repoBuscaCep.ObterPorCep(_cep);
			Assert.NotNull(logradouroPorCep);

			// Remover
			var repoRemover = new LogradouroRepository(ConnectionString, DatabaseType);
			var resultadoRemocao = await repoRemover.Remover(logradouroPorCep.Id);
			Assert.True(resultadoRemocao);

			// ObterPorId
			var repoPorId = new LogradouroRepository(ConnectionString, DatabaseType);
			var logradouroRemovido = await repoPorId.ObterPorId(logradouroPorCep.Id);
			Assert.Null(logradouroRemovido);
		}


		[Fact]
		public async Task Logradouro_ObterPorCidade()
		{
			var cidadeExistente = "Lages";
			// ObterPorCidade

			var repoLogradouroPorCidade = new LogradouroRepository(ConnectionString, DatabaseType);
			var resultados = await repoLogradouroPorCidade.ObterPorCidade(cidadeExistente);
			Assert.NotNull(resultados);
			Assert.NotEmpty(resultados);
		}

		[Fact]
		public async Task Logradouro_ObterTodos()
		{
			// ObterTodos

			var repoLogradouroTodos = new LogradouroRepository(ConnectionString, DatabaseType);

			var resultado = await repoLogradouroTodos.ObterTodos();
			Assert.NotNull(resultado);
		}
	}
}
