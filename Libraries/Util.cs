using Microsoft.AspNetCore.Mvc;
using Mysqlx.Expr;
using System.Net;
using System.Text.Json;

namespace TetrisAPI.Libraries
{
    public class Util
    {
        public static ContentResult GenerateError(string message, HttpStatusCode code = HttpStatusCode.NotFound)
        {
            string result = JsonSerializer.Serialize(new { errors = message });

            return new ContentResult
            {
                Content = result,
                ContentType = "application/json",
                StatusCode = (int)code
            };
        }

        public static string GenerateTokenJsonString(string token) =>
            JsonSerializer.Serialize(new { token = token });
    }
}
