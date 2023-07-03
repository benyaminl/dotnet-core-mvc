using Post.Domain.Models;

namespace Post.Domain;
public interface IPostRepository
{
    public PostModel AddPost(PostModel post);
    public PostModel UpdatePost(PostModel post);

    public Task<PostModel?> GetAsync(int id);
    public Task<List<PostModel>> GetPostsAsync();
    public void SaveChange();
}
