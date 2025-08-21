using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
    public class DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger)
        : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {

            var coupon = await dbContext.
                Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

            if (coupon is null)
                coupon = new Coupon { ProductName = "No discount", Amount = 0, Description = "Not Discount desc" };

            var couponModel = coupon.Adapt<CouponModel>();

            return couponModel;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();

            if (coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Request"));

            dbContext.Coupons.Add(coupon);
            await dbContext.SaveChangesAsync();

            var couponModel = coupon.Adapt<CouponModel>();

            return couponModel;
        }
        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();

            if (coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Request"));

            dbContext.Coupons.Update(coupon);
            await  dbContext.SaveChangesAsync();

            var couponModel = coupon.Adapt<CouponModel>();

            return couponModel;
        }
        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext.
                Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

            if (coupon is null)
               return new DeleteDiscountResponse { Success = false };

            dbContext.Coupons.Remove(coupon);
            var deleted = await dbContext.SaveChangesAsync() > 0;

            return new DeleteDiscountResponse { Success = deleted };

        }
    }
}
