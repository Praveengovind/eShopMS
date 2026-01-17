using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    //Pipline behavior for validation
    config.AddOpenBehavior(typeof(BuildingBlocks.Behaviors.ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(assembly);

// Add services to the container.
builder.Services.AddCarter();

builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("MartenDB")!);
}).UseLightweightSessions();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter();

// Exception handling
app.UseExceptionHandler(ex =>
{
    ex.Run(async context =>
    {
        var exception = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>()?.Error;
        if (exception is null)
        {            
            return;
        }

        var problemDetails = new Microsoft.AspNetCore.Mvc.ProblemDetails
        {
            Title = exception.Message,
            Status = 500,
            Detail = exception.StackTrace
        };

        var logger = app.Services.GetRequiredService<ILogger<Program>>();
        logger.LogError(exception, exception.Message);

        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        
        await context.Response.WriteAsJsonAsync(problemDetails);
    });
});


app.Run();
