using ClientAuthentication;
using ClientAuthentication.API.AuthenticationHandler;
using Ghtk.Authorization;
using Microsoft.AspNetCore.Authentication;

namespace Ghtk.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();

        builder.Services.AddControllers();
        
        IClientSourceAuthenticationHandler clientSourceAuthenticationHandler = new RemoteClientSourceAuthentication(builder.Configuration["AuthenticationService"] ?? throw new InvalidOperationException());
        
        builder.Services.AddAuthentication("X-Client-Source").AddXClientSource(options =>
        {
            options.ClientValidator = (clientSource, token, principal) => clientSourceAuthenticationHandler.Validate(clientSource);
            options.IssuerSigningKey = builder.Configuration["Jwt:IssuerSigningKey"] ?? string.Empty;
            
        });
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        
        app.UseAuthorization();

        app.MapControllers();
     
        app.Run();
    }
}