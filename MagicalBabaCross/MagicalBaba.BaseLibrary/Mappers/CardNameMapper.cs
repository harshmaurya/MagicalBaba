using System.Collections.Generic;

namespace MagicalBaba.BaseLibrary.Mappers
{
    internal class CardNameMapper
    {
        private readonly Dictionary<string, string> _shortToLong = new Dictionary<string, string>();
        private readonly Dictionary<string, string> _longToShort = new Dictionary<string, string>();

        public CardNameMapper()
        {
            AddMapping("2c", "Two of clubs");
            AddMapping("3c", "Three of clubs");
            AddMapping("4c", "Four of clubs");
            AddMapping("5c", "Five of clubs");
            AddMapping("6c", "Six of clubs");
            AddMapping("7c", "Seven of clubs");
            AddMapping("8c", "Eight of clubs");
            AddMapping("9c", "Nine of clubs");
            AddMapping("tc", "Ten of clubs");
            AddMapping("jc", "Jack of clubs");
            AddMapping("qc", "Queen of clubs");
            AddMapping("kc", "King of clubs");
            AddMapping("ac", "Ace of clubs");

            AddMapping("2h", "Two of hearts");
            AddMapping("3h", "Three of hearts");
            AddMapping("4h", "Four of hearts");
            AddMapping("5h", "Five of hearts");
            AddMapping("6h", "Six of hearts");
            AddMapping("7h", "Seven of hearts");
            AddMapping("8h", "Eight of hearts");
            AddMapping("9h", "Nine of hearts");
            AddMapping("th", "Ten of hearts");
            AddMapping("jh", "Jack of hearts");
            AddMapping("qh", "Queen of hearts");
            AddMapping("kh", "King of hearts");
            AddMapping("ah", "Ace of hearts");

            AddMapping("2s", "Two of spades");
            AddMapping("3s", "Three of spades");
            AddMapping("4s", "Four of spades");
            AddMapping("5s", "Five of spades");
            AddMapping("6s", "Six of spades");
            AddMapping("7s", "Seven of spades");
            AddMapping("8s", "Eight of spades");
            AddMapping("9s", "Nine of spades");
            AddMapping("ts", "Ten of spades");
            AddMapping("js", "Jack of spades");
            AddMapping("qs", "Queen of spades");
            AddMapping("ks", "King of spades");
            AddMapping("as", "Ace of spades");


            AddMapping("2d", "Two of diamond");
            AddMapping("3d", "Three of diamond");
            AddMapping("4d", "Four of diamond");
            AddMapping("5d", "Five of diamond");
            AddMapping("6d", "Six of diamond");
            AddMapping("7d", "Seven of diamond");
            AddMapping("8d", "Eight of diamond");
            AddMapping("9d", "Nine of diamond");
            AddMapping("td", "Ten of diamond");
            AddMapping("jd", "Jack of diamond");
            AddMapping("qd", "Queen of diamond");
            AddMapping("kd", "King of diamond");
            AddMapping("ad", "Ace of diamond");

            AddMapping("jk", "Joker");
        }

        public string GetShortName(string longName)
        {
            return _longToShort[longName];
        }

        public string GetLongName(string shortName)
        {
            return _shortToLong[shortName];
        }

        public bool IsValidCardName(string name)
        {
            return _longToShort.ContainsKey(name) || _shortToLong.ContainsKey(name);
        }

        private void AddMapping(string shortName, string longName)
        {
            _shortToLong.Add(shortName, longName);
            _longToShort.Add(longName, shortName);
        }
    }
}
