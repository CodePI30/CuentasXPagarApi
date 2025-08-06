namespace CuentasXPagar.api.Models
{
    public class CatalogoResponse
    {
        public bool Success { get; set; }
        public List<Cuenta> Data { get; set; }
        public Meta Meta { get; set; }
    }

    public class Cuenta
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int TipoCuenta_Id { get; set; }
        public bool PermiteTransacciones { get; set; }
        public int Nivel { get; set; }
        public int? CuentaMayor_Id { get; set; }
        public decimal Balance { get; set; }
        public int Estado_Id { get; set; }
        public TiposCuentum TiposCuentum { get; set; }
    }

    public class TiposCuentum
    {
        public string Descripcion { get; set; }
        public string Origen { get; set; }
    }

    public class Meta
    {
        public int Total { get; set; }
        public string ApiKey { get; set; }
        public string? SistemaOrigen { get; set; }
    }

}
