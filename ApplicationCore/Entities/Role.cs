using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities
{
    public class Role : BaseEntity
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "La cuenta de usuario es requerida.")]
        public string Name { get; set; }

        [Display(Name = "Habilitado")]
        public bool Enabled { get; set; }

        [Display(Name = "Registro estático")]
        public bool IsStatic { get; set; }
    }
}
