using DomainModel.Models;
using System;
using System.Collections.Generic;

namespace DomainModel.Repositories
{
    public interface IFileRepository
    {
        void Add(FileModel item);
        IEnumerable<FileModel> GetList(string orderby, string direction);
        IEnumerable<FileModel> RowList(); 
        void Delete(Guid idFile);
        FileModel Get(Guid idFile);

    }
}
