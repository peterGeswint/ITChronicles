using Microsoft.AspNetCore.Identity;

namespace IT_Chronicles.Repositories
{
	public interface IUsersRepository
	{
		Task<IEnumerable<IdentityUser>> GetAllAsync();
	}
}
