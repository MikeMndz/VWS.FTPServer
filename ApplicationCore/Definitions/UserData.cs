using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Definitions
{
    public class UserData
    {
        public int Id { get; set; }

        public string Uid { get; set; }

        [Display(Name = "Cuenta de usuario")]
        [Required(ErrorMessage = "La cuenta de usuario es requerida.")]
        [StringLength(30, ErrorMessage = "La cuenta de usuario debe tener como mínimo 8 caracteres y 30 caracteres como máximo.", MinimumLength = 8)]
        public string Account { get; set; }

        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Nombre(s)")]
        [Required(ErrorMessage = "El nombre es requerido.")]
        public string Name { get; set; }

        [Display(Name = "Apellido paterno")]
        public string LastName { get; set; }

        [Display(Name = "Apellido materno")]
        public string MothersLastName { get; set; }

        [Display(Name = "Habilitado")]
        public bool Enabled { get; set; }

        [Display(Name = "Rol de usuario")]
        [Required(ErrorMessage = "El rol de usuario es requerido.")]
        public int IdRole { get; set; }

        public string Role { get; set; }


    }
}
