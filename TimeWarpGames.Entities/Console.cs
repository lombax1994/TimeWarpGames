using System;
using System.ComponentModel.DataAnnotations;

namespace TimeWarpGames.Entities
{
    public class Console : Product
    {
        [Required(ErrorMessage = "Merk is verplicht")]
        public string Brand { get; set; }
        [Required(ErrorMessage = "Model is verplicht")]
        public string Model { get; set; }
        [Required(ErrorMessage = "Releasedatum is verplicht")]
        public DateTime ReleaseDate { get; set; }
        [Required(ErrorMessage = "Staat is verplicht")]
        public State State { get; set; }

        public Console()
        {

        }

        public Console(string name, bool isBoxed, string image, string description, decimal price, int stock, string brand, string model, DateTime releaseDate, State state)
            : base(name, isBoxed, image, description, price, stock)
        {
            Brand = brand;
            Model = model;
            ReleaseDate = releaseDate;
            State = state;
        }
    }
}