using AutoMapper;
using BookStore.DBOperations;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Application.GenreOperations.Queries.GetGenres
{
    public class GetGenresQuery
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetGenresQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<GenresViewModel> Handle()
        {
            var genreList = _context.Genres.Where(x => x.IsActive).OrderBy(x => x.GenreID).ToList();
            List<GenresViewModel> viewModel = _mapper.Map<List<GenresViewModel>>(genreList);

            return viewModel;
        }
    }

    public class GenresViewModel
    {
        public int GenreID { get; set; }
        public string Name { get; set; }
    }
}
