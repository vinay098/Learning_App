using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;
using API.DTOs.Batch_DTO;
using API.DTOs.Course_DTO;
using API.DTOs.MapperDto;
using API.DTOs.Module_DTO;
using API.DTOs.Skills_DTO;
using API.Models;
using AutoMapper;

namespace API.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Batch, BatchDto>()
            .ForMember(dest => dest.Id, m => m.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, m => m.MapFrom(src => src.BatchName))
            .ForMember(dest => dest.StartDate, m => m.MapFrom(src => src.StartDate.ToString()))
            .ForMember(dest => dest.EndDate, m => m.MapFrom(src => src.EndDate.ToString()))
            .ForMember(dest => dest.Capacity, m => m.MapFrom(src => src.Capacity))
            .ForMember(dest => dest.Technology, m => m.MapFrom(src => src.Technology));

            CreateMap<LearnModule,ModuleDto>()
             .ForMember(dest=>dest.Name,m=>m.MapFrom(src=>src.Module_Name))
             .ForMember(dest=>dest.Level,m=>m.MapFrom(src=>src.Proefficiency_level));

            CreateMap<Skills,SkillDto>()
            .ForMember(dest=>dest.Name,m=>m.MapFrom(src=>src.Skill_Name))
            .ForMember(dest=>dest.Family,m=>m.MapFrom(src=>src.Skill_Family));

            CreateMap<BatchFaculty,FacultyData>()
            .ForMember(dest=>dest.Batch_Id,m=>m.MapFrom(src=>src.BatchId))
            .ForMember(dest=>dest.Batch_Name,m=>m.MapFrom(src=>src.Batch.BatchName))
            .ForMember(dest=>dest.Capacity,m=>m.MapFrom(src=>src.Batch.Capacity))
            .ForMember(dest=>dest.Start,m=>m.MapFrom(src=>src.Batch.StartDate.ToString()))
            .ForMember(dest=>dest.End,m=>m.MapFrom(src=>src.Batch.EndDate.ToString()))
            .ForMember(dest=>dest.Technology,m=>m.MapFrom(src=>src.Batch.Technology));

            
        }
    }
}