using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiptGenerator.Domain.Tax.Rules.Priority
{
    internal static class TaxRulePriorities
    {
        public const int Fallback = 0;
        public const int Specific = 100;
    }
}
