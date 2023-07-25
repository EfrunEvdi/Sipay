using AutoMapper;
using BookStore.DBOperations;
using BookStore.Entities;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.AuthorOperation.Queries.GetAuthors
{
    public class GetAuthorsQuery
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAuthorsQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<AuthorsViewModel> Handle()
        {
            var authors = _dbContext.Authors.Include(x => x.Book).OrderBy(x => x.AuthorID);
            List<AuthorsViewModel> viewModel = _mapper.Map<List<AuthorsViewModel>>(authors);
            return viewModel;
        }
    }

    public class AuthorsViewModel
    {
        public int AuthorID { get; set; }
        public string Book { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string DateOfBirth { get; set; }
    }
}

