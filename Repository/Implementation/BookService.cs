using BookStore.Models.Domain;
using BookStore.Repository.Abstract;

namespace BookStore.Repository.Implementation
{
    public class BookService : IBookService
    {
        private readonly BSDatabaseContext dbc;

        public BookService(BSDatabaseContext dbc)
        {
            this.dbc = dbc;
        }
        public bool Add(Book model)
        {
            try
            {
                dbc.Book.Add(model);
                dbc.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var data = this.FindById(id);
                if (data == null)
                    return false;
                dbc.Book.Remove(data);
                dbc.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Book FindById(int id)
        {
            return dbc.Book.Find(id);
        }

        public IEnumerable<Book> GetAll()
        {
            var data = (from book in dbc.Book
                        join author in dbc.Author
                        on book.AuthorId equals author.id
                        join publisher in dbc.Publisher on book.PublisherId equals publisher.id
                        join genre in dbc.Genre on book.GenreId equals genre.id
                        select new Book
                        {
                            Id = book.Id,
                            AuthorId = book.AuthorId,
                            GenreId = book.GenreId,
                            Isbn = book.Isbn,
                            PublisherId = book.PublisherId,
                            Title = book.Title,
                            TotalPages = book.TotalPages,
                            GenreName = genre.name,
                            AuthorName = author.authorname,
                            PublisherName = publisher.publishername
                        }
                        ).ToList();
            return data;
        }

        public bool Update(Book model)
        {
            try
            {
                dbc.Book.Update(model);
                dbc.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
