﻿namespace WebApi.Repository
{

    public interface ILoginRepository
    {
        Task<Login> RegisterAsync(Login newUser);
        Task<List<Login>> GetAllLoginAsync();
        Task<Login?> FindLoginByIdAsync(int loginId);
        Task<Login?> FindLoginByEmailAsync(string email);
        Task<Login?> UpdateLoginById(int loginId, Login updatedLogin);
        Task<Login?> DeleteLoginByIdAsync(int loginId);
    }

    public class LoginRepository : ILoginRepository
    {
        private readonly DatabaseContext _context;

        public LoginRepository(DatabaseContext context) { _context = context; }

        public async Task<Login> RegisterAsync(Login newUser)
        {
            if (_context.User.Any(u => u.Login.Email.ToLower() == newUser.Email.ToLower()))
            {
                throw new Exception(string.Format("User already exists", newUser.Email));
            }

            _context.Login.Add(newUser);
            await _context.SaveChangesAsync();
            return newUser;
        }

        public async Task<List<Login>> GetAllLoginAsync()
        {
            return await _context.Login
                .Include(login => login.User)
                .Include(posts => posts.User.Posts.OrderByDescending(Post => Post.Date))
                .ThenInclude(post => post.PostLikes)
                .ToListAsync();
        }

        public async Task<Login?> FindLoginByIdAsync(int loginId)
        {
            return await _context.Login
                .Include(login => login.User)
                .Include(posts => posts.User.Posts)
                .ThenInclude(post => post.PostLikes)
                .FirstOrDefaultAsync(login => login.LoginId == loginId);
        }

        public async Task<Login?> FindLoginByEmailAsync(string email)
        {
            return await _context.Login
                .Include(login => login.User)
                .Include(posts => posts.User.Posts.OrderByDescending(post => post.Date))
                .ThenInclude(post => post.PostLikes)
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<Login?> UpdateLoginById(int loginId, Login updatedLogin)
        {
            var login = await FindLoginByIdAsync(loginId);

            if (login != null)
            {
                login.Role = updatedLogin.Role;
                login.Email = updatedLogin.Email;
                login.Password = updatedLogin.Password;

                await _context.SaveChangesAsync();
            }
            return login;
        }

        public async Task<Login?> DeleteLoginByIdAsync(int loginId)
        {
            var login = await FindLoginByIdAsync(loginId);

            if (login != null)
            {
                _context.Remove(login);
                await _context.SaveChangesAsync();
            }

            return login;
        }
    }
}
