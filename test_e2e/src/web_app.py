from selenium import webdriver
from data.config import webapp_url


class WebApp:
    instance = None

    @classmethod
    def get_instance(cls):
        if cls.instance is None:
            cls.instance = WebApp()
        return cls.instance

    def __init__(self):
        self.driver = webdriver.Chrome()

    def get_driver(self):
        return self.driver

    def load_website(self):
        self.driver.get(webapp_url)
        self.driver.maximize_window()

    def close(self):
        self.driver.close()

    def goto_page(self, page):
        self.driver.get(f"{webapp_url}/{page.lower()}")


webapp = WebApp.get_instance()
