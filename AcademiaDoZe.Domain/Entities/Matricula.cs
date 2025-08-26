//Vanessa Furtado Nunes
using AcademiaDoZe.Domain.Enums;
using AcademiaDoZe.Domain.Exceptions;
using AcademiaDoZe.Domain.ValueObject;

namespace AcademiaDoZe.Domain.Entities;

public class Matricula : Entity
{
    public Aluno AlunoMatricula { get; private set; }
    public EMatriculaPlano Plano { get; private set; }
    public DateOnly DataInicio { get; private set; }
    public DateOnly? DataFim { get; private set; }
    public string Objetivo { get; private set; }
    public EMatriculaRestricoes? RestricoesMedicas { get; private set; }
    public string ObservacoesRestricoes { get; private set; }
    public Arquivo LaudoMedico { get; private set; }

	public Colaborador? Colaborador { get; set; }
	public int ColaboradorId => Colaborador?.Id ?? 0;

	public Matricula(Aluno alunoMatricula,
    EMatriculaPlano plano,
    DateOnly dataInicio,
    DateOnly? dataFim,
    string objetivo,
    EMatriculaRestricoes? restricoesMedicas,
    Arquivo laudoMedico)

    : base()
    {
        AlunoMatricula = alunoMatricula;
        Plano = plano;
        DataInicio = dataInicio;
        DataFim = dataFim;
        Objetivo = objetivo;
        RestricoesMedicas = restricoesMedicas;
        LaudoMedico = laudoMedico;

		if (alunoMatricula is null)
			throw new ArgumentNullException(nameof(alunoMatricula));

		if (dataInicio == default || dataFim == default)
			throw new ArgumentException("Data de início e fim não podem ser nulas.", nameof(dataInicio));

		if (dataInicio > dataFim)
			throw new ArgumentException("Data de início não pode ser maior que a data de fim.", nameof(dataInicio));

		if (string.IsNullOrWhiteSpace(objetivo))
			throw new ArgumentException("Objetivo não pode ser vazio.", nameof(objetivo));

		if (alunoMatricula.Idade() < 12)
			throw new InvalidOperationException("Aluno deve ter pelo menos 12 anos para se matricular.");

		if (alunoMatricula.Idade() < 17 && laudoMedico is null)
			throw new InvalidOperationException("Aluno menor de 17 anos deve possuir laudo medico para ser cadastrado.");

		if (restricoesMedicas.HasValue && restricoesMedicas.Value != EMatriculaRestricoes.None && laudoMedico is null)
			throw new InvalidOperationException("Aluno com restrições deve possuir um laudo médico.");
	}

	public static Matricula Criar(
	Aluno aluno,
	EMatriculaPlano plano,
	DateOnly dataInicio,
	DateOnly? dataFim,
	string objetivo,
	EMatriculaRestricoes? restricoes,
	Arquivo? laudo)
	{
		if (aluno is null)
			throw new DomainException(nameof(aluno));

		if (dataInicio == default || !dataFim.HasValue)
			throw new DomainException("Data de início e fim não podem ser nulas.");

		if (dataInicio > dataFim.Value)
			throw new DomainException("Data de início não pode ser maior que a data de fim.");

		if (string.IsNullOrWhiteSpace(objetivo))
			throw new DomainException("Objetivo não pode ser vazio.");

		if (aluno.Idade() < 12)
			throw new DomainException("Aluno deve ter pelo menos 12 anos para se matricular.");

		if (aluno.Idade() < 17 && laudo is null)
			throw new DomainException("Aluno menor de 17 anos deve possuir laudo medico para ser cadastrado.");

		if ((restricoes.HasValue && restricoes.Value != EMatriculaRestricoes.None) && laudo is null)
			throw new DomainException("Aluno com restrições deve possuir um laudo médico.");

		return new Matricula(aluno, plano, dataInicio, dataFim, objetivo, restricoes, laudo);
	}

}
