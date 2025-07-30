//Vanessa Furtado Nunes
namespace AcademiaDoZe.Domain.ValueObject;

public record Arquivo
{
    public byte[] Conteudo { get; }
    public Arquivo(byte[] conteudo)
    {
        Conteudo = conteudo;
    }
}
