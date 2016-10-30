using DomainModel.Helpers;
using DomainModel.Models;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DomainModel.Repositories
{

    public class FileRepository : IFileRepository
    {
        public void Add(FileCreateModel item)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(item);
                    transaction.Commit();
                }
            }
        }

        public IEnumerable<FileViewModel> Find(string attribute, string searchline)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var items = session
                    .CreateCriteria(typeof(FileViewModel))
                    .Add(Restrictions.Like(attribute, searchline, MatchMode.Anywhere))
                    .List<FileViewModel>();

                return items;
            }
        }

        public FileCreateModel Get(Guid idFile)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var criteria = session.CreateCriteria(typeof(FileCreateModel));

                criteria.Add(Restrictions.Where<FileCreateModel>(f => f.IdFile == idFile));

                return criteria.UniqueResult<FileCreateModel>();
            }
        }

        public IEnumerable<FileViewModel> GetList()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var items = session.CreateCriteria(typeof(FileViewModel)).List<FileViewModel>();
                return items;
            }
        }

        public void Delete(Guid idFile)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var criteria = session.CreateCriteria(typeof(FileViewModel));

                    criteria.Add(Restrictions.Where<FileViewModel>(f => f.IdFile == idFile));

                    var files = criteria.List<FileViewModel>();
                    try
                    {
                        session.Delete(files.FirstOrDefault());
                    }
                    catch (System.Exception)
                    {
                        transaction.Rollback();
                    }
                    transaction.Commit();
                }
            }
        }


    }
}
