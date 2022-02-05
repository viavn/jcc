from features.pages.base_page import BasePage
from features.pages.locators import DashboardPageLocators


class DashboardPage(BasePage):
    def is_manage_accounts_button_hidden(self):
        return self.is_element_hidden(DashboardPageLocators.MANAGE_ACCOUNTS_BUTTON)

    def is_manage_accounts_button_visible(self):
        return self.is_element_visible(DashboardPageLocators.MANAGE_ACCOUNTS_BUTTON)

    def is_settings_button_visible(self):
        return self.is_element_visible(DashboardPageLocators.SETTINGS_BUTTON)

    def is_logout_button_visible(self):
        return self.is_element_visible(DashboardPageLocators.LOGOUT_BUTTON)

    def click_logout_button(self):
        element = self.driver.find_element(*DashboardPageLocators.LOGOUT_BUTTON)
        element.click()

    def click_settings_button(self):
        element = self.driver.find_element(*DashboardPageLocators.SETTINGS_BUTTON)
        element.click()

    def click_manage_accounts_button(self):
        element = self.driver.find_element(*DashboardPageLocators.MANAGE_ACCOUNTS_BUTTON)
        element.click()

    def send_keys_to_filter(self, value):
        element = self.driver.find_element(*DashboardPageLocators.TABLE_FILTER)
        element.send_keys(value)

    def is_family_acronym_equals(self, value):
        element = self.driver.find_element(*DashboardPageLocators.FAMILY_ACRONYM)
        return element.text == value

    def is_child_name_equals(self, value):
        element = self.driver.find_element(*DashboardPageLocators.CHILD_NAME)
        return element.text == value

    def is_legal_responsible_equals(self, value):
        element = self.driver.find_element(*DashboardPageLocators.LEGAL_RESPONSIBLE)
        return element.text == value