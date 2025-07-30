//Vanessa Furtado Nunes
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
			throw new ArgumentException("CPF não pode ser vazio.", nameof(cpf));
		if (string.IsNullOrWhiteSpace(nome))
			throw new ArgumentException("Nome não pode ser vazio.", nameof(nome));
		if (dataNascimento == default)
			throw new ArgumentException("Data de nascimento não pode ser nula.", nameof(dataNascimento));
		if (string.IsNullOrWhiteSpace(telefone))
			throw new ArgumentException("Telefone não pode ser vazio.", nameof(telefone));
		if (string.IsNullOrWhiteSpace(senha))
			throw new ArgumentException("Senha não pode ser vazia.", nameof(senha));
		if (endereco is null)
			throw new ArgumentNullException(nameof(endereco), "Endereço não pode ser nulo.");
		if (string.IsNullOrWhiteSpace(numero))
			throw new ArgumentException("Número não pode ser vazio.", nameof(numero));
	}

	public virtual int Idade()
	{
		var hoje = DateOnly.FromDateTime(DateTime.Today);
		var idade = hoje.Year - DataNascimento.Year;

		if (hoje.Month < DataNascimento.Month || (hoje.Month == DataNascimento.Month && hoje.Day < DataNascimento.Day))
			idade--;

		return idade;
	}

	public virtual void Entrar()
	{
		var registro = new Catraca(this, DateTime.Now);
	}

	public virtual void Sair()
	{
		var registro = new Catraca(this, DateTime.Now);
	}
}
