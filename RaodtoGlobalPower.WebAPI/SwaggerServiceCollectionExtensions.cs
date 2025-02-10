using System.ComponentModel;
using System.Reflection;
using Microsoft.OpenApi.Models;

namespace RaodtoGlobalPower.WebAPI;

internal static class SwaggerServiceCollectionExtensions
{
    public static void ConfigureSwagger(this IServiceCollection services)
    {
        TypeDescriptor.AddAttributes(typeof(DateOnly), new TypeConverterAttribute(typeof(SwaggerDateOnlyConverter)));

        services.AddSwaggerGen(o =>
        {
            o.MapType(typeof(TimeSpan), () => new OpenApiSchema { Type = "string", Format = "time-span" });
            o.MapType(typeof(TimeSpan?), () => new OpenApiSchema { Type = "string", Format = "time-span" });
            o.MapType(typeof(decimal), () => new OpenApiSchema { Type = "number", Format = "decimal" });
            o.MapType(typeof(decimal?), () => new OpenApiSchema { Type = "number", Format = "decimal" });
            o.SwaggerDoc("v1", new OpenApiInfo { Title = "Employee&Attestation Service", Version = "v1" });
            o.DescribeAllParametersInCamelCase();
            o.SupportNonNullableReferenceTypes();
            o.UseAllOfToExtendReferenceSchemas();

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            o.IncludeXmlComments(xmlPath);
            o.AddSecurityDefinition("Auth", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Auth",
                Description = "Authorization header, using provided scheme.\r\n\r\n" +
                              "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n" +
                              "Example: \"Bearer example\""
            });

            var key = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Auth"
                },
                In = ParameterLocation.Header
            };

            var requirement = new OpenApiSecurityRequirement { { key, Array.Empty<string>() } };

            o.AddSecurityRequirement(requirement);
        })
        .AddSwaggerGenNewtonsoftSupport();
    }
}
