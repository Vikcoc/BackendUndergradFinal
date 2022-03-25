using AutoMapper;
using Communication.SourceVariantDto;
using DataLayer.Entities;
using System;
using System.Linq;

namespace BackendUndergradFinal.AutoMapperProfiles
{
    public class SourceVariantProfile : Profile
    {
        public SourceVariantProfile()
        {
            CreateMap<WaterSourceVariant, WaterSourceVariantDto>()
                .ForMember(x => x.Picture, x => x.MapFrom(y => y.Pictures.Any() ? y.Pictures.FirstOrDefault().Id : (Guid?)null));
        }
    }
}
