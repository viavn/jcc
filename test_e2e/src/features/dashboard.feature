Feature: Dashboard/Home

  Scenario Outline: Filtro de tabela das criancas
    Given acesso a pagina de login do sistema
    When preencho o campo de login com valor "<login>"
    And preencho o campo de senha com valor "<password>"
    And clicar no botao entrar
    When pesquiso por "<filter_query>" na tabela de criancas
    Then vai aparecer linha filtrada com "<family_acronym>" "<child_name>" "<legal_responsible>"
    Examples:
      | login      | password | filter_query | family_acronym | child_name | legal_responsible |
      | admin-test | 123      | nogueira     | B              | Jhonatan   | Fernanda Nogueira |
      | test       | 123      | nogueira     | B              | Jhonatan   | Fernanda Nogueira |
