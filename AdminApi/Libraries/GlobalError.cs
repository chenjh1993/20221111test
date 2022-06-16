﻿using Common;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AdminApi.Libraries
{


    public class GlobalError
    {


        public static Task ErrorEvent(HttpContext httpContext)
        {
            var feature = httpContext.Features.Get<IExceptionHandlerFeature>();
            var error = feature?.Error;

            var ret = new
            {
                errMsg = "系统全局内部异常"
            };


            string path = httpContext.GetUrl();

            var parameter = httpContext.GetParameter();

            var parameterStr = JsonHelper.ObjectToJson(parameter);

            if (parameterStr.Length > 102400)
            {
                _ = parameterStr[..102400];
            }

            var authorization = httpContext.Request.Headers["Authorization"].ToString();

            var content = new
            {
                path,
                parameter,
                authorization,
                error = new
                {
                    error?.Source,
                    error?.Message,
                    error?.StackTrace
                }
            };

            string strContent = JsonHelper.ObjectToJson(content);

            var logger = httpContext.RequestServices.GetRequiredService<ILogger<GlobalError>>();

            logger.LogError(strContent);

            httpContext.Response.StatusCode = 400;

            return httpContext.Response.WriteAsJsonAsync(ret);
        }


    }
}
