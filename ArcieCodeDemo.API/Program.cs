using ArcieCodeDemo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection: Registering the DbContext
builder.Services.AddDbContext<AdventureWorksContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AdventureWorksDatabase")));

var connectionString = builder.Configuration.GetConnectionString("AdventureWorksDatabase");
try
{
    using (var connection = new SqlConnection(connectionString))
    {
        connection.Open();
        Console.WriteLine("Database connection successful!");

        // Test if the GetTopProducts stored procedure exists
        using (var command = new SqlCommand("SELECT OBJECT_ID('dbo.GetTopProducts', 'P')", connection))
        {
            var result = command.ExecuteScalar();
            if (result != DBNull.Value)
            {
                Console.WriteLine("GetTopProducts stored procedure exists.");
            }
            else
            {
                Console.WriteLine("GetTopProducts stored procedure does not exist!");
            }
        }
    }
}
catch (SqlException ex)
{
    Console.WriteLine($"Database connection failed: {ex.Message}");
}

// Dependency Injection: Registering the repository
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Add CORS support
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowWebApp",
        builder => builder.WithOrigins("https://localhost:7002") // Adjust this to match your Web project's URL
                          .AllowAnyMethod()
                          .AllowAnyHeader());
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