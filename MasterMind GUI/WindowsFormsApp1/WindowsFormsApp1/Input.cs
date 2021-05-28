using masterMindGUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace masterMindGUI
{
    public static class Input { 
        static public int[] RandomCode()
        {
           int[] codeMast;
           codeMast = new int[4];
            Random rnd;
            rnd = new Random();
            for (int i = 0; i < 4; i++)                           // random secret code of 4 numbers between 1 and 8                                 
            {
                codeMast[i] = rnd.Next(1, 8);
                // random numbers from 1 to 8
            }
            return codeMast;
        }
    }
}
