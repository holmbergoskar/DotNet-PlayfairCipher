using System.Collections.Generic;
using System.Linq;
using PlayfairCipher.Extensions;

namespace PlayfairCipher
{
    interface IPlayfairCipher
    {
        void SetKeyTable(KeyTable keyTable);
        string Encrypt(PlayfairMessage message);
        string Decrypt(PlayfairMessage message);

    }
    public class PlayfairCipher: IPlayfairCipher
    {
        private KeyTable Table { get; set; } = KeyTable.Empty;
        
        public void SetKeyTable(KeyTable keyTable)
        {
            Table = keyTable;
        }

        public string Encrypt(PlayfairMessage message) => CipherDigraphsOnKeyTable(message.BreakMessageIntoDigraphs()); 
        
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
                    return $"{Table.LetterAtRightNeighbor(position1)}{Table.LetterAtRightNeighbor(position2)}";

                // Different row, different column
                return $"{Table.Value[position1.Y][position2.X]}{Table.Value[position2.Y][position1.X]}";
            }));
        
        private static bool IsSameRow(Position position1, Position position2) => position1.Y == position2.Y;
        private static bool IsSameColumn(Position position1, Position position2) => position1.X == position2.X;
        
        public string Decrypt(PlayfairMessage message) =>
            throw new System.NotImplementedException();

    }
}