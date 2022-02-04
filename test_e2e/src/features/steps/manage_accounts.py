from behave import given, when, then

from login_step import step_impl_given_login_as_admin


@when(u'acesso a pagina de usuarios')
def step_impl(context):
    context.dashboard_page.click_manage_accounts_button()


@when(u'crio um novo usuario')
def create_new_user(context):
    context.dashboard_page.click_manage_accounts_button()
    context.manage_accounts_page.click_new_account_button()
    step_impl_fill_user_form(context)


@when(u'clico no botao de novo usuario')
def step_impl(context):
    context.manage_accounts_page.click_new_account_button()


@given(u'inativo um usuario')
def step_impl(context):
    step_impl_given_login_as_admin(context)
    create_new_user(context)
    step_impl_search_for_user(context, 'user-e2e-1')
    context.manage_accounts_page.click_on_table_row()
    context.manage_accounts_page.click_on_disable_user_button()


@given(u'preencho o formulario')
def step_impl_fill_user_form(context):
    context.manage_accounts_page.send_keys_to_login('user-e2e-1')
    context.manage_accounts_page.send_keys_to_name('User E2E 1')
    context.manage_accounts_page.send_keys_to_password('123')
    context.manage_accounts_page.select_user_type('Normal')
    context.manage_accounts_page.click_submit_button()


@when(u'pesquiso por "{filter_query}" na tabela de usuarios')
def step_impl_search_for_user(context, filter_query):
    context.manage_accounts_page.send_keys_to_filter(filter_query)


@then(u'linha filtrada deve conter informacoes do usuario criado')
def step_impl(context):
    context.manage_accounts_page.is_login_equals('user-e2e-1')
    context.manage_accounts_page.is_child_name_equals('User E2E 1')
    context.manage_accounts_page.is_admin_equals('Não')
    context.manage_accounts_page.is_disabled_equals('Não')
