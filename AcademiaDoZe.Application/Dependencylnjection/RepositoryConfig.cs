using AcademiaDoZe.Application.Enums;

namespace AcademiaDoZe.Application.Dependencylnjection
{
	public class RepositoryConfig
	{
		public required string ConnectionString { get; set; }
		public required EAppDatabaseType DatabaseType { get; set; }
	}
}
