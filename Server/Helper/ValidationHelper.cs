using Server.Extensions;

namespace Server.Helper
{
    public static class ValidationHelper
    {
        public static void ValidateStringValue(string value, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                ThrowRequiredParameterValueException(parameterName);
            }
        }

        public static void ValidateIntValue(int value, string parameterName)
        {
            if (value <= 0)
            {
                ThrowRequiredParameterValueException(parameterName);
            }
        }

        public static void ValidateDateTimeValue(DateTime? value, string parameterName)
        {
            if (value == default)
            {
                ThrowRequiredParameterValueException(parameterName);
            }
        }

        public static void ValidateIEnumerable<T>(IEnumerable<T> list, string parameterName)
        {
            if (!list.HasValues())
            {
                ThrowRequiredParameterValueException(parameterName);
            }
        }

        public static void ValidateObject<T>(T objectValue, string parameterName) where T : class
        {
            if (objectValue == default(T))
            {
                throw new ArgumentException($"El objecto {parameterName} es requerido");
            }
        }
        private static void ThrowRequiredParameterValueException(string parameterName)
        {
            throw new ArgumentNullException($"El campo: {parameterName} es requerido");
        }
    }
}
