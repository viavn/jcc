Feature: Gerencia conta de usuarios

  @newRegularUser
  Scenario: Criar novo usuario do sistema
    Given acesso o sistema como usuario admin
    When crio um novo usuario
    And pesquiso por "user-e2e-1" na tabela de usuarios
    Then linha filtrada deve conter informacoes do usuario criado

  @newRegularUser
  Scenario: Ao inativar um usuario do sistema, este nao deve consegui entrar
    Given inativo um usuario
    And faco logout do sistema
    When preencho o campo de login com valor "user-e2e-1"
    And preencho o campo de senha com valor "123"
    And clicar no botao entrar
    Then deve aparecer uma mensagem de erro
    And a mensagem de erro deve desaparecer
