using BookStore.Models.Domain;

namespace BookStore.Repository.Abstract
{
    public interface IAuthorService
    {
        bool Add(author model);
        bool Update(author model);
        bool Delete(int id);
        author FindById(int id);
        IEnumerable<author> GetAll();
    }
}
