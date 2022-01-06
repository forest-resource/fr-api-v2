using AutoMapper;
using fr.Database.Model.Entities.Trees;

namespace fr.Service.Model.Trees
{
    public class TreeProfile : Profile
    {
        public TreeProfile()
        {
            CreateMap<TreeDetailModel, TreeDetail>()
                .ForMember(r => r.Key, source => source.MapFrom(r => r.Key))
                .ForMember(r => r.Value, source => source.MapFrom(r => r.Value))
                .ForAllOtherMembers(source => source.Ignore());

            CreateMap<TreeDetail, TreeDetailModel>()
                .ForMember(r => r.Id, source => source.MapFrom(r => r.Id))
                .ForMember(r => r.Key, source => source.MapFrom(r => r.Key))
                .ForMember(r => r.Value, source => source.MapFrom(r => r.Value));

            CreateMap<TreeCreateAndUpdateModel, Tree>()
                .ForMember(r => r.CommonName, source => source.MapFrom(r => r.CommonName))
                .ForMember(r => r.ScienceName, source => source.MapFrom(r => r.ScienceName))
                .ForMember(r => r.Family, source => source.MapFrom((r) => r.Family))
                .ForMember(r => r.IconId, source => source.MapFrom(r => r.IconId))
                .ForMember(r => r.IsDeleted, source => source.MapFrom(r => false))
                .ForMember(r => r.TreeDetails, source => source.MapFrom((source, dest) =>
                {
                    var treeDetails = source.TreeDetails ?? new List<TreeDetailModel>();

                    dest.TreeDetails = treeDetails.Select(r => new TreeDetail(null)
                    {
                        Key = r.Key,
                        Value = r.Value,
                        TreeId = source.Id ?? default
                    }).ToList();

                    return dest.TreeDetails;
                }))
                .ForAllOtherMembers(source => source.Ignore());

            CreateMap<Tree, TreeModel>()
                .ForMember(r => r.Id, source => source.MapFrom(r => r.Id))
                .ForMember(r => r.CommonName, source => source.MapFrom(r => r.CommonName))
                .ForMember(r => r.ScienceName, source => source.MapFrom(r => r.ScienceName))
                .ForMember(r => r.IconId, source => source.MapFrom(r => r.IconId))
                .ForMember(r => r.IconData, source => source.MapFrom(r => r.Icon.IconData))
                .ForMember(r => r.TreeDetails, source => source.MapFrom(r => r.TreeDetails));
        }
    }
}
