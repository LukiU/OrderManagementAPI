using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using OrderManagement.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Order Management API", Version = "v1" });
}); 
var dbConfig = builder.Configuration.GetSection("OrderManagemmentDatabase");

var mongoClient = new MongoClient(dbConfig.GetValue<string>("ConnectionURI"));
builder.Services.AddSingleton<IMongoClient>(mongoClient);

var orderRepository = new OrderRepository(mongoClient, dbConfig.GetValue<string>("DatabaseName"), dbConfig.GetValue<string>("CollectionName"));
builder.Services.AddSingleton<IOrderRepository>(orderRepository); 
var reportService = new ReportService(orderRepository);

builder.Services.AddSingleton<IReportService>(reportService); 
builder.Services.AddHostedService<ReportService>(sp => reportService);

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("../swagger/v1/swagger.json", "Order Management API v1");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
