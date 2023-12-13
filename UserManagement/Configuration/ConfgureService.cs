﻿namespace UserManagement.Client.Configuration
{
    public static class ConfigureService
    {
        public static IServiceCollection AddMvcUIServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Program).Assembly);
            return services;
        }
    }
}
