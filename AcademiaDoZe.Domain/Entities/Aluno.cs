//Vanessa Furtado Nunes
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

	public string GetTempoPermanencia(DateTime Inicio, DateTime FIm)
	{
		throw new NotImplementedException("Método não implementado.");
	}

	public string GetTempoContrato()
	{
		throw new NotImplementedException("Método não implementado.");
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
}
