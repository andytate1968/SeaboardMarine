using AutoMapper;
using DataAccess.Data;

using Models;

namespace Business.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DataAccess.Data.Task, TaskDto>().ReverseMap();
            CreateMap<DataAccess.Data.TaskTag, TaskTagDto>().ReverseMap();
        }
    }
}
