using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;


namespace SantaClausHarmadikBeadando.Models
{
    public class SantaClaus
    {

        // Kulcs
        [Key]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Név")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Szín")]
        public string Colour { get; set; }

        // Pozitív szám megkötés
        [Required]
        [Display(Name = "Súly")]
        [Range(1, int.MaxValue, ErrorMessage = "A kívánság súlyának pozitív számnak kell lenni!")]
        public int Weigth { get; set; }

        // 1-10 határ megkötés
        [Required]
        [Display(Name = "Prioritás")]
        [Range(1, 10)]
        public int Priority { get; set; }

    }
}
