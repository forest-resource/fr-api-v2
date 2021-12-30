using AutoMapper;
using fr.Database.Model.Entities.Icons;

namespace fr.Service.Model.Icons
{
    public class IconProfile : Profile
    {
        public IconProfile()
        {
            CreateMap<Icon, IconModel>()
                .ForMember(r => r.LastUpdatedBy, source => source.MapFrom(r => r.UpdatedBy))
                .ForMember(r => r.LastUpdatedTime, source => source.MapFrom(r => r.UpdatedTime));

            CreateMap<IconCreateUpdateModel, Icon>()
                .ForMember(r => r.IconName, source => source.MapFrom(r => r.IconName))
                .ForMember(r => r.IconData, source => source.MapFrom(r => r.IconData));
        }
    }
}
