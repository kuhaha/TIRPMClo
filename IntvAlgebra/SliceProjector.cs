using System;
using System.Collections.Generic;
using System.Text;

namespace TIRPClo
{
    //This class represents a slice projector
    public class SliceProjector
    {
        //Supporting entities of the slice
        public List<string> sup_entities = new List<string>();
        //For each supporting record (of the slice) in a sequence db, it keeps the first occurrence of the tiep within it  
        public Dictionary<int, int> co_starts = new Dictionary<int, int>();
    }
}