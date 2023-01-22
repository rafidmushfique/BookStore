using BookStore.Models.Domain;
using BookStore.Repository.Abstract;

namespace BookStore.Repository.Implementation
{
    public class PublisherService : IPublisherService
    {
        private readonly BSDatabaseContext dbc;

        public PublisherService(BSDatabaseContext dbc)
        {
            this.dbc = dbc;
        }
        public bool Add(Publisher model)
        {
            try { 
               dbc.Publisher.Add(model);
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

                dbc.Publisher.Remove(data);
                dbc.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Publisher FindById(int id)
        {
            return dbc.Publisher.Find(id);
        }

        public IEnumerable<Publisher> GetAll()
        {
            return dbc.Publisher.ToList();
        }

        public bool Update(Publisher model)
        {
            try
            {
                dbc.Publisher.Update(model);
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
