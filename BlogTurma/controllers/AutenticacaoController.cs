using BlogTurma.models;
using BlogTurma.services;
using BlogTurma.views;

namespace BlogTurma.controllers;

public class AutenticacaoController
{
    private readonly AutenticacaoView _view;
    private readonly UsuarioService _usuarioService;
    private readonly SessaoUsuario _sessao;

    public AutenticacaoController(AutenticacaoView view, UsuarioService usuarioService, SessaoUsuario sessao)
    {
        _view = view;
        _usuarioService = usuarioService;
        _sessao = sessao;
    }

    public void Cadastrar()
    {
        var (nome, email, senha) = _view.SolicitarDadosCadastro();

        try
        {
            _usuarioService.Cadastrar(nome, email, senha);
            _view.ExibirMensagem("Conta criada com sucesso! Agora você pode entrar.");
        }
        catch (Exception ex)
        {
            _view.ExibirMensagem($"Erro ao cadastrar: {ex.Message}");
        }
    }

    public void Entrar()
    {
        if (_sessao.EstaAutenticado)
        {
            _view.ExibirMensagem($"Você já está conectado como {_sessao.NomeUsuario}.");
            return;
        }

        var (email, senha) = _view.SolicitarCredenciais();

        Usuario? usuario = _usuarioService.Autenticar(email, senha);

        if (usuario != null)
        {
            _sessao.Iniciar(usuario);
            _view.ExibirMensagem($"Bem-vindo, {_sessao.NomeUsuario}!");
        }
        else
        {
            _view.ExibirMensagem("E-mail ou senha incorretos.");
        }
    }

    public void Sair()
    {
        if (!_sessao.EstaAutenticado)
        {
            _view.ExibirMensagem("Você não está conectado.");
            return;
        }

        _sessao.Encerrar();
        _view.ExibirMensagem("Você saiu da conta.");
    }
}