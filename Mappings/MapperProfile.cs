using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LocalRecipes.Dtos;
using LocalRecipes.Models;

namespace LocalRecipes.Mappings
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<RecipeCreateDto, Recipe>();
                // .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now)) 
                // .ForMember(dest => dest.Votes, opt => opt.MapFrom(src => 0));
            
        }
    }
}