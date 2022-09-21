using System;
using System.Collections.Generic;
using System.Text;

namespace TIRPClo
{
    //This class represents a coincidence sequence 
    public class CoincidenceSequence
    {
        //The first coincidence of the sequence
        public Coincidence coes;
        //The entity it belongs
        public string entity;
        //The partial coincidence that starts the sequence if there is such
        public Coincidence partial;
        public CoincidenceSequence(){
            partial = null;
        }
        public override string ToString()
        {
            string res = "";
            
            Coincidence curr = coes;
            while (curr != null)
            {
                if (curr.isMeet) res += "@";
                res += "[" + curr.StartTime() + "," + curr.FinishTime() + ")" + curr.type ;
                res += "<";
                foreach (Slice t in curr.tieps)
                {
                    res += "[" + t.time + t.type + ":'" + t.sym + "']";
                }
                curr = curr.next;
                res += ">\n";
            }
            return res;
        }
       
    }
}