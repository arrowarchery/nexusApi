using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using Microsoft.EntityFrameworkCore;
using NexusAPI;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(cfg => { }, typeof(Program).Assembly);

builder.Services
    .AddAuthenticationJwtBearer(s => s.SigningKey = "ThisIsASuperSecretJwtKeyThatIsAtLeast32CharsLong")
    .AddAuthentication();

builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policyBuilder =>
        policyBuilder.AllowAnyOrigin()
            .WithMethods("GET", "POST", "PUT", "PATCH", "DELETE")
            .AllowAnyHeader()
    )
);

builder.Services.AddFastEndpoints().SwaggerDocument(options => 
{
    options.ShortSchemaNames = true;
});

builder.Services.AddDbContext<NexusDbContext>();

WebApplication app = builder.Build();


app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.UseFastEndpoints(options =>
{
    options.Endpoints.RoutePrefix = "api"; 
    options.Endpoints.ShortNames = true;
}).UseSwaggerGen();

app.MapGet("/", () => "Nexus API est en ligne !");

app.Run();