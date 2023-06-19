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

        [Fact]
        public async void FindByIdAsync_ShouldReturnNull_WhenLikeDoesNotExist()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            var userId = 1;
            var postId = 1;

            // Act
            var result = await _likeRepository.FindByIdAsync(userId, postId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void FindAllByUserIdAsync_ShouldReturnListOfLikes_WhereLikesExists()
        {
            // Arangeet
            await _context.Database.EnsureDeletedAsync();

            int userId = 1;

            _context.User.Add(
                new User
                {
                    UserId = userId,
                });

            _context.Post.AddRange(
                new Post
                {
                    PostId = 1,
                    User = new()
                    {
                        UserId = 2,
                    }
                },
                new Post
                {
                    PostId = 2,
                    User = new()
                    {
                        UserId = 3,
                    }
                });

            _context.Like.AddRange(
                new Like
                {
                    UserId = userId,
                    PostId = 1,
                },
                new Like
                {
                    UserId = userId,
                    PostId = 2,
                });

            await _context.SaveChangesAsync();

            // Act
            var result = await _likeRepository.FindAllByUserIdAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Like>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void CreateAsync_ShouldFailToAddNewLike_WhenLikeIdAlreadyExists()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            Like like = new()
            {
                UserId = 1,
                PostId = 1,
            };

            var result = await _likeRepository.CreateAsync(like);

            // Act
            async Task action() => await _likeRepository.CreateAsync(like);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("An item with the same key has already been added", ex.Message);
        }

        [Fact]
        public async void DeleteAsync_ShouldReturnDeletedLike_WhenLikeIsDeleted()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            int userId = 1;
            int postId = 1;

            Like like = new()
            {
                UserId = userId,
                PostId = postId,
            };
            _context.Like.Add(like);

            await _context.SaveChangesAsync();

            // Act
            var result = await _likeRepository.DeleteAsync(userId, postId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Like>(result);
            Assert.Equal(userId, result?.UserId);
            Assert.Equal(postId, result?.PostId);
        }

        [Fact]
        public async void DeleteAsync_ShouldReturnNull_WhenLikeDoesNotExist()
        {
            // Arange
            await _context.Database.EnsureDeletedAsync();

            int userId = 1;
            int postId = 1;

            // Act
            var result = await _likeRepository.DeleteAsync(userId, postId);

            // Assert
            Assert.Null(result);
        }
    }
}
