Feature: Gerencia conta de usuarios

  Scenario: Criar novo usuario do sistema
    Given acesso a pagina de login do sistema
    When acesso o sistema como usuario admin
    And acesso a pagina de usuarios
    And clico no botao de novo usuario
    Given preencho o formulario
      | login      | name       | password | type   |
      | user-e2e-1 | User E2E 1 | 123      | Normal |
    And pesquiso por "user-e2e-1" na tabela de usuarios
    Then linha filtrada deve conter
      | login      | name       | admin | disabled |
      | user-e2e-1 | User E2E 1 | Não   | Não      |
    And exclui usuario pelo login "user-e2e-1"

  Scenario: Ao inativar um usuario do sistema, este nao deve consegui entrar
    Given acesso a pagina de login do sistema
    When acesso o sistema como usuario admin
    And acesso a pagina de usuarios
    And clico no botao de novo usuario
    Given preencho o formulario
      | login      | name       | password | type   |
      | user-e2e-1 | User E2E 1 | 123      | Normal |
    And pesquiso por "user-e2e-1" na tabela de usuarios
    And visualizo o detalhe do usuario
    And clico no botao inativar usuario
    And faco logout do sistema
    And acesso a pagina de login do sistema
    When preencho o campo de login com valor "user-e2e-1"
    And preencho o campo de senha com valor "123"
    And clicar no botao entrar
    Then deve aparecer uma mensagem de erro
    And a mensagem de erro deve desaparecer
    And exclui usuario pelo login "user-e2e-1"
