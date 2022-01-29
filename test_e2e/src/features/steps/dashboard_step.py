from behave import when, then


@when(u'pesquiso por "{filter_query}" na tabela de criancas')
def step_impl(context, filter_query):
    context.dashboard_page.send_keys_to_filter(filter_query)


@then(u'vai aparecer linha filtrada com "{family_acronym}" "{child_name}" "{legal_responsible}"')
def step_impl(context, family_acronym, child_name, legal_responsible):
    assert context.dashboard_page.is_family_acronym_equals(family_acronym), True
    assert context.dashboard_page.is_child_name_equals(child_name), True
    assert context.dashboard_page.is_legal_responsible_equals(legal_responsible), True
