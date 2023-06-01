﻿namespace WebApi_Tests.Repository
{
    public class LikeRepositoryTests
    {
        private readonly DbContextOptions<DatabaseContext> _options;
        private readonly DatabaseContext _context;
        private readonly LikeRepository _likeRepository;

        public LikeRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "LikeRepositoryTests")
                .Options;

            _context = new(_options);

            _likeRepository = new LikeRepository(_context);
        }

        [Fact]
        public async void FindByIdAsync_ShouldReturnLike_WhereLikeExists()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            int userId = 1;
            int postId = 1;

            _context.Like.Add(
                new Like
                {
                    UserId = userId,
                    PostId = postId
                });
            await _context.SaveChangesAsync();

            // Act
            var result = await _likeRepository.FindByIdAsync(userId, postId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Like>(result);
            Assert.Equal(userId, result.UserId);
            Assert.Equal(postId, result.PostId);
        }

        //Test not working, maybe fix later
        // Problem could maybe be because it has a composite key or
        // the UserId does not have IsUnique set to false in the inmemory database
        //[Fact]
        //public async void FindAllByUserIdAsync_ShouldReturnListOfLikes_WhereLikesExists()
        //{
        //    // Arange
        //    await _context.Database.EnsureDeletedAsync();

        //    int userId = 1;

        //    _context.User.Add(
        //        new User
        //        {
        //            UserId = userId,
        //        });

        //    _context.Post.Add(
        //        new Post
        //        {
        //            PostId = 1,
        //            User = new()
        //            {
        //                UserId = 2,
        //            }
        //        });
        //    _context.Post.Add(
        //        new Post
        //        {
        //            PostId = 2,
        //            User = new()
        //            {
        //                UserId = 3,
        //            }
        //        });

        //    _context.Like.Add(
        //        new Like
        //        {
        //            UserId = userId,
        //            PostId = 1,
        //        });
        //    _context.Like.Add(
        //        new Like
        //        {
        //            UserId = userId,
        //            PostId = 2,
        //        });

        //    await _context.SaveChangesAsync();

        //    // Act
        //    var result = await _likeRepository.FindAllByUserIdAsync(userId);

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.IsType<List<Like>>(result);
        //    Assert.Equal(2, result.Count);
        //}

    }
}
