using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WelcomeToHogwarts.Persistance.Enums;

namespace WelcomeToHogwarts.Persistance.Entities
{
    public class Student : EntityBase
    {
        [Required]
        public string Name { get; set; }
        [Column(TypeName = "varchar(50)"), Required]
        public HouseType HouseType { get; set; }
        [Column(TypeName = "varchar(50)"), Required]
        public PetType PetType { get; set; }
        public Guid? RoomId { get; set; }
    }
}
