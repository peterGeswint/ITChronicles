using IT_Chronicles.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IT_Chronicles.Repositories
{
	public class UserRepository : IUsersRepository
	{
		private readonly AuthDbContext authDbContext;

		public UserRepository(AuthDbContext authDbContext)
        {
			this.authDbContext = authDbContext;
		}

        public async Task<IEnumerable<IdentityUser>> GetAllAsync()
		{
			var users = await authDbContext.Users.ToListAsync();

			var superAdminUser = await authDbContext.Users.FirstOrDefaultAsync(x => x.Email == "superadminuser@itchronicles.com");
			if(superAdminUser is not null)
			{
				users.Remove(superAdminUser);
			}

			return users;
		}
	}
}
