using AutoMapper;
using fr.Database.Model.Entities.Users;

namespace fr.Service.Model.Account
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<RegisterModel, User>()
                .ForMember(dest => dest.Email, source => source.MapFrom(r => r.Email))
                .ForMember(dest => dest.UserName, source => source.MapFrom((r1, r2) => r1.Email.Split('@')[0]))
                .ForMember(dest => dest.PhoneNumber, source => source.MapFrom(r => r.PhoneNumber))
                .ForMember(dest => dest.FirstName, source => source.MapFrom(r => r.FirstName))
                .ForMember(dest => dest.LastName, source => source.MapFrom(r => r.LastName))
                .ForMember(dest => dest.StudentCode, source => source.MapFrom(r => r.StudentCode))
                .ForMember(dest => dest.DayOfBird, source => source.MapFrom(r => r.DayOfBird));

            CreateMap<User, UserProfile>()
                .ForMember(dest => dest.Id, source => source.MapFrom(r => r.Id))
                .ForMember(dest => dest.FirstName, source => source.MapFrom(r => r.FirstName))
                .ForMember(dest => dest.LastName, source => source.MapFrom(r => r.LastName))
                .ForMember(dest => dest.Email, source => source.MapFrom(r => r.Email))
                .ForMember(dest => dest.StudentCode, source => source.MapFrom(r => r.StudentCode))
                .ForMember(dest => dest.DateOfBird, source => source.MapFrom(r => r.DayOfBird))
                .ForAllOtherMembers(source => source.Ignore());
        }
    }
}
