using LifeTrackerDatabaseAPI.Intefaces;
using LifeTrackerDatabaseAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDataAccess, DataAccess>();
builder.Services.AddScoped<IDiaryManager, DiaryManager>();
builder.Services.AddScoped<ISportManager, SportManager>();
builder.Services.AddScoped<IAccountManager, AccountManager>();

var corsSettings = builder.Configuration.GetSection("CorsSettings");

builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(policy =>
	{
		var allowedOrigins = corsSettings["AllowedOrigins"]?.Split(',') ?? new[] { "*" };
		var allowedMethods = corsSettings["AllowedMethods"]?.Split(',') ?? new[] { "GET", "POST" };
		var allowedHeaders = corsSettings["AllowedHeaders"]?.Split(',') ?? new[] { "*" };

		policy.WithOrigins(allowedOrigins)
			  .WithMethods(allowedMethods)
			  .WithHeaders(allowedHeaders);

		if (bool.TryParse(corsSettings["AllowCredentials"], out var allowCredentials) && allowCredentials)
		{
			policy.AllowCredentials();
		}
		else
		{
			policy.DisallowCredentials();
		}

		if (int.TryParse(corsSettings["MaxAge"], out var maxAge))
		{
			policy.SetPreflightMaxAge(TimeSpan.FromSeconds(maxAge));
		}

		var exposedHeaders = corsSettings["ExposedHeaders"]?.Split(',') ?? Array.Empty<string>();
		if (exposedHeaders.Any())
		{
			policy.WithExposedHeaders(exposedHeaders);
		}
	});
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
