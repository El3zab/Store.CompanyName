
using Domain.Contracts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using Persistence.Data;
using Services;
using Services.Abstraction;
using Shared.ErrorModels;
using Store.CompanyName.Api.Extensions;
using Store.CompanyName.Api.Middlewares;
using System.Threading.Tasks;

namespace Store.CompanyName.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.RegisterAllServices(builder.Configuration);


            var app = builder.Build();


            // Configure the HTTP request pipeline.

            await app.ConfigureMiddlewares();

            app.Run();
        }
    }
}
