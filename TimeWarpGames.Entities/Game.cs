using System;
using System.ComponentModel.DataAnnotations;

namespace TimeWarpGames.Entities
{
    public class Game : Product
    {
        [Required(ErrorMessage = "Platform is verplicht")]
        public Platform Platform { get; set; }
        [Required(ErrorMessage = "Genre is verplicht")]
        public Genre Genre { get; set; }
        [Required(ErrorMessage = "Ontwikkelaar is verplicht")]
        [StringLength(100, ErrorMessage = "Ontwikkelaar mag maximaal 100 karakters zijn")]
        public string Developer { get; set; }
        [Required(ErrorMessage = "Releasedatum is verplicht")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        [Required(ErrorMessage = "Leeftijdsgrens is verplicht")]
        [Range(0, 18, ErrorMessage = "Leeftijdsgrens moet tussen 0 en 18 jaar zijn")]
        public int AgeRating { get; set; }

        public Game(string name, bool isBoxed, string image, string description, decimal price, int stock, Platform platform, Genre genre, string developer, DateTime releaseDate, int ageRating)
            : base(name, isBoxed, image, description, price, stock)
        {
            Platform = platform;
            Genre = genre;
            Developer = developer;
            ReleaseDate = releaseDate;
            AgeRating = ageRating;
        }

        public Game()
        {
        }
    }
}