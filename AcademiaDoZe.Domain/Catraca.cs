using AcademiaDoZe.Domain.Entities;

namespace AcademiaDoZe.Domain;

public class Catraca
{
    public Pessoa Pessoa { get; set; }
    public DateTime DataHoraEntrada { get; set; }
    public DateTime DataHoraSaida { get; set; }

    public Catraca(Pessoa pessoa, DateTime dataHoraEntrada, DateTime dataHoraSaida)
    {
        Pessoa = pessoa;
        DataHoraEntrada = dataHoraEntrada;
        DataHoraSaida = dataHoraSaida;
    }
}
