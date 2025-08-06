using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CuentasXPagar.api.Models
{
    public class ApiResponseModel
    {
        public bool success { get; set; }
        public Data data { get; set; }
        public string message { get; set; }
        public Meta meta { get; set; }
    }

    public class Data
    {
        public int id { get; set; }
    }
}
