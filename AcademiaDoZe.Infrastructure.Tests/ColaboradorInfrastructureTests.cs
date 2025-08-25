﻿using AcademiaDoZe.Domain.Entities;
using AcademiaDoZe.Domain.Enums;
using AcademiaDoZe.Domain.ValueObject;
using AcademiaDoZe.Infrastructure_.Repositories;

namespace AcademiaDoZe.Infrastructure.Tests
{
	public class ColaboradorInfrastructureTests : TestBase
	{
		[Fact]
		public async Task Colaborador_LogradouroPorId_CpfJaExiste_Adicionar()
		{
			// com base em logradouroID, acessar logradourorepository e obter o logradouro

			var logradouroId = 4;
			var repoLogradouroObterPorId = new LogradouroRepository(ConnectionString, DatabaseType);
			Logradouro? logradouro = await repoLogradouroObterPorId.ObterPorId(logradouroId);
			// cria um arquivo de exemplo

			Arquivo arquivo = Arquivo.Criar(new byte[] { 1, 2, 3 }, "jpg");

			var _cpf = "12345678900";
			// verifica se cpf já existe

			var repoColaboradorCpf = new ColaboradorRepository(ConnectionString, DatabaseType);

			var cpfExistente = await repoColaboradorCpf.CpfJaExiste(_cpf);
			Assert.False(cpfExistente, "CPF já existe no banco de dados.");
			var colaborador = Colaborador.Criar(
				dataAdmissao: new DateOnly(2024, 05, 04),
				tipoColaborador: EColaboradorTipo.Administrador,
				vinculo: EColaboradorVinculo.CLT,
				cpf: _cpf,
				nome: "zé",
				dataNascimento: new DateOnly(2010, 10, 09),
				email: "ze@com.br",
				telefone: "49999999999",
				senha: "abcBolinhas",
				foto: arquivo,
				logradouro: logradouro!,
				numero: "123",
				complemento: "complemento casa"
			);
			// Adicionar

			var repoColaboradorAdicionar = new ColaboradorRepository(ConnectionString, DatabaseType);
			var colaboradorInserido = await repoColaboradorAdicionar.Adicionar(colaborador);
			Assert.NotNull(colaboradorInserido);
			Assert.True(colaboradorInserido.Id > 0);
		}

		[Fact]
		public async Task Colaborador_ObterPorCpf_Atualizar()
		{
			var _cpf = "12345678900";
			Arquivo arquivo = Arquivo.Criar(new byte[] { 1, 2, 3 }, "jpg");

			var repoColaborador = new ColaboradorRepository(ConnectionString, DatabaseType);
			var colaboradorExistente = await repoColaborador.ObterPorCpf(_cpf);
			Assert.NotNull(colaboradorExistente);

			// Extrair os tipos corretos
			DateOnly dataNascimento = colaboradorExistente.DataNascimento;
			EColaboradorTipo tipo = colaboradorExistente.Tipo;

			// Criar colaborador atualizado
			var colaboradorAtualizado = Colaborador.Criar(
				nome: "zé dos testes 123",
				cpf: colaboradorExistente.Cpf,
				dataNascimento: dataNascimento,
				telefone: colaboradorExistente.Telefone,
				email: colaboradorExistente.Email,
				logradouro: colaboradorExistente.Endereco,
				numero: colaboradorExistente.Numero,
				complemento: colaboradorExistente.Complemento,
				senha: colaboradorExistente.Senha,
				foto: arquivo,
				dataAdmissao: colaboradorExistente.DataAdmissao,
				tipoColaborador: tipo,
				vinculo: colaboradorExistente.Vinculo
			);

			// Ajustar o ID via reflexão
			var idProperty = typeof(Entity).GetProperty("Id");
			idProperty?.SetValue(colaboradorAtualizado, colaboradorExistente.Id);

			// Atualizar no repositório
			var resultadoAtualizacao = await repoColaborador.Atualizar(colaboradorAtualizado);
			Assert.NotNull(resultadoAtualizacao);

			Assert.Equal("zé dos testes 123", resultadoAtualizacao.Nome);
		}


		[Fact]
		public async Task Colaborador_ObterPorCpf_TrocarSenha()
		{
			var _cpf = "12345678900";
			Arquivo arquivo = Arquivo.Criar(new byte[] { 1, 2, 3 }, "jpg");
			var repoColaboradorObterPorCpf = new ColaboradorRepository(ConnectionString, DatabaseType);
			var colaboradorExistente = await repoColaboradorObterPorCpf.ObterPorCpf(_cpf);
			Assert.NotNull(colaboradorExistente);
			var novaSenha = "novaSenha123";
			var repoColaboradorTrocarSenha = new ColaboradorRepository(ConnectionString, DatabaseType);

			var resultadoTrocaSenha = await repoColaboradorTrocarSenha.TrocarSenha(colaboradorExistente.Id, novaSenha);
			Assert.True(resultadoTrocaSenha);

			var repoColaboradorObterPorId = new ColaboradorRepository(ConnectionString, DatabaseType);
			var colaboradorAtualizado = await repoColaboradorObterPorId.ObterPorId(colaboradorExistente.Id);
			Assert.NotNull(colaboradorAtualizado);
			Assert.Equal(novaSenha, colaboradorAtualizado.Senha);
		}

		[Fact]
		public async Task Colaborador_ObterPorCpf_Remover_ObterPorId()
		{
			var _cpf = "12345678900";
			var repoColaboradorObterPorCpf = new ColaboradorRepository(ConnectionString, DatabaseType);
			var colaboradorExistente = await repoColaboradorObterPorCpf.ObterPorCpf(_cpf);
			Assert.NotNull(colaboradorExistente);

			// Remover
			var repoColaboradorRemover = new ColaboradorRepository(ConnectionString, DatabaseType);
			var resultadoRemover = await repoColaboradorRemover.Remover(colaboradorExistente.Id);
			Assert.True(resultadoRemover);

			var repoColaboradorObterPorId = new ColaboradorRepository(ConnectionString, DatabaseType);
			var resultadoRemovido = await repoColaboradorObterPorId.ObterPorId(colaboradorExistente.Id);
			Assert.Null(resultadoRemovido);
		}

		[Fact]
		public async Task Colaborador_ObterTodos()
		{
			var repoColaboradorRepository = new ColaboradorRepository(ConnectionString, DatabaseType);
			var resultado = await repoColaboradorRepository.ObterTodos();
			Assert.NotNull(resultado);
		}
	}
}
