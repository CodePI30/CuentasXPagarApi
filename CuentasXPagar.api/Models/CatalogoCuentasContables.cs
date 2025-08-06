namespace CuentasXPagar.api.Models
{
    public class CatalogoCuentasContable
    {
        public string descripcion { get; set; }
    }

    public class Auxiliare
    {
        public string nombre { get; set; }
    }

    public class EntradaContable
    {
        public int id { get; set; }
        public string descripcion { get; set; }
        public int auxiliar_Id { get; set; }
        public int cuenta_Id { get; set; }
        public string tipoMovimiento { get; set; }
        public DateTime fechaAsiento { get; set; }
        public decimal montoAsiento { get; set; }
        public int estado_Id { get; set; }
        public CatalogoCuentasContable CatalogoCuentasContable { get; set; }
        public Auxiliare Auxiliare { get; set; }
    }

    public class ApiResponse<T>
    {
        public bool success { get; set; }
        public List<EntradaContable> data { get; set; }
    }

}
