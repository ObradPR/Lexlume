using System.Text.RegularExpressions;

namespace Lexlume.Common;

public class Constants
{
    public const string SOLUTION_NAME = "Lexlume";

    // System User

    public const long SYSTEM_USER_ID = -1;
    public const string SYSTEM_USER_USERNAME = "Lexlume";
    public const string SYSTEM_USER_EMAIL = "lexlume@gmail.com";

    // DB

    public const string DB_DEFAULT_SCHEMA = "dbo";

    public const string DB_TYPE_DATE = "date";

    // Grid - Params

    public const int DEFAULT_PAGE_SIZE = 10;
    public const int MAX_PAGE_SIZE = 50;

    // Grid - Sorting

    public const string SORTING_ORDER_DESC = "desc";
    public const string SORTING_ORDER_ASC = "asc";

    // CORS

    public const string CORS_ANGULAR_CLIENT_POLICY = "AngularClient";

    // Validations

    public static readonly Regex REGEX_PASSWORD = new(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^a-zA-Z0-9]).{8,}$");
}