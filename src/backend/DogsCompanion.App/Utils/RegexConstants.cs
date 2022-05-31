using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogsCompanion.App.Utils
{
    public static class RegexConstants
    {
        public const string PasswordPattern = @"(?=.*[0-9])(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z])[0-9a-zA-Z!@#$%^&*]{6,20}";
    }
}
