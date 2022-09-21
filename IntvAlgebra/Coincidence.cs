using System;
using System.Collections.Generic;
using System.Text;

namespace TIRPClo
{
    //This class represents a coincidence
    public class Coincidence
    {
        //If the coincidence is a meet one
        public bool isMeet;
        //The coincidence's slices
        public List<Slice> tieps;
        //The next coincidence within the sequence
        public Coincidence next;
        //The coincidence's index
        public int index;
        //If it is partial. "partial" = already projected by some slice
        public bool isCo;

        public Coincidence prev;
        public char type; //starting(+), finishing(-), overlapping(x)
        public int st_count;
        public int fin_count;
        public int st_time;
        public int fin_time;
        public Coincidence()
        {
            isMeet = false;
            tieps = new List<Slice>();
            index = -1;
            next = null;
            isCo = false;

            prev = null;
            type = Constants.UNDEF_TYPE; 
            st_time = Constants.UNDEF_TIME;
            fin_time = Constants.UNDEF_TIME;
            st_count = 0;
            fin_count = 0;
        }

        public void AddTiep(Slice t)
        {
            tieps.Add(t);
            if (t.type == Constants.ST_REP) st_count++;
            if (t.type == Constants.FIN_REP) fin_count++;
            SetType();
        }
        public void RemoveTiep(Slice t)
        {
            tieps.Remove(t);
            if (t.type == Constants.ST_REP) st_count--;
            if (t.type == Constants.FIN_REP) fin_count--;
            SetType();
        }

        private void SetType()
        {
            if (st_count > 0 && fin_count > 0)
            {
                type = Constants.OVLP_REP;
            }
            if (st_count > 0 && fin_count <= 0)
            {
                type = Constants.ST_REP;
            }
            if (st_count <= 0 && fin_count > 0)
            {
                type = Constants.FIN_REP;
            }
        }
        
        public int StartTime()
        {
            if (type == Constants.ST_REP || type == Constants.OVLP_REP)
            {
                return st_time;
            }
            if (prev != null)
            {
                return prev.FinishTime();
            }
            return Constants.UNDEF_TIME;
        }

        public int FinishTime()
        {
            if (type == Constants.FIN_REP || type == Constants.OVLP_REP)
            {
                return fin_time;
            }
            if (next != null)
            {
                return next.StartTime();
            }
            return Constants.UNDEF_TIME;
        }

        public int Duration()
        {
            int s = StartTime();
            int f = FinishTime();
            if (s == Constants.UNDEF_TIME || f == Constants.UNDEF_TIME)
            {
                return Constants.UNDEF_TIME;
            }
            return f - s;
        }
    }
}