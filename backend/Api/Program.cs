using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Api;

public class Program
{
	public static async Task Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		builder.Services.AddDbContext<BrewWholesaleDbContext>(options =>
			options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

		const string allowAll = "_allowAll";
		builder.Services.AddCors(opt =>
		{
			opt.AddPolicy(allowAll, p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
		});

		builder.Services.AddControllers();
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();

		var app = builder.Build();

		if (app.Environment.IsDevelopment())
		{
			using var scope = app.Services.CreateScope();
			var db = scope.ServiceProvider.GetRequiredService<BrewWholesaleDbContext>();
			await db.Database.MigrateAsync();

			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseCors(allowAll);

		app.MapControllers();

		await app.RunAsync();
	}
}