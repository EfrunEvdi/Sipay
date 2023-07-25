using AutoMapper;
using BookStore.DBOperations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BookStore.Application.AuthorOperation.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQuery
    {
        public int AuthorID { get; set; }
        public readonly IBookStoreDbContext _context;
        public readonly IMapper _mapper;

        public GetAuthorDetailQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public AuthorsDetailViewModel Handle()
        {
            var author = _context.Authors.Include(x => x.Book).SingleOrDefault(x => x.AuthorID == AuthorID);
            if (author is null)
            {
                throw new InvalidOperationException("Yazar Bulunamadı.");
            }
            return _mapper.Map<AuthorsDetailViewModel>(author);
        }
    }

    public class AuthorsDetailViewModel
    {
        public int AuthorID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Book { get; set; }
    }
}