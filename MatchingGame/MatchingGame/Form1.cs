using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatchingGame
{
    public partial class Form1 : Form
    {
        Label firstClicked = null;
        Label secondClicked = null;

        //Choose random icons for the squares
        Random random = new Random();

        //Choose icons in the Webdings font to use for the game
        List<string> icons = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "k", "k",
            "b", "b", "v", "v", "w", "w", "z", "z"
        };

        public Form1()
        {
            InitializeComponent();
            AssignIconsToSquares();
        }

        //Assign icons to random squares
        private void AssignIconsToSquares()
        {
            foreach(Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if(iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    iconLabel.ForeColor = iconLabel.BackColor;
                    icons.RemoveAt(randomNumber);
                }
            }
            
        }

        private void label_Click(object sender, EventArgs e)
        {
            //ignore any inputs when the timer is running
            if(timer1.Enabled == true)
            {
                return;
            }

            Label clickedLabel = sender as Label;

            //ignore inputs if the player has already clicked twice
            if(secondClicked != null)
            {
                return;
            }

            if(clickedLabel != null)
            {
                //Ignore already clicked(black) labels
                if(clickedLabel.ForeColor == Color.Black)
                {
                    return;
                }

                //determine the order the labels have been clicked in and reveal the first choice
                if(firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;

                    return;
                }

                //reveal the second choice
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                CheckForWinner();

                //keep matching pairs visible
                if(firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }

                timer1.Start();
            }
        }

        //timer until revealed icons are hidden again
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            //Hide both icons
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            //reset firstClicked and secondClicked
            firstClicked = null;
            secondClicked = null;
        }

        private void CheckForWinner()
        {
            foreach(Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                //check if all labels have been matched
                if(iconLabel != null)
                {
                    if(iconLabel.ForeColor == iconLabel.BackColor)
                    {
                        return;
                    }
                }
            }

            MessageBox.Show("You matched all the icons!", "Congratulations");
            Close();
        }
    }
}
