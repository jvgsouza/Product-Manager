using Usuario.API.Extension;
using Usuario.API.Middleware;
using Usuario.API.Routes;
using Usuario.Application.IoC;
using Usuario.Infra.IoC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServiceDependencyResolver();
builder.Services.AddInfraDependencyResolver();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.AuthenticationEndpoints();
app.ConfigureLogging();

app.UseMiddleware<LogMiddleware>();
app.UseMiddleware<ErrorMiddleware>();

app.Run();

