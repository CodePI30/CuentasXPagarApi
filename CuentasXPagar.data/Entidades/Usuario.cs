using System.ComponentModel.DataAnnotations;

namespace CuentasXPagar.data.Entidades
{
    public class Usuario
    {
        public int id { get; set; }

        [Required, MaxLength(50)]
        public string? nombre_usuario { get; set; }

        [Required, MaxLength(100)]
        public string? contrasena { get; set; }

        [Required, MaxLength(20)]
        public string? rol { get; set; } // Valid values: "Admin", "Usuario"

        public bool estado { get; set; } = true;
    }
}
