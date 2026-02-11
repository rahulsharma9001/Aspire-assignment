using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add SQLite database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=items.db"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Add Item endpoint
app.MapPost("/add-item", async (AppDbContext db, string name) =>
{
    var item = new Item { Name = name };
    db.Items.Add(item);
    await db.SaveChangesAsync();
    return Results.Ok(item);
});

// Get all items endpoint
app.MapGet("/items", async (AppDbContext db) =>
{
    return await db.Items.ToListAsync();
});

app.Run();

// Model class
class Item
{
    public int Id { get; set; }
    public string Name { get; set; }
}

// DbContext class
class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Item> Items => Set<Item>();
}
