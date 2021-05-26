﻿using System.Linq;

namespace PlayfairCipher
{
    public record PlayfairMessage
    {
        public string Value { get; }

        private PlayfairMessage(string message) => Value = message;

        public static PlayfairMessage Create(string message)
        {
            var normalized = string.Concat(message.ToUpper().Where(char.IsLetter)).Replace('J', 'I');
            normalized = InsertXsIfOneChunkAreTheSame(normalized);
            return new(normalized);
        }

        private static string InsertXsIfOneChunkAreTheSame(string message)
        {
            var result = message;
            for (var i = 0; i < result.Length - 2; i += 2)
            {
                if (result[i] == result[i + 1] && result[i] != 'X')
                    result = result.Insert(i + 1, "X");
            }
            return result;
        }
        
    }
}