using System;
using System.Collections.Generic;
using System.Text;

namespace YoungDevelopers.Utils
{
    internal static class ErrorConstants
    {
        public const string PasswordRequirements = "Пароль должен содержать от 6 до 20 символов и иметь хотя бы одну цифру, латинскую букву в нижнем регистре, латинскую букву в верхнем регистре и спецсимвол [!@#$%^&*]";

        public const string WeightError = "Вес собаки должен быть целым от 1 до 200 кг";
    }
}
