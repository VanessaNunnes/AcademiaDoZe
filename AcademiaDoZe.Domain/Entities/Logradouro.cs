using AcademiaDoZe.Domain.Exceptions;
using AcademiaDoZe.Domain.Services;
using System.Runtime.ConstrainedExecution;

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

		if (string.IsNullOrWhiteSpace(nome))
			throw new ArgumentException("Nome do logradouro não pode ser vazio.", nameof(nome));
		if (string.IsNullOrWhiteSpace(cep))
			throw new ArgumentException("CEP não pode ser vazio.", nameof(cep));
		if (string.IsNullOrWhiteSpace(pais))
			throw new ArgumentException("País não pode ser vazio.", nameof(pais));
		if (string.IsNullOrWhiteSpace(cidade))
			throw new ArgumentException("Cidade não pode ser vazia.", nameof(cidade));
		if (string.IsNullOrWhiteSpace(bairro))
			throw new ArgumentException("Bairro não pode ser vazio.", nameof(bairro));
	}
}