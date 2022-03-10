using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectV1.DAL.Entities
{
    public class Player
    {
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
