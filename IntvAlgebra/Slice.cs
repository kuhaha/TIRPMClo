using System;
using System.Collections.Generic;
using System.Text;

namespace TIRPClo
{
    //This class represents a slice
    // NEW: changed name from tiep to slice
    public class Slice
    {
        //Slice's type
        public char type;
        //Slice's sti
        public STI e;
        //Slice's symbol
        public int sym;
        //Slice's coincidence
        public Coincidence c;
        //Slice's primitive rep.
        public string premitive_rep;
        //Slice's original slice if it is a co-slice
        public Slice orig;
        //Slice's order within the ordered set of slices from the same symbol and type in a specific entity
        public int ms_index = -1;
        //Slice's time
        public int time;
        public Slice(char t, int ti, STI ei, Coincidence coi)
        {
            c = coi;
            e = ei;
            time = ti;
            sym = ei.sym;
            type = t;
            premitive_rep = sym + "" + t;
            orig = null;
        }
        public Slice(Slice s)
        {
            c = s.c;
            e = s.e;
            time = s.time;
            sym = s.sym;
            type = s.type;
            premitive_rep = s.premitive_rep;
            ms_index = s.ms_index;
            orig = s;
        }
    }
}