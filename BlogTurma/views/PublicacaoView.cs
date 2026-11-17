using BlogTurma.models;

namespace BlogTurma.views;

public class PublicacaoView
{
    public (string Titulo, string Texto) SolicitarDadosPublicacao()
    {
        Console.WriteLine("\n=== NOVA PUBLICAÇÃO ===");
        Console.Write("Título: ");
        string titulo = Console.ReadLine() ?? string.Empty;

        Console.Write("Texto: ");
        string texto = Console.ReadLine() ?? string.Empty;

        return (titulo, texto);
    }

    public int SolicitarId()
    {
        Console.Write("\nDigite o ID da publicação que deseja abrir: ");
        string? entrada = Console.ReadLine();

        if (int.TryParse(entrada, out int id))
        {
            return id;
        }

        return -1;
    }

    public void ExibirPublicacoes(List<Publicacao> publicacoes)
    {
        Console.WriteLine("\n========= PUBLICAÇÕES =========");

        if (publicacoes.Count == 0)
        {
            Console.WriteLine("Ainda não há publicações cadastradas.");
            return;
        }

        foreach (Publicacao publicacao in publicacoes)
        {
            Console.WriteLine($"[ID: {publicacao.Id}] - {publicacao.Titulo} ({publicacao.DataCriacao:dd/MM/yyyy HH:mm})");
        }
        Console.WriteLine("--------------------------------");
    }

    public void ExibirDetalhes(Publicacao publicacao, string nomeAutor, List<(string ComentarioTexto, string AutorComentario)> comentarios)
    {
        Console.WriteLine("\n========================================");
        Console.WriteLine($"TÍTULO: {publicacao.Titulo}");
        Console.WriteLine($"AUTOR: {nomeAutor}");
        Console.WriteLine($"DATA: {publicacao.DataCriacao:dd/MM/yyyy HH:mm}");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine(publicacao.Texto);
        Console.WriteLine("----------------------------------------");
        Console.WriteLine("COMENTÁRIOS:");

        if (comentarios.Count == 0)
        {
            Console.WriteLine("(Nenhum comentário até o momento)");
        }
        else
        {
            foreach (var c in comentarios)
            {
                Console.WriteLine($"- {c.AutorComentario}: {c.ComentarioTexto}");
            }
        }
        Console.WriteLine("========================================");
    }

    public void ExibirMensagem(string mensagem)
    {
        Console.WriteLine($"\n[AVISO]: {mensagem}");
    }
}