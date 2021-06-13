using CSharpWebServer.Server.Common;
using System;
using System.Collections.Generic;

namespace CSharpWebServer.Server.Http
{
    public class HttpContentType
    {
        public const string PlainText = "text/plain; charset=utf-8;";
        public const string Html = "text/html; charset=utf-8;";
        public const string FormUrlEncoded = "application/x-www-form-urlencoded";

        public static string GetByFileExtention(string fileExtention) => fileExtention switch
        {
            "css" => "text/css",
            "js" => "application/javascript",
            "jpg" or "jpeg" => "image/jpg",
            _ => PlainText
        };
    }
}
