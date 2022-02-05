from selenium.common.exceptions import NoSuchElementException

from helpers.helper_webapp import webapp
from pages.child_details_page import ChildDetailsPage
from pages.dashboard_page import DashboardPage
from pages.login_page import LoginPage
from pages.manage_accounts_page import ManageAccountsPage
from pages.message_result_page import MessagesResultPage
from pages.settings_page import SettingsPage
from repository import queries


def before_all(context):
    queries.insert_user('admin-test', 'Test E2E', '123', 1)
    queries.insert_user('test', 'Test E2E - Regular', '123', 2)

    context.webapp = webapp
    context.webapp.maximize()
    context.login_page = LoginPage(webapp.driver)
    context.messages_popup = MessagesResultPage(webapp.driver)
    context.dashboard_page = DashboardPage(webapp.driver)
    context.manage_accounts_page = ManageAccountsPage(webapp.driver)
    context.settings_page = SettingsPage(webapp.driver)
    context.child_details_page = ChildDetailsPage(webapp.driver)


def after_scenario(context, scenario):
    try:
        if 'changeRegularUserPassword' in scenario.tags:
            queries.reset_user_password('test')
        elif 'newRegularUser' in scenario.tags:
            queries.delete_user('user-e2e-1')
        elif 'child' in scenario.tags:
            child = queries.get_child_by_name('Jhonatan')
            queries.delete_godparent(child[0])

        context.dashboard_page.click_logout_button()
        context.webapp.wait(3)
    except NoSuchElementException:
        context.webapp.load_website()


def after_all(context):
    queries.delete_user('admin-test')
    queries.delete_user('test')
    context.webapp.close()
