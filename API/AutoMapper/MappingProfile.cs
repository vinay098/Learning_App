using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.MapperDto;
using API.Models;
using AutoMapper;

namespace API.AutoMapper
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<SkillModule,SkillModuleValuesDto>()
            .ForMember(sm=>sm.SkillName,m=>m.MapFrom(sm=>sm.Skills.Skill_Name))
            .ForMember(sm=>sm.SkillFamily,m=>m.MapFrom(sm=>sm.Skills.Skill_Family))
            .ForMember(sm=>sm.ModuleName,m=>m.MapFrom(sm=>sm.Module.Module_Name))
            .ForMember(sm=>sm.Level,m=>m.MapFrom(sm=>sm.Module.Proefficiency_level))
            .ForMember(sm=>sm.Learning_Type,m=>m.MapFrom(sm=>sm.Module.Learning_Type))
            .ForMember(sm=>sm.Certification_Type,m=>m.MapFrom(sm=>sm.Module.Certification_Type));

            CreateMap<BatchModule,BatchModuleDisplayDto>()
            .ForMember(bm=>bm.BatchName,m=>m.MapFrom(bm=>bm.Batch.BatchName))
            .ForMember(bm=>bm.Capacity,m=>m.MapFrom(bm=>bm.Batch.Capacity))
            .ForMember(bm=>bm.Technology,m=>m.MapFrom(bm=>bm.Batch.Technology))
            .ForMember(bm=>bm.Batch_Start,m=>m.MapFrom(bm=>bm.Batch.StartDate.ToString()))
            .ForMember(bm=>bm.Batch_End,m=>m.MapFrom(bm=>bm.Batch.EndDate.ToString()))
            .ForMember(bm=>bm.ModuleName,m=>m.MapFrom(bm=>bm.Module.Module_Name))
            .ForMember(bm=>bm.Level,m=>m.MapFrom(bm=>bm.Module.Proefficiency_level))
            .ForMember(bm=>bm.Certification_Type,m=>m.MapFrom(bm=>bm.Module.Certification_Type));
            
        }
    }
}