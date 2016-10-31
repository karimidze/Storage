using DomainModel.Helpers;
using DomainModel.Models;
using NHibernate;
using NHibernate.Criterion;

namespace DomainModel.Repositories
{
    public class UserRepository : IUserRepository
    {

        public int PreventDuplicate(string username)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var items = session
                    .CreateCriteria(typeof(UserAccount))
                    .Add(Restrictions.Eq("Username", username))
                    .List<UserAccount>();

                return items.Count;
            }
        }

        public void Add(UserAccount item)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Save(item);
                    }
                    catch (System.Exception)
                    {
                    
                        transaction.Rollback();
                    }
                    transaction.Commit();
                }
            }
        }

        public int Find(string username, string password)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var items = session
                    .CreateCriteria(typeof(UserAccount))
                    .Add(Restrictions.Eq("Username", username))
                    .Add(Restrictions.Eq("Password", password))
                    .List<UserAccount>();

                return items.Count;
            }
        }
    }
}
