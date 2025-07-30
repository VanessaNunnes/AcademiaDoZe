using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDoZe.Domain.Entities
{
	public class Catraca : Entity
	{
		public Catraca(Pessoa pessoa, DateTime dataHora)
		{
			if (pessoa is null)
				throw new ArgumentNullException(nameof(pessoa), "Pessoa não pode ser nula.");

			if (dataHora == default)
				throw new ArgumentException("Data e hora não podem ser nulas.", nameof(dataHora));

			Pessoa = pessoa;
			DataHora = dataHora;
		}

		public Pessoa Pessoa { get; set; }
		public DateTime DataHora { get; set; }
	}
}
