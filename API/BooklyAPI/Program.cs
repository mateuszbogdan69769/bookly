using BooklyAPI;
using Domain;
using ApiApplication;
using Infrastructure;
using BooklyAPI.Misc;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDomainServices();
builder.Services.AddAPIServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApiApplicationServices(builder.Configuration);

var app = builder.Build();
app.UseCors("corspolicy");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSwaggerAuthorized();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/api/swagger.json", "Bookly API");
    c.DocumentTitle = "Bookly API";
    c.DocExpansion(DocExpansion.None);
    c.DisplayRequestDuration();
});

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller}/{action}/{id?}");

app.Run();
