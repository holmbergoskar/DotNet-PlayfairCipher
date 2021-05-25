using System;
using System.Collections.Generic;

namespace PlayfairCipher.Extensions
{
    public static class SplitList
    {
        public static IEnumerable<List<char>> Split(List<char> locations, int nSize=5)  
        {        
            for (var i = 0; i < locations.Count; i += nSize) 
            { 
                yield return locations.GetRange(i, Math.Min(nSize, locations.Count - i)); 
            }  
        } 
    }
}