using UnityEngine;
using System.Collections;

public static class ApiErrors
{
    public const int SYSTEM_ERROR = 100;

    public const int WRONG_REQUEST_TYPE = 101;
    public const int WRONG_REQUEST_FORMAT = 102;
    public const int WRONG_REQUEST_SECRET = 103;
    public const int MISSING_REQUIRED_REQUEST_FIELD = 104;
    public const int INVALID_PACKAGE_NAME = 105;
    public const int MISSING_REQUIRED_HEADER_FIELD = 106;

//USER ACTIONS ERRORS

    public const int USER_DOESNT_EXISTS = 201;
    public const int WRONG_USER_PASSWORD = 202;
    public const int USER_WITH_THAT_LOGIN_ALREADY_IN_USE = 203;
    public const int INVALID_AUTH_CODE = 204;
    public const int USER_IS_UNCONFIRMED = 205;
    public const int USER_WITH_CURRENT_LOGIN_IS_UNREGISTERED = 206;
    public const int USER_HAVE_NO_ENOUGH_BEES = 207;
    public const int ERROR_LOADING_USERS_IMAGE = 208;
    public const int USER_BANNED = 209;
    public const int WRONG_AUTH_TOKEN = 210;
    public const int AUTH_TOKEN_EXPIRED = 211;
    public const int AUTH_TOKEN_IS_NOT_EXPIRED = 212;

//VALIDATION ERRORS

    public const int USER_ALREADY_EXISTS = 300;
    public const int WRONG_FIELD_TYPE_INT_REQUIRED = 301;
    public const int WRONG_FIELD_TYPE_EMAIL_REQUIRED = 302;
    public const int WRONG_FIELD_LENGTH_MIN = 303;
    public const int WRONG_FIELD_LENGTH_MAX = 304;
    public const int WRONG_FIELD_TYPE_NOT_NULL_FIELD_REQUIRED = 305 ;
    public const int WRONG_FIELD_TYPE_WRONG_FILE = 306;
    public const int WRONG_FIELD_TYPE_WRONG_FILE_FORMAT = 307;

//GAME ERRORS

    public const int INVALID_GAME_TOKEN = 401;
    public const int REACHED_BEES_GAME_LIMIT = 402;
    public const int REACHED_DAILY_BEES_LIMIT = 403;

//PHONE ERRORS

    public const int INVALID_PHONE_NUMBER = 501;

//CAMPAIGN ERRORS

    public const int ERROR_LOADING_CAMPAIGN_IMAGE = 601;
    public const int USER_HAVE_NO_ACTIVE_CAMPAIGNS = 602;
    public const int USER_CANT_CREATE_MORE_THAN_ONE_CAMPAIGN = 603;
    public const int CAMPAIGN_IS_ALREADY_REPORTED = 604;
    public const int WRONG_CAMPAIGN_ID = 605;
    public const int NO_WAY_TO_DELETE_LAST_POST = 606;

}
