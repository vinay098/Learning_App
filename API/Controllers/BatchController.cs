
using System.Security.Claims;
using API.DTOs.Batch_DTO;
using API.Interface;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BatchController : ControllerBase
    {
        private readonly IBatch _batchrepo;
        private readonly IHttpContextAccessor _accessor;
        public BatchController(IBatch batchrepo,
        IHttpContextAccessor accessor)
        {
            _accessor = accessor;
            _batchrepo = batchrepo;
        }

        [HttpPost("add-batch")]
        public async Task<ActionResult> AddBatch(BatchDto obj)
        {
            string user_Id = _accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                var res = await _batchrepo.AddBatchAsync(user_Id,obj);
                return Ok(res);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

         [HttpGet("batches")]
        public async Task<ActionResult> GetBatches()
        {
            try
            {
                var res = await _batchrepo.GetAllAsync();
                return Ok(res);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpGet("batch/{id}")]
        public async Task<ActionResult> GetBatchDto(int id)
        {
            try
            {
                var res = await _batchrepo.GetBatchDtoByIdAsync(id);
                return Ok(res);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpPut("update-batch/{id}")]
        public async Task<ActionResult> UpdateBatch(int id,BatchDto batch)
        {
            try
            {
                await _batchrepo.UpdateBatchAsync(id,batch);
                return Ok();
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpDelete("delete-batch/{id}")]
        public async Task<ActionResult> DeleteBatch(int id)
        {
            try
            {
                var batch = await _batchrepo.GetBatchByIdAsync(id);
                if(batch == null)
                {
                    return NotFound(new
                    {
                        status=404,
                        message = "batch not found"
                    });
                }
                await _batchrepo.DeleteBatchAsync(batch);
                return Ok(batch);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpPut("assign-batch")]
        public async Task<ActionResult> AssignBatch(AssignBatch obj)
        {
            try
            {
                await _batchrepo.AssignBatch(obj);
                return Ok();
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpGet("get-assigned-batch")]
        public async Task<ActionResult> GetAssignedBatch()
        {
            try
            {
                var response = await _batchrepo.GetAssignedBatch();
                return Ok(response);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

    }
}