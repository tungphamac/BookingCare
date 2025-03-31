using ServerApp.Models;

namespace ServerApp.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();
        void Add(Category category);
    }
}
