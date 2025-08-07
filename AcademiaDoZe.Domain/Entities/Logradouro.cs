//Vanessa Furtado Nunes
using AcademiaDoZe.Domain.Exceptions;
using AcademiaDoZe.Domain.Services;

namespace AcademiaDoZe.Domain.Entities;

public sealed class Logradouro : Entity
{
    public string Cep { get; }
    public string Nome { get; }
    public string Bairro { get; }
    public string Cidade { get; }
    public string Estado { get; }
    public string Pais { get; }
    public Logradouro(string cep,
    string nome,
    string bairro,
    string cidade,
    string estado,
    string pais) : base()

    {
        Cep = cep;
        Nome = nome;
        Bairro = bairro;
        Cidade = cidade;
        Estado = estado;
        Pais = pais;
	}

	public static Logradouro Criar(string nomeLogradouro, string cEP, string pais, string estado, string cidade, string bairro)
	{
		cEP = TextoNormalizadoService.LimparEDigitos(cEP);
		nomeLogradouro = TextoNormalizadoService.LimparEspacos(nomeLogradouro);
		pais = TextoNormalizadoService.LimparEspacos(pais);
		estado = TextoNormalizadoService.LimparTodosEspacos(estado);
		cidade = TextoNormalizadoService.LimparEspacos(cidade);
		bairro = TextoNormalizadoService.LimparEspacos(bairro);

		if (string.IsNullOrWhiteSpace(nomeLogradouro))
			throw new DomainException("Nome do logradouro não pode ser vazio.");
		if (string.IsNullOrWhiteSpace(cEP))
			throw new DomainException("CEP não pode ser vazio.");
		if (cEP.Length != 8)
			throw new DomainException("CEP deve conter 8 dígitos.");
		if (string.IsNullOrWhiteSpace(pais))
			throw new DomainException("País não pode ser vazio.");
		if (string.IsNullOrWhiteSpace(estado))
			throw new DomainException("Estado não pode ser vazio.");
		if (string.IsNullOrWhiteSpace(cidade))
			throw new DomainException("Cidade não pode ser vazia.");
		if (string.IsNullOrWhiteSpace(bairro))
			throw new DomainException("Bairro não pode ser vazio.");

		return new Logradouro(cEP, nomeLogradouro, bairro, cidade, estado, pais);

	}
}