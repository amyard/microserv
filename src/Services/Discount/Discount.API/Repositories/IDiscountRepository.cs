﻿using System.Diagnostics.Metrics;
using Discount.API.Entities;

namespace Discount.API.Repositories;

public interface IDiscountRepository
{
    Task<Coupon> GetCoupon(string productName);

    Task<bool> CreateDiscount(Coupon coupon);
    Task<bool> UpdateDiscount(Coupon coupon);
    Task<bool> DeleteDiscount(string productName);
}