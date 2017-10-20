using System;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.Common.Infrastructure.Core
{
    public class AppSrvBuilder
    {
        public AppSrvBuilder(IServiceCollection services) {
            Services = services;
        }

        public IServiceCollection Services { get; }

        public AppSrvBuilder AddScoped(Type serviceType) {
            Services.AddScoped(serviceType);
            return this;
        }

        public AppSrvBuilder AddTransient(Type serviceType) {
            Services.AddTransient(serviceType);
            return this;
        }

        public AppSrvBuilder AddTransient<TService, TImplementation>() where TService : class
            where TImplementation : class, TService {
            Services.AddTransient<TService, TImplementation>();
            return this;
        }

        public AppSrvBuilder AddScoped(Type serviceType, Type concreteType) {
            Services.AddScoped(serviceType, concreteType);
            return this;
        }

        public AppSrvBuilder AddScoped<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService {
            Services.AddScoped<TService, TImplementation>();
            return this;
        }

        public AppSrvBuilder AddScoped<T>() where T : class {
            Services.AddScoped<T>();
            return this;
        }

        public AppSrvBuilder AddSingleton<TService, TImplementation>() where TService : class
            where TImplementation : class, TService {
            Services.AddSingleton<TService, TImplementation>();
            return this;
        }

        public AppSrvBuilder AddSingleton<TService>(TService implementationInstance) where TService : class {
            Services.AddSingleton(implementationInstance);
            return this;
        }

        public AppSrvBuilder AddSingleton<TService>() where TService : class {
            Services.AddSingleton<TService>();
            return this;
        }
    }
}