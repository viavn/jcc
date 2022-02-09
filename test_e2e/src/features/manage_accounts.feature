# language: pt

Funcionalidade: Gerencia conta de usuarios

  @newRegularUser
  Cenario: Criar novo usuario do sistema
    Dado acesso o sistema como usuario admin
    Quando crio um novo usuario
    E pesquiso por "user-e2e-1" na tabela de usuarios
    Entao linha filtrada deve conter informacoes do usuario criado

  @newRegularUser
  Cenario: Ao inativar um usuario do sistema, este nao deve consegui entrar
    Dado inativo um usuario
    E faco logout do sistema
    Quando preencho o campo de login com valor "user-e2e-1"
    E preencho o campo de senha com valor "123"
    E clicar no botao entrar
    Entao deve aparecer uma mensagem de erro
    E a mensagem de erro deve desaparecer
