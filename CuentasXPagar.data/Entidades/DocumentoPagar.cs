using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CuentasXPagar.data.Entidades
{
    public class DocumentoPagar
    {
        public int id { get; set; }

        [Required, MaxLength(20)]
        public string? numero_documento { get; set; }

        [Required, MaxLength(20)]
        public string? numero_factura { get; set; }

        [Required]
        public DateTime fecha_documento { get; set; }

        [Required]
        public decimal monto { get; set; }

        public DateTime fecha_registro { get; set; } = DateTime.Now;

        [Required]
        public int proveedor_id { get; set; }

        [Required]
        public int concepto_id { get; set; }

        [MaxLength(20)]
        public string estado_pago { get; set; } = "Pendiente"; // Valid values: "Pendiente", "Pagado"

        public string? asientoId { get; set; }
    }
}
