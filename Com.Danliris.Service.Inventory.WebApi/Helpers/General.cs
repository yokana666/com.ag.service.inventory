﻿namespace Com.Danliris.Service.Inventory.WebApi.Helpers
{
    public static class General
    {
        public const int OK_STATUS_CODE = 200;
        public const int CREATED_STATUS_CODE = 201;
        public const int NOT_FOUND_STATUS_CODE = 404;
        public const int BAD_REQUEST_STATUS_CODE = 400;
        public const int INTERNAL_ERROR_STATUS_CODE = 500;

        public const string OK_MESSAGE = "Ok";
        public const string NOT_FOUND_MESSAGE = "Data Not Found";
        public const string BAD_REQUEST_MESSAGE = "Data does not pass validation";
        public const string CSV_ERROR_MESSAGE = "The header row of CSV file is not valid";
        public const string NO_FILE_ERROR_MESSAGE = "File not found";
    }
}
