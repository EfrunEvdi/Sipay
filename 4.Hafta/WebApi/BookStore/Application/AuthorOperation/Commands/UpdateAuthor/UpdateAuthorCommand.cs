using BookStore.DBOperations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BookStore.Application.AuthorOperation.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        public int AuthorID { get; set; }
        public UpdateAuthorModel Model { get; set; }
        private readonly IBookStoreDbContext _dbContext;

        public UpdateAuthorCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var author = _dbContext.Authors.SingleOrDefault(x => x.AuthorID == AuthorID);
            if (author is null)
            {
                throw new InvalidOperationException("Yazar bulunamadı.");
            }

            author.Name = Model.Name == default ? author.Name : Model.Name;
            author.Surname = Model.Surname == default ? author.Surname : Model.Surname;
            author.DateOfBirth = Convert.ToDateTime(Model.DateOfBirth);

            _dbContext.Authors.Update(author);
            _dbContext.SaveChanges();
        }
    }

    public class UpdateAuthorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}