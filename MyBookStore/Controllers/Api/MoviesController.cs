using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBookStore.Dtos;
using MyBookStore.Models;

namespace MyBookStore.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {

        private ConnectionClass _db;
        private readonly IMapper _mapper;

        public MoviesController(ConnectionClass db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        // api/Movies
        [HttpGet]
        public IEnumerable<MovieDto> GetMovies()
        {
            var movie = _db.Movies.ToList();
            var movieDto = _mapper.Map<List<MovieDto>>(movie);
            return movieDto;
        }
        //api/Movies/id
        [HttpGet("{Id}", Name = "GetMovies")]
        public MovieDto GetMovies(int id)
        {
            var movie = _db.Movies.SingleOrDefault(m => m.Id == id);
            var movieDto = _mapper.Map<MovieDto>(movie);

            if (movie == null)
                Response.StatusCode = 404;

            return movieDto;
        }
        
        //POST api/Movies
        [HttpPost]
        public MovieDto CreateMovie(MovieDto movieDto)
        {
            var movie = _mapper.Map<Movie>(movieDto);
            _db.Movies.Add(movie);
            _db.SaveChanges();

            movieDto.Id = movie.Id;
            return movieDto;
        }

        //PUT aapi/Movies/id

        [HttpPut]
        public void UpdateMovie(int id, MovieDto movieDto)
        {
            var movie = _db.Movies.SingleOrDefault(m => m.Id == id);

            //if(movie == null)
            //throw un exception
            _mapper.Map(movie, movieDto);
            //movieInDb.Name = movie.Name;
            //movieInDb.Genre = movie.Genre;
            //movieInDb.GenreId = movie.GenreId;
            //movieInDb.DateAdded = movie.DateAdded;
            //movieInDb.NumberInStock = movie.NumberInStock;
            //movieInDb.ReleaseDate = movie.ReleaseDate;

            _db.SaveChanges();
        }

        //DELETE api/Movies/id
        [HttpDelete]
        public void DeleteMovie(int id)
        {
            var movie = _db.Movies.SingleOrDefault(m => m.Id == id);

            // if (movie == null)
            //    throw Exception;
            _db.Movies.Remove(movie);
            _db.SaveChanges();

        }

    }
}