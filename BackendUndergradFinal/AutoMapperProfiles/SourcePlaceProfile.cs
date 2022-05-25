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
        }
    }
}
