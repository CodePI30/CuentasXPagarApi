using System.ComponentModel.DataAnnotations;
using System.Text.Json.Nodes;

namespace CuentasXPagar.data.Entidades
{
    public class LogOperacion
    {
        public int id { get; set; }

        [Required, MaxLength(50)]
        public string? tabla_afectada { get; set; }

        [Required, MaxLength(10)]
        public string? tipo_operacion { get; set; } // Valid values: "INSERT", "UPDATE", "DELETE"

        public string? usuario { get; set; }

        public DateTime fecha_operacion { get; set; } = DateTime.Now;

        public string? datos_anteriores { get; set; }

        public string? datos_nuevos { get; set; }
    }
}
