using Microsoft.EntityFrameworkCore;
using ServerApp.Data;
using ServerApp.Models;
using ServerApp.ViewModels;

namespace ServerApp.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDbContext _context;

        public PostRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PostVm> AddPostAsync(PostVm postVm)
        {
            var post = new Post
            {
                Title = postVm.Title,
                ShortDescription = postVm.ShortDescription,
                Content = postVm.Content,
                FeaturedImageUrl = postVm.FeaturedImageUrl,
                UrlHandle = postVm.UrlHandle,
                PublishedDate = postVm.PublishedDate,
                Author = postVm.Author,
                IsVisible = postVm.IsVisible,
                CategoryId = postVm.CategoryId
            };
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            postVm.Id = post.Id;
            return postVm;
        }

        public async Task<bool> DeletePostAsync(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null) return false;
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<PostVm>> GetAllPostsAsync()
        {
            return await _context.Posts.Include(p => p.Category)
            .Select(p => new PostVm
            {
                Id = p.Id,
                Title = p.Title,
                ShortDescription = p.ShortDescription,
                Content = p.Content,
                FeaturedImageUrl = p.FeaturedImageUrl,
                UrlHandle = p.UrlHandle,
                PublishedDate = p.PublishedDate,
                Author = p.Author,
                IsVisible = p.IsVisible,
                CategoryId = p.CategoryId,
                CategoryName = p.Category.Name
            }).ToListAsync();
        }

        public async Task<PostVm> GetPostByIdAsync(int id)
        {
            var post = await _context.Posts.Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
            if (post == null) return null;
            return new PostVm
            {
                Id = post.Id,
                Title = post.Title,
                ShortDescription = post.ShortDescription,
                Content = post.Content,
                FeaturedImageUrl = post.FeaturedImageUrl,
                UrlHandle = post.UrlHandle,
                PublishedDate = post.PublishedDate,
                Author = post.Author,
                IsVisible = post.IsVisible,
                CategoryId = post.CategoryId,
                CategoryName = post.Category.Name
            };
        }

        public async Task<PostVm> UpdatePostAsync(int id, PostVm postVm)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null) return null;
            post.Title = postVm.Title;
            post.ShortDescription = postVm.ShortDescription;
            post.Content = postVm.Content;
            post.FeaturedImageUrl = postVm.FeaturedImageUrl;
            post.UrlHandle = postVm.UrlHandle;
            post.PublishedDate = postVm.PublishedDate;
            post.Author = postVm.Author;
            post.IsVisible = postVm.IsVisible;
            post.CategoryId = postVm.CategoryId;
            await _context.SaveChangesAsync();
            return postVm;
        }
    }
}
