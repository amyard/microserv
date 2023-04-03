using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DiscountController : ControllerBase
{
    private readonly IDiscountRepository _discountRepository;

    public DiscountController(IDiscountRepository discountRepository)
    {
        _discountRepository = discountRepository;
    }

    [HttpGet("{productName}", Name = "GetDiscount")]
    [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(Coupon))]
    public async Task<ActionResult<Coupon>> GetDiscount(string productName)
    {
        var discount = await _discountRepository.GetCoupon(productName);

        return Ok(discount);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(Coupon))]
    public async Task<ActionResult<Coupon>> CreateDiscount([FromBody] Coupon coupon)
    {
        await _discountRepository.CreateDiscount(coupon);

        return CreatedAtRoute("GetDiscount", new {productName=coupon.ProductName}, coupon);
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(Coupon))]
    public async Task<ActionResult<Coupon>> UpdateDiscount([FromBody] Coupon coupon)
    {
        return Ok(await _discountRepository.UpdateDiscount(coupon));
    }
    
    [HttpDelete("{productName}", Name = "DeleteDiscount")]
    [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(Coupon))]
    public async Task<ActionResult<Coupon>> DeleteDiscount(string productName)
    {
        return Ok(await _discountRepository.DeleteDiscount(productName));
    }
}