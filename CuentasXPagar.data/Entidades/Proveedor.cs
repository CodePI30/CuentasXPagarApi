using System.ComponentModel.DataAnnotations;

namespace CuentasXPagar.data.Entidades
{
    public class Proveedor
    {
        public int id { get; set; }

        [Required, MaxLength(100)]
        public string nombre { get; set; }

        [Required, MaxLength(20)]
        public string tipo_persona { get; set; } // Valid values: "fisica", "juridica"

        [Required, MaxLength(20)]
        public string cedula_rnc { get; set; }

        public decimal balance { get; set; } = 0.00M;

        public bool estado { get; set; } = true;
    }
}
