//Vanessa Furtado Nunes
using AcademiaDoZe.Domain.Enums;
using AcademiaDoZe.Domain.Exceptions;
using AcademiaDoZe.Domain.ValueObject;

namespace AcademiaDoZe.Domain.Entities;

public class Colaborador : Pessoa
{
	public DateOnly DataAdmissao { get; private set; }
	public EColaboradorTipo Tipo { get; private set; }
	public EColaboradorVinculo Vinculo { get; private set; }

	public Colaborador(string nomeCompleto,
	string cpf,
	DateOnly dataNascimento,
	string telefone,
	string email,
	Logradouro endereco,
	string numero,
	string complemento,
	string senha,
	Arquivo foto,
	DateOnly dataAdmissao,
	EColaboradorTipo tipo,
	EColaboradorVinculo vinculo)

	: base(nomeCompleto, cpf, dataNascimento, telefone, email, endereco, numero, complemento, senha, foto)
	{
		DataAdmissao = dataAdmissao;
		Tipo = tipo;
		Vinculo = vinculo;

		if (dataAdmissao == default)
			throw new ArgumentException("Data de admissão não pode ser nula.", nameof(dataAdmissao));

		if (dataAdmissao > DateOnly.FromDateTime(DateTime.Now))
			throw new ArgumentException("Data de admissão não pode ser no futuro.", nameof(dataAdmissao));
	}

	public static Colaborador Criar(DateOnly dataAdmissao, EColaboradorTipo tipoColaborador, EColaboradorVinculo vinculo,
								  string cpf, string nome, DateOnly dataNascimento, string email,
								  string telefone, string senha, Arquivo foto, Logradouro logradouro,
								  string numero, string complemento)
	{

		if (dataAdmissao == default)
			throw new DomainException("Data de admissão não pode ser nula.");

		if (dataAdmissao > DateOnly.FromDateTime(DateTime.Now))
			throw new DomainException("Data de admissão não pode ser no futuro.");

		return new Colaborador(nome, cpf, dataNascimento, telefone, email, logradouro,
							   numero, complemento, senha, foto, dataAdmissao, tipoColaborador, vinculo);
	}

	public Catraca Entrar(Aluno aluno)
	{
		try
		{
			var registro = Catraca.Criar(this, DateTime.Now, EPessoaTipo.Colaborador);
			return registro;
		}
		catch (DomainException ex)
		{
			throw new DomainException("Erro ao registrar entrada: " + ex.Message);
		}
	}

	public Catraca Sair(Aluno aluno)
	{
		try
		{
			var registro = Catraca.Criar(this, DateTime.Now, EPessoaTipo.Colaborador);
			return registro;
		}
		catch (DomainException ex)
		{
			throw new DomainException("Erro ao registrar entrada: " + ex.Message);
		}
	}

	public Catraca RegistrarEntradaAluno(Aluno aluno)
	{
		try
		{
			var registro = Catraca.Criar(aluno, DateTime.Now, EPessoaTipo.Aluno);
			return registro;
		}
		catch (DomainException ex)
		{
			throw new DomainException("Erro ao registrar entrada do aluno: " + ex.Message);
		}
	}

	public Catraca RegistrarSaidaAluno(Aluno aluno)
	{
		try
		{
			var registro = Catraca.Criar(aluno, DateTime.Now, EPessoaTipo.Aluno);
			return registro;
		}
		catch (DomainException ex)
		{
			throw new DomainException("Erro ao registrar entrada do aluno: " + ex.Message);
		}
	}

	public Aluno CadastrarAluno(string cpf, string nome, DateOnly dataNascimento, string? email, string telefone,
		string senha, Arquivo? foto, Logradouro logradouro, string numero, string? complemento)
	{
		if (this.Tipo == EColaboradorTipo.Instrutor)
			throw new DomainException("Somente atendentes e administradores podem cadastrar alunos.");
		try
		{
			var novoAluno = Aluno.Criar(cpf, nome, dataNascimento, email, telefone,
				senha, foto, logradouro, numero, complemento ?? "");

			return novoAluno;
		}
		catch (DomainException ex)
		{
			throw new DomainException("Erro ao cadastrar aluno: " + ex.Message);
		}
	}

	public Matricula MatricularAluno(Aluno aluno, EMatriculaPlano plano, DateOnly dataInicio, DateOnly dataFim, string objetivo, EMatriculaRestricoes? restricoes, Arquivo? laudo)
	{
		if (this.Tipo == EColaboradorTipo.Instrutor)
			throw new DomainException("Somente atendentes e administradores podem cadastrar alunos.");

		try
		{
			var matricula = Matricula.Criar(aluno, plano, dataInicio, dataFim, objetivo, restricoes, laudo);
			return matricula;
		}
		catch (DomainException ex)
		{
			throw new DomainException("Erro ao matricular aluno: " + ex.Message);
		}
	}
}
