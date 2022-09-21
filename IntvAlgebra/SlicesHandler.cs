using System;
using System.Collections.Generic;
using System.Text;

namespace TIRPClo
{
    //This class is responsible for the tieps mapper index 
    public class SlicesHandler
    {
        //Master tieps for each frequent tiep 
        public static Dictionary<string, MasterSlice> master_tieps = 
            new Dictionary<string, MasterSlice>();
        //Add a new tiep to the index
        public static int addTiepOccurrence(string t, string entity, Slice s)
        {
            if (!master_tieps.ContainsKey(t))
            {
                master_tieps.Add(t, new MasterSlice());
            }
            return master_tieps[t].addOccurrence(entity, s);
        }
    }
}