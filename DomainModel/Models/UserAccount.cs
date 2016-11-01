using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Models
{
    public class UserAccount
    {
        [Required]
        public virtual int Id { get; set; }
        
        [Required]
        public virtual string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public virtual string Password { get; set; }
    }
}
