using Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class AuthorCollection
    {
        public ICollection<Author> Authors { get; set; }
    }
}
