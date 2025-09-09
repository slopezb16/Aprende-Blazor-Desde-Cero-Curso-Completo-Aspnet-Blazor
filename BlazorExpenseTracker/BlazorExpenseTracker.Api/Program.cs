using BlazorExpenseTracker.Data.Data;
using BlazorExpenseTracker.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Mappeamos los Repositorios con sus Interfaces
// Aca continuamos lo que indicamos en el controlador CategoryController para la inyeccion de dependencias
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();

// Configurar la conexion a la base de datos
//Vamos a traer la conexion de base de datos
var sqlConnectionConfiguration = new SqlConfiguration(builder.Configuration.GetConnectionString("SqlConnection"));
builder.Services.AddSingleton(sqlConnectionConfiguration);

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
