using AutoMapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;

namespace Discount.Grpc.Mapper
{
    public class DiscoutProfile : Profile
    {
        protected DiscoutProfile()
        {
            CreateMap<Coupon, CouponModel>().ReverseMap();
        }
    }
}