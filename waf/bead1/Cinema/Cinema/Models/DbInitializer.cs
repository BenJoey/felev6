using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Cinema.Models
{
    public class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new CinemaContext(
                serviceProvider.GetRequiredService<DbContextOptions<CinemaContext>>()))
            {
                if (context.Movies.Any())
                {
                    return;
                }
                Movie yourName = new Movie()
                {
                    Title = "Your Name",
                    PosterPath = "images/posters/YourName.png",
                    Director = "Makoto Shinkai",
                    Lenght = 106,
                    Description =
                        "Mitsuha Miyamizu, a high school girl, yearns to live the life of a boy in the bustling city of " +
                        "Tokyo—a dream that stands in stark contrast to her present life in the countryside. Meanwhile in the city, " +
                        "Taki Tachibana lives a busy life as a high school student while juggling his part-time job and " +
                        "hopes for a future in architecture. One day, Mitsuha awakens in a room that is not her own " +
                        "and suddenly finds herself living the dream life in Tokyo—but in Taki's body! " +
                        "Elsewhere, Taki finds himself living Mitsuha's life in the humble countryside.In pursuit of " +
                        "an answer to this strange phenomenon, they begin to search for one another."
                };
                Movie yugioh = new Movie()
                {
                    Title = "YuGiOh - Dark Side of Dimensions",
                    PosterPath = "images/posters/YuGiOh.png",
                    Director = "Satoshi Kuwabara",
                    Lenght = 130,
                    Description =
                        "Set six months after the events of the original Yu-Gi-Oh! manga, Yuugi Mutou and his friends are in their " +
                        "final year of high school and are talking about what they will do in the future. " +
                        "Meanwhile, Seto Kaiba has commissioned an excavation to retrieve the disassembled Millennium " +
                        "Puzzle from the ruins of the \"Shrine of the Underworld.\" The item had previously housed " +
                        "the soul of his rival, Atem, whom he hopes to revive in order to settle their score. " +
                        "The excavation is interrupted by a mysterious man, who faces Kaiba in a game of " +
                        "\"Magic & Wizards\" and steals two pieces of the recovered Puzzle. What is his ulterior motive?"
                };
                Movie eva = new Movie()
                {
                    Title = "The End of Evangelion",
                    PosterPath = "images/posters/EndOfEva.png",
                    Director = "Hideaki Anno",
                    Lenght = 87,
                    Description =
                        "With the final Angel vanquished, Nerv has one last enemy left to face—the humans under Seele's command." +
                        "Left in a deep depression nearing the end of the original series, an indecisive Shinji Ikari struggles with the ultimatum presented " +
                        "to him: to completely accept mankind's existence, or renounce humanity's individuality. Meanwhile, at the core of a compromised " +
                        "Nerv, Gendou Ikari and Rei Ayanami approach Lilith in an attempt to realize their own ideals concerning the future of the world."
                };
                Movie lovelive = new Movie()
                {
                    Title = "Love Live! The School Idol Movie",
                    PosterPath = "images/posters/LL.png",
                    Director = "Kyogoku Takahiko",
                    Lenght = 102,
                    Description =
                        "Hot on the heels of the third year students' graduation, μ's is invited to New York in hopes of spreading the joy of school idols to other parts of the world. " +
                        "Due to the events of the recent Love Live!, μ's has reached eminent stardom which results in crowds swarming them whenever they appear in public. " +
                        "With the increased attention, however, comes a difficult choice."
                };

                Movie nogamenolife = new Movie()
                {
                    Title = "No Game No Life Zero",
                    PosterPath = "images/posters/NGNLZ.png",
                    Director = "Atsuko Ishizuka",
                    Lenght = 106,
                    Description =
                        "In ancient Disboard, Riku is an angry, young warrior intent on saving humanity from the warring Exceed, " +
                        "the sixteen sentient species, fighting to establish the \"One True God\" amongst the Old Deus. " +
                        "In a lawless land, humanity's lack of magic and weak bodies have made them easy targets for the other Exceed, " +
                        "leaving the humans on the brink of extinction. One day, however, hope returns to humanity " +
                        "when Riku finds a powerful female Ex-machina, whom he names Schwi, in an abandoned elf city. " +
                        "Exiled from her Cluster because of her research into human emotions, Schwi is convinced that " +
                        "humanity has only survived due to the power of these feelings and is determined to understand " +
                        "the human heart. Forming an unlikely partnership in the midst of the overwhelming chaos, " +
                        "Riku and Schwi must now find the answers to their individual shortcomings in each other, " +
                        "and discover for themselves what it truly means to be human as they fight for their lives " +
                        "together against all odds. Each with a powerful new ally in tow, it is now up to them to " +
                        "prevent the extinction of the human race and establish peace throughout Disboard!"
                };
                // Add movies to db
                context.Movies.Add(yourName);
                context.Movies.Add(yugioh);
                context.Movies.Add(eva);
                context.Movies.Add(lovelive);
                context.Movies.Add(nogamenolife);

                //create rooms
                Room mikuRoom = new Room()
                {
                    RoomName = "Room Miku",
                    NumOfCols = 9,
                    NumOfRows = 12
                };

                Room nicoRoom = new Room()
                {
                    RoomName = "Room Nico",
                    NumOfCols = 11,
                    NumOfRows = 14
                };

                Room aquaRoom = new Room()
                {
                    RoomName = "Room Aqua",
                    NumOfCols = 7,
                    NumOfRows = 10
                };
                context.Rooms.Add(mikuRoom);
                context.Rooms.Add(nicoRoom);
                context.Rooms.Add(aquaRoom);

                // Create shows
                List<Show> programs = new List<Show>()
                {
                    new Show()
                    {
                        MovieOnAir = nogamenolife,
                        Place = mikuRoom,
                        StartTime = DateTime.Parse("2019-03-11 12:30:00")
                    },
                    new Show()
                    {
                        MovieOnAir = lovelive,
                        Place = nicoRoom,
                        StartTime = DateTime.Parse("2019-03-22 16:45:00")
                    },
                    new Show()
                    {
                        MovieOnAir = yourName,
                        Place = aquaRoom,
                        StartTime = DateTime.Parse("2019-03-11 13:00:00")
                    },
                };
                foreach (var prog in programs)
                {
                    for (int i = 0; i < prog.Place.NumOfRows; i++)
                    {
                        for (int j = 0; j < prog.Place.NumOfCols; j++)
                        {
                            context.Seats.Add(
                                new Seat()
                                {
                                    Row = i+1,
                                    Col = j+1,
                                    Room = prog.Place,
                                    Show = prog,
                                    State = State.Free
                                });
                        }
                    }
                    context.Shows.Add(prog);
                }
                context.SaveChanges();
            }
        }
    }
}
