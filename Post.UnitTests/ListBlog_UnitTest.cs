using Moq;
using MvcNet.Controllers;
using Post.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Post.UnitTests;

public class ListBlog_UnitTest
{
    private List<Post.Domain.Models.PostModel> GetPosts()
    {
        return new List<Domain.Models.PostModel>() {(new Domain.Models.PostModel()
        {
            id = 1,
            comments = new List<Post.Domain.Models.PostCommentModel>(),
            title = "title 1",
            content = "ASD ASD ASD",
        })};
    }

    [Fact]
    public void ListBlog_ReturnList()
    {
        var fakePostRepo = new Mock<IPostRepository>();
        fakePostRepo
            .Setup(pr => pr.GetPostsAsync())
            .Returns(Task.FromResult(GetPosts()));

        var fakeCommentRepo = new Mock<IPostCommentRepository>();
        fakeCommentRepo.Setup(cr => cr.AddComment(new Domain.Models.PostCommentModel()
        {
            id = 1,
            postId = 1,
            name = "a",
            email = "abc@abc.com",
            content = "aaa",
            insertDate = DateTime.Now,
            publishDate = DateTime.Now
        }));
        
        fakeCommentRepo.Object.SaveChange();

        var homeCtrl = new HomeController(postRepository: fakePostRepo.Object, commentRepository: fakeCommentRepo.Object);

        var res = homeCtrl.Blog(0);
        var viewResult = Assert.IsType<ViewResult>(res);
        Assert.IsAssignableFrom<List<Post.Domain.Models.PostModel>>(viewResult.ViewData.Model);
    }
}