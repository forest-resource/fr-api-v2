using AutoMapper;
using fr.Database.Model.Entities.Plots;
using fr.Database.Model.Entities.Trees;
using fr.Service.Model.Trees;

namespace fr.Service.Model.Plots
{
    public class PlotProfile : Profile
    {
        public PlotProfile()
        {
            CreateMap<PlotCreateUpdateModel, Plot>()
                .ForMember(r => r.PlotName, source => source.MapFrom(r => r.Name))
                .ForMember(r => r.Title, source => source.MapFrom(r => r.Title))
                .ForMember(r => r.Subtitle, source => source.MapFrom(r => r.Subtitle))
                .ForMember(r => r.Description, source => source.MapFrom(r => r.Description))
                .ForMember(r => r.IsCurrent, source => source.MapFrom(r => r.IsCurrent))
                .ForMember(r => r.PlotPoints, source => source.MapFrom((src, dest) =>
                {
                    dest.PlotPoints = src.PlotPoints.Select(r => new PlotPoint
                    {
                        Id = r.Id ?? Guid.Empty,
                        Sub = r.Sub,
                        Code = r.Code,
                        X = r.X,
                        Y = r.Y,
                        Diameter = r.Diameter,
                        TreeId = r.TreeId,
                        PlotId = src.Id ?? Guid.Empty,
                        Tree = new Tree
                        {
                            ScienceName = r.ScienceName,
                            CommonName = r.CommonName,
                            Family = r.Family
                        }
                    }).ToList();

                    return dest.PlotPoints;
                }))
                .ForAllOtherMembers(source => source.Ignore());
            CreateMap<Plot, PlotModel>()
                .ForMember(r => r.Id, source => source.MapFrom(r => r.Id))
                .ForMember(r => r.Name, source => source.MapFrom(r => r.PlotName))
                .ForMember(r => r.Title, source => source.MapFrom(r => r.Title))
                .ForMember(r => r.Subtitle, source => source.MapFrom(r => r.Subtitle))
                .ForMember(r => r.Description, source => source.MapFrom(r => r.Description))
                .ForMember(r => r.PlotPoints, source => source.MapFrom((src, dest) =>
                {
                    dest.PlotPoints = src.PlotPoints.Select(r => new PlotPointModel
                    {
                        Id = r.Id,
                        Sub = r.Sub,
                        Code = r.Code,
                        TreeId = r.TreeId,
                        X = r.X,
                        Y = r.Y,
                        Diameter = r.Diameter,
                        Height = r.Height,
                        CommonName = r.Tree.CommonName,
                        ScienceName = r.Tree.ScienceName,
                        Family = r.Tree.Family
                    })
                    .ToList();
                    return dest.PlotPoints;
                }))
                .ForAllOtherMembers(source => source.Ignore());
        }
    }
}
