using masterMindGUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace masterMindGUI
{
    public class CodeBreakPlayer : Player                                    // Player 1 child, Player parent abstract
    {
        private int[] codeBreak = new int[4];                       
        private string name;
        public CodeBreakPlayer()
        {
            this.codeBreak = null;
            this.name = null;
        }
        public override void SetName(string name)                           //set player name override abstract
        {
            this.name = name;
        }
        public override string GetName()                                    //get player name override abstract
        {
          return this.name;
        }
        public override void SetCode(int[] codes)                           //set code override abstract
        {
            this.codeBreak = codes;
        }
        public override int[] GetCode()                          //get code override abstract
        {
            return this.codeBreak;
        }
    }
}
