using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Security.Claims;
using ToDoLibrary.Data;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusesController : ControllerBase
    {
        private readonly IStatusData _statusData;
        private readonly IMapper _mapper;
        private readonly ILogger<StatusesController> _logger;

        public StatusesController(IStatusData statusData, IMapper mapper,
            ILogger<StatusesController> logger)
        {
            _statusData = statusData;
            _mapper = mapper;
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<StatusModel>> Get()
        {
            return _mapper.Map<IEnumerable<StatusModel>>(await _statusData.GetUserStatusesAsync(GetUserId()));
        }

        [Authorize]
        [HttpPost]
        public async void Post([FromBody] StatusModel status)
        {
            await _statusData.CreateStatusAsync(_mapper.Map<ToDoLibrary.Models.StatusDbModel>(status), GetUserId());
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _statusData.ChangeStatusActiveFieldAsync(id, GetUserId());
            }
            catch (SqlException ex)
            {
                if (ex.Number == 77777)
                {
                    _logger.LogError(ex,"Cannot delete status wich id = {id}",id);
                    throw new Exception($"You cannot delete this status");
                }

                _logger.LogError(ex,"Exception during deleting status");
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
