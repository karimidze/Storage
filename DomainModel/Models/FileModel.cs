using System;
using System.Collections.Generic;

namespace DomainModel.Models
{

    public class FileModel
    {
        public virtual int Id { get; set; }
        public virtual Guid IdFile { get; set; }

        public virtual string Title { get; set; }

        public virtual byte[] FileData { get; set; }

        public virtual string CreationAuthor { get; set; }

        public virtual DateTime CreationDate { get; set; }
    }
}
