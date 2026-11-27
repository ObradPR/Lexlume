export class Constants {
  // RegEx
  static REGEX_PASSWORD = /^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^a-zA-Z0-9]).{8,}$/;

  // Messages
  static SUCCESS = "Success";
  static SUCCESS_MESSAGE = "Successfully done.";
  static FAILED = "Failed";
  static FAILED_INPUT_MESSAGE = "Check your input!";

  // Errors
  static ERROR_BAD_REQUEST = "Bad request";
  static ERROR_UNAUTHORIZED = "Unauthorized";
  static ERROR_FORBIDDEN = "Forbidden";
  static ERROR_NOT_FOUND = "Not found";
  static ERROR_VALIDATION = "Validation error";
  static ERROR_SERVER = "Server error";
  static ERROR_SERVER_MESSAGE =
    "Something unexpected went wrong. Please contact administrator for more information.";

  // Font
  static APP_FONT = "Roboto, sans-serif";
}

export class RouteConstants {
  static TITLE = "Lexlume";

  // Auth
  static ROUTE_AUTH = "auth";

  static ROUTE_LOG_IN = "log-in";
  static TITLE_LOG_IN = "Log In | " + this.TITLE;
  static ROUTE_REGISTER = "register";
  static TITLE_REGISTER = "Register | " + this.TITLE;

  // Admin
  static ROUTE_ADMIN = "admin";
  static TITLE_ADMIN = "Admin";

  // Pages
  static ROUTE_HOME = "user/:username";
  static TITLE_HOME = "Home | " + this.TITLE;
}
