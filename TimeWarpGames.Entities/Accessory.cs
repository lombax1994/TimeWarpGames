using System.ComponentModel.DataAnnotations;

namespace TimeWarpGames.Entities
{
    public class Accessory : Product
    {
        [Required(ErrorMessage = "Merk is verplicht")]
        [StringLength(100, ErrorMessage = "Merk mag maximaal 100 karakters zijn")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "Platform is verplicht")]
        public Platform Platform { get; set; }

        [Required(ErrorMessage = "Type is verplicht")]
        public AccessoryType Type { get; set; }

        [Required(ErrorMessage = "Staat is verplicht")]
        public State State { get; set; }

        public Accessory()
        {
        }

        public Accessory(string name, bool isBoxed, string image, string description, decimal price, int stock,
            string brand, Platform platform, AccessoryType type, State state)
            : base(name, isBoxed, image, description, price, stock)
        {
            Brand = brand;
            Platform = platform;
            Type = type;
            State = state;
        }
    }
}