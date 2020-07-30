using Api.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData(
                new Author
                {
                    Id = 1,
                    FirstName = "William",
                    LastName = "Shakespeare",
                    Genre = "Drama"
                },
                new Author
                {
                    Id = 2,
                    FirstName = "Stephen",
                    LastName = "King",
                    Genre = "Horror"
                },
                new Author()
                {
                    Id = 3,
                    FirstName = "Douglas",
                    LastName = "Adams",
                    Genre = "Science fiction"
                }
            );
            modelBuilder.Entity<Book>().HasData(
                new Book { 
                    Id = 1, 
                    AuthorId = 1, 
                    Title = "Hamlet", 
                    Description = "Tragedy in five acts by William Shakespeare, written about 1599–1601 and published in a quarto edition in 1603 from an unauthorized text." },
                new Book { 
                    Id = 2, 
                    AuthorId = 1, 
                    Title = "King Lear",
                    Description = "King Lear is a tragedy written by William Shakespeare.It tells the tale of a king who bequeaths his power and land to two of his three daughters, after they declare their love for him in an extremely fawning and obsequious manner."
                },
                new Book { 
                    Id = 3, 
                    AuthorId = 1, 
                    Title = "Othello",
                    Description = "The story of an African general in the Venetian army who is tricked into suspecting his wife of adultery"
                },
                new Book()
                {
                    Id = 4,
                    AuthorId = 2,
                    Title = "The Shining",
                    Description = "The Shining is a horror novel by American author Stephen King. Published in 1977, it is King's third published novel and first hardback bestseller: the success of the book firmly established King as a preeminent author in the horror genre. "
                },
                new Book()
                {
                    Id = 5,
                    AuthorId = 2,
                    Title = "Misery",
                    Description = "Misery is a 1987 psychological horror novel by Stephen King. This novel was nominated for the World Fantasy Award for Best Novel in 1988, and was later made into a Hollywood film and an off-Broadway play of the same name."
                },
                new Book()
                {
                    Id = 6,
                    AuthorId = 2,
                    Title = "It",
                    Description = "It is a 1986 horror novel by American author Stephen King. The story follows the exploits of seven children as they are terrorized by the eponymous being, which exploits the fears and phobias of its victims in order to disguise itself while hunting its prey. 'It' primarily appears in the form of a clown in order to attract its preferred prey of young children."
                },
                new Book()
                {
                    Id = 7,
                    AuthorId = 2,
                    Title = "The Stand",
                    Description = "The Stand is a post-apocalyptic horror/fantasy novel by American author Stephen King. It expands upon the scenario of his earlier short story 'Night Surf' and outlines the total breakdown of society after the accidental release of a strain of influenza that had been modified for biological warfare causes an apocalyptic pandemic which kills off the majority of the world's human population."
                },
                new Book()
                {
                    Id = 8,
                    AuthorId = 3,
                    Title = "The Hitchhiker's Guide to the Galaxy",
                    Description = "The Hitchhiker's Guide to the Galaxy is the first of five books in the Hitchhiker's Guide to the Galaxy comedy science fiction 'trilogy' by Douglas Adams."
                }
            );
        }
    }
}
