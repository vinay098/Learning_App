using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.Batch_DTO;
using API.Models;

namespace API.Interface
{
    public interface IBatch
    {
        Task<IList<BatchDto>> GetAllAsync();
        Task<BatchDto> GetBatchDtoByIdAsync(int id);
        Task<BatchDto> AddBatchAsync(string user_id,BatchDto batch);
        Task DeleteBatchAsync(Batch batch);
        Task UpdateBatchAsync(int id,BatchDto batch);
        // Task<List<BatchDto>> GetAll();
        Task<Batch> GetBatchByIdAsync(int id);
        Task AssignBatch(AssignBatch obj);

        Task<List<AssignBatchDisplay>> GetAssignedBatch();
    }
}