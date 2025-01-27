using ConfigurationWebApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Redis bağlantı dizesini al
var redisConnectionString = builder.Configuration.GetConnectionString("RedisConnection");
if (string.IsNullOrEmpty(redisConnectionString))
{
    throw new InvalidOperationException("Redis bağlantı dizesi eksik. Lütfen appsettings.json dosyasını kontrol edin.");
}

// Repository'yi Dependency Injection'a ekle
builder.Services.AddScoped<IConfigurationRepository, RedisConfigurationRepository>(provider =>
    new RedisConfigurationRepository(redisConnectionString));

// CORS Ekle
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

// Controller'ları ekle
builder.Services.AddControllers();

var app = builder.Build();

// Geliştirme ortamı için hata sayfası
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// CORS middleware
app.UseCors();

// Route middleware
app.UseRouting();

// Controller'ları haritala
app.MapControllers();

app.Run();
