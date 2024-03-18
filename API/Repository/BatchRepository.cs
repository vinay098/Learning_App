
using API.Context;
using API.DTOs.Batch_DTO;
using API.Interface;
using API.Models;

using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class BatchRepository : IBatch
    {
        private readonly ApplicationDbContext _context;
        public BatchRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BatchDto> AddBatchAsync(string user_id, BatchDto batch)
        {
            var skill_id = await _context.Skills
            .Where(s=>s.Skill_Name == batch.Skill_Name)
            .Select(s=>s.Id).FirstAsync();
            var addedBatch = new Batch
            {
                BatchName = batch.Name,
                StartDate = Convert.ToDateTime(batch.StartDate),
                EndDate = Convert.ToDateTime(batch.EndDate),
                Capacity = batch.Capacity,
                SkillId = skill_id,
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
                dto.Skill_Name =batch.Skill_Name;
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
            var skill_name = await _context.Batches.Include(b => b.Skills.Select(s => s.Skill_Name)).ToListAsync();
            var batches = await _context.Batches.ToListAsync();
            var batch_dto = new List<BatchDto>();
            foreach (var batch in batches)
            {
                var dto = new BatchDto();
                dto.Id = batch.Id;
                dto.Name = batch.BatchName;
                dto.StartDate = Convert.ToString(batch.StartDate);
                dto.EndDate = Convert.ToString(batch.EndDate);
                dto.Capacity = batch.Capacity;
                dto.Technology = batch.Technology;
                batch_dto.Add(dto);
            }
            // foreach(var name in skill_name)
            // {
            //     var dto = new BatchDto();
            //     dto.Skill_Name = na
            // }
            return batch_dto;

        }


        public async Task<BatchDto> GetBatchDtoByIdAsync(int id)
        {
             var skill_name = _context.Batches
            .Join(_context.Skills,
                b => b.SkillId,
                s => s.Id,
                (s, b) => new
                {
                    Batch_id = s.Id,
                    Batch_Name = s.BatchName,
                    Start_Date = s.StartDate,
                    End_Date = s.EndDate,
                    Capacity = s.Capacity,
                    Technology = s.Technology,
                    Skill_Name = b.Skill_Name
                }
            );
            var res = await skill_name.ToListAsync();
            var res2 = res.FirstOrDefault(x=>x.Batch_id == id);
            if(res2 == null){
                throw new Exception("Value does not exist");
            }
            var dto = new BatchDto{
                Id = res2.Batch_id,
                Name = res2.Batch_Name,
                StartDate = Convert.ToString(res2.Start_Date),
                EndDate = Convert.ToString(res2.End_Date),
                Capacity = res2.Capacity,
                Technology = res2.Technology,
                Skill_Name = res2.Skill_Name
            };
            return dto;
        }

        public async Task<List<BatchDto>> GetAll()
        {
            var skill_name = _context.Batches
            .Join(_context.Skills,
                b => b.SkillId,
                s => s.Id,
                (s, b) => new
                {
                    Batch_id = s.Id,
                    Batch_Name = s.BatchName,
                    Start_Date = s.StartDate,
                    End_Date = s.EndDate,
                    Capacity = s.Capacity,
                    Technology = s.Technology,
                    Skill_Name = b.Skill_Name
                }
            );

            var res = await skill_name.ToListAsync();
            var ans = new List<BatchDto>();
            foreach (var val in res)
            {
                var dto = new BatchDto();
                dto.Id = val.Batch_id;
                dto.Name = val.Batch_Name;
                dto.StartDate = Convert.ToString(val.Start_Date);
                dto.EndDate = Convert.ToString(val.End_Date);
                dto.Capacity = val.Capacity;
                dto.Technology = val.Technology;
                dto.Skill_Name =  val.Skill_Name;
                ans.Add(dto);
            }
            return ans;
        }

        public async Task UpdateBatchAsync(int id,BatchDto batch)
        {
            var updateBatch = await _context.Batches.FindAsync(id);
            var skill_id = await _context.Skills.Where(x=>x.Skill_Name ==batch.Skill_Name).Select(x=>x.Id).FirstAsync();
            if(updateBatch == null)
            {
                throw new Exception("Not found batch");
            }
            updateBatch.BatchName = batch.Name;
            updateBatch.StartDate = Convert.ToDateTime(batch.StartDate);
            updateBatch.EndDate = Convert.ToDateTime(batch.EndDate);
            updateBatch.Capacity = batch.Capacity;
            updateBatch.Technology = batch.Technology;
            updateBatch.SkillId = skill_id;

            await _context.SaveChangesAsync();
        }

        public async Task<Batch> GetBatchByIdAsync(int id)
        {
            var batch = await _context.Batches.FindAsync(id);
            return batch;
        }
    }
}