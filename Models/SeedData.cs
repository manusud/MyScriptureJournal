using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyScriptureJournal.Data;
using System;
    using System.Linq;

namespace MyScriptureJournal.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MyScriptureJournalContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MyScriptureJournalContext>>()))
            {
                // Look for any movies.
                if (context.Journal.Any())
                {
                    return;   // DB has been seeded
                }

                context.Journal.AddRange(
                    new Journal
                    {
                        FavoriteScripture = "1 nephi 3:7",
                        Notes = "Really True!",
                        Book = "The Book Of Mormom",
                        EditionDate = DateTime.Parse("2020-02-12")
                    },

                    new Journal
                    {
                        FavoriteScripture = "Alma 32:3",
                        Notes = "I need more faith",
                        Book = "The Book Of Mormom",
                        EditionDate = DateTime.Parse("2020-02-15")
                    },

                    new Journal
                    {
                        FavoriteScripture = "2 nephi 32:8-9",
                        Notes = "Pray is the Key!",
                        Book = "The Book Of Mormom",
                        EditionDate = DateTime.Parse("2020-08-12")
                    },

                    new Journal
                    {
                        FavoriteScripture = "Mark 1:1",
                        Notes = "I need read one more time this",
                        Book = "New Testament",
                        EditionDate = DateTime.Parse("2020-10-18")
                    }
                );
                context.SaveChanges();
            }
        }
    }
}