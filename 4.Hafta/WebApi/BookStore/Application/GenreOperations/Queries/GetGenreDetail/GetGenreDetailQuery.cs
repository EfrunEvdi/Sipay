using AutoMapper;
using BookStore.DBOperations;
using System;
using System.Linq;

namespace BookStore.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQuery
    {
        public int GenreID { get; set; }
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetGenreDetailQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public GenreDetailViewModel Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x => x.IsActive && x.GenreID == GenreID);
            if (genre is null)
            {
                throw new InvalidOperationException("Kategori bulunamadı.");
            }
            GenreDetailViewModel viewModel = _mapper.Map<GenreDetailViewModel>(genre);

            return viewModel;
        }
    }

    public class GenreDetailViewModel
    {
        public int GenreID { get; set; }
        public string Name { get; set; }
    }
}
