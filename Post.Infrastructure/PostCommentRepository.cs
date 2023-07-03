using Post.Domain;
using Post.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Post.Infrastructure;

public class PostCommentRepository : IPostCommentRepository
{
    private readonly PostContext _context;

    public PostCommentRepository(PostContext context)
    {
        _context = context;
    }

    public PostCommentModel AddComment(PostCommentModel post)
    {
        return _context
            .Add(post)
            .Entity;
    }

    public Task<PostCommentModel?> GetAsync(int id)
    {
        return _context
            .Set<PostCommentModel>()
            .FirstOrDefaultAsync(d => d.id == id);
    }

    public Task<List<PostCommentModel>> GetCommentList(int PostId)
    {
        return _context
            .Set<PostCommentModel>()
            .Where(d => d.postId == PostId)
            .ToListAsync();
    }

    public void SaveChange()
    {
        _context.SaveChanges();
    }

    public PostCommentModel UpdateComment(PostCommentModel post)
    {
        throw new NotImplementedException();
    }
}