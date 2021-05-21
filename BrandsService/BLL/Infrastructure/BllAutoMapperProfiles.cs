﻿using AutoMapper;
using BLL.DTOs;
using DAL.Entities;

namespace BLL.Infrastructure
{
    public class BllAutoMapperProfiles : Profile
    {
        public BllAutoMapperProfiles()
        {
            CreateMap<Brand, BrandDto>().ReverseMap();
        }
    }
}