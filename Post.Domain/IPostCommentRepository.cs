using Post.Domain.Models;

namespace Post.Domain;

public interface IPostCommentRepository
{
    public PostCommentModel AddComment(PostCommentModel post);
    public PostCommentModel UpdateComment(PostCommentModel post);

    public Task<PostCommentModel?> GetAsync(int id);
    public Task<List<PostCommentModel>> GetCommentList(int PostId);
    public void SaveChange(); 
}