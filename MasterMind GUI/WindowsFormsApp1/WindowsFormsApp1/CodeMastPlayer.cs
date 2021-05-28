using masterMindGUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace masterMindGUI
{
    public class CodeMastPlayer : Player                        //CodeMastPlayer child, Player parent abstract
    {
        private string name;
        private int[] codeMast = new int[4];

        public CodeMastPlayer()
        {
            this.name = null;
            this.codeMast = null;
        }

        public override void SetName(string name)               // set name codeMaster override abstract
        {
            this.name = name;
        }
        public override string GetName()                        //get name codeMaster  override abstract
        {
            return name;
        }
        public override void SetCode(int[] codeMast)           //set secret code override abstract
        {
            this.codeMast = codeMast;
        }
        public override int[] GetCode()                      // get secrect code override abstract
        {
            return this.codeMast;
        }
    }
}
