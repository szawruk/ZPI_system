using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;
namespace Backend.Acefb9Utils
{
    public static class ErrorFunctionality
    {
        public static string ErrorsToString(ModelStateDictionary.ValueEnumerable values,
            string separator=";")
        { 
            return string.Join(separator, values
                    .SelectMany(v => v.Errors)
                    .Select(err => err.ErrorMessage));
        }

        public static object ObjectErrorReturn(int statusCode, ModelStateDictionary.ValueEnumerable values,
            string separator = ";")
        {
            return new { statusCode, error = ErrorsToString(values, separator) };
        }
    }
}
