using System;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;

namespace TimeWarpGames.Entities
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Naam is verplicht")]
        [StringLength(100, ErrorMessage = "Naam mag maximaal 100 karakters zijn")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Geef aan of het product nog in de verpakking zit.")]
        public bool IsBoxed { get; set; }
        [Required(ErrorMessage = "Afbeelding is verplicht")]
        public string Image { get; set; }
        [Required(ErrorMessage = "Beschrijving is verplicht")]
        [StringLength(500, ErrorMessage = "Beschrijving mag maximaal 500 karakters zijn")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Prijs is verplicht")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Voorraad is verplicht")]
        [Range(0, int.MaxValue, ErrorMessage = "Voorraad moet een positief getal zijn")]
        public int Stock { get; set; }

        public Product(string name, bool isBoxed, string image, string description, decimal price, int stock)
        {
            Name = name;
            IsBoxed = isBoxed;
            Image = image;
            Description = description;
            Price = price;
            Stock = stock;
        }

        public Product()
        {
        }

    }
}