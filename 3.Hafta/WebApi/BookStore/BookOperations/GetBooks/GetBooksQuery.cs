using AutoMapper;
using BookStore.Common;
using BookStore.DBOperations;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetBooksQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<BooksViewModel> Handle()
        {
            var bookList = _context.Books.OrderBy(x => x.BookID).ToList<Book>();
            List<BooksViewModel> viewModel = _mapper.Map<List<BooksViewModel>>(bookList); // new List<BooksViewModel>();
            //foreach (var book in bookList)
            //{
            //    viewModel.Add(new BooksViewModel()
            //    {
            //        Title = book.Title,
            //        Genre = ((GenreEnum)book.GenreID).ToString(),
            //        PageCount = book.PageCount,
            //        PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy"),
            //    });
            //}
            return viewModel;
        }
    }

    public class BooksViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
    }
}
