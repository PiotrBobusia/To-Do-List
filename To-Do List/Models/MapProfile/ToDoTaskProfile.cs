using AutoMapper;
using To_Do_List.Models.DTOs;

namespace To_Do_List.Models.MapProfile
{
    public class ToDoTaskProfile : Profile
    {
        public ToDoTaskProfile()
        {
            CreateMap<ToDoTask, ToDoTaskDTO>().ReverseMap();
        }
    }
}
