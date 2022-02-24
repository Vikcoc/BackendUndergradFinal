using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Exceptions
{
    public static class ErrorStrings
    {
        public const string EmailAlreadyInUse = "EMAIL_ALREADY_IN_USE";
        public const string LoginFail = "EMAIL_OR_PASSWORD_INCORRECT";
        public const string NoImageEntry = "IMAGE_DOES_NOT_EXIST";
        public const string InvalidFile = "BAD_FILE_FORMAT";
    }
}
