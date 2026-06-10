using Helpers.Zim.Clients;
using Helpers.Zim.Clients.Concrete;
using Helpers.Zim.Services;
using Helpers.Zim.Services.Concrete;
using System.Xml.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
	.AddMemoryCache()
	.AddTransient<HttpMessageHandler>(_ => new HttpClientHandler { AllowAutoRedirect = false, })
	.AddCachingHandler(c => c.Expiration = TimeSpan.FromHours(1))
	.AddSingleton(new XmlSerializerFactory())
	.AddHttpClient<IZimClient, ZimClient>(c => c.BaseAddress = new Uri("https://download.kiwix.org/"))
		.ConfigurePrimaryHttpMessageHandler<HttpMessageHandler>()
		.AddHttpMessageHandler<CachingHandler>()
		.Services
	.AddTransient<IZimService, ZimService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
}

app.MapControllers();

app.Run();
