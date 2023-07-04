using Moq;
using MvcNet;
using MvcNet.Controllers;
using Post.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Post.Infrastructure;

namespace Post.UnitTests;

public class ListBlog_UnitTest
{
    private readonly DbContextOptions<PostContext> _dbOptions;
    private readonly PostContext postContext;
    public ListBlog_UnitTest()
    {
        _dbOptions = new DbContextOptionsBuilder<PostContext>()
            .UseInMemoryDatabase(databaseName: "in-memory")
            .Options;
        this.postContext = new PostContext(_dbOptions);
        postContext.posts.AddRange(GetPosts());

        // postContext.SaveChanges();
    }

    private List<Post.Domain.Models.PostModel> GetPosts()
    {
        return new List<Domain.Models.PostModel>() {
            new Domain.Models.PostModel()
            {
                id = 1,
                comments = new List<Post.Domain.Models.PostCommentModel>(),
                title = "title 1",
                content = "ASD ASD ASD",
            },
            new Domain.Models.PostModel()
            {
                id = 2,
                comments = new List<Post.Domain.Models.PostCommentModel>(),
                title = "title 2",
                content = "ASD ASD ASD",
            },
        };
    }

    [Fact]
    public void ListBlog_ReturnList()
    {
        // var fakePostRepo = new Mock<IPostRepository>();
        // fakePostRepo
        //     .Setup(pr => pr.GetPostsAsync())
        //     .Returns(Task.FromResult(GetPosts()));
        var fakePostRepo = new PostRepository(postContext);

        // var fakeCommentRepo = new Mock<IPostCommentRepository>();
        var fakeCommentRepo = new PostCommentRepository(postContext);
        // fakeCommentRepo.Setup(cr => cr.AddComment(new Domain.Models.PostCommentModel()
        // {
        //     id = 1,
        //     postId = 1,
        //     name = "a",
        //     email = "abc@abc.com",
        //     content = "aaa",
        //     insertDate = DateTime.Now,
        //     publishDate = DateTime.Now
        // }));
        
        // fakeCommentRepo.Object.SaveChange();

        var homeCtrl = new HomeController(postRepository: fakePostRepo, commentRepository: fakeCommentRepo);

        // var res = homeCtrl.Blog(0);
        // var viewResult = Assert.IsType<ViewResult>(res);
        // Assert.IsAssignableFrom<List<Post.Domain.Models.PostModel>>(viewResult.ViewData.Model);
        // var list = (List<Post.Domain.Models.PostModel>)viewResult.ViewData.Model;
        // Assert.True(list.Count > 0);
    }

    [Fact]
    public void TestRepoAdd()
    {
        // Given
        var postRepo = new Post.Infrastructure.PostRepository(this.postContext);
        // When
        var data = postRepo.GetPostsAsync().Result;
        
        postRepo.AddPost(new Domain.Models.PostModel()
        {
            id = 5,
            comments = new List<Post.Domain.Models.PostCommentModel>(),
            title = "title 3",
            content = "ASD ASD ASD",
        });
        postRepo.SaveChange();
        var data2 = postRepo.GetPostsAsync().Result;

        // Then 
        Assert.True(data.Count < data2.Count);
    }
}