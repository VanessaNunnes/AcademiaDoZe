using AcademiaDoZe.Domain.Entities;
using AcademiaDoZe.Domain.Repositories;
using AcademiaDoZe.Domain.ValueObject;
using AcademiaDoZe.Infrastructure_.Data;
using System.Data.Common;
using System.Data;
using AcademiaDoZe.Domain.Enums;

namespace AcademiaDoZe.Infrastructure_.Repositories
{
	public class MatriculaRepository : BaseRepository<Matricula>, IMatriculaRepository
	{
		public MatriculaRepository(string connectionString, DatabaseType databaseType)
			: base(connectionString, databaseType)
		{
		}

		public override async Task<Matricula> Adicionar(Matricula entity)
		{
			try
			{
				await using var connection = await GetOpenConnectionAsync();
				string query = _databaseType == DatabaseType.SqlServer
					? $"INSERT INTO {TableName} (aluno_id, colaborador_id, plano, data_inicio, data_fim, objetivo, restricao_medica, laudo_medico, obs_restricao) " +
					  "OUTPUT INSERTED.id_matricula " +
					  "VALUES (@AlunoId, @ColaboradorId, @Plano, @DataInicio, @DataFim, @Objetivo, @RestricaoMedica, @LaudoMedico, @ObsRestricao);"
					: $"INSERT INTO {TableName} (aluno_id, colaborador_id, plano, data_inicio, data_fim, objetivo, restricao_medica, laudo_medico, obs_restricao) " +
					  "VALUES (@AlunoId, @ColaboradorId, @Plano, @DataInicio, @DataFim, @Objetivo, @RestricaoMedica, @LaudoMedico, @ObsRestricao); " +
					  "SELECT LAST_INSERT_ID();";

				await using var command = DbProvider.CreateCommand(query, connection);
				command.Parameters.Add(DbProvider.CreateParameter("@AlunoId", entity.AlunoMatricula.Id, DbType.Int32, _databaseType));
				command.Parameters.Add(DbProvider.CreateParameter("@ColaboradorId", entity.Colaborador?.Id ?? 0, DbType.Int32, _databaseType));
				command.Parameters.Add(DbProvider.CreateParameter("@Plano", (int)entity.Plano, DbType.Int32, _databaseType));
				command.Parameters.Add(DbProvider.CreateParameter("@DataInicio", entity.DataInicio, DbType.Date, _databaseType));
				command.Parameters.Add(DbProvider.CreateParameter("@DataFim", entity.DataFim, DbType.Date, _databaseType));
				command.Parameters.Add(DbProvider.CreateParameter("@Objetivo", entity.Objetivo, DbType.String, _databaseType));
				command.Parameters.Add(DbProvider.CreateParameter("@RestricaoMedica", entity.RestricoesMedicas.HasValue ? (int)entity.RestricoesMedicas.Value : (object)DBNull.Value, DbType.Int32, _databaseType));
				command.Parameters.Add(DbProvider.CreateParameter("@LaudoMedico", entity.LaudoMedico?.Conteudo ?? (object)DBNull.Value, DbType.Binary, _databaseType));
				command.Parameters.Add(DbProvider.CreateParameter("@ObsRestricao", entity.ObservacoesRestricoes ?? (object)DBNull.Value, DbType.String, _databaseType));

				var id = await command.ExecuteScalarAsync();
				if (id != null && id != DBNull.Value)
				{
					typeof(Entity).GetProperty("Id")?.SetValue(entity, Convert.ToInt32(id));
				}
				return entity;
			}
			catch (DbException ex)
			{
				throw new InvalidOperationException($"Erro ao adicionar matrícula: {ex.Message}", ex);
			}
		}

		public override async Task<Matricula> Atualizar(Matricula entity)
		{
			try
			{
				await using var connection = await GetOpenConnectionAsync();
				string query = $"UPDATE {TableName} SET " +
							   "aluno_id = @AlunoId, " +
							   "colaborador_id = @ColaboradorId, " +
							   "plano = @Plano, " +
							   "data_inicio = @DataInicio, " +
							   "data_fim = @DataFim, " +
							   "objetivo = @Objetivo, " +
							   "restricao_medica = @RestricaoMedica, " +
							   "laudo_medico = @LaudoMedico, " +
							   "obs_restricao = @ObsRestricao " +
							   "WHERE id_matricula = @Id";

				await using var command = DbProvider.CreateCommand(query, connection);
				command.Parameters.Add(DbProvider.CreateParameter("@Id", entity.Id, DbType.Int32, _databaseType));
				command.Parameters.Add(DbProvider.CreateParameter("@AlunoId", entity.AlunoMatricula.Id, DbType.Int32, _databaseType));
				command.Parameters.Add(DbProvider.CreateParameter("@ColaboradorId", entity.Colaborador?.Id ?? 0, DbType.Int32, _databaseType));
				command.Parameters.Add(DbProvider.CreateParameter("@Plano", (int)entity.Plano, DbType.Int32, _databaseType));
				command.Parameters.Add(DbProvider.CreateParameter("@DataInicio", entity.DataInicio, DbType.Date, _databaseType));
				command.Parameters.Add(DbProvider.CreateParameter("@DataFim", entity.DataFim, DbType.Date, _databaseType));
				command.Parameters.Add(DbProvider.CreateParameter("@Objetivo", entity.Objetivo, DbType.String, _databaseType));
				command.Parameters.Add(DbProvider.CreateParameter("@RestricaoMedica", entity.RestricoesMedicas.HasValue ? (int)entity.RestricoesMedicas.Value : (object)DBNull.Value, DbType.Int32, _databaseType));
				command.Parameters.Add(DbProvider.CreateParameter("@LaudoMedico", entity.LaudoMedico?.Conteudo ?? (object)DBNull.Value, DbType.Binary, _databaseType));
				command.Parameters.Add(DbProvider.CreateParameter("@ObsRestricao", entity.ObservacoesRestricoes ?? (object)DBNull.Value, DbType.String, _databaseType));

				int rowsAffected = await command.ExecuteNonQueryAsync();
				if (rowsAffected == 0)
				{
					throw new InvalidOperationException($"Nenhuma matrícula encontrada com o ID {entity.Id} para atualização.");
				}
				return entity;
			}
			catch (DbException ex)
			{
				throw new InvalidOperationException($"Erro ao atualizar matrícula com ID {entity.Id}: {ex.Message}", ex);
			}
		}

