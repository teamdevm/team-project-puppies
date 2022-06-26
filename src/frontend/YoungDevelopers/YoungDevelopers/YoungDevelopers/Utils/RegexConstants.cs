using System;
using System.Collections.Generic;
using System.Text;

namespace YoungDevelopers.Utils
{
    internal static class RegexConstants
    {
        public const string Password = @"(?=.*[0-9])(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z])[0-9a-zA-Z!@#$%^&*]{6,20}";

        public const string DogName = @"^[\w'\-,.][^0-9_!¡?÷?¿/\\+=@#$%ˆ&*(){}|~<>;:[\]]{2,}$";

        public const string LastName = @"^[\w'\-,.][^0-9_!¡?÷?¿/\\+=@#$%ˆ&*(){}|~<>;:[\]]{2,}$";

        public const string Email = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

        public const string Weight = @"[+-]?([0-9]*[.])?[0-9]+";
    }
}
