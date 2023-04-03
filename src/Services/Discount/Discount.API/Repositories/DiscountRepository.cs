using Dapper;
using Discount.API.Entities;
using Npgsql;

namespace Discount.API.Repositories;

public class DiscountRepository : IDiscountRepository
{
    private readonly IConfiguration _config;

    public DiscountRepository(IConfiguration config)
    {
        _config = config;
    }
    public async Task<Coupon> GetCoupon(string productName)
    {
        await using var connection = new NpgsqlConnection(_config.GetValue<string>("DiscountRepository:ConnectionString"));
        
        var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
            ("SELECT * FROM Coupon WHERE ProductName=@ProductName", new { ProductName = productName });

        if (coupon == null)
            coupon = new Coupon() {ProductName = "No Discount", Amount = 0, Description = "No Discount Desc"};

        return coupon;
    }

    public async Task<bool> CreateDiscount(Coupon coupon)
    {
        await using var connection = new NpgsqlConnection(_config.GetValue<string>("DiscountRepository:ConnectionString"));

        var affected = await connection.ExecuteAsync
        ("insert into Coupon(ProductName, Description, Amount) values (@ProductName, @Description, @Amount)",
            new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount });

        if (affected == 0)
            return false;

        return true;
    }

    public async Task<bool> UpdateDiscount(Coupon coupon)
    {
        await using var connection = new NpgsqlConnection(_config.GetValue<string>("DiscountRepository:ConnectionString"));

        var affected = await connection.ExecuteAsync(
            "update Coupon set ProductName=@ProductName,Description=@Description,Amount=@Amount where id=@Id",
            new
            {
                ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount,
                Id = coupon.Id
            });

        if (affected == 0)
            return false;

        return true;
    }

    public async Task<bool> DeleteDiscount(string productName)
    {
        await using var connection = new NpgsqlConnection(_config.GetValue<string>("DiscountRepository:ConnectionString"));

        var affected = await connection.ExecuteAsync("delete from coupon where ProductName=@ProductName",
            new { ProductName = productName });

        if (affected == 0)
            return false;

        return true;
    }
}