from features.pages.base_page import BasePage
from features.pages.locators import SettingsPageLocators


class SettingsPage(BasePage):
    def send_keys_to_password(self, value):
        element = self.driver.find_element(*SettingsPageLocators.PASSWORD_INPUT)
        element.send_keys(value)

    def click_submit_button(self):
        element = self.driver.find_element(*SettingsPageLocators.SUBMIT_BUTTON)
        element.click()