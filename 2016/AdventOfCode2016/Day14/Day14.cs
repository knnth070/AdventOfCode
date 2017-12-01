using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AdventOfCode2016.Day5.MD5;

namespace AdventOfCode2016.Day14
{
    public class Day14
    {
        private string salt;
        private int stretches;

        Dictionary<int, string> cache;
        Dictionary<int, string> validKeys;

        public Day14(string salt, int stretches = 0)
        {
            this.salt = salt;
            this.stretches = stretches;
            cache = new Dictionary<int, string>();
            validKeys = new Dictionary<int, string>();
        }

        public void CollectKeys()
        {
            for (int index = 0; validKeys.Count < 64; index++)
            {
                var hash = GetHash(index);
                if (IsValidKey(index))
                {
                    Console.WriteLine($"{index}, {hash}");
                    validKeys.Add(index, hash);
                }
            }
        }

        public string GetHash(int index)
        {
            if (cache.ContainsKey(index))
                return cache[index];

            var hash = GetMD5Hash($"{salt}{index}");

            for (int s = 0; s < stretches; s++)
                hash = GetMD5Hash(hash);

            cache.Add(index, hash);
            return hash;
        }

        public string GetTriplet(string hash)
        {
            for (int i = 0; i < hash.Length - 2; i++)
                if (hash[i] == hash[i + 1] && hash[i] == hash[i + 2])
                    return hash.Substring(i, 3);

            return string.Empty;
        }

        private bool ContainsQuintet(string hash, char character)
        {
            var quintet = new string(character, 5);

            return hash.Contains(quintet);
        }

        public bool IsValidKey(int index)
        {
            var hash = GetHash(index);

            if (GetTriplet(hash) == string.Empty)
                return false;

            var character = GetTriplet(hash)[0];

            // fill cache
            for (int i = index + 1; i <= index + 1000; i++)
            {
                GetHash(i);
            }

            return cache.Where(c => c.Key > index && c.Key <= index + 1000)
                .Any(c => ContainsQuintet(c.Value, character));
        }
    }
}
