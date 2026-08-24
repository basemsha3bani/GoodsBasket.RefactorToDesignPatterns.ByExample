using ReceiptGenerator.Domain.Product;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReceiptGenerator.Domain.Tax.Rules.Resolving
{
    public class Resolver
    {
        private readonly IList<ITaxRule> _rules;

        public Resolver(IList<ITaxRule> rules)
        {
            _rules = rules;
        }

        public ITaxRule resolve(BasketItem item)
        {

            var applicableRules = _rules
      .Where(rule => rule.AppliesTo(item))
      .ToList();

            if (applicableRules.Count == 0)
            {
                throw new InvalidOperationException(
                    "No applicable tax rule was found.");
            }

            int highestPriority =
                applicableRules.Max(rule => rule.Priority);

            var highestPriorityRules = applicableRules
                .Where(rule =>
                    rule.Priority == highestPriority)
                .ToList();

            if (highestPriorityRules.Count > 1)
            {
                throw new InvalidOperationException(
                    "Multiple tax rules matched at the highest priority.");
            }

            return highestPriorityRules[0];
        }
    }
}
