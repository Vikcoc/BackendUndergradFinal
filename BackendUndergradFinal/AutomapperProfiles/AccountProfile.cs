using AutoMapper;
using Communication.AccountDto;
using DataLayer.Entities;

namespace BackendUndergradFinal.AutoMapperProfiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<UserSignUpDto, WaterUser>();
        }
    }
}
