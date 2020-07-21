using AutoMapper;
using MyBookStore.Dtos;
using MyBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            //Mapping Customer data
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>();

            //Mapping Movie data
            CreateMap<Movie, MovieDto>();
            CreateMap<MovieDto, Movie>();
            

        }
    }

