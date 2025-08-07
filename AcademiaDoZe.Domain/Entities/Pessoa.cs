//Vanessa Furtado Nunes
using AcademiaDoZe.Domain.Exceptions;
using AcademiaDoZe.Domain.ValueObject;

namespace AcademiaDoZe.Domain.Entities;

public abstract class Pessoa : Entity
{
    public string Nome { get; protected set; }
    public string Cpf { get; protected set; }
    public DateOnly DataNascimento { get; protected set; }
    public string Telefone { get; protected set; }
    public string Email { get; protected set; }
    public Logradouro Endereco { get; protected set; }
    public string Numero { get; protected set; }
    public string Complemento { get; protected set; }
    public string Senha { get; protected set; }
    public Arquivo Foto { get; protected set; }
    protected Pessoa(string nome,
    string cpf,

    DateOnly dataNascimento,
    string telefone,
    string email,
    Logradouro endereco,
    string numero,
    string complemento,
    string senha,
    Arquivo foto) : base()

    {
        Nome = nome;
        Cpf = cpf;
        DataNascimento = dataNascimento;
        Telefone = telefone;
        Email = email;
        Endereco = endereco;
        Numero = numero;
        Complemento = complemento;
        Senha = senha;
        Foto = foto;

		if (string.IsNullOrWhiteSpace(cpf))
			throw new DomainException("CPF não pode ser vazio.");
		if (string.IsNullOrWhiteSpace(nome))
			throw new DomainException("Nome não pode ser vazio.");
		if (dataNascimento == default)
			throw new DomainException("Data de nascimento não pode ser nula.");
		if (string.IsNullOrWhiteSpace(telefone))
			throw new DomainException("Telefone não pode ser vazio.");
		if (string.IsNullOrWhiteSpace(senha))
			throw new DomainException("Senha não pode ser vazia.");
		if (endereco is null)
			throw new DomainException("Endereco não pode ser nulo.");
		if (string.IsNullOrWhiteSpace(numero))
			throw new DomainException("Número não pode ser vazio.");
	}

	public virtual int Idade()
	{
		var hoje = DateOnly.FromDateTime(DateTime.Today);
		var idade = hoje.Year - DataNascimento.Year;

		if (hoje.Month < DataNascimento.Month || (hoje.Month == DataNascimento.Month && hoje.Day < DataNascimento.Day))
			idade--;

		return idade;
	}

	public virtual Catraca Entrar()
	{
		return Catraca.Criar(this, DateTime.Now, 0);
	}

	public virtual Catraca Sair()
	{
		return Catraca.Criar(this, DateTime.Now, 0);
	}
}
