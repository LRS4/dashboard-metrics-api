using System;
using System.Collections;
using System.Collections.Generic;

namespace Advantage.API
{
    public class Helpers
    {
        private static Random _rand = new Random();

        private static string GetRandom(IList<string> items)
        {
            return items[_rand.Next(items.Count)];
        }

        internal static string MakeUniqueCustomerName(List<string> names)
        {
            var maxNames = businessPrefix.Count * businessSuffix.Count;

            if (names.Count >= maxNames)
            {
                throw new System.InvalidOperationException("Maximum number of unique names exceeded");
            }

            var prefix = GetRandom(businessPrefix);
            var suffix = GetRandom(businessSuffix);
            var businessName = prefix + suffix;

            if (names.Contains(businessName))
            {
                MakeUniqueCustomerName(names);
            }

            return businessName;
        }

        internal static string MakeCustomerEmail(string customerName)
        {
            return $"contact@{customerName.ToLower()}.com";
        }

        internal static string GetRandomState()
        {
            return GetRandom(states);
        }

        internal static decimal GetRandomOrderTotal()
        {
            return _rand.Next(100, 5000);
        }

        internal static DateTime GetRandomOrderPlaced()
        {
            var end = DateTime.Now;
            var start = end.AddDays(-90);

            TimeSpan possibleSpan = end - start;
            TimeSpan newSpan = new TimeSpan(0, _rand.Next(0, (int)possibleSpan.TotalMinutes), 0);

            return start + newSpan;
        }

        internal static DateTime? GetRandomOrderCompleted(DateTime orderPlaced)
        {
            var now = DateTime.Now;
            var minLeadTime = TimeSpan.FromDays(7);
            var timePassed = now - orderPlaced;

            if (timePassed < minLeadTime)
            {
                return null;
            }

            return orderPlaced.AddHours(_rand.Next(7, 14));
        }

        private static readonly List<string> businessPrefix = new List<string>()
        {
            "ABC",
            "XYZ", 
            "Sales",
            "MainSt",
            "Enterprise",
            "Ready",
            "Quick",
            "Peak",
            "Budget",
            "Magic",
            "Family",
            "Comfort"
        };

        private static readonly List<string> businessSuffix = new List<string>()
        {
            "Corporation",
            "Co",
            "Logistics",
            "Transit",
            "Bakery",
            "Goods",
            "Foods",
            "Cleaners",
            "Hotels",
            "Planners",
            "Automotive",
            "Books"
        };

        private static readonly List<string> states = new List<string>()
        {
            "AL", "AK", "AZ", "AR", "CA", "CO", "CT", "DE", "DC", "FL", "GA",
            "HI", "ID", "IL", "IN", "IA", "KS", "KY", "LA", "ME", "MD",
            "MA", "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ",
            "NM", "NY", "NC", "ND", "OH", "OK", "OR", "PA", "RI", "SC",
            "SD", "TN", "TX", "UT", "VT", "VA", "WA", "WV", "WI", "WY",
        };
    }
}
