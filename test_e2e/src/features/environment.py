from helpers.helper_webapp import webapp


def before_all(context):
    context.webapp = webapp
    context.webapp.maximize()


def after_all(context):
    context.webapp.close()
