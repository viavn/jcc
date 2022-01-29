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

#  Scenario: Inativar um usuario do sistema