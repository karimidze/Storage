using DomainModel.Models;

namespace DomainModel.Repositories
{
    public interface IUserRepository
    {
        void Add(UserAccount item);
        int Find(string username, string password);
    }
}
