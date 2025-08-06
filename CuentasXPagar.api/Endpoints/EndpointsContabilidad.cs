namespace CuentasXPagar.api.Endpoints
{
    public static class EndpointsContabilidad
    {
        public const string GetCatalogoCuentas = "api/public/catalogo-cuentas";
        public const string GetEntradasContables = "api/public/entradas-contables?fechaInicio={0}&fechaFin={1}&cuenta_Id={2}";
        public const string PostEntradaContable = "api/public/entradas-contables";
        public const string GetBalances = "api/public/balances";
    }
}
