from selenium.webdriver.common.by import By


class LoginPageLocators(object):
    LOGIN_INPUT = (By.ID, 'login')
    PASSWORD_INPUT = (By.ID, 'password')
    LOGIN_BUTTON = (By.CSS_SELECTOR, 'button[type=submit]')


class DashboardPageLocators(object):
    MANAGE_ACCOUNTS_BUTTON = (By.CLASS_NAME, 'manage_accounts')
    SETTINGS_BUTTON = (By.CLASS_NAME, 'settings')
    LOGOUT_BUTTON = (By.CLASS_NAME, 'logout')
    TABLE_FILTER = (By.ID, 'filter-input')
    FAMILY_ACRONYM = (By.CSS_SELECTOR, '.mat-cell.mat-column-familyAcronym')
    CHILD_NAME = (By.CSS_SELECTOR, '.mat-cell.mat-column-name')
    LEGAL_RESPONSIBLE = (By.CSS_SELECTOR, '.mat-cell.mat-column-legalResponsible')


class ManageAccountsPageLocators(object):
    NEW_USER_BUTTON = (By.CLASS_NAME, 'new-user-button')
    LOGIN_INPUT = (By.ID, 'account_login')
    NAME_INPUT = (By.ID, 'account_name')
    PASSWORD_INPUT = (By.ID, 'account_password')
    USER_TYPE_RADIO_BUTTON = (By.CLASS_NAME, 'mat-radio-label-content')
    SUBMIT_BUTTON = (By.CSS_SELECTOR, 'button[type=submit]')
    TABLE_FILTER = (By.ID, 'filter-input')

    LOGIN_CELL = (By.CSS_SELECTOR, '.mat-cell.mat-column-login')
    NAME_CELL = (By.CSS_SELECTOR, '.mat-cell.mat-column-name')
    ADMIN_CELL = (By.CSS_SELECTOR, '.mat-cell.mat-column-isAdmin')
    DISABLED_CELL = (By.CSS_SELECTOR, '.mat-cell.mat-column-isDeleted')


class SettingsPageLocators(object):
    PASSWORD_INPUT = (By.ID, 'password')
    SUBMIT_BUTTON = (By.CSS_SELECTOR, 'button[type=submit]')


class MessagesResultLocators(object):
    ERROR_MESSAGE = (By.CLASS_NAME, 'mat-card.error')
