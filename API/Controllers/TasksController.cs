using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Security.Claims;
using ToDoLibrary.Data;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskData _data;
        private readonly IMapper _mapper;
        private readonly ILogger<TasksController> _logger;

        public TasksController(ITaskData data, IMapper mapper,
            ILogger<TasksController> logger)
        {
            _data = data;
            _mapper = mapper;
            _logger = logger;
        }
        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<TaskModel>> Get()
        {
            string userId = GetUserId();

            return _mapper.Map<IEnumerable<TaskModel>>(await _data.GetTasksAsync(userId));
        }

        [Authorize]
        [HttpGet("bystatus/{statusId}")]
        public async Task<IEnumerable<TaskModel>> Get(int statusId)
        {
            string userId = GetUserId();

            return _mapper.Map<IEnumerable<TaskModel>>(await _data.GetTasksByStatusAsync(statusId, userId));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TaskModel taskModel)
        {
            string userId = GetUserId();
            try 
            {
                await _data.CreateTaskAsync(_mapper.Map<ToDoLibrary.Models.TaskDbModel>(taskModel), userId);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 77777)
                {
                    _logger.LogError(ex, "Task has incorrect status");
                    throw new Exception("Incorrect status");
                }
                _logger.LogError(ex, null);
                throw new Exception("Error");
            }
            return Ok();
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] TaskModel taskModel)
        {
            string userId = GetUserId();
            try
            {
                await _data.UpdateTaskAsync(_mapper.Map<ToDoLibrary.Models.TaskDbModel>(taskModel), userId);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 77777)
                {
                    _logger.LogError(ex, "Task has incorrect status");
                    throw new Exception("Incorrect status");
                }
                _logger.LogError(ex,null);
                throw new Exception("Error");
            }
            return Ok();
        }

        [Authorize]
        [HttpPut("status")]
        public async Task<IActionResult> Put(int taskId, int statusId)
        {
            string userId = GetUserId();

            try
            {
                await _data.UpdateTaskStatusAsync(taskId, statusId, userId);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 77777)
                {
                    _logger.LogError(ex, "Task has incorrect status");
                    throw new Exception("Incorrect status");
                }
                _logger.LogError(ex,null);
                throw new Exception("Error");
            }
            return Ok();
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            string userId = GetUserId();
            try
            {
                await _data.ChangeTaskActiveFieldAsync(id, userId);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex,null);
                throw new Exception("Error");
            }
            return Ok();
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
