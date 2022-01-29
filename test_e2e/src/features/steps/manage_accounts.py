from behave import given, when, then


@when(u'acesso a pagina de usuarios')
def step_impl(context):
    context.dashboard_page.click_manage_accounts_button()


@when(u'clico no botao de novo usuario')
def step_impl(context):
    context.manage_accounts_page.click_new_account_button()


@given(u'preencho o formulario')
def step_impl(context):
    row = context.table[0]
    context.manage_accounts_page.send_keys_to_login(row['login'])
    context.manage_accounts_page.send_keys_to_name(row['name'])
    context.manage_accounts_page.send_keys_to_password(row['password'])
    context.manage_accounts_page.select_user_type(row['type'])
    context.manage_accounts_page.click_submit_button()


@given(u'pesquiso por "{filter_query}" na tabela de usuarios')
def step_impl(context, filter_query):
    context.manage_accounts_page.send_keys_to_filter(filter_query)


@then(u'linha filtrada deve conter')
def step_impl(context):
    row = context.table[0]
    context.manage_accounts_page.is_login_equals(row['login'])
    context.manage_accounts_page.is_child_name_equals(row['name'])
    context.manage_accounts_page.is_admin_equals(row['admin'])
    context.manage_accounts_page.is_disabled_equals(row['disabled'])
