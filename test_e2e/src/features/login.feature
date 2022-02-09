# language: pt

Funcionalidade: Login

  Cenario: Exibir mensagem de erro quando login ou senha sao invalidos
    Dado que tento entrar no sistema com login invalido
    Entao deve aparecer uma mensagem de erro

  Cenario: Exibir Dashboard com botoes apenas para admin
    Dado acesso o sistema como usuario admin
    Entao deve aparecer os botões disponiveis para usuario admin

  Cenario: Exibir Dashboard com botoes para usuario regular
    Dado acesso o sistema como usuario regular
    Entao deve aparecer apenas os botões disponiveis para usuario regular

  Cenario: Usuario regular nao deve ter acesso a rota de gerenciar contas
    Dado acesso o sistema como usuario regular
    Quando tento acessar a pagina de gerenciar contas
    Entao devo retornar para pagina de dashboard

  Cenario: Usuario nao deve ter acesso a dashboard quando nao estiver logado
    Dado acesso a pagina de dashboard
    Entao devo retornar para pagina de login

  @changeRegularUserPassword
  Cenario: Ao mudar senha de um usuario do sistema, devo entrar com a nova senha
    Dado altero a senha do usuario regular
    E faco logout do sistema
    Quando preencher os campos de login com a nova senha
    Entao deve aparecer apenas os botões disponiveis para usuario regular
