using System.Linq;
using AutoMapper;
using Communication.SourcePlaceDto;
using DataLayer.Entities;

namespace BackendUndergradFinal.AutoMapperProfiles
{
    public class SourcePlaceProfile : Profile
    {
        public SourcePlaceProfile()
        {
            CreateMap<WaterSourcePlaceCreateDto, WaterSourcePlace>()
                .ForMember(x => x.Pictures, x => x.Ignore());
            CreateMap<WaterSourcePlace, WaterSourcePlaceListingDto>()
                .ForMember(x => x.Picture,
                    x => x.MapFrom(y => y.Pictures.FirstOrDefault().Id));
            CreateMap<WaterSourcePlace, WaterSourcePlaceListingWithContributionDto>()
                .ForMember(x => x.Picture,
                    x => x.MapFrom(y => y.Pictures.FirstOrDefault().Id))
                .ForMember(x => x.Contribution, x => x.MapFrom(y => y.Contributions.FirstOrDefault()));

        }
    }
}
