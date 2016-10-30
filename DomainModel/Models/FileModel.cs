using System;
using System.Collections.Generic;

namespace DomainModel.Models
{

    public class FileViewModel
    {
        public virtual int Id { get; set; }
        public virtual Guid IdFile { get; set; }

        public virtual string Title { get; set; }

        public virtual string CreationAuthor { get; set; }

        public virtual DateTime CreationDate { get; set; }
    }

    public class FileCreateModel
    {
        public virtual int Id { get; set; }

        public virtual Guid IdFile { get; set; }
        
        public virtual string Title { get; set; }

        public virtual byte[] FileData { get; set; }

        public virtual string CreationAuthor { get; set; }

        public virtual DateTime CreationDate { get; set; }

    }
}
