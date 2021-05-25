using System.Collections.Generic;
using System.Linq;
using PlayfairCipher.Extensions;

namespace PlayfairCipher
{
    interface IPlayfairCipher
    {
        void SetKeyTable(KeyTable keyTable);
        string Encrypt(string message);
        string Decrypt(string message);

    }
    public class PlayfairCipher: IPlayfairCipher
    {
        private KeyTable Table { get; set; }= KeyTable.Empty;
        
        public void SetKeyTable(KeyTable keyTable)
        {
            Table = keyTable;
        }

        public string Encrypt(string message)
        {
            var normalizedMessage = NormalizeMessage(message);
            var digraphs = BreakMessageIntoDigraphs(normalizedMessage);
            return CipherDigraphsOnKeyTable(digraphs);
        }
        
        private static string NormalizeMessage(string message)
        {
            var normalized = string.Concat(message.ToUpper().Where(char.IsLetter)).Replace('J', 'I');
            var result = normalized;
            for (int i = 0; i < normalized.Length - 2; i += 2)
            {
                if (normalized[i] == normalized[i + 1])
                    result = normalized.Insert(i + 1, "X");
            }
            return result;
        }
        
        private static IEnumerable<string> BreakMessageIntoDigraphs(string message)
        {
            var digraphs = SplitList.Split(message.ToList(), 2);
            return digraphs.Select(digraph =>
            {
                if (digraph.Count == 1)
                    return $"{digraph[0]}X";

                if (digraph[0] == digraph[1])
                    return $"{digraph[0]}X";

                return string.Concat(digraph);
            });
        }
        
        private string CipherDigraphsOnKeyTable(IEnumerable<string> digraphs) =>
            string.Concat(digraphs.Select(digraph =>
            {
                var position1 = Table.GetPositionOfCharacter(digraph[0]);
                var position2 = Table.GetPositionOfCharacter(digraph[1]);

                if (digraph[0] == 'X' && digraph[1] == 'X')
                    return "YY";

                if (IsSameColumn(position1, position2))
                    return $"{Table.LetterBelow(position1)}{Table.LetterBelow(position2)}";

                if (IsSameRow(position1, position2))
                    return
                        $"{Table.LetterAtRightNeighbor(position1)}{Table.LetterAtRightNeighbor(position2)}";

                // Different row, different column
                return $"{Table.Table[position1.Y][position2.X]}{Table.Table[position2.Y][position1.X]}";
            }));
        
        private static bool IsSameRow(Position position1, Position position2) => position1.Y == position2.Y;
        private static bool IsSameColumn(Position position1, Position position2) => position1.X == position2.X;
        
        
        public string Decrypt(string message)
        {
            throw new System.NotImplementedException();
        }

    }
}