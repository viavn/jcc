from pages.base_page import BasePage
from pages.locators import ManageAccountsPageLocators


class ManageAccountsPage(BasePage):
    def click_new_account_button(self):
        element = self.driver.find_element(*ManageAccountsPageLocators.NEW_USER_BUTTON)
        element.click()

    def send_keys_to_login(self, value):
        element = self.driver.find_element(*ManageAccountsPageLocators.LOGIN_INPUT)
        element.send_keys(value)

    def send_keys_to_name(self, value):
        element = self.driver.find_element(*ManageAccountsPageLocators.NAME_INPUT)
        element.send_keys(value)

    def send_keys_to_password(self, value):
        element = self.driver.find_element(*ManageAccountsPageLocators.PASSWORD_INPUT)
        element.send_keys(value)

    def select_user_type(self, value):
        elements = self.driver.find_elements(*ManageAccountsPageLocators.USER_TYPE_RADIO_BUTTON)
        for element in elements:
            if element.text == value:
                element.click()
                break

    def click_submit_button(self):
        element = self.driver.find_element(*ManageAccountsPageLocators.SUBMIT_BUTTON)
        element.click()

    def click_on_disable_user_button(self):
        element = self.driver.find_element(*ManageAccountsPageLocators.DISABLE_USER_BUTTON)
        element.click()

    def send_keys_to_filter(self, value):
        element = self.driver.find_element(*ManageAccountsPageLocators.TABLE_FILTER)
        element.send_keys(value)

    def is_login_equals(self, value):
        element = self.driver.find_element(*ManageAccountsPageLocators.LOGIN_CELL)
        return element.text == value

    def is_child_name_equals(self, value):
        element = self.driver.find_element(*ManageAccountsPageLocators.NAME_CELL)
        return element.text == value

    def is_admin_equals(self, value):
        element = self.driver.find_element(*ManageAccountsPageLocators.ADMIN_CELL)
        return element.text == value

    def is_disabled_equals(self, value):
        element = self.driver.find_element(*ManageAccountsPageLocators.DISABLED_CELL)
        return element.text == value