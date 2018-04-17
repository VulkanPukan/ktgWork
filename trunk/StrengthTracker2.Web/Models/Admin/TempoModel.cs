using System.Collections.Generic;
using System.Linq;

namespace StrengthTracker2.Web.Models.Admin
{
    public static class TempoModel
    {
        private static readonly List<KeyValuePair<string, string>> Phases = new List<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string, string>("Anatomical Adaptation", ""),
            new KeyValuePair<string, string>("Hypertrophy", "4"),
            new KeyValuePair<string, string>("Power", "1"),
            new KeyValuePair<string, string>("Strength", "3"),
            new KeyValuePair<string, string>("Muscular Endurance","2"),
            new KeyValuePair<string, string>("Other", "")
        };

        public static List<KeyValuePair<string, string>> Tempos
        {
            get{
                return new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("1010", "1"),
                    new KeyValuePair<string, string>("2010", "2"),
                    new KeyValuePair<string, string>("3011", "3"),
                    new KeyValuePair<string, string>("4110", "4"),
                    new KeyValuePair<string, string>("Slow", "5"),
                    new KeyValuePair<string, string>("Medium", "6"),
                    new KeyValuePair<string, string>("Fast", "7"),
                    new KeyValuePair<string, string>("Add New Tempo", "8")
                };
            }
        }

        public static string PrefferedTempo(string key)
        {
            return Phases.FirstOrDefault(p => p.Key == key).Value;
        }
    }
}