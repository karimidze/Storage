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

        public IEnumerable<FileViewModel> Find(string attribute, string searchline, DateTime? dateattribute)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                if (searchline == "")
                {
                    if (dateattribute == null)
                    {
                        var items = session.CreateCriteria(typeof(FileViewModel)).List<FileViewModel>();
                        return items;
                    }
                    else
                    {
                        DateTime initDate = dateattribute.Value.Date;
                        DateTime endDate = dateattribute.Value.Date.AddDays(1).AddSeconds(-1);
                        var items = session
                        .CreateCriteria(typeof(FileViewModel))
                        .Add(Expression.Between("CreationDate", initDate, endDate))
                        .List<FileViewModel>();
                        return items;
                    }
                }

                else
                {
                    if (dateattribute == null)
                    {
                        var items = session
                        .CreateCriteria(typeof(FileViewModel))
                        .Add(Restrictions.Like(attribute, searchline, MatchMode.Anywhere))
                        .List<FileViewModel>();
                        return items;
                    }
                    else
                    {
                        DateTime initDate = dateattribute.Value.Date;
                        DateTime endDate = dateattribute.Value.Date.AddDays(1).AddSeconds(-1);
                        var items = session
                        .CreateCriteria(typeof(FileViewModel))
                        .Add(Expression.Between("CreationDate", initDate, endDate))
                        .Add(Restrictions.Like(attribute, searchline, MatchMode.Anywhere))
                        .List<FileViewModel>();
                        return items;
                    }
                }
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

        public IEnumerable<FileViewModel> RowList()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var items = session.CreateCriteria(typeof(FileViewModel)).List<FileViewModel>();

                return items;
            }
        }

        public IEnumerable<FileViewModel> GetList(string orderby, string direction)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var criteria = session.CreateCriteria(typeof(FileViewModel));

                if (orderby != null && direction != null)
                {
                    switch (orderby)
                    {
                        case "Title":
                            if (direction == "asc")
                            {
                                criteria.AddOrder(Order.Asc("Title"));
                            }
                            else
                            {
                                criteria.AddOrder(Order.Desc("Title"));
                            }
                            break;
                        case "CreationDate":
                            if (direction == "asc")
                            {
                                criteria.AddOrder(Order.Asc("CreationDate"));
                            }
                            else
                            {
                                criteria.AddOrder(Order.Desc("CreationDate"));
                            }
                            break;
                        case "CreationAuthor":
                            if (direction == "asc")
                            {
                                criteria.AddOrder(Order.Asc("CreationAuthor"));
                            }
                            else
                            {
                                criteria.AddOrder(Order.Desc("CreationAuthor"));
                            }
                            break;
                    }

                    }
                else
                {
                    criteria.AddOrder(Order.Asc("CreationDate"));
                }

                var items = criteria.List<FileViewModel>();

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
