using BlazorExpenseTracker.UI.Interfaces;
using BlazorExpenseTracker.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

//por defecto nos muestra los errores en el navegador - > activamos para que nos lo muestre en el navegador
builder.Services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; });

// Agregamos la URl del consumo de la API
// Para los diferentes Servicios
builder.Services.AddHttpClient<ICategoryService, CategoryService>
    (
        //client hay que definir el cliente en este caso la API
        //BaseAddress indicar que es una ubicacion
        //Uri representa un objeto URL
        //para saber la UTR nos vamos a la API, en Properties - launchSettings y aparece aca "applicationUrl": "http://localhost:6389" o "applicationUrl": "https://localhost:5001;http://localhost:5000" o "sslPort": 44362
        client =>
        {
            client.BaseAddress = new Uri("https://localhost:44373");
        }
    );

//injeccion para la categoria de expenses
builder.Services.AddHttpClient<IExpenseService, ExpenseService>(
    client => { client.BaseAddress = new Uri("https://localhost:44373"); });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
