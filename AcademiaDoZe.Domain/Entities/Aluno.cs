//Vanessa Furtado Nunes
using AcademiaDoZe.Domain.Enums;
using AcademiaDoZe.Domain.Exceptions;
using AcademiaDoZe.Domain.ValueObject;

namespace AcademiaDoZe.Domain.Entities;

public class Aluno : Pessoa
{
	public Aluno(string nomeCompleto,
				 string cpf,
				 DateOnly dataNascimento,
				 string telefone,
				 string email,
				 Logradouro endereco,
				 string numero,
				 string complemento,
				 string senha,
				 Arquivo foto)
		: base(nomeCompleto, cpf, dataNascimento, telefone, email, endereco, numero, complemento, senha, foto)
	{
	}

	public string GetTempoPermanencia(DateTime inicio, DateTime fim)
	{
		if (fim < inicio)
			throw new ArgumentException("A data final não pode ser anterior à data inicial.");

		TimeSpan tempo = fim - inicio;
		return $"{(int)tempo.TotalHours}h {tempo.Minutes}min";
	}

	public string GetTempoContrato()
	{
		var tempo = DateTime.Now - DataNascimento.ToDateTime(TimeOnly.MinValue);
		return $"{(int)(tempo.TotalDays / 30)} meses";
	}

	public void TrocarSenha(string senhaAtual, string novaSenha)
	{
		if (string.IsNullOrWhiteSpace(senhaAtual))
			throw new ArgumentException("Senha atual não pode ser vazia.", nameof(senhaAtual));

		if (string.IsNullOrWhiteSpace(novaSenha))
			throw new ArgumentException("Nova senha não pode ser vazia.", nameof(novaSenha));

		if (senhaAtual != Senha)
			throw new InvalidOperationException("Senha atual está incorreta.");

		Senha = novaSenha;
	}

	public static Aluno Criar(string nome,
							  string cpf,
							  DateOnly dataNascimento,
							  string telefone,
							  string email,
							  Logradouro logradouro,
							  string numero,
							  string complemento,
							  string senha,
							  Arquivo foto)
	{
		return new Aluno(nome, cpf, dataNascimento, telefone, email, logradouro, numero, complemento, senha, foto);
	}

	public override Catraca Entrar()
	{
		try
		{
			var registro = Catraca.Criar(this, DateTime.Now, EPessoaTipo.Aluno);
			return registro;
		}
		catch (DomainException ex)
		{
			throw new DomainException("Erro ao registrar entrada: " + ex.Message);
		}
	}

	public override Catraca Sair()
	{
		try
		{
			var registro = Catraca.Criar(this, DateTime.Now, EPessoaTipo.Aluno);
			return registro;
		}
		catch (DomainException ex)
		{
			throw new DomainException("Erro ao registrar saída: " + ex.Message);
		}
	}
}
