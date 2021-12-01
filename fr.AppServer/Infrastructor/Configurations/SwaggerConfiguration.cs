using fr.AppServer.Infrastructor.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

namespace fr.AppServer.Infrastructor.Configurations
{
    public static class SwaggerConfiguration
    {
        public static readonly string ApplicationApi = "Forest Resource Api";
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(action =>
            {
                action.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = ApplicationApi,
                    Version = "v1"
                });

                action.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri("https://localhost:44345/connect/authorize"),
                            TokenUrl = new Uri("https://localhost:44345/connect/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                { "api1", $"{ApplicationApi} API - full access" }
                            }
                        }
                    }
                });
                action.OperationFilter<AuthorizeCheckOperationFilter>();
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder builder)
        {
            builder.UseSwagger();
            builder.UseSwaggerUI(action =>
            {
                action.SwaggerEndpoint("v1/swagger.json", $"{ApplicationApi} v1");

                action.OAuthClientId("demo_api_swagger");
                action.OAuthAppName("Demo API - Swagger");
                action.OAuthUsePkce();
            });

            return builder;
        }
    }
}
