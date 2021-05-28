using System;
using System.Collections.Generic;
using System.Text;

namespace masterMindGUI
{
    public class Pegs
    {
        protected int redPeg;
        protected int whitePeg;
        private int[] matchingWhite;
        private int[] matchingRed;

        public Pegs()
        {
            this.redPeg = 0;
            this.whitePeg = 0;
            this.MatchingWhite = null;
            this.MatchingRed = null;
            this.MatchingWhite = null;
            this.MatchingRed = null;
        }

        public int[] MatchingWhite { get => matchingWhite; set => matchingWhite = value; }
        public int[] MatchingRed { get => matchingRed; set => matchingRed = value; }
        public virtual int GetRedPeg()              //virtual methods will be overridden in Solution class(child class)
        {
            return this.redPeg;
        }
        public virtual int GetWhitePeg()          //virtual methods will be overridden in Solution class(child class)     
        {
            return this.whitePeg;
        }
    }
}
