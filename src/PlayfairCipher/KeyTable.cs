using System;
using System.Collections.Generic;
using System.Linq;
using PlayfairCipher.Extensions;

namespace PlayfairCipher
{
    public record KeyTable
    {
        public List<List<char>> Value { get; }
        
        private const int TableSize = 5;

        private KeyTable(List<List<char>> value) => Value = value;

        public static KeyTable Empty = new(SplitList.Split(Enumerable.Range('A', 26).Select(x => (char) x).Where(c => c != 'J').ToList()).ToList());
        
        public static KeyTable Create(string key)
        {
            var normalizedKey = key.ToUpper().Where(char.IsLetter);
            var visitedCharacters = new HashSet<char>();
            var alphabet = Enumerable.Range('A', 26).Select(x => (char)x).ToArray();

            foreach (var c in normalizedKey)
            {
                var normalizedChar = c == 'J' ? 'I' : c;
                
                if (visitedCharacters.Contains(normalizedChar))
                    continue;

                visitedCharacters.Add(normalizedChar);
            }

            var finalVisitedCharacters = string.Concat(visitedCharacters.ToList());
            var remainingLettersInAlphabet = string.Concat(alphabet.Where(letterInAlphabet => !visitedCharacters.Contains(letterInAlphabet) && letterInAlphabet != 'J'));
            var table = SplitList.Split(String.Concat($"{finalVisitedCharacters}{remainingLettersInAlphabet}").ToList()).ToList();
            return new KeyTable(table);
        }

        public Position GetPositionOfCharacter(char c)
        {
            for (var y = 0; y < Value.Count; y++)
                for (var x = 0; x < Value[y].Count; x++)
                {
                    if (Value[y][x] == c)
                        return new Position
                        {
                            X = x,
                            Y = y
                        };
                }

            throw new Exception("Char not found in key table");
        }
        
        public char LetterBelow(Position position)
        {
            return Value[(position.Y + 1) % TableSize][position.X];
        }

        public char LetterAtRightNeighbor(Position position)
        {
            return Value[position.Y][(position.X + 1) % TableSize];
        }
    }
    
}