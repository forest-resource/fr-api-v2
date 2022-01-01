using AutoMapper;
using fr.Core.Expressions;
using Newtonsoft.Json;

namespace fr.Service.Model
{
    public class OtherProfile : Profile
    {
        public OtherProfile()
        {
            CreateMap<object, ExpressionRule>()
                .ConvertUsing(o => JsonConvert.DeserializeObject<ExpressionRule>(JsonConvert.SerializeObject(o)) ?? new());
        }
    }
}
