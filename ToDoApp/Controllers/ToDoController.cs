using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private ResponseDto _response;
        private readonly IMapper _mapper;
        public ToDoController(IMapper mapper) 
        { 
            _response = new ResponseDto();
            _mapper = mapper;
        }
        private static List<TaskToDo> TaskToDo = new List<TaskToDo>()
        {
            new TaskToDo()
            {
                Title = "Cook",
                Description ="Cooking Mokimo",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
            }
        };

        [HttpGet]
        public ActionResult<ResponseDto> getAllTasks()
        {
            _response.Result = TaskToDo;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        [HttpGet("{guid}")] 
        public ActionResult<ResponseDto> getOneTask(Guid guid)
        {
            var newTask = TaskToDo.Find(x => x.Id == guid);
            if (newTask == null)
            {
                //not found
                _response.Result = null;
                _response.Message = "Task not Found";
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }
            _response.Result = newTask;
            return Ok(_response);
        }
        [HttpPost] 
        public ActionResult<ResponseDto> addTask(AddTaskDto newTask)
        {
            
            //manual way
            var newTasks = new TaskToDo()
            {
                Title = newTask.Title,
                Description = newTask.Description,
                StartDate = newTask.StartDate,
                EndDate = newTask.EndDate,
               
            };
            //var newTasks = _mapper.Map<TaskToDo>(newTask);
            _response.Result = "Task Added Successfully";
            _response.StatusCode = HttpStatusCode.Created;
            TaskToDo.Add(newTasks);

            return Created($"api/ToDo/{newTasks.Id}", _response);


        }
        [HttpPatch("{id}")]
        public ActionResult<ResponseDto> updateTask(Guid id, AddTaskDto UptTask)
        {
            var newTask = TaskToDo.Find(x => x.Id == id);
            if (newTask == null)
            {
                //not found
                _response.Result = null;
                _response.Message = "Task not Found";
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }

            //Spread opertor
            _mapper.Map(UptTask, newTask);

            _response.Result = newTask;
            _response.Result = "Task Updated Successfully";
            return Ok(_response);
        }
        [HttpDelete("{id}")]
        public ActionResult<ResponseDto> deleteTrainee(Guid id)
        {
            var newTask = TaskToDo.Find(x => x.Id == id);
            if (newTask == null)
            {
                //not found
                _response.Result = null;
                _response.Message = "Task not Found";
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }
            //delete
            TaskToDo.Remove(newTask);
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.Result = "Task Removed Successfully";
            return NoContent();
        }
    }
}
