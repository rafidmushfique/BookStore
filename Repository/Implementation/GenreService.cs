using BookStore.Models.Domain;
using BookStore.Repository.Abstract;

namespace BookStore.Repository.Implementation
{
    public class GenreService : IGenreService
    {
        private readonly BSDatabaseContext dbc;

        public GenreService(BSDatabaseContext dbc)
        {
            this.dbc = dbc;
        }
        public bool Add(Genre model)
        {
            try { 
               dbc.Genre.Add(model);
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
                var data=this.FindById(id);
                if(data==null) return false;

                dbc.Genre.Remove(data);
                dbc.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Genre FindById(int id)
        {
            return dbc.Genre.Find(id);
        }

        public IEnumerable<Genre> GetAll()
        {
            return dbc.Genre.ToList();
        }

        public bool Update(Genre model)
        {
            try
            {
                dbc.Genre.Update(model);
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
