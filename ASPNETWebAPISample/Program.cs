var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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

app.Use(async(HttpContext ctx, Func < Task > next) =>
{
    if(DateTime.Now > new DateTime(2023, 1, 19, 11, 10, 0))
    {
        await next();
        if(ctx.Response.StatusCode == 404)
        {
            ctx.Response.StatusCode = 200;
        }
    }
    else
    {
        ctx.Response.StatusCode = 418;
        await ctx.Response.WriteAsync("I'm a Teapot");
    }
});



app.UseAuthorization();

app.MapControllers();

app.Run();
