using AutoMapper;
using ToDoApp.Models;

namespace ToDoApp.Profiles
{
    public class TasksProfile:Profile
    {
        public TasksProfile()
        {
            CreateMap<AddTaskDto, TaskToDo>();
        }
    }
}
