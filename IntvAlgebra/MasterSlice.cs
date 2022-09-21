using System;
using System.Collections.Generic;
using System.Text;

namespace TIRPClo
{
    //This class represents a master slice
    public class MasterSlice
    {
        //The tiep's occurrences by entity
        public Dictionary<string, List<Slice>> tiep_occurrences;
        //The tiep's supporting entities
        public List<string> supporting_entities;
        public MasterSlice(){
            tiep_occurrences = new Dictionary<string, List<Slice>>();
            supporting_entities = new List<string>();
        }
        //Add a slice's occurrence to the master slice
        public int addOccurrence(string entity, Slice t)
        {
            if (!tiep_occurrences.ContainsKey(entity)) {
                tiep_occurrences.Add(entity, new List<Slice>());
                supporting_entities.Add(entity);
            }
            t.ms_index = tiep_occurrences[entity].Count;
            tiep_occurrences[entity].Add(t);
            return t.ms_index;
        }
    }
}