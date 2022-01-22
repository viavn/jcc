from behave import given, when, then
from web_app import webapp


@given(u'Eu navego para o site')
def step_load_website(context):
    webapp.load_website()


@when(u'Eu navego para a pagina {page}')
def step_goto_page(context, page):
    webapp.goto_page(page)


@then(u'Fecho o navegador')
def step_close_navigator(context):
    webapp.close()
