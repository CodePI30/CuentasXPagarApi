using System.ComponentModel.DataAnnotations;

namespace CuentasXPagar.data.Entidades
{
    public class Concepto
    {
        public int id { get; set; }

        [Required]
        public string? descripcion { get; set; }

        public bool estado { get; set; } = true;
    }
}
