using BurgissTechStackDemo.API.DataModels;
using BurgissTechStackDemo.API.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);




// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<EmployeeAdminContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeAdminPortalDb")));
builder.Services.AddScoped<IEmployeeRepository, SqlEmployeeRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
