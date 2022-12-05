﻿using common.libs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace common.server.middleware
{
    /// <summary>
    /// 
    /// </summary>
    public static class ServiceCollectionExtends
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblys"></param>
        /// <returns></returns>
        public static ServiceCollection AddMiddleware(this ServiceCollection services, Assembly[] assemblys)
        {

            services.AddSingleton<MiddlewareTransfer>();
            foreach (Type item in ReflectionHelper.GetSubClass(assemblys, typeof(MiddlewareBase)))
            {
                services.AddSingleton(item);
            }

            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblys"></param>
        /// <returns></returns>
        public static ServiceProvider UseMiddleware(this ServiceProvider services, Assembly[] assemblys)
        {
            MiddlewareTransfer transfer = services.GetService<MiddlewareTransfer>();
            foreach (Type item in ReflectionHelper.GetSubClass(assemblys, typeof(MiddlewareBase)))
            {
                transfer.Load(services.GetService(item) as MiddlewareBase);
            }

            return services;
        }
    }
}
