using FileUploadAPI.Services;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// MongoDB connection configuration
string connectionString = builder.Configuration.GetConnectionString("MongoDbConnection");
builder.Services.AddSingleton<IMongoClient>(_ => new MongoClient(connectionString));

// Use environment variable to distinguish test environment
builder.Services.AddScoped<IFileService>(provider =>
{
    var mongoClient = provider.GetRequiredService<IMongoClient>();
    bool isTestEnvironment = builder.Environment.IsEnvironment("Testing");
    return new FileService(mongoClient, isTestEnvironment);
});

// CORS configuration to allow all origins for development
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

var app = builder.Build();

// Apply CORS policy globally (allowing all origins)
app.UseCors("AllowAllOrigins");

// Enable Swagger in development mode
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Apply Authorization middleware
app.UseAuthorization();

// Map controllers to the endpoints
app.MapControllers();

app.Run();
