namespace BlogTurma.views;

public class AutenticacaoView
{
    public (string Nome, string Email, string Senha) SolicitarDadosCadastro()
    {
        Console.WriteLine("\n=== CRIAR CONTA ===");
        Console.Write("Nome: ");
        string nome = Console.ReadLine() ?? string.Empty;

        Console.Write("E-mail: ");
        string email = Console.ReadLine() ?? string.Empty;

        Console.Write("Senha: ");
        string senha = Console.ReadLine() ?? string.Empty;

        return (nome, email, senha);
    }

    public (string Email, string Senha) SolicitarCredenciais()
    {
        Console.WriteLine("\n=== ENTRAR NA CONTA ===");
        Console.Write("E-mail: ");
        string email = Console.ReadLine() ?? string.Empty;

        Console.Write("Senha: ");
        string senha = Console.ReadLine() ?? string.Empty;

        return (email, senha);
    }

    public void ExibirMensagem(string mensagem)
    {
        Console.WriteLine($"\n[AVISO]: {mensagem}");
    }
}