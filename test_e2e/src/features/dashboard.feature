# language: pt

Funcionalidade: Dashboard

  Esquema do Cenario: Filtro de tabela das criancas
    Dado acesso o sistema com usuario "<user_type>"
    Quando pesquiso por "<filter_query>" na tabela de criancas
    Entao vai aparecer linha filtrada com "<family_acronym>" "<child_name>" "<legal_responsible>"
    Exemplos:
      | user_type | filter_query | family_acronym | child_name | legal_responsible |
      | admin     | nogueira     | B              | Jhonatan   | Fernanda Nogueira |
      | regular   | nogueira     | B              | Jhonatan   | Fernanda Nogueira |
