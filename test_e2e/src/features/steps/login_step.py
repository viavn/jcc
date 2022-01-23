from behave import when, then, given
import time

base_url = "https://ge.globo.com/"


@given(u'acesso a pagina de login do sistema')
def step_impl(context):
    context.webapp.load_website()


@then(u'x tem que ser igual a y')
def step_impl(context):
    time.sleep(3)
    raise NotImplementedError(u'STEP: Then x tem que ser igual a y')
