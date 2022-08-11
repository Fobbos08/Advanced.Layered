﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

using Business.Categories.Commands.AddCategory;
using Business.Common.Behaviors;

using FluentValidation;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

namespace Business
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices (this IServiceCollection services)
        {
            //services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            //services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddMediatR(typeof(AddCategoryCommand).GetTypeInfo().Assembly);
            //services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

            return services;
        }
    }
}
