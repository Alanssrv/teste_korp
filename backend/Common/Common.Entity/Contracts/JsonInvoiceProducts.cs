using System.ComponentModel.DataAnnotations;
using Common.Util.Validators;

namespace Common.Entity.Contracts
{
    public class JsonInvoiceProducts
    {
        [ProductCode]
        public string Code { get; set; }

        [MinValue(1)]
        public int Quantity { get; set; }
    }
}