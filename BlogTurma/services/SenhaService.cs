using Isopoh.Cryptography.Argon2;

namespace BlogTurma.services;

public class SenhaService
{
    public string GerarHash(string senha)
    {
        return Argon2.Hash(senha);
    }

    public bool Verificar(string senha, string hash)
    {
        return Argon2.Verify(hash, senha);
    }
}