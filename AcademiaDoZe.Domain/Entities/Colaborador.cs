using AcademiaDoZe.Domain.Enums;
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

	public Catraca RegistrarEntradaAluno(Aluno aluno)
	{
		try
		{
			var registro = new Catraca(aluno, DateTime.Now);
			return registro;
		}
		catch (InvalidOperationException ex)
		{
			throw new InvalidOperationException("Erro ao registrar entrada do aluno: " + ex.Message);
		}
	}

	public Catraca RegistrarSaidaAluno(Aluno aluno)
	{
		try
		{
			var registro = new Catraca(aluno, DateTime.Now);
			return registro;
		}
		catch (InvalidOperationException ex)
		{
			throw new InvalidOperationException("Erro ao registrar entrada do aluno: " + ex.Message);
		}

	}

	public Aluno CadastrarAluno(string cpf, string nome, DateOnly dataNascimento, string? email, string telefone,
		string senha, string? foto, Logradouro logradouro, string numero, string? complemento)
	{
		if (this.Tipo == EColaboradorTipo.Instrutor)
			throw new InvalidOperationException("Somente atendentes e administradores podem cadastrar alunos.");
		try
		{
			var arquivoFoto = new Arquivo(new byte[0]);

			var novoAluno = new Aluno(nome, cpf, dataNascimento, telefone, email,
				logradouro, numero, complemento ?? "", senha, arquivoFoto);

			return novoAluno;
		}
		catch (ArgumentException ex)
		{
			throw new InvalidOperationException("Erro ao cadastrar aluno: " + ex.Message);
		}
	}

	public Matricula MatricularAluno(Aluno aluno, EMatriculaPlano plano, DateOnly dataInicio, DateOnly dataFim, string objetivo, EMatriculaRestricoes? restricoes, Arquivo? laudo)
	{
		if (this.Tipo == EColaboradorTipo.Instrutor)
			throw new InvalidOperationException("Somente atendentes e administradores podem cadastrar alunos.");

		try
		{
			var matricula = new Matricula(aluno, plano, dataInicio, dataFim, objetivo, restricoes, laudo);
			return matricula;
		}
		catch (ArgumentException ex)
		{
			throw new InvalidOperationException("Erro ao matricular aluno: " + ex.Message);
		}
	}
}
