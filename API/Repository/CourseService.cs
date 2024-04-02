
using API.Context;
using API.DTOs;
using API.DTOs.Course_DTO;
using API.Interface;
using API.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class CourseService : ICourse
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly IMapper _mapper;

        public CourseService(ApplicationDbContext context
        , IWebHostEnvironment environment, IMapper mapper)
        {
            _context = context;
            _environment = environment;
            _mapper = mapper;
        }

        public async Task<Course> AddCourseAsync(CourseDto obj)
        {
            var batch_Id = await _context.Batches
            .Where(b => b.BatchName == obj.BatchName)
            .Select(b => b.Id).FirstAsync();

            var add_to_course_model = new Course()
            {
                Name = obj.CourseName,
                Description = obj.CourseDescription,
                BatchId = batch_Id,
            };

            _context.Courses.Add(add_to_course_model);
            await _context.SaveChangesAsync();
            return add_to_course_model;
        }



        public async Task<Course> AddCoursewithImage(string User_Id, CourseCreateDto model)
        {
            // Image Handling:
            if (model.Image != null)
            {
                var imagePath = SaveImage(model.Image); // Save image to storage
                model.image_path = imagePath;

            }

            // Create Course:
            var course = new Course
            {
                Name = model.Name,
                Description = model.Description,
                ImageUrl = model.image_path,
                BatchId = model.Batch_Id,
                UserId = User_Id,
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return course;
        }

        private string SaveImage(IFormFile image)
        {
            var contentPath = _environment.ContentRootPath;
            var path = Path.Combine(contentPath, "Uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var ext = Path.GetExtension(image.FileName);
            var allowedExtensions = new string[] { ",jpg", ".png", ".jpeg" };
            if (!allowedExtensions.Contains(ext))
            {
                string msg = string.Format("only {0} extension are allowed", string.Join(",", allowedExtensions));
                return msg;
            }
            string uniqueString = Guid.NewGuid().ToString();
            //to create a unique fileName
            var newFileName = uniqueString + ext;
            var fileWithPath = Path.Combine(path, newFileName);
            var stream = new FileStream(fileWithPath, FileMode.Create);
            image.CopyTo(stream);
            stream.Close();
            return newFileName;
        }

        private string GetImage(string fileName)
        {
            var contentPath = _environment.ContentRootPath;
            var path = Path.Combine(contentPath, "Uploads", fileName);
            if (System.IO.File.Exists(path))
            {
                return path;
            }
            else
            {
                return null; 
            }
           
        }


        public async Task AddCourseSkillAsync(CourseSkillDto obj)
        {
            var course_id = await _context.Courses
            .Where(c => c.Name == obj.CourseName)
            .Select(c => c.Id).FirstAsync();

            var skill_id = await _context.Skills
            .Where(s => s.Skill_Name == obj.SkillName)
            .Select(s => s.Id).FirstAsync();

            var addval = new CourseSkills()
            {
                CourseId = course_id,
                SkillId = skill_id
            };

            _context.CourseSkills.Add(addval);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCource(Course obj)
        {
            _context.Courses.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<CourseDisplayDto>> GetAllAsync()
        {
            var courses = await _context.Courses.ToListAsync();
            var addedData = new List<CourseDisplayDto>();
            // var res = new List<string>();
            foreach (var val in courses)
            {
                var dto = new CourseDisplayDto();
                dto.CourseId = val.Id;
                dto.CourseName = val.Name;
                dto.CourseDescription = val.Description;

                var get_Batch = await _context.Batches
                .Where(x => x.Id == Convert.ToInt32(val.BatchId))
                .Select(x => x.BatchName).ToListAsync();

                foreach (var batch in get_Batch)
                {
                    dto.BatchName = batch;
                }

                var names = await GetAllSkillName(val.Id);
                foreach (var name in names)
                {
                    dto.AddSkill(name);
                }

                addedData.Add(dto);

            }
            return addedData;
        }

        public async Task<IList<string>> GetAllSkillName(int course_id)
        {
            var ans = await _context.CourseSkills
                  .Where(c => c.CourseId == course_id)
                  .Select(s => s.Skills.Skill_Name).ToListAsync();

            return ans;
        }

        public async Task<Course> GetById(int id)
        {
            var res = await _context.Courses.FindAsync(id);
            return res;
        }

        public async Task<CourseDisplayDto> GetCourseDisplayDtoById(int id)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
            {
                throw new Exception("Course Does Not Exist");
            }

            var batch_name = await _context.Batches
            .Where(x => x.Id == Convert.ToInt32(course.BatchId))
            .Select(x => x.BatchName).FirstAsync();


            var valuetodisplay = new CourseDisplayDto
            {
                CourseId = course.Id,
                CourseName = course.Name,
                CourseDescription = course.Description,
                BatchName = batch_name,
            };
            var names = await GetAllSkillName(valuetodisplay.CourseId);
            foreach (var name in names)
            {
                valuetodisplay.AddSkill(name);
            }
            return valuetodisplay;
        }

        public async Task UpdateCourse(int id, CourseDisplayDto obj)
        {
            var course = await _context.Courses.FindAsync(id);

            var batch_id = await _context.Batches
            .Where(x => x.BatchName == obj.BatchName)
            .Select(x => x.Id).FirstAsync();


            if (course == null)
            {
                throw new Exception("Course Does not exist");
            }
            course.Name = obj.CourseName;
            course.Description = obj.CourseDescription;
            course.BatchId = batch_id;
            await _context.SaveChangesAsync();
        }

        public async Task<List<FacultyCourseDto>> GetCourseByFacultyId(string user_id)
        {
            var courses = await _context.Courses
                        .Where(c => c.UserId == user_id)
                        .ToListAsync();

            var response = new List<FacultyCourseDto>();
            foreach (var val in courses)
            {
                var dto = new FacultyCourseDto();
                dto.CourseId = val.Id;
                dto.CourseName = val.Name;
                dto.CourseDescription = val.Description;
                dto.BatchName = await _context.Batches
                                .Where(b => b.Id == val.BatchId)
                                .Select(b => b.BatchName).FirstAsync();
                dto.CourseImage = val.ImageUrl;
                response.Add(dto);
            }

            return response;
        }


    }
}