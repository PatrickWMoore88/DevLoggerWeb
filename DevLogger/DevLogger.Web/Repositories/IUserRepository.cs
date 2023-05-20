using Microsoft.AspNetCore.Identity;

namespace DevLogger.Web.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAll();
    }
}
