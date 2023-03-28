﻿namespace WebApi.Repository
{
    public interface IPostRepository
    {

        // POST
        Task<List<Post>> GetAllAsync();
        Task<Post?> GetPostByPostIdAsync(int PostId);
        Task<List<Post>> GetAllPostsByUserIdAsync(int UserId);
        Task<Post> CreatePostAsync(Post newPost);
        Task<Post> DeletePostAsync(int id);
        Task<Post> UpdatePostAsync(int id, Post updatePost);

        // POST UPDATE LIKES
        Task<Post> UpdatePostLikesAsync(int id, int like);

        // POSTTAGS
        Task<List<PostTag>> GetPostTagsByPostId(int postId);
        Task<PostTag> CreatePostTagAsync(PostTag newPostsTag);
        Task<PostTag> DeletePostTagAsync(PostTag newPostsTag);
        Task<PostTag> UpdatePostTagAsync(PostTag newPostsTag);
    }

    public class PostRepository : IPostRepository
    {
        private readonly DatabaseContext _context;

        public PostRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Post>> GetAllAsync()
        {
            return await _context.Post
                .Include(c => c.User)
                .Include(x => x.PostLikes)
                .OrderByDescending(d => d.Date)
                .ToListAsync();
        }

        public async Task<Post?> GetPostByPostIdAsync(int postId)
        {
            return await _context.Post
                .Include(c => c.User)
                .Include(x => x.PostLikes)
                .FirstOrDefaultAsync(x => postId == x.PostId);
        }

        public async Task<List<Post>> GetAllPostsByUserIdAsync(int userId)
        {
            return await _context.Post
                .Include(c => c.User)
                .Include(x => x.PostLikes)
                .Where(x => userId == x.UserId)
                .ToListAsync();
        }

        public async Task<Post> CreatePostAsync(Post newPost)
        {
            _context.Post.Add(newPost);
            await _context.SaveChangesAsync();
            return newPost;
        }

        public async Task<Post> UpdatePostAsync(int id, Post updatePost)
        {
            var post = await GetPostByPostIdAsync(id);

            if (post != null)
            {
                post.Title = updatePost.Title;
                post.Desc = updatePost.Desc;

                _context.Update(post);
                await _context.SaveChangesAsync();
            }

            return post;
        }

        public async Task<Post> DeletePostAsync(int id)
        {
            var post = await GetPostByPostIdAsync(id);

            if (post != null)
            {
                _context.Remove(post);
                await _context.SaveChangesAsync();
            }
            return post;
        }

        // For updating each post that has/needs a like from likeService
        public async Task<Post> UpdatePostLikesAsync(int id, int like)
        {
            var post = await GetPostByPostIdAsync(id);

            if (post != null)
            {
                post.PostLikes.Likes += like;

                _context.Update(post);
                await _context.SaveChangesAsync();
            }

            return post;
        }

        public async Task<List<PostTag>> GetPostTagsByPostId(int postId)
        {
            return await _context.PostTag
                .Include(p => p.Post)
                .Include(t => t.Tag)
                .Where(p => p.PostId == postId)
                .Select(p => p)
                .ToListAsync();
        }

        public async Task<PostTag> CreatePostTagAsync(PostTag postTag)
        {
            _context.PostTag.Add(postTag);
            await _context.SaveChangesAsync();
            return postTag;
        }

        public async Task<PostTag> UpdatePostTagAsync(PostTag postsTag)
        {
            var postag2 = from posttag in _context.PostTag
                          where posttag.PostId == postsTag.PostId
                          where posttag.TagId != postsTag.TagId
                          select posttag;

            var postag = await _context.PostTag
                .Where(x => x.PostId == postsTag.PostId)
                .Where(x => x.TagId == postsTag.TagId)
                .Select(x => x)
                .ToListAsync();

            _context.PostTag.Add(postsTag);
            await _context.SaveChangesAsync();
            return postsTag;
        }

        public async Task<PostTag> DeletePostTagAsync(PostTag postsTag)
        {
            _context.Remove(postsTag);
            _context.PostTag.Remove(postsTag);
            await _context.SaveChangesAsync();
            return postsTag;
        }
    }
}
