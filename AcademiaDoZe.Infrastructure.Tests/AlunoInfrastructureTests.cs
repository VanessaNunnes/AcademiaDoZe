////Vanessa Furtado Nunes
//using AcademiaDoZe.Domain.Entities;
//using AcademiaDoZe.Domain.ValueObject;
//using AcademiaDoZe.Infrastructure_.Data;
//using AcademiaDoZe.Infrastructure_.Repositories;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace AcademiaDoZe.Infrastructure.Tests
//{
//	public class AlunoInfrastructureTests : TestBase
//	{
//		private const string TestCpf = "98765432100";

//		[Fact]
//		public async Task Aluno_Adicionar_ObterPorCpf_ObterPorId()
//		{
//			var logradouroRepo = new LogradouroRepository(ConnectionString, DatabaseType);
//			var logradouro = await logradouroRepo.ObterPorId(1);
//			var cpfNovo = Guid.NewGuid().ToString("N").Substring(0, 11); 
//			Assert.NotNull(logradouro);

//			var arquivo = Arquivo.Criar(new byte[] { 1, 2, 3 });

//			var aluno = Aluno.Criar(
//				cpf: cpfNovo,
//				nome: "Vanessaaa terste",
//				dataNascimento: new DateOnly(2005, 8, 25),
//				email: "vanessa@test.com",
//				telefone: "49999999988",
//				senha: "senha123",
//				foto: arquivo,
//				logradouro: logradouro!,
//				numero: "123",
//				complemento: "Casa"
//			);

//			var repo = new AlunoRepository(ConnectionString, DatabaseType);
//			var alunoInserido = await repo.Adicionar(aluno);

//			Assert.NotNull(alunoInserido);
//			Assert.True(alunoInserido.Id > 0);

//			var alunoObtido = await repo.ObterPorCpf(cpfNovo);
//			Assert.NotNull(alunoObtido);
//			Assert.Equal("Vanessa", alunoObtido!.Nome);

//			var alunoPorId = await repo.ObterPorId(alunoInserido.Id);
//			Assert.NotNull(alunoPorId);
//			Assert.Equal(cpfNovo, alunoPorId!.Cpf);
//		}

//		[Fact]
//		public async Task Aluno_CpfJaExiste()
//		{
//			var repo = new AlunoRepository(ConnectionString, DatabaseType);
//			var existe = await repo.CpfJaExiste(TestCpf);
//			Assert.True(existe, "CPF deve existir no banco após inserção.");
//		}

//		[Fact]
//		public async Task Aluno_TrocarSenha()
//		{
//			var repo = new AlunoRepository(ConnectionString, DatabaseType);
//			var aluno = await repo.ObterPorCpf("98765432100");
//			Assert.NotNull(aluno);

//			var novaSenha = "novaSenha123";
//			var resultado = await repo.TrocarSenha(aluno!.Id, novaSenha);
//			Assert.True(resultado);

//			var alunoAtualizado = await repo.ObterPorId(aluno.Id);
//			Assert.NotNull(alunoAtualizado);
//			Assert.Equal(novaSenha, alunoAtualizado!.Senha);
//		}

//		[Fact]
//		public async Task Aluno_Atualizar()
//		{
//			var repo = new AlunoRepository(ConnectionString, DatabaseType);
//			var alunoExistente = await repo.ObterPorCpf("98765432100");
//			Assert.NotNull(alunoExistente);

//			// Criar novo aluno baseado no existente, alterando apenas o Nome
//			var alunoAtualizado = Aluno.Criar(
//				"Vanessa Atualizada",
//				alunoExistente.Cpf,
//				alunoExistente.DataNascimento,
//				 alunoExistente.Telefone,
//				 alunoExistente.Email,
//				 alunoExistente.Endereco,
//				 alunoExistente.Numero,
//				 alunoExistente.Complemento,
//				 alunoExistente.Senha,
//				 alunoExistente.Foto
//			);

//			// Usar reflection para manter o mesmo ID
//			var idProperty = typeof(Entity).GetProperty("Id");
//			idProperty?.SetValue(alunoAtualizado, alunoExistente.Id);

//			var resultado = await repo.Atualizar(alunoAtualizado);

//			Assert.NotNull(resultado);
//			Assert.Equal("Vanessa Atualizada", resultado.Nome);
//		}


//		[Fact]
//		public async Task Aluno_ObterTodos()
//		{
//			var repo = new AlunoRepository(ConnectionString, DatabaseType);
//			var alunos = await repo.ObterTodos();
//			Assert.NotNull(alunos);
//			Assert.Contains(alunos, a => a.Cpf == TestCpf);
//		}

//		[Fact]
//		public async Task Aluno_Remover()
//		{
//			var repo = new AlunoRepository(ConnectionString, DatabaseType);
//			var aluno = await repo.ObterPorCpf("98765432100");
//			Assert.NotNull(aluno);

//			var resultado = await repo.Remover(aluno!.Id);
//			Assert.True(resultado);

//			var alunoRemovido = await repo.ObterPorId(aluno.Id);
//			Assert.Null(alunoRemovido);
//		}
//	}
//}
