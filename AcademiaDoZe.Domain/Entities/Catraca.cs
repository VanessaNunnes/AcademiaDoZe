//Vanessa Furtado Nunes
using AcademiaDoZe.Domain.Enums;
using AcademiaDoZe.Domain.Exceptions;

namespace AcademiaDoZe.Domain.Entities;
	public class Catraca : Entity
	{
		public Catraca(Pessoa pessoa, DateTime dataHora, EPessoaTipo tipoPessoa)
		{
			if (pessoa is null)
				throw new ArgumentNullException(nameof(pessoa), "Pessoa não pode ser nula.");

			if (dataHora == default)
				throw new ArgumentException("Data e hora não podem ser nulas.", nameof(dataHora));

			Pessoa = pessoa;
			DataHora = dataHora;
			TipoPessoa = tipoPessoa;
	}

		public Pessoa Pessoa { get; set; }
		public DateTime DataHora { get; set; }
		public EPessoaTipo TipoPessoa { get; set; }

	public static Catraca Criar(Pessoa pessoa, DateTime dataHora, EPessoaTipo tipoPessoa)
	{
		if (pessoa is null)
			throw new DomainException("Pessoa não pode ser nula.");

		if (dataHora == default)
			throw new DomainException("Data e hora não podem ser nulas.");

		return new Catraca(pessoa, dataHora, tipoPessoa);
	}
}