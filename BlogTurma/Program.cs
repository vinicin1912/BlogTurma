using DotNetEnv;
using BlogTurma.controllers;
using BlogTurma.data;
using BlogTurma.services;
using BlogTurma.views;

Env.Load();

var conexao = new Conexao();

// Sessão Única Compartilhada da Aplicação
var sessao = new SessaoUsuario();

var publicacaoService = new PublicacaoService(conexao, sessao);
var publicacaoView = new PublicacaoView();
var publicacaoController = new PublicacaoController(publicacaoService, publicacaoView, sessao);

var usuarioService = new UsuarioService(conexao);
var autenticacaoView = new AutenticacaoView();
var autenticacaoController = new AutenticacaoController(autenticacaoView, usuarioService, sessao);

var menuView = new MenuView(sessao);

bool executando = true;

while (executando)
{
    int escolha = menuView.ExibirMenu();

    switch (escolha)
    {
        case 1:
            publicacaoController.Listar();
            break;
        case 2:
            publicacaoController.Visualizar();
            break;
        case 3:
            autenticacaoController.Cadastrar();
            break;
        case 4:
            autenticacaoController.Entrar();
            break;
        case 5:
            publicacaoController.Criar();
            break;
        case 9:
            autenticacaoController.Sair();
            break;
        case 0:
            Console.WriteLine("Programa encerrado. Até mais!");
            executando = false;
            break;
        default:
            Console.WriteLine("Opção inválida! Tente novamente.");
            break;
    }
}