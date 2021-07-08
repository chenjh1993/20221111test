﻿using Common.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Cms.Filters
{


    /// <summary>
    /// 缓存过滤器
    /// </summary>
    public class CacheDataFilter : Attribute, IActionFilter
    {

        /// <summary>
        /// 缓存时效有效期，单位 秒
        /// </summary>
        public int TTL { get; set; }



        /// <summary>
        /// 是否使用 Token
        /// </summary>
        public bool UseToken { get; set; }


        void IActionFilter.OnActionExecuting(ActionExecutingContext context)
        {
            string key = "";

            if (UseToken)
            {
                var token = context.HttpContext.Session.GetString("userId");

                key = context.ActionDescriptor.DisplayName + "_" + context.HttpContext.Request.QueryString+"_"+token;
            }
            else
            {
                key = context.ActionDescriptor.DisplayName + "_" + context.HttpContext.Request.QueryString;
            }

            key = "CacheData_" + Common.CryptoHelper.GetMd5(key);

            try
            {

                var cacheInfo = Common.RedisHelper.StringGet(key);

                if (!string.IsNullOrEmpty(cacheInfo))
                {
                    var x = JsonHelper.GetValueByKey(cacheInfo, "value");

                    context.Result = new ObjectResult(x);
                }
            }
            catch
            {
                Console.WriteLine("缓存模块异常");
            }
        }


        void IActionFilter.OnActionExecuted(ActionExecutedContext context)
        {
            try
            {
                var value = JsonHelper.ObjectToJSON(context.Result);

                string key = "";

                if (UseToken)
                {
                    var token = context.HttpContext.Session.GetString("userId");

                    key = context.ActionDescriptor.DisplayName + "_" + context.HttpContext.Request.QueryString + "_" + token;
                }
                else
                {
                    key = context.ActionDescriptor.DisplayName + "_" + context.HttpContext.Request.QueryString;
                }

                key = "CacheData_" + Common.CryptoHelper.GetMd5(key);



                Common.RedisHelper.StringSet(key, value,TimeSpan.FromSeconds(TTL));

            }
            catch
            {
                Console.WriteLine("缓存模块异常");
            }

        }
    }
}
