using API.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDoLibrary.Data;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskData _data;

        public TasksController(ITaskData data)
        {
            _data = data;
        }
        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<TaskModel>> Get()
        {
            string userId = GetUserId();

            return (await _data.GetTasksAsync(userId))
                        .Adapt<IEnumerable<TaskModel>>();
        }

        // GET api/<TasksController>/bystatus
        [Authorize]
        [HttpGet("bystatus/{statusId}")]
        public async Task<IEnumerable<TaskModel>> Get(int statusId)
        {
            string userId = GetUserId();

            return (await _data.GetTasksByStatusAsync(statusId, userId))
                        .Adapt<IEnumerable<TaskModel>>();
        }

        // POST api/<TasksController>
        //TODO: To change
        [Authorize]
        [HttpPost("{statusId}")]
        public async void Post([FromBody] TaskModel taskModel, int statusId)
        {
            string userId = GetUserId();

            await _data.CreateTaskAsync(taskModel.Adapt<ToDoLibrary.Models.TaskModel>(), userId, statusId);
        }

        // PUT api/<TasksController>/5
        [Authorize]
        [HttpPut("{statusId}")]
        public async void Put([FromBody] TaskModel taskModel,int statusId)
        {
            string userId = GetUserId();

            await _data.UpdateTaskAsync(taskModel.Adapt<ToDoLibrary.Models.TaskModel>(), statusId, userId);
        }

        [Authorize]
        [HttpPut("status")]
        public async void Put(int taskId, int statusId)
        {
            string userId = GetUserId();

            await _data.UpdateTaskStatusAsync(taskId, statusId, userId);
        }

        // DELETE api/<TasksController>/5
        [Authorize]
        [HttpDelete]
        public async void Delete(int id)
        {
            string userId = GetUserId();

            await _data.ChangeTaskActiveFieldAsync(id, userId);
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
