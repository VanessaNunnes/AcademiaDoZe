﻿//Vanessa Furtado Nunes
namespace AcademiaDoZe.Domain.Entities;

public class Acesso : Entity
{
    public Pessoa AlunoColaborador { get; private set; }
    public DateTime DataHora { get; private set; }
    public Acesso(Pessoa pessoa, DateTime dataHora)
    {
        AlunoColaborador = pessoa;
        DataHora = dataHora;
    }
}