		protected override async Task<Matricula> MapAsync(DbDataReader reader)
		{
			var alunoId = Convert.ToInt32(reader["aluno_id"]);
			var alunoRepository = new AlunoRepository(_connectionString, _databaseType);
			var aluno = await alunoRepository.ObterPorId(alunoId)
						?? throw new InvalidOperationException($"Aluno com ID {alunoId} não encontrado.");

			var matricula = Matricula.Criar(
				aluno: aluno,
				plano: (EMatriculaPlano)Convert.ToInt32(reader["plano"]),
				dataInicio: DateOnly.FromDateTime(Convert.ToDateTime(reader["data_inicio"])),
				dataFim: reader["data_fim"] is DBNull ? null : DateOnly.FromDateTime(Convert.ToDateTime(reader["data_fim"])),
				objetivo: reader["objetivo"].ToString(),
				restricoes: reader["restricao_medica"] is DBNull ? null : (EMatriculaRestricoes)Convert.ToInt32(reader["restricao_medica"]),
				laudo: reader["laudo_medico"] is DBNull ? null : Arquivo.Criar((byte[])reader["laudo_medico"])
			);

			typeof(Entity).GetProperty("Id")?.SetValue(matricula, Convert.ToInt32(reader["id_matricula"]));
			return matricula;
		}

		public async Task<IEnumerable<Matricula>> ObterAtivas()
		{
			try
			{
				await using var connection = await GetOpenConnectionAsync();
				string query = $"SELECT * FROM {TableName} WHERE data_fim >= @Hoje";
				await using var command = DbProvider.CreateCommand(query, connection);
				command.Parameters.Add(DbProvider.CreateParameter("@Hoje", DateTime.Today, DbType.Date, _databaseType));

				var matriculas = new List<Matricula>();
				using var reader = await command.ExecuteReaderAsync();
				while (await reader.ReadAsync())
				{
					matriculas.Add(await MapAsync(reader));
				}
				return matriculas;
			}
			catch (DbException ex)
			{
				throw new InvalidOperationException($"Erro ao obter matrículas ativas: {ex.Message}", ex);
			}
		}

		public async Task<IEnumerable<Matricula>> ObterPorAluno(int alunoId)
		{
			try
			{
				await using var connection = await GetOpenConnectionAsync();
				string query = $"SELECT * FROM {TableName} WHERE aluno = @AlunoId";
				await using var command = DbProvider.CreateCommand(query, connection);
				command.Parameters.Add(DbProvider.CreateParameter("@AlunoId", alunoId, DbType.Int32, _databaseType));

				var matriculas = new List<Matricula>();
				using var reader = await command.ExecuteReaderAsync();
				while (await reader.ReadAsync())
				{
					matriculas.Add(await MapAsync(reader));
				}
				return matriculas;
			}
			catch (DbException ex)
			{
				throw new InvalidOperationException($"Erro ao obter matrículas do aluno {alunoId}: {ex.Message}", ex);
			}
		}

		public async Task<IEnumerable<Matricula>> ObterVencendoEmDias(int dias)
		{
			try
			{
				await using var connection = await GetOpenConnectionAsync();
				string query = $"SELECT * FROM {TableName} WHERE DATEDIFF(day, @Hoje, data_fim) <= @Dias AND data_fim >= @Hoje";
				if (_databaseType == DatabaseType.MySql)
				{
					// Ajuste para MySQL
					query = $"SELECT * FROM {TableName} WHERE DATEDIFF(data_fim, @Hoje) <= @Dias AND data_fim >= @Hoje";
				}

				await using var command = DbProvider.CreateCommand(query, connection);
				command.Parameters.Add(DbProvider.CreateParameter("@Hoje", DateTime.Today, DbType.Date, _databaseType));
				command.Parameters.Add(DbProvider.CreateParameter("@Dias", dias, DbType.Int32, _databaseType));

				var matriculas = new List<Matricula>();
				using var reader = await command.ExecuteReaderAsync();
				while (await reader.ReadAsync())
				{
					matriculas.Add(await MapAsync(reader));
				}
				return matriculas;
			}
			catch (DbException ex)
			{
				throw new InvalidOperationException($"Erro ao obter matrículas vencendo em {dias} dias: {ex.Message}", ex);
			}
		}

	}
}
