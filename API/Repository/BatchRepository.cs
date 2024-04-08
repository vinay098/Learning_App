
using API.Context;
using API.DTOs.Batch_DTO;
using API.Interface;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class BatchRepository : IBatch
    {
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public BatchRepository(AppDbContext context, IMapper mapper, UserManager<User> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
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
                dto.StartDate = Convert.ToString(addedBatch.StartDate);
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
            var ans = _mapper.Map<List<Batch>, List<BatchDto>>(res);
            return ans;
        }
        public async Task<BatchDto> GetBatchDtoByIdAsync(int id)
        {
            var res = await _context.Batches.ToListAsync();
            var ans = _mapper.Map<List<Batch>, List<BatchDto>>(res);
            var res2 = ans.FirstOrDefault(x => x.Id == id);

            if (res2 == null)
            {
                throw new Exception("Value does not exist");
            }
            return res2;
        }
        public async Task UpdateBatchAsync(int id, BatchDto batch)
        {
            var updateBatch = await _context.Batches.FindAsync(id);
            if (updateBatch == null)
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

        public async Task<List<AssignBatchDisplay>> GetAssignedBatch()
        {
            var batches = await _context.Batches.ToListAsync();

            var res = new List<AssignBatchDisplay>();

            foreach (var val in batches)
            {
                var dto = new AssignBatchDisplay();
                dto.Batch_Id = val.Id;
                dto.Batch_Name = val.BatchName;
                dto.StartDate = val.StartDate.ToString();
                dto.EndDate = val.EndDate.ToString();
                dto.Capacity = val.Capacity;
                dto.Technology = val.Technology;
                var FacultyId = await _context.BatchFaculties
                                 .Where(b => b.BatchId == val.Id)
                                 .Select(b => b.UserId).ToListAsync();
                foreach (var id in FacultyId)
                {
                    var user = await _userManager.FindByIdAsync(id);
                    var name = await _userManager.GetUserNameAsync(user);
                    dto.Faculty.Add(name);
                }

                res.Add(dto);
            }

            return res;

        }

        public async Task<List<FacultyData>> FacultyDataByID(string id)
        {
            var Batch_Id = await _context.BatchFaculties
                            .Where(b => b.UserId == id)
                            .Select(b => new
                            {
                                b.Batch.Id,
                                b.Batch.BatchName,
                                b.Batch.StartDate,
                                b.Batch.EndDate,
                                b.Batch.Capacity,
                                b.Batch.Technology,
                            }).ToListAsync();

            var res = new List<FacultyData>();
            foreach (var val in Batch_Id)
            {
                var dto = new FacultyData();
                dto.Batch_Id = val.Id;
                dto.Batch_Name = val.BatchName;
                dto.Start = val.StartDate.ToString();
                dto.End = val.EndDate.ToString();
                dto.Capacity = val.Capacity;
                dto.Technology = val.Technology;
                res.Add(dto);
            }

            return res;
        }

        public async Task<List<EmployeeData>> GetEmployeeData()
        {
             var batches = await _context.Batches.ToListAsync();
             var res = new List<EmployeeData>();
             foreach(var val in batches)
             {
                var dto = new EmployeeData();
                dto.BatchId = val.Id;
                dto.BatchName = val.BatchName;
                dto.Start = val.StartDate.ToString();
                dto.End = val.EndDate.ToString();
                dto.Capacity = val.Capacity;
                dto.Technology = val.Technology;
                var module = await _context.BatchModules
                .Where(x=>x.BatchId==val.Id)
                .Select(x=>x.ModuleId)
                .ToListAsync();
                
                foreach(var id in module)
                {
                    var ans = await _context.Modules.FirstOrDefaultAsync(x=>x.Id==id);
                    dto.ModuleName.Add(ans.Module_Name);
                    dto.ModuleLevel.Add(ans.Proefficiency_level);
                }
                res.Add(dto);
             }

             return res;
        }
    }
}