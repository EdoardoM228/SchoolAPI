using System;
using APISolution.Dtos;
using APISolution.Models;
using AutoMapper;

namespace APISolution.Profiles
{
    public class ClassmatesProfile : Profile
    {
        public ClassmatesProfile()
        {
            //Source => target 
            CreateMap<Classmate, ClassmateReadDto>();
            CreateMap<ClassmateCreatDto, Classmate>();
            CreateMap<ClassmateUpdateDto, Classmate>();
            CreateMap<Classmate, ClassmateUpdateDto>();
        }
    }
}

