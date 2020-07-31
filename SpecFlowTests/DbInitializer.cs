using Api.Data;
using Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpecFlowTests
{
    public static class DbInitializer
    {
        public static void SeedData(DataContext context)
        {
            var authors = context.Authors.ToList();
            if (authors.Count() == 0)
            {
                context.AddRange(GetAuthors());
                context.SaveChanges();
            }
        }

        private static IEnumerable<Author> GetAuthors() {
            return new List<Author>() {
                new Author
                {
                    FirstName = "William",
                    LastName = "Shakespeare",
                    Genre = "Drama"
                },
                new Author
                {
                    FirstName = "Stephen",
                    LastName = "King",
                    Genre = "Horror"
                },
                new Author()
                {
                    FirstName = "Douglas",
                    LastName = "Adams",
                    Genre = "Science fiction"
                }
            };
        } 

    }
}
