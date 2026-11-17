using BlogTurma.services;

namespace BlogTurma.views;

public class MenuView
{
    private readonly SessaoUsuario _sessao;

    public MenuView(SessaoUsuario sessao)
    {
        _sessao = sessao;
    }

    public int ExibirMenu()
    {
        Console.WriteLine("\n=== BLOG DA TURMA ===");
        if (_sessao.EstaAutenticado)
        {
            Console.WriteLine($"[Usuário autenticado: {_sessao.NomeUsuario}]");
        }
        else
        {
            Console.WriteLine("[Visitante não autenticado]");
        }

        Console.WriteLine("1. Listar publicações");
        Console.WriteLine("2. Abrir publicação");
        Console.WriteLine("3. Criar conta");
        Console.WriteLine("4. Entrar");
        Console.WriteLine("5. Criar publicação");
        Console.WriteLine("9. Sair da conta");
        Console.WriteLine("0. Encerrar aplicação");
        Console.Write("Escolha uma opção: ");

        string? entrada = Console.ReadLine();

        if (int.TryParse(entrada, out int escolha))
        {
            return escolha;
        }

        return -1;
    }
}