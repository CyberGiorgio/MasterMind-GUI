using System;
using System.Collections.Generic;
using System.Text;

namespace masterMindGUI
{                             // abstract class and methods, base for CodeBreakerPlayer,codeMasterPlayer, codeMasterCOMPlayer
    public abstract class Player     
    {                                                        // methods must be initialized in each child class
        public int[] codes;
        public abstract int[] GetCode();
        public abstract void SetCode(int[] codes);
        public abstract string GetName();
        public abstract void SetName(string name);
    }
}
