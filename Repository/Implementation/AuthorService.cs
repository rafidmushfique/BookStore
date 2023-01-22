

using BookStore.Models.Domain;
using BookStore.Repository.Abstract;

namespace BookStore.Repository.Implementation
{
    public class AuthorService : IAuthorService
    {
        private readonly BSDatabaseContext dbc;

        public AuthorService(BSDatabaseContext dbc)
        {
            this.dbc = dbc;
        }

        public bool Add(author model)
        {
            try
            {
                dbc.Author.Add(model);
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
                if (data == null) return false;

                dbc.Author.Remove(data);
                dbc.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public author FindById(int id)
        {
            return dbc.Author.Find(id);
        }

        public IEnumerable<author> GetAll()
        {
            return dbc.Author.ToList();
        }

        public bool Update(author model)
        {
            try
            {
                dbc.Author.Update(model);
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
