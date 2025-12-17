using Dignitas.Specmatic.Provider.API.Exceptions;
using Dignitas.Specmatic.Provider.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<CartService>();

var app = builder.Build();

app.MapOpenApi();
app.MapControllers();

app.UseExceptionHandler(new ExceptionHandlerOptions
{
    StatusCodeSelector = ex => ex switch
    {
        NotFoundException _ => StatusCodes.Status404NotFound,
        ConflictException _ => StatusCodes.Status409Conflict,
        _ => StatusCodes.Status500InternalServerError,
    }
});

await app.RunAsync();
