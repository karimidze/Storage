using DomainModel.Models;
using System;
using System.Collections.Generic;

namespace DomainModel.Repositories
{
    public interface IFileRepository
    {
        void Add(FileCreateModel item);
        IEnumerable<FileViewModel> GetList();
        void Delete(Guid idFile);
        FileCreateModel Get(Guid idFile);

    }
}
