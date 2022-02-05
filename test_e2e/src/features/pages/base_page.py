from selenium.common.exceptions import NoSuchElementException

from features.pages.locators import BaseLocators


class BasePage(object):
    def __init__(self, driver):
        self.driver = driver

    def is_element_visible(self, locator):
        element = self.driver.find_element(*locator)
        return element is not None

    def is_element_hidden(self, locator):
        try:
            return not self.is_element_visible(locator)
        except NoSuchElementException:
            return True
        except Exception as e:
            raise e

    def click_on_table_row(self):
        element = self.driver.find_element(*BaseLocators.MAT_ROW)
        element.click()
