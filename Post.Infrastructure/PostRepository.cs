using Microsoft.EntityFrameworkCore;
using Post.Domain.Models;
using Post.Domain;

namespace Post.Infrastructure;
public class PostRepository : IPostRepository
{
    private readonly DbContext _postRepository;
    public PostRepository(DbContext repository)
    {
        _postRepository = repository;
    }

    public PostModel AddPost(PostModel post)
    {
        return _postRepository
            .Set<PostModel>()
            .Add(post)
            .Entity;
    }

    public async Task<PostModel?> GetAsync(int id)
    {
        return await _postRepository
            .Set<PostModel>()
            .Where(d => d.id == id)
            .SingleOrDefaultAsync();
    }

    public async Task<List<PostModel>> GetPostsAsync()
    {
        return await _postRepository
            .Set<PostModel>()
            .ToListAsync();
    }

    public PostModel UpdatePost(PostModel post)
    {
        return _postRepository
            .Set<PostModel>()
            .Update(post)
            .Entity;
    }

    public async void SaveChange()
    {
        await _postRepository.SaveChangesAsync();
    }
}
