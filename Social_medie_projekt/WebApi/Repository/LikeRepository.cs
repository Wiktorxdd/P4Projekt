﻿namespace WebApi.Repository
{
    public interface ILikeRepository
    {
        Task<Like?> FindLikeAsync(int userId, int postId);
        Task<List<Like>?> GetAllLikesFromUserAsync(int userId);
        Task<Like> CreateLikeAsync(Like newLike);
        Task<Like> DeleteLikeAsync(int userId, int postId);
    }

    public class LikeRepository : ILikeRepository
    {
        private readonly DatabaseContext _context;

        public LikeRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Like?> FindLikeAsync(int userId, int postId)
        {
            return await _context.Like
                .Where(x => userId == x.UserId)
                .Where(y => postId == y.PostId)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Like>?> GetAllLikesFromUserAsync(int userId)
        {
            return await _context.Like
                .Include(c => c.User)
                .Where(x => userId == x.UserId)
                .ToListAsync();
        }


        public async Task<Like> CreateLikeAsync(Like like)
        {
            if (await FindLikeAsync(like.UserId, like.PostId) != null)
            {
                throw new Exception("Post aldready liked");
            }

            _context.Like.Add(like);
            await _context.SaveChangesAsync();
            return like;
        }

        public async Task<Like> DeleteLikeAsync(int userId, int postId)
        {
            var like = await FindLikeAsync(userId, postId);

            if (like != null)
            {
                _context.Remove(like);
                await _context.SaveChangesAsync();
            }

            return like;
        }
    }
}
