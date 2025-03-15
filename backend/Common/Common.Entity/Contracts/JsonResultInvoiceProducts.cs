using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entity.Contracts
{
    public class JsonResultInvoiceProducts
    {
        public Invoice Invoice { get; set; }

        public Product Product { get; set; }
    }
}
