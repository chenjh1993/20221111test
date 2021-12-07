﻿using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace TaskAdmin.Filters
{
    [AttributeUsage(AttributeTargets.All)]
    public class GlobalFilter : Attribute, IActionFilter
    {

        void IActionFilter.OnActionExecuting(ActionExecutingContext context)
        {

        }


        void IActionFilter.OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}