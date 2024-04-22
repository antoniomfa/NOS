using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOS.Engineering.Challenge.API.Tests.MockData
{
    public class ContentMockData
    {
        public Content Content => new()
        {
            Id = 1,
            Title = "New title",
            SubTitle = "New subtitle",
            Description = "Description",
            ImageUrl = "Image",
            Duration = 10,
            StartTime = new DateTime(2024, 10, 10),
            EndTime = new DateTime(2024, 10, 11),
            GenreList = new List<string>()
            {
                "Genre 1", "Genre 2", "Genre 3"
            }
        };

        public IQueryable<Content> Contents => new List<Content>()
        {
            new Content()
            {
                Id = 1,
                Title = "New title",
                SubTitle = "New subtitle",
                Description = "Description",
                ImageUrl = "Image",
                Duration = 10,
                StartTime = new DateTime(2024, 10, 10),
                EndTime = new DateTime(2024, 10, 11),
                GenreList = new List<string>()
                {
                    "Genre 1", "Genre 2", "Genre 3"
                }
            },
            new Content()
            {
                Id = 2,
                Title = "New title 2",
                SubTitle = "New subtitle 2",
                Description = "Description 2",
                ImageUrl = "Image 2",
                Duration = 20,
                StartTime = new DateTime(2024, 10, 5),
                EndTime = new DateTime(2024, 10, 11),
                GenreList = new List<string>()
                {
                    "Genre 1", "Genre 4"
                }
            }
        };
    }
}
