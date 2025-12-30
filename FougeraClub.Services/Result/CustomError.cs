using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FougeraClub.Services.Result
{
    public class CustomError
    {
        public string Code = string.Empty;
        public string Message = string.Empty;
        public CustomError(string code, string message)
        {
            Code = code;
            Message = message;
        }
        private static readonly string _ValidationError = "Input InValid";
        private static readonly string _NotFoundError = "Record Not Found";
        private static readonly string _ServerError = "Faild in Server";

        public static CustomError None => new CustomError(string.Empty, string.Empty);
        public static CustomError ValidationError(string message) => new CustomError(_ValidationError, message);
        public static CustomError NotFoundError(string message) => new CustomError(_NotFoundError, message);
        public static CustomError ServerError(string message) => new CustomError(_ServerError, message);
        public override string ToString()
        {
            return $"{Code} : {Message}";
        }
    }
}
