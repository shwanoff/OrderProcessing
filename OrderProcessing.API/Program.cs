using OrderProcessing.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddSerilogService(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddLogging();
builder.Services.AddAutoMapper();
builder.Services.AddMediatR();
builder.Services.AddFluentValidation();
builder.Services.AddRepositories();
builder.Services.AddCustomOpenApi();

if (builder.Environment.IsDevelopment() || builder.Environment.IsEnvironment("Local"))
{
	builder.Services.AddSqlServerDatabase(builder.Configuration);
}
else if (builder.Environment.IsEnvironment("Testing"))
{
	builder.Services.AddInMemoryDatabase();
}

var app = builder.Build();

if (app.Environment.IsDevelopment() || builder.Environment.IsEnvironment("Local"))
{
	app.Services.MigrateDatabase();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapOpenApi();
app.UseCustomSwaggerUI();

Log.Warning("Application started");

app.Run();

// TODO: this is a hack to expose the Program type to the test project
public partial class Program { }