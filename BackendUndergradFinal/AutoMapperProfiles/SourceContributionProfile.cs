using AutoMapper;
using Communication.SourceContributionDto;
using DataLayer.Entities;

namespace BackendUndergradFinal.AutoMapperProfiles
{
    public class SourceContributionProfile : Profile
    {
        public SourceContributionProfile()
        {
            CreateMap<WaterSourceContribution, WaterSourceContributionDto>();
        }
    }
}
