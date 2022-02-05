Feature: Dashboard

  Scenario Outline: Filtro de tabela das criancas
    Given acesso o sistema com usuario "<user_type>"
    When pesquiso por "<filter_query>" na tabela de criancas
    Then vai aparecer linha filtrada com "<family_acronym>" "<child_name>" "<legal_responsible>"
    Examples:
      | user_type | filter_query | family_acronym | child_name | legal_responsible |
      | admin     | nogueira     | B              | Jhonatan   | Fernanda Nogueira |
      | regular   | nogueira     | B              | Jhonatan   | Fernanda Nogueira |
