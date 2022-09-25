using System;
using System.Collections.Generic;

namespace TIRPClo
{
    //This class is responsible for the main process of TPM algorithm for the purpose of discovering the set of closed TIRPs
    public class TIRPMCloAlg
    {
        public static void runTIRPMClo(SequenceDB tdb)
        {
            //int count = 0;
            List<string> rks = new List<string>(); // infrequent master slices to remove
            foreach (KeyValuePair<string, MasterSlice> mtiep in SlicesHandler.master_tieps)
            {
                if (mtiep.Value.supporting_entities.Count < Constants.MINSUP)
                {
                    rks.Add(mtiep.Key);
                }
            }
            foreach(string rk in rks)
            {
                SlicesHandler.master_tieps.Remove(rk);
            }
            tdb.filterInfrequentTiepsFromInitialSDB();
            foreach (KeyValuePair<string, MasterSlice> mtiep in SlicesHandler.master_tieps)
            {
                //For each frequent start tiep
                //count++;
                string t = mtiep.Key;
                //Console.WriteLine(count + "/" + SlicesHandler.master_tieps.Count + "/" + t);
                if (t[t.Length - 1] == Constants.ST_REP)
                {
                    //Project the sequence db by the tiep
                    bool is_closed = true;
                    Dictionary<string, List<BESlice>> bets = new Dictionary<string, List<BESlice>>();
                    SequenceDB projDB = tdb.first_projectDB(t, mtiep.Value.supporting_entities, ref is_closed, ref bets);
                    //Continue if it can be extended to form a closed TIRP
                    if (is_closed)
                    {
                        extendTIRP(t, projDB, t, null, ref bets);
                    }
                }
            }
        }
        //Extend frequent sequences recursively  
        private static void extendTIRP(string p, SequenceDB projDB, string last, Dictionary<string, 
            SliceProjector> sf, ref Dictionary<string, List<BESlice>> bets)
        {
            Dictionary<string, SliceProjector> LFs = projDB.tiepsFreq_alt(last, sf);
            //Only if it forms a TIRP
            if(SequenceDB.allInPairs(projDB.trans_db))
            {
                bool is_closed = true;
                //Verify if it is a closed TIRP 
                foreach (KeyValuePair<string, SliceProjector> entry in LFs)
                {
                    if (projDB.sup == entry.Value.sup_entities.Count)
                    {
                        if (entry.Key[entry.Key.Length - 1] == Constants.ST_REP)
                        {
                            is_closed = false;
                            break;
                        }
                        string t = entry.Key[0] == Constants.CO_REP ? entry.Key.Substring(1) : entry.Key;
                        
                        t = t.Replace(Constants.FIN_REP, Constants.ST_REP);
                        if (bets.ContainsKey(t))
                        {
                            if (projDB.checkStFinMatch(bets[t], entry.Value))
                            {
                                is_closed = false;
                                break;
                            }
                        }
                    }
                }
                //Check if the grown sequence is indeed a closed TIRP
                if (is_closed)
                {
                    TIRPMsWriter.addPattern(projDB);
                }
            }
            //For each frequent tiep
            foreach(KeyValuePair<string, SliceProjector> z in LFs)
            {
                if (z.Value.sup_entities.Count < Constants.MINSUP)
                {
                    continue;
                }
                //Finishing tieps 
                if (z.Key[z.Key.Length - 1] == Constants.FIN_REP)
                {
                    string tmp = z.Key[0] == Constants.CO_REP ? z.Key.Substring(1) : z.Key;
                    
                    if (projDB.pre_matched.Contains(tmp))
                    { 
                        SequenceDB alpha_projDB = projDB.projectDB(z.Key, LFs[z.Key]);
                        if (alpha_projDB.sup >= Constants.MINSUP)
                        {
                            Dictionary<string, List<BESlice>> nbets = new Dictionary<string, List<BESlice>>();
                            //Continue only if it can be extended to form a closed TIRP
                            if (alpha_projDB.back_scan(ref nbets))
                            {
                                extendTIRP(p + z.Key, alpha_projDB, z.Key, LFs, ref nbets);
                            }
                        }
                    }
                }
                else
                {
                    //Starting tieps 
                    SequenceDB alpha_projDB = projDB.projectDB(z.Key, LFs[z.Key]);
                    if (alpha_projDB.sup >= Constants.MINSUP)
                    {
                        Dictionary<string, List<BESlice>> nbets = new Dictionary<string, List<BESlice>>();
                        //Continue only if it can be extended to form a closed TIRP
                        if (alpha_projDB.back_scan(ref nbets))
                        {
                            extendTIRP(p+ z.Key, alpha_projDB, z.Key, LFs, ref nbets);
                        }
                    }
                }
            }
        }
    }
}