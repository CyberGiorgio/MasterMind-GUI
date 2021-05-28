using masterMindGUI;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;


namespace masterMindGUI
{
    public class Solution  : Pegs   //Solution child, Pegs parent
    {
        private int attempt;
        public Solution()
        {
            this.MatchingRed = null;
            this.MatchingWhite = null;
            this.redPeg = 0;
            this.whitePeg = 0;
            this.attempt = 0;
        }
        public int GetAttempt()                                       //get attempts
        {
            return this.attempt;
        }
        public void SetAttempt(int attempt)                          //set attempts
        {
            this.attempt = attempt;
        }
        public override int GetRedPeg()                           //get red pegs, hint for the player 
        {
            return redPeg;
        }
        public override int GetWhitePeg()                        //red pegs, hint for the player
        {
            return whitePeg;
        }
        public void CheckSolution(int[] codeMast, int[] codeBreak)         //check if solution is correct
        {
            redPeg = 0;
            whitePeg = 0;
            MatchingRed = new int[4];
            MatchingWhite = new int[4];

            for (int i = 0; i < 4; i++)                                       //red pegs, right colours and positions
            {
                if (codeBreak[i] == codeMast[i])
                {
                    MatchingRed[i] = codeBreak[i];
                    redPeg++;               //index found != -1; index not found == -1
                }
            }
            for (int i = 0; i < codeMast.Length; i++)                           //white pegs, right colours and wrong position
            {
                for (int j = 0; j < codeBreak.Length; j++)
                {
                    if (codeMast[i] == codeBreak[j])
                    {
                        MatchingWhite[i] = codeMast[i];     
                        whitePeg++;
                    }
                }
            }
            if (whitePeg > 4)
            {                                   //this part of the code could be useful in case we would like to use the exact number of pegs
                whitePeg = 4;                   //instead of their position
            }
            whitePeg -= redPeg;
            
            if (whitePeg < 0)
            {
                whitePeg = 0;
            }
        }
    }
}