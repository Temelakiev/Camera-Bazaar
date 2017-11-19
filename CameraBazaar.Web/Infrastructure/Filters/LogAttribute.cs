﻿using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CameraBazaar.Web.Infrastructure.Filters
{
    public class LogAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            using (var writer = new StreamWriter("logs.txt", true))
            {
                var dateTime = DateTime.UtcNow;
                var ipAddress = context.HttpContext.Connection.RemoteIpAddress;
                var userName = context.HttpContext.User?.Identity?.Name ?? "Anonymous";
                var controller = context.Controller.GetType().Name;
                var action = context.RouteData.Values["action"];

                var logMessagse = $"{dateTime} - {ipAddress} - {userName} {controller}.{action}";

                if (context.Exception !=null)
                {
                    var exceptionType = context.Exception.GetType().Name;
                    var exceptionMessage = context.Exception.Message;

                    logMessagse = $"[!] {logMessagse} - {exceptionType} - {exceptionMessage}";
                }
                writer.WriteLine(logMessagse);
            }
        }
    }
}
