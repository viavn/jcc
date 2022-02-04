from behave import given, when, then

from login_step import step_impl_login_as_admin, step_impl_login_as_regular


@given(u'acesso o sistema com usuario "{user_type}"')
def step_impl_login_by_user_type(context, user_type):
    if user_type == 'admin':
        step_impl_login_as_admin(context)
    else:
        step_impl_login_as_regular(context)


@when(u'pesquiso por "{filter_query}" na tabela de criancas')
def step_impl(context, filter_query):
    context.dashboard_page.send_keys_to_filter(filter_query)


@then(u'vai aparecer linha filtrada com "{family_acronym}" "{child_name}" "{legal_responsible}"')
def step_impl(context, family_acronym, child_name, legal_responsible):
    assert context.dashboard_page.is_family_acronym_equals(family_acronym), True
    assert context.dashboard_page.is_child_name_equals(child_name), True
    assert context.dashboard_page.is_legal_responsible_equals(legal_responsible), True
