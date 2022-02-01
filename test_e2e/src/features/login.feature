Feature: Login

  Scenario: Exibir mensagem de erro quando login ou senha sao invalidos
    Given acesso a pagina de login do sistema
    When preencho o campo de login com valor "user-inexisteste"
    And preencho o campo de senha com valor "xxxx"
    And clicar no botao entrar
    Then deve aparecer uma mensagem de erro
    Then a mensagem de erro deve desaparecer

  Scenario: Exibir Dashboard com botoes apenas para admin
    Given acesso a pagina de login do sistema
    When acesso o sistema como usuario admin
    Then deve aparecer o botao de gerenciar contas
    Then deve aparecer o botao de configuracoes da conta
    Then deve aparecer o botao de logout

  Scenario: Exibir Dashboard com botoes para usuario regular
    Given acesso a pagina de login do sistema
    When acesso o sistema como usuario regular
    Then nao deve aparecer o botao de gerenciar contas
    Then deve aparecer o botao de configuracoes da conta
    Then deve aparecer o botao de logout

  Scenario: Usuario regular nao deve ter acesso a rota de gerenciar contas
    Given acesso a pagina de login do sistema
    And acesso o sistema como usuario regular
    When tento acessar a pagina de gerenciar contas
    Then devo retornar para pagina de dashboard

  Scenario: Usuario nao deve ter acesso a dashboard quando nao estiver logado
    Given acesso a pagina de dashboard
    Then devo retornar para pagina de login

  Scenario: Ao mudar senha de um usuario do sistema, devo entrar com a nova senha
    Given acesso a pagina de login do sistema
    When acesso o sistema como usuario regular
    Given acesso a pagina de mudanca de senha
    And altero a senha do usuario regular para "122"
    And faco logout do sistema
    And acesso o sistema como usuario regular
    Then deve aparecer uma mensagem de erro
    And a mensagem de erro deve desaparecer
    When preencho o campo de login com valor "test"
    And preencho o campo de senha com valor "122"
    And clicar no botao entrar
    Given acesso a pagina de mudanca de senha
    And altero a senha do usuario regular para "123"
