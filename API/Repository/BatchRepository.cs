
using API.Context;
using API.DTOs.Batch_DTO;
using API.Interface;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class BatchRepository : IBatch
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public BatchRepository(ApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<BatchDto> AddBatchAsync(string user_id, BatchDto batch)
        {

            var addedBatch = new Batch
            {
                BatchName = batch.Name,
                StartDate = Convert.ToDateTime(batch.StartDate),
                EndDate = Convert.ToDateTime(batch.EndDate),
                Capacity = batch.Capacity,
                UserId = user_id,
                Technology = batch.Technology
            };
            _context.Batches.Add(addedBatch);
            await _context.SaveChangesAsync();
             var dto = new BatchDto();
            {
                dto.Id = addedBatch.Id;
                dto.Name = addedBatch.BatchName;
                dto.StartDate =Convert.ToString(addedBatch.StartDate);
                dto.EndDate = Convert.ToString(addedBatch.EndDate);
                dto.Capacity = addedBatch.Capacity;
                dto.Technology = addedBatch.Technology;
            }
            return dto;
        }

        public async Task DeleteBatchAsync(Batch batch)
        {
            _context.Batches.Remove(batch);
            await _context.SaveChangesAsync();
        }
        public async Task<IList<BatchDto>> GetAllAsync()
        {
            var res = await _context.Batches.ToListAsync();
            var ans=  _mapper.Map<List<Batch>,List<BatchDto>>(res);
            return ans;
        }
        public async Task<BatchDto> GetBatchDtoByIdAsync(int id)
        {
            var res = await _context.Batches.ToListAsync();
            var ans=  _mapper.Map<List<Batch>,List<BatchDto>>(res);
            var res2 = ans.FirstOrDefault(x=>x.Id==id);
            
            if(res2 == null){
                throw new Exception("Value does not exist");
            }
            return res2;
        }
        public async Task UpdateBatchAsync(int id,BatchDto batch)
        {
            var updateBatch = await _context.Batches.FindAsync(id);
            if(updateBatch == null)
            {
                throw new Exception("Not found batch");
            }
            updateBatch.BatchName = batch.Name;
            updateBatch.StartDate = Convert.ToDateTime(batch.StartDate);
            updateBatch.EndDate = Convert.ToDateTime(batch.EndDate);
            updateBatch.Capacity = batch.Capacity;
            updateBatch.Technology = batch.Technology;

            await _context.SaveChangesAsync();
        }

        public async Task<Batch> GetBatchByIdAsync(int id)
        {
            var batch = await _context.Batches.FindAsync(id);
            return batch;
        }
    }
}