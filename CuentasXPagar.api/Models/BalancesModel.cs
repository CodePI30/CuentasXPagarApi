namespace CuentasXPagar.api.Models
{
    public class BalancesModel
    {
        public bool success { get; set; }
        public List<Balance> data { get; set; }
        public Meta meta { get; set; }
    }

    public class Balance
    {
        public int id { get; set; }
        public string descripcion { get; set; }
        public long balance { get; set; }
        public TiposCuentum TiposCuentum { get; set; }
    }
}
