using ServerApp.ViewModels;

namespace ServerApp.Repositories
{
    public interface IPostRepository
    {
        Task<IEnumerable<PostVm>> GetAllPostsAsync();
        Task<PostVm> GetPostByIdAsync(int id);
        Task<PostVm> AddPostAsync(PostVm postVm);
        Task<PostVm> UpdatePostAsync(int id, PostVm postVm);
        Task<bool> DeletePostAsync(int id);
    }
}
