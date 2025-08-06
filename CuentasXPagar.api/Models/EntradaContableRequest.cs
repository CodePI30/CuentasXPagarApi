namespace CuentasXPagar.api.Models
{
    public class EntradaContableRequest
    {
        public string Descripcion { get; set; }
        public int Cuenta_Id { get; set; }
        public int Auxiliar_Id { get; set; }
        public string TipoMovimiento { get; set; }
        public DateTime FechaAsiento { get; set; }
        public decimal MontoAsiento { get; set; }
    }

}
