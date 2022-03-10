using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectV1.DAL
{
    public class Player
    {
       //Entitatea pentru jucator
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Nationality { get; set; }
        public DateTime Birth_Date { get; set; }
        public float Height { get; set; }
        [Required]
        public string Foot { get; set; }
        [Required]
        public string Position { get; set; }

    }
}
