using AutoMapper;
using BookStore.Common;
using BookStore.DBOperations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BookStore.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQuery
    {
        public int BookID { get; set; }
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetBookDetailQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public BookDetailViewModel Handle()
        {
            var book = _context.Books.Include(x => x.Genre).Where(book => book.BookID == BookID).SingleOrDefault();
            if (book is null)
            {
                throw new InvalidOperationException("Kitap bulunamadı.");
            }
            BookDetailViewModel viewModel = _mapper.Map<BookDetailViewModel>(book);

            return viewModel;
        }
    }

    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
    }
}
