using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Tarea
    {
        public int Id { get; set; }
        
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Nombre { get; set; }
        
        [Required]
         public bool Estado { get; set; }

    }
}