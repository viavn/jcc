Feature: Login

  Scenario: Exibir mensagem de erro quando login ou senha sao invalidos
    Given que tento entrar no sistema com login invalido
    Then deve aparecer uma mensagem de erro

  Scenario: Exibir Dashboard com botoes apenas para admin
    Given acesso o sistema como usuario admin
    Then deve aparecer os botões disponiveis para usuario admin

  Scenario: Exibir Dashboard com botoes para usuario regular
    Given acesso o sistema como usuario regular
    Then deve aparecer apenas os botões disponiveis para usuario regular

  Scenario: Usuario regular nao deve ter acesso a rota de gerenciar contas
    Given acesso o sistema como usuario regular
    When tento acessar a pagina de gerenciar contas
    Then devo retornar para pagina de dashboard

  Scenario: Usuario nao deve ter acesso a dashboard quando nao estiver logado
    Given acesso a pagina de dashboard
    Then devo retornar para pagina de login

  @changeRegularUserPassword
  Scenario: Ao mudar senha de um usuario do sistema, devo entrar com a nova senha
    Given altero a senha do usuario regular
    And faco logout do sistema
    When preencher os campos de login com a nova senha
    Then deve aparecer apenas os botões disponiveis para usuario regular
