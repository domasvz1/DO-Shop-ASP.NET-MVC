using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects.Krepselis
{
    public class ManoKrepselis
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public ICollection<ManoKrepselioPrekes> KrepselioPrekes { get; set; }
        public int KrepselioKaina { get; set; }

        public int CountCartPrice(ICollection<ManoKrepselioPrekes> Prekes)
        {
            if (Prekes == null) // jeigu nera prekiu, griztam i meniu
                return 0;
            int bendraKaina = 0;
            foreach (var krepselioPreke in Prekes)
                bendraKaina += krepselioPreke.Preke.Kaina * krepselioPreke.Kiekis; // Kiek turime kiekio kart kaina

            return bendraKaina; // Grazinam gauta sudauginta kaina
        }
    }
}