
using System.Security.Claims;
using API.DTOs.Batch_DTO;
using API.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BatchController : ControllerBase
    {
        private readonly IBatch _batchrepo;
        private readonly IHttpContextAccessor _accessor;
        private readonly SieveProcessor _sieveProcessor;
        public BatchController(IBatch batchrepo,
        IHttpContextAccessor accessor,SieveProcessor sieveProcessor)
        {
            _accessor = accessor;
            _batchrepo = batchrepo;
            _sieveProcessor = sieveProcessor;
        }

        [HttpPost("add-batch")]
        public async Task<ActionResult> AddBatch(BatchDto obj)
        {
            string user_Id = _accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                var res = await _batchrepo.AddBatchAsync(user_Id, obj);
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
        public async Task<ActionResult> UpdateBatch(int id, BatchDto batch)
        {
            try
            {
                await _batchrepo.UpdateBatchAsync(id, batch);
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
                if (batch == null)
                {
                    return NotFound(new
                    {
                        status = 404,
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

        [HttpGet("get-data-for-facylty")]
        public async Task<ActionResult> GetDataForDaculty()
        {
            string user_Id = _accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                var response = await _batchrepo.FacultyDataByID(user_Id);
                return Ok(response);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpGet("batch-to-enroll")]
        public async Task<ActionResult> BatchToEnroll()
        {
            try
            {
                var response = await _batchrepo.GetEmployeeData();
                return Ok(response);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpGet("search-batch")]
        public async Task<ActionResult<List<BatchDto>>> SearchBatch([FromQuery]SieveModel model)
        {
            try
            {
                var response = await _batchrepo.SearchBatch();
                response = _sieveProcessor.Apply(model,response);
                return response.ToList();
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpGet("get-batch-by-params")]
        public async Task<ActionResult> GetBatches(string term,string sort,int page=1,int limit=4)
        {
            try
            {
                var response = await _batchrepo.GetBatchDtoByQueryParams(term,sort,page,limit);
                return Ok(response);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpGet("get-emp-data-by-params")]
        public async Task<ActionResult> GetEmpData(string term,string sort,int page=1,int limit=4)
        {
            try
            {
                var response = await _batchrepo.GetEmployeeDataByParams(term,sort,page,limit);
                return Ok(response);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpGet("batch-count")]
        public async Task<ActionResult> GetBatchCount()
        {
            try
            {
                var ans = await _batchrepo.GetBatchCount();
                return Ok(ans);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

    }
}