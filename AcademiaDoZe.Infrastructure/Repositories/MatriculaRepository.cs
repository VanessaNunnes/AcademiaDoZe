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
					? $"INSERT INTO {TableName} (aluno, plano, data_inicio, data_fim, objetivo, restricoes_medicas, laudo_medico, observacoes_restricoes) "
					+ "OUTPUT INSERTED.id_matricula "
					+ "VALUES (@Aluno, @Plano, @Data_inicio, @Data_fim, @Objetivo, @Restricoes_medicas, @Laudo_medico, @Observacoes_restricoes);"
					: $"INSERT INTO {TableName} (aluno, plano, data_inicio, data_fim, objetivo, restricoes_medicas, laudo_medico, observacoes_restricoes) "
					+ "VALUES (@Aluno, @Plano, @Data_inicio, @Data_fim, @Objetivo, @Restricoes_medicas, @Laudo_medico, @Observacoes_restricoes); "
					+ "SELECT LAST_INSERT_ID();";

				await using var command = DbProvider.CreateCommand(query, connection);
				command.Parameters.Add(DbProvider.CreateParameter("@Aluno", entity.AlunoMatricula.Id, DbType.Int32, _databaseType));
				command.Parameters.Add(DbProvider.CreateParameter("@Plano", entity.Plano, DbType.Int32, _databaseType));
				command.Parameters.Add(DbProvider.CreateParameter("@Data_inicio", entity.DataInicio, DbType.Date, _databaseType));
				command.Parameters.Add(DbProvider.CreateParameter("@Data_fim", entity.DataFim, DbType.Date, _databaseType));
				command.Parameters.Add(DbProvider.CreateParameter("@Objetivo", entity.Objetivo, DbType.String, _databaseType));
				command.Parameters.Add(DbProvider.CreateParameter("@Restricoes_medicas", entity.RestricoesMedicas ?? (object)DBNull.Value, DbType.Int32, _databaseType));
				command.Parameters.Add(DbProvider.CreateParameter("@Laudo_medico", entity.LaudoMedico?.Conteudo ?? (object)DBNull.Value, DbType.Binary, _databaseType));
				command.Parameters.Add(DbProvider.CreateParameter("@Observacoes_restricoes", entity.ObservacoesRestricoes ?? string.Empty, DbType.String, _databaseType));

				var id = await command.ExecuteScalarAsync();
				if (id != null && id != DBNull.Value)
				{
					var idProperty = typeof(Entity).GetProperty("Id");
					idProperty?.SetValue(entity, Convert.ToInt32(id));
				}
				return entity;
			}
			catch (DbException ex)
			{
				throw new InvalidOperationException($"Erro ao adicionar matricula: {ex.Message}", ex);
			}
		}

		public override async Task<Matricula> Atualizar(Matricula entity)
		{
			try
			{
				await using var connection = await GetOpenConnectionAsync();
				string query = $"UPDATE {TableName} "
							 + "SET aluno = @Aluno, "
							 + "plano = @Plano, "
							 + "data_inicio = @Data_inicio, "
							 + "data_fim = @Data_fim, "
							 + "objetivo = @Objetivo, "
							 + "restricoes_medicas = @Restricoes_medicas, "
							 + "laudo_medico = @Laudo_medico, "
							 + "observacoes_restricoes = @Observacoes_restricoes "
							 + "WHERE id_matricula = @Id";

				await using var command = DbProvider.CreateCommand(query, connection);
				command.Parameters.Add(DbProvider.CreateParameter("@Id", entity.Id, DbType.Int32, _databaseType));
				command.Parameters.Add(DbProvider.CreateParameter("@Aluno", entity.AlunoMatricula.Id, DbType.Int32, _databaseType));
				command.Parameters.Add(DbProvider.CreateParameter("@Plano", entity.Plano, DbType.Int32, _databaseType));
				command.Parameters.Add(DbProvider.CreateParameter("@Data_inicio", entity.DataInicio, DbType.Date, _databaseType));
				command.Parameters.Add(DbProvider.CreateParameter("@Data_fim", entity.DataFim, DbType.Date, _databaseType));
				command.Parameters.Add(DbProvider.CreateParameter("@Objetivo", entity.Objetivo, DbType.String, _databaseType));
				command.Parameters.Add(DbProvider.CreateParameter("@Restricoes_medicas", entity.RestricoesMedicas ?? (object)DBNull.Value, DbType.Int32, _databaseType));
				command.Parameters.Add(DbProvider.CreateParameter("@Laudo_medico", entity.LaudoMedico?.Conteudo ?? (object)DBNull.Value, DbType.Binary, _databaseType));
				command.Parameters.Add(DbProvider.CreateParameter("@Observacoes_restricoes", entity.ObservacoesRestricoes ?? string.Empty, DbType.String, _databaseType));

				int rowsAffected = await command.ExecuteNonQueryAsync();
				if (rowsAffected == 0)
				{
					throw new InvalidOperationException($"Nenhuma matricula encontrada com o ID {entity.Id} para atualização.");
				}
				return entity;
			}
			catch (DbException ex)
			{
				throw new InvalidOperationException($"Erro ao atualizar matricula com ID {entity.Id}: {ex.Message}", ex);
			}
		}

		public Task<IEnumerable<Matricula>> ObterAtivas()
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Matricula>> ObterPorAluno(int alunoId)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Matricula>> ObterVencendoEmDias(int dias)
		{
			throw new NotImplementedException();
		}

		protected override async Task<Matricula> MapAsync(DbDataReader reader)
		{
			try
			{
				var alunoId = Convert.ToInt32(reader["aluno"]);
				var alunoRepository = new AlunoRepository(_connectionString, _databaseType);
				var aluno = await alunoRepository.ObterPorId(alunoId) ??
							throw new InvalidOperationException($"Aluno com ID {alunoId} não encontrado.");

				var matricula = Matricula.Criar(
					aluno: aluno,
					plano: (EMatriculaPlano)Convert.ToInt32(reader["plano"]),
					dataInicio: DateOnly.FromDateTime(Convert.ToDateTime(reader["data_inicio"])),
					dataFim: DateOnly.FromDateTime(Convert.ToDateTime(reader["data_fim"])),
					objetivo: reader["objetivo"].ToString(),
					restricoes: reader["restricoes_medicas"] is DBNull ? null : (EMatriculaRestricoes)Convert.ToInt32(reader["restricoes_medicas"]),
					laudo: reader["laudo_medico"] is DBNull ? null : Arquivo.Criar((byte[])reader["laudo_medico"], "jpg")
				);

				var idProperty = typeof(Entity).GetProperty("Id");
				idProperty?.SetValue(matricula, Convert.ToInt32(reader["id_matricula"]));

				return matricula;
			}
			catch (DbException ex)
			{
				throw new InvalidOperationException($"Erro ao mapear dados da matricula: {ex.Message}", ex);
			}
		}
	}
}
