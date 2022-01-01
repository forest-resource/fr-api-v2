using AutoMapper;
using fr.Database.Model.Entities.Icons;

namespace fr.Service.Model.Icons
{
    public class IconProfile : Profile
    {
        public IconProfile()
        {
            CreateMap<Icon, IconModel>()
                .ForMember(r => r.Id, source => source.MapFrom(r => r.Id))
                .ForMember(r => r.IconName, source => source.MapFrom(r => r.IconName))
                .ForMember(r => r.IconData, source => source.MapFrom(r => r.IconData))
                .ForMember(r => r.LastUpdatedBy, source => source.MapFrom(r => r.UpdatedBy))
                .ForMember(r => r.LastUpdatedTime, source => source.MapFrom(r => r.UpdatedTime))
                .ForMember(r => r.TreeCommonNameUsed, source => source.MapFrom(r => r.Tree.CommonName))
                .ForMember(r => r.TreeScienceNameUsed, source => source.MapFrom(r => r.Tree.ScienceName))
                .ForAllOtherMembers(source => source.Ignore());

            CreateMap<IconCreateUpdateModel, Icon>()
                .ForMember(r => r.IconName, source => source.MapFrom(r => r.IconName))
                .ForMember(r => r.IconData, source => source.MapFrom(r => r.IconData))
                .ForMember(r => r.IsDeleted, source => source.MapFrom(r => false))
                .ForAllOtherMembers(source => source.Ignore());
        }
    }
}
