using BlogTurma.models;

namespace BlogTurma.services;

public class SessaoUsuario
{
    public int? UsuarioId { get; private set; }
    public string? NomeUsuario { get; private set; }

    public bool EstaAutenticado => UsuarioId.HasValue;

    public void Iniciar(Usuario usuario)
    {
        UsuarioId = usuario.Id;
        NomeUsuario = usuario.Nome;
    }

    public void Encerrar()
    {
        UsuarioId = null;
        NomeUsuario = null;
    }
}