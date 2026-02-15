
namespace Discount.Grpc.Services;

public class DiscountAvailService(DiscountContext dbContext, ILogger<DiscountAvailService> logger)
    : DiscountService.DiscountServiceBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext
            .Coupons.FirstOrDefaultAsync(c => c.ProductName == request.ProductName);
        if (coupon is null)
        {
            coupon = new Coupon
            {
                ProductName = request.ProductName,
                Description = "No discount available",
                Amount = 0
            };
        }
        logger.LogInformation("Discount is retrieved for ProductName: {ProductName}, Amount: {Amount}", coupon.ProductName, coupon.Amount);
        var couponModal = coupon.Adapt<CouponModel>();
        return couponModal;
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();
        if(coupon is null)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid coupon data"));
        }
        dbContext.Coupons.Add(coupon);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Discount is successfully created. ProductName: {ProductName}", coupon.ProductName);
        var couponModal = coupon.Adapt<CouponModel>();
        return couponModal;
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();
        if(coupon is null)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid coupon data"));
        }
        var existingCoupon = dbContext.Coupons.FirstOrDefault(c => c.Id == coupon.Id);
        if(existingCoupon is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Coupon with Id {coupon.Id} not found"));
        }
        existingCoupon.ProductName = coupon.ProductName;
        existingCoupon.Description = coupon.Description;
        existingCoupon.Amount = coupon.Amount;
        dbContext.Coupons.Update(existingCoupon);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Discount is successfully updated. ProductName: {ProductName}", existingCoupon.ProductName);
        var couponModal = existingCoupon.Adapt<CouponModel>();
        return couponModal;
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var coupon = dbContext.Coupons.FirstOrDefault(c => c.Id == request.Id);
        if(coupon is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Coupon with Id {request.Id} not found"));
        }
        dbContext.Coupons.Remove(coupon);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Discount is successfully deleted. ProductName: {ProductName}", coupon.ProductName);
        var response = new DeleteDiscountResponse
        {
            Success = true
        };
        return response;
    }
}   