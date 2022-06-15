﻿using Logger.DataBase;
using Logger.DataBase.Models;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Microsoft.Extensions.Logging
{

    public static class ILoggingBuilderExtensions
    {

        public static void AddDataBaseLogger(this ILoggingBuilder builder, Action<LoggerConfiguration> action)
        {
            builder.Services.Configure(action);
            builder.Services.AddSingleton<ILoggerProvider, DataBaseLoggerProvider>();
        }
    }
}