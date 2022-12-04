using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.API.DbContexts;
using ShoppingCart.API.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options
    => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var shoppingCart = app.MapGroup("/api/carts")
    .WithOpenApi();

var products = app.MapGroup("/api/carts/products")
    .WithOpenApi();

shoppingCart.MapGet("/{userId}", async ([FromServices] ApplicationDbContext db, string userId) =>
{
    var shoppingCartHeader = await db.ShoppingCartHeaders
        .AsNoTracking()
        .FirstOrDefaultAsync(c => c.UserId == userId);

    if(shoppingCartHeader is null)
        return Results.NotFound();

    var shoppingCartDetails = await db.ShoppingCartDetails
        .ToListAsync();

    foreach (var detail in shoppingCartDetails)
    {
        var productFromDb = await db.Products
            .FirstOrDefaultAsync(x => x.Id == detail.ProductId);

        detail.Products.Add(productFromDb);
    }

    var shoppingCart = shoppingCartHeader;

    shoppingCart.ShoppingCartDetails = shoppingCartDetails;

    return Results.Ok(shoppingCart);
}).WithSummary("Get shopping cart");

//Add Product to Shopping Cart 
shoppingCart.MapPost("/{productId}", async ([FromServices] ApplicationDbContext db, string productId) =>
{
    var product = await db.Products.FirstOrDefaultAsync(x => x.Id == productId);

    var hasShoppingCart = await db.ShoppingCartHeaders
        .FirstOrDefaultAsync(c => c.UserId == product.CreatedBy);

    if (hasShoppingCart is null)
    {
        var shoppingCartDetails = new ShoppingCartDetailsModel
        {
            Id = Guid.NewGuid().ToString(),
            ProductId = product.Id,
            CreatedDate = DateTime.Now
        };

        var shoppingCartHeader = new ShoppingCartHeaderModel
        {
            Id = Guid.NewGuid(),
            ShoppingCartDetailsId = shoppingCartDetails.Id.ToString(),
            ShoppingCartDetails = new List<ShoppingCartDetailsModel>
            {
                shoppingCartDetails
            },
            ShippingAddress = string.Empty,
            TotalPrice = product.Price,
            UserId = product.CreatedBy,
            CreatedDate = DateTime.Now,
        };

        shoppingCartDetails.ShoppingCartHeaderId = shoppingCartHeader.Id;

        db.ShoppingCartHeaders.Add(shoppingCartHeader);

        db.ShoppingCartDetails.Add(shoppingCartDetails);

        await db.SaveChangesAsync();
    }
    else
    {
        var updatedShoppingCartHeader = hasShoppingCart;

        updatedShoppingCartHeader.TotalPrice += product.Price;

        //Create a shopping cart details
        var shoppingCartDetails = new ShoppingCartDetailsModel
        {
            Id = Guid.NewGuid().ToString(),
            ShoppingCartHeaderId = hasShoppingCart.Id,
            ProductId = product.Id,
            CreatedDate = DateTime.Now
        };

        db.ShoppingCartDetails.Add(shoppingCartDetails);
        await db.SaveChangesAsync();
    }

    return Results.Ok(product);
}).WithSummary("Add product to shopping cart");

//Remove Product From Shopping Cart
shoppingCart.MapDelete("/{productId}", async ([FromServices] ApplicationDbContext db, string productId) =>
{
    var deletedProduct = await db.Products.FirstOrDefaultAsync(x => x.Id == productId);
    
    if (deletedProduct is null)
        return Results.NotFound(productId);

    var shoppingCartDetails = await db.ShoppingCartDetails
        .ToListAsync();

    var currentShoppingCartDetails = shoppingCartDetails
        .FirstOrDefault(x => x.ProductId == productId);

    if (currentShoppingCartDetails is not null)
        db.ShoppingCartDetails.Remove(currentShoppingCartDetails);

    if (shoppingCartDetails.Count() == 1)
    {
        var deletedShoppingCartHeader = await db.ShoppingCartHeaders
            .FirstOrDefaultAsync(x => x.UserId == deletedProduct.CreatedBy);

        db.ShoppingCartHeaders.Remove(deletedShoppingCartHeader);
    }

    var shoppingCartHeader = await db.ShoppingCartHeaders
        .FirstOrDefaultAsync(x => x.UserId == deletedProduct.CreatedBy);

    var updatedShoppingCartHeader = shoppingCartHeader;

    updatedShoppingCartHeader.TotalPrice -= deletedProduct.Price;

    await db.SaveChangesAsync();

    return Results.Ok();
}).WithSummary("remove product from shopping cart");

products.MapGet("/{userId}", async ([FromServices] ApplicationDbContext db, string userId) =>
{
    var productsFromDb = await db.Products
        .Where(x => x.CreatedBy == userId)
        .ToListAsync();

    if(productsFromDb.Count == 0)
        return Results.NotFound();

    return Results.Ok(productsFromDb);
});

//Add product to products
products.MapPost("/", async ([FromServices] ApplicationDbContext db, ProductModel product) =>
{
    if (product is null)
        return Results.BadRequest();

    db.Products.Add(product);

    await db.SaveChangesAsync();

    return Results.Ok(product);
}).WithSummary("Add product to products");

app.Run();