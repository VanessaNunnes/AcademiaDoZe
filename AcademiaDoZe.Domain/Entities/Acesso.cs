//Vanessa Furtado Nunes
using AcademiaDoZe.Domain.Enums;
using AcademiaDoZe.Domain.Exceptions;

namespace AcademiaDoZe.Domain.Entities;

public class Acesso : Entity
{
    public Pessoa AlunoColaborador { get; private set; }
    public DateTime DataHora { get; private set; }
	public EPessoaTipo Tipo { get; private set; }
	public Acesso(Pessoa pessoa, DateTime dataHora, EPessoaTipo tipo)
    {
        AlunoColaborador = pessoa;
        DataHora = dataHora;
		Tipo = tipo;
	}

	public static Acesso Criar(EPessoaTipo tipo, Pessoa pessoa, DateTime dataHora)
	{

		if (!Enum.IsDefined(tipo)) throw new DomainException("TIPO_OBRIGATORIO");
		if (pessoa == null) throw new DomainException("PESSOA_OBRIGATORIA");
		if (dataHora < DateTime.Now) throw new DomainException("DATAHORA_INVALIDA");
		if (dataHora.TimeOfDay < new TimeSpan(6, 0, 0) || dataHora.TimeOfDay > new TimeSpan(22, 0, 0))
			throw new DomainException("DATAHORA_INTERVALO");

		if (pessoa is Aluno aluno)

		{

		}
		else if (pessoa is Colaborador colaborador)
		{

		}

		return new Acesso(pessoa, dataHora, tipo);
	}
}
