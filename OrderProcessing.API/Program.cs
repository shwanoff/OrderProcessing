using FluentValidation;
using FluentValidation.AspNetCore;
using MartinCostello.OpenApi;
using Microsoft.EntityFrameworkCore;
using OrderProcessing.Application.Handlers.Commands;
using OrderProcessing.Application.Interfaces;
using OrderProcessing.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

builder.Services.AddLogging();

builder.Services.AddDbContext<OrderDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddMediatRServices("OrderProcessing.Application");

builder.Services.AddValidatorsFromAssemblyContaining<CreateOrderCommandValidator>();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// TODO: Fix issue with OpenAPI generation
// There is a well-known issue with the OpenAPI generation
// https://github.com/dotnet/aspnetcore/issues/57332#issuecomment-2479286855
// To fix this issue, we need to add the following line of code from personal library
// https://github.com/martincostello/openapi-extensions?tab=readme-ov-file
builder.Services.AddOpenApiExtensions(options => options.AddServerUrls = true);
builder.Services.AddHttpContextAccessor();
// end of fix

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();

	app.UseSwaggerUI(options =>
	{
		options.SwaggerEndpoint("/openapi/v1.json", "OrderProcessing.API v1");
		options.RoutePrefix = string.Empty;
	});
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
