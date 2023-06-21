﻿namespace WebApi.DAL.Repository
{
    public interface ITagRepository
    {
        Task<List<Tag>> GetAllAsync();
        Task<List<Tag>?> FindAllByPostIdAsync(int postId);
        Task<Tag?> FindByIdAsync(int tagId);
        Task<Tag?> FindByNameAsync(string tagName);
        Task<Tag?> CreateAsync(Tag newTag);
    }

    public class TagRepository : ITagRepository
    {
        private readonly DatabaseContext _context;
        public TagRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Tag>> GetAllAsync()
        {
            return await _context.Tag.ToListAsync();
        }

        public async Task<List<Tag>?> FindAllByPostIdAsync(int postId)
        {
            return await _context.PostTag
                .Include(p => p.Post)
                .Include(t => t.Tag)
                .Where(x => x.PostId == postId)
                .Select(x => x.Tag)
                .ToListAsync();
        }

        public async Task<Tag?> FindByIdAsync(int id)
        {
            return await _context.Tag.FindAsync(id);
        }

        public async Task<Tag?> FindByNameAsync(string tagName)
        {
            return await _context.Tag
                .FirstOrDefaultAsync(x => x.Name == tagName);
        }

        public async Task<Tag?> CreateAsync(Tag newTag)
        {
            _context.Tag.Add(newTag);
            await _context.SaveChangesAsync();

            // Returns the tag with the id that was just created
            return await FindByIdAsync(newTag.TagId);
        }
    }
}
