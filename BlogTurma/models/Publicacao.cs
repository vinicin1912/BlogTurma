
namespace BlogTurma.models;

public class Publicacao
{
    public int Id { get; set; }

    public string Titulo { get; set; } = "";

    public string Texto { get; set; } = "";

    public DateTime DataCriacao { get; set; }

    public int UsuarioId { get; set; }
}
