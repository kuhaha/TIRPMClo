using System;
using System.Collections.Generic;
using System.Text;

namespace TIRPClo
{
    public class Constants
    {
        //Coincidence Representation

        //type of endtime
        public const bool START = true; //starting 
        public const bool FINISH = false; //finishing 

        //type of slice, coincidence
        public const char ST_REP = '+'; //starting slice/coincidence rep. plus
        public const char FIN_REP = '-'; //finishing slice/coincidence rep. minus
        public const char OVLP_REP = 'x'; //NEW! overlapping coincidence rep. x 
        public const char MEET_REP = '@'; //meet slice rep. at-mark. 

        public const char UNDEF_TYPE = '?'; //NEW! unknown type of slice / coincidence  
        public const int  UNDEF_TIME = -1; //NEW! unknown time 

        // Credentials and/or constraints
        public static int MINSUP = 0; //Minimum vertical support 
        public static int MAX_GAP = 0; //Maximal gap
        public static int MIN_GAP = 0; //NEW! Minimal gap
        public static int MIN_DURATION = 0; //NEW! Minimal duration
        public static int MAX_DURATION = 0; //NEW! Maximal duration

        //For the coincidences creation at the final stage
        public const char CO_REP = '!'; // NEW! changed from '_'

        //Input & Output files 
        public static string FILE_NAME = "Smarthome_3EWD_NoCluster_KL_sorted_fixed.csv";
        public static string OUT_FILE = "Smarthome_3EWD_NoCluster_KL_sorted_fixed";
        public const string FILE_START = "startToncepts";
        public const string FILE_NUM = "numberOfEntities";
        public const string FILE_FORMAT_ERR = "incorrect file format";

        //Allen's tmporal relations
        public const char ALLEN_BEFORE = '<';
        public const char ALLEN_MEET = 'm';
        public const char ALLEN_OVERLAP = 'o';
        public const char ALLEN_FINISHBY = 'f';
        public const char ALLEN_CONTAIN = 'c';
        public const char ALLEN_EQUAL = '=';
        public const char ALLEN_STARTS = 's';
    }
}