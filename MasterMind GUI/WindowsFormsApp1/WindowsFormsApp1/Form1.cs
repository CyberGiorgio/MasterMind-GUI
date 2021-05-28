using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace masterMindGUI
{
    public partial class Form1 : Form
    {
        Player codeBreakPl;
        Player codeMastPl;
        
        private Button[] buttonDashboard;                   //Buttons codeBreaker
        private Button[] buttonMaster;                     //Buttons codeMater
        private PictureBox[]  pictureBoxes;             //hint pegs
       
        private int[] codeMast;
        private int[] codeBreak;
        private int attempt;
        private int attemptMax;
        private int iColour;                    //swap colour button
        private int ticks;                      //Time Counter

        private MessageBoxButtons caption;
        DialogResult dialogResult;

        private List<Image> imagePeg;               //image for big Pegs
        private List<Image> imageSmallRed;              //Image small pegs
        private List<Image> imageSmallWhite;            //Image small pegs

        public Form1()                                      //form Initialized
        {
            InitializeComponent();
            DialogResult dr = MessageBox.Show("Welcome back to the future, are you ready to play?", "MasterMind backToTheFuture", MessageBoxButtons.YesNo);
            if (dr == DialogResult.No)
            {
                Environment.Exit(0);
            }
            Lists();
            attempt = 0;
        }
        private void Start(object sender, EventArgs e)                     // button start 
        {
            SecretPanel.Visible = false;
            ConfigurePlayers();                                              //1 player, 2 players
            Level();                                                        //level: 1easy, 2medium, 3hard
            ConfirmButton.Enabled = true;
            timer1.Start();                                                 //start timer
        }
        private int Level()                                                     //level: 1easy, 2medium, 3hard
        {
            string levelStr;
            bool levelDifficult;
            do
            {
                levelStr = Interaction.InputBox("Choose a level: type 1 easy, 2 medium or 3 hard" +
                        "\n       1 for 10 attempts, " +
                        "\n         2 for 8 attempts, " +
                        "\n           3 for 6 attempts", "MasterMind backToThe Future: Level");
                switch (levelStr)
                {
                    case "1":                              //if you press 1 = 10 attempts/ rows
                        attemptMax = 10;
                        levelDifficult = true;
                        break;
                    case "2":                             //if you press 2 = 8 attempts/ rows
                        attemptMax = 8;
                        levelDifficult = true;
                        //end of loop
                        break;
                    case "3":                            //if you press 3 = 6 attempts/ rows
                        attemptMax = 6;
                        levelDifficult = true;
                        break;
                    default:                                 //levelDifficult false, keep looping and clear the previous choice, error
                        Debug.WriteLine("Input was not 1, 2 or 3");
                        MessageBox.Show("This is not a valid number, 1, 2 or 3?");
                        levelDifficult = false;
                        break;
                }
            } while (levelDifficult == false);                      

            return attemptMax;
        }
        private void ConfigurePlayers()                             //1 player VS COM, 1 player VS Player
        {
            StartButton.Enabled = false;
            string title = "MasterMind backToTheFuture";
            string messageNumPlayer = "How many players?";
            string messagePlayer1 = "Player 1 codeBreaker Input your name please, you will play VS COM";
            string errorName = "Empy name not valid";
            string numPlayerStr = "";

            bool numPlayers;
            bool namePlayerEmpty;
            bool namePlayerEmpty2;
            do
            {
                numPlayers = true;
                numPlayerStr = Interaction.InputBox(messageNumPlayer, title, numPlayerStr);         //interactionBox 1 or 2 Players

                codeMast = new int[4];
                codeBreakPl = new CodeBreakPlayer();
                if (numPlayerStr == "1")
                {
                    do
                    {
                        codeMastPl = new CodeMastCOMPlayer();                   //new codeMaster COM, random code
                        codeMastPl.SetName("COM");

                        namePlayerEmpty = true;
                        codeBreakPl.SetName(Interaction.InputBox(messagePlayer1, title));
                        if (codeBreakPl.GetName() == "")
                        {
                            Debug.WriteLine("Input was an empty string");
                            MessageBox.Show(errorName, title, caption, MessageBoxIcon.Error);          //error message: name cannot be empty string
                            namePlayerEmpty = false;
                        }
                    } while (namePlayerEmpty == false);

                    codeMast = Input.RandomCode();
                    for(int i = 0; i < codeMast.Length; i++)                    //generate a random masterCode
                    {
                        buttonMaster[i].Image = imagePeg[codeMast[i]];
                    }
                    codeMastPl.SetCode(codeMast);

                    button1.Enabled = true;                     // buttons enabled to start, first row 
                    button2.Enabled = true;
                    button3.Enabled = true;
                    button4.Enabled = true;
                }
                else if (numPlayerStr == "2")                           //2 players
                {
                    codeMastPl = new CodeMastPlayer();                  // new codeMaster human
                    
                    numPlayers = true;
                    do
                    {
                        namePlayerEmpty = true;
                        codeBreakPl.SetName(Interaction.InputBox(messagePlayer1, title));
                        if (codeBreakPl.GetName() == "")
                        {
                            Debug.WriteLine("Input was an empty string");
                            MessageBox.Show(errorName, title, caption, MessageBoxIcon.Error);     //error message: name cannot be empty string
                            namePlayerEmpty = false;
                        }
                    } while (namePlayerEmpty == false);

                    string messagePlayer2 = "Player 2 codeMaker Input your name, and choose your secret code in the blue box below please";
                    do
                    {
                        namePlayerEmpty2 = true;
                        codeMastPl.SetName(Interaction.InputBox(messagePlayer2, title));
                        if (codeMastPl.GetName() == "")
                        {
                            Debug.WriteLine("Input was an empty string");
                            MessageBox.Show(errorName, title, caption, MessageBoxIcon.Error);              //error message: name cannot be empty string
                            namePlayerEmpty2 = false;
                        }
                    } while (namePlayerEmpty2 == false);
                    
                    SecretPanel.Visible = true;
                    ConfirmButton.Visible = false;
                    SecretPanel.BringToFront();
                    button48.Enabled = true;                     // buttons enabled to start
                    button49.Enabled = true;
                    button50.Enabled = true;
                    button51.Enabled = true;
                    ConfirmSecretCode.Enabled = true;               //codeMaster can input his secret code
                }
                else
                {
                    string error = "Invalid number of players";
                    Debug.WriteLine("Input was not 1 or 2");
                    MessageBox.Show(error, title, caption, MessageBoxIcon.Error);               //must press 1 or 2 number of players
                    numPlayers = false;
                }
            } while (numPlayers == false);
            Player1Label.Text = codeBreakPl.GetName();              //chenge of label names
            Player2Label.Text = codeMastPl.GetName();
        }
        private void TimerC(object sender, EventArgs e)                 //Timer Counter
        {
            TimerCounter.Text = ticks.ToString();
            ticks++;
        }
        private bool SwitchButtons(int attempt)  {                              //Method Enable/Disable buttons 4 by 4 (4 x 10)
            for (int i = 4 * attempt; i < 4 * attempt + 4; i++)
            {                                                                   
                if(attempt == attemptMax)
                {
                    return false;
                }
                buttonDashboard[i].Enabled = !buttonDashboard[i].Enabled;
            }
            return true;
        }
        private void ConfirmSecretCode_Click(object sender, EventArgs e)                //input secret code CodeMaster
        {
            for (int i = 0; i < codeMast.Length; i++)
            {
                if (buttonMaster[i].Tag == null)
                {
                    Debug.WriteLine("At least 1 peg was still gray");
                    MessageBox.Show("Select all colours, Gray not valid!");
                    return;
                }
                codeMast[i] = Int32.Parse(buttonMaster[i].Tag.ToString());
            }
            codeMastPl.SetCode(codeMast);

            button1.Enabled = true;                     // buttons enabled to start, first row + change image buttons
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            SecretPanel.Visible = false;
            SecretPanel.SendToBack();
            ConfirmButton.Visible = true;
        }
        private void Confirm(object sender, EventArgs e)                        //confirm button CodeBreaker
        {
            NumberAttempt.Text = (attemptMax - attempt - 1).ToString();
            for (int i = 4 * attempt; i < 4 * attempt + 4; i++)
            {
                if (buttonDashboard[i].Tag == null)                                      //cannot leave buttons gray, null not accepted
                {
                    Debug.WriteLine("At least 1 peg was still gray");
                    MessageBox.Show("Select all colours, Gray not valid!");
                    return;
                }
            }
            codeBreak = new int[4];
            for (int i = 4 * attempt; i < 4 * attempt + 4; i++)                             // store codeBreak guess
            {
                codeBreak[i-4 * attempt]=Int32.Parse(buttonDashboard[i].Tag.ToString());
            }
            codeBreakPl.SetCode(codeBreak);
            SwitchButtons(attempt);                 //switch rows button

            Solution solution = new Solution(); 
            solution.CheckSolution (codeBreak, codeMast);  //compare secret code to codeBreak code
            for (int i = 4 * attempt; i < 4 * attempt + 4; i++)
            {
                if (solution.MatchingWhite[i - 4 * attempt] != 0)                 
                {
                    pictureBoxes[i].Image = imageSmallWhite[solution.MatchingWhite[i - 4 * attempt]];         //white Pegs displayed
                }
                if (solution.MatchingRed[i - 4 * attempt] != 0)
                {
                    pictureBoxes[i].Image = imageSmallRed[solution.MatchingRed[i - 4 * attempt]];         //red Pegs displayed
                }
            }

            attempt++;

            SwitchButtons(attempt);                                 //switch rows button
            
            if (attempt == attemptMax)                           //looser, no attempt left
            {
                SecretPanel.Show();
                SecretPanel.BringToFront();
                timer1.Stop();
                LabelSecretCode.Text = "SecretCode was ----->";
                for(int i = 0; i < codeMast.Length; i++)            //show secret code to the codeBreaker
                {
                    buttonMaster[i].Enabled = true;
                }
                dialogResult = MessageBox.Show(codeBreakPl.GetName() + " lost in " + TimerCounter.Text + " seconds, Would you like to play again?", 
                    "MasterMind BackToTheFuture", MessageBoxButtons.YesNo);
                FinalQuestion();
            }
            else if (solution.GetRedPeg() == 4)                 //winner, 4 redPeg
            {
                timer1.Stop();
                dialogResult = MessageBox.Show("You are a MasterMind, "+ codeBreakPl.GetName() + " won in "+ TimerCounter.Text + " seconds, Would you like to play again??",
                    "MasterMind BackToTheFuture", MessageBoxButtons.YesNo);
                FinalQuestion();
            }
        }
        private void FinalQuestion()
        {
            if (dialogResult == DialogResult.Yes)
            {
                Restarter();                                    //restart the game
            }
            else
            {
                Application.Exit();                                 //close the game
            }
        }
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)         //exit from stripMenu
        {
            System.Environment.Exit(0);
        }
        public void Restarter()                                         //restarter function, reset all settings
        {
            foreach (Button button in buttonDashboard)
            {
                button.Image = Properties.Resources.gray;
                button.Enabled = false;
            }
            foreach (Button buttonMaster in buttonMaster)
            {
                buttonMaster.Image = Properties.Resources.gray;
            }
            for (int i = 0; i < 40; i++)
            {
                pictureBoxes[i].Image = Properties.Resources.graySmall;
            }
            SecretPanel.Visible = false;
            StartButton.Enabled = true;
            ConfirmButton.Enabled = false;
            LabelSecretCode.Text = "Choose SecretCode ----->";
            Player1Label.Text = "Player1";
            Player2Label.Text = "Player2";
            TimerCounter.Text = "Timer";
            NumberAttempt.Text = "";
            
            attempt = 0;
            ticks = 0;

            timer1.Stop();
        }
        private void NewGameToolStripMenuItem_Click(object sender, EventArgs e)                 //stripMenu new Game
        {
            Restarter();
        }
        private void Lists()                             //Array Lists
        {
            imagePeg = new List<Image>                                  //list button colours
            {
                Properties.Resources.blue,
                Properties.Resources.green,
                Properties.Resources.darkGreen,
                Properties.Resources.lightBlue,
                Properties.Resources.orange,
                Properties.Resources.pink,
                Properties.Resources.purple,
                Properties.Resources.yellow
            };
            imageSmallRed = new List<Image>                             //list hint redPegs
            {
                Properties.Resources.redSmall,
                Properties.Resources.redSmall,
                Properties.Resources.redSmall,
                Properties.Resources.redSmall,
                Properties.Resources.redSmall,
                Properties.Resources.redSmall,
                Properties.Resources.redSmall,
                Properties.Resources.redSmall
            };
            imageSmallWhite = new List<Image>                           //list hint whitePegs
            {
                Properties.Resources.Smallwhite,
                Properties.Resources.Smallwhite,
                Properties.Resources.Smallwhite,
                Properties.Resources.Smallwhite,
                Properties.Resources.Smallwhite,
                Properties.Resources.Smallwhite,
                Properties.Resources.Smallwhite,
                Properties.Resources.Smallwhite
            };
            pictureBoxes = new PictureBox[]                             //pictureBox hint pegs, red and white 4 x 10
            {
               pictureBox1,
               pictureBox2,
               pictureBox3,
               pictureBox4,
               pictureBox5,
               pictureBox6,
               pictureBox7,
               pictureBox8,
               pictureBox9,
               pictureBox10,
               pictureBox11,
               pictureBox12,
               pictureBox13,
               pictureBox14,
               pictureBox15,
               pictureBox16,
               pictureBox17,
               pictureBox18,
               pictureBox19,
               pictureBox20,
               pictureBox21,
               pictureBox22,
               pictureBox23,
               pictureBox24,
               pictureBox25,
               pictureBox26,
               pictureBox27,
               pictureBox28,
               pictureBox29,
               pictureBox30,
               pictureBox31,
               pictureBox32,
               pictureBox33,
               pictureBox34,
               pictureBox35,
               pictureBox36,
               pictureBox37,
               pictureBox38,
               pictureBox39,
               pictureBox40,
            };
            buttonDashboard = new Button []                          //array buttons Dashboard 4 x 10
            {
                button1,
                button2,
                button3,
                button4,
                button5,
                button6,
                button7,
                button8,
                button9,
                button10,
                button11,
                button12,
                button13,
                button14,
                button15,
                button16,
                button17,
                button18,
                button19,
                button20,
                button21,
                button22,
                button23,
                button24,
                button25,
                button26,
                button27,
                button28,
                button29,
                button30,
                button31,
                button32,
                button33,
                button34,
                button35,
                button36,
                button37,
                button38,
                button39,
                button40,
            };
            buttonMaster = new Button[]                     //button codeMaster
            {
              button48,
              button49,
              button50,
              button51
            };
        }
        private void SwapColour(object sender, MouseEventArgs e)            //swap button colour + 1 circle
        {
            Button btn;
            btn = (Button)sender;

            iColour = (iColour + 1) % 8;

            btn.Image = imagePeg[iColour];
            btn.Tag = iColour;
        }
        private void SwapColourRightClick(object sender, MouseEventArgs e)              //swap colour -1 circle
        {
            Button btn;
            btn = (Button)sender;
            if (e.Button == MouseButtons.Right)
            {
                iColour = (7 - -iColour) % (7 + 1);

                btn.Image = imagePeg[iColour];
                btn.Tag = iColour;
            }
        }
        private void BackButtonRules(object sender, EventArgs e)
        {
            panelRules.SendToBack();
            panelRules.Visible = false;
        }

        private void ButtonRules(object sender, EventArgs e)
        {
            panelRules.BringToFront();
            panelRules.Visible = true;
        }
    }
}