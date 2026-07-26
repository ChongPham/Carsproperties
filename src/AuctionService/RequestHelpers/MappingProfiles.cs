using AuctionService.DTO;
using AuctionService.Entities;
using AutoMapper;

namespace AuctionService.RequestHelpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Auction, AuctionDto>().IncludeMembers(s => s.Item);
        CreateMap<Item, AuctionDto>();
        CreateMap<CreateAuctionDto, Auction>()
            .ForMember(dest => dest.Item, opt => opt.MapFrom(s => s));
        CreateMap<CreateAuctionDto, Item>();
        CreateMap<UpdateAuctionDto, Auction>()
            .ForMember(dest => dest.Item, opt => opt.MapFrom(s => s));
        CreateMap<UpdateAuctionDto, Item>();
    }
}