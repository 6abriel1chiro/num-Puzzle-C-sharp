using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace numPuzzle
{


    class Puzzle
    {



    }

    public partial class Form1 : Form
    {

        Puzzle game;
        public Form1()
        {

            game = new Puzzle();
            InitializeComponent();
            label2.Text = "0 segundos";

        }
        private void timer_Tick(object sender, EventArgs e)
        {
            label2.Text =sw.Elapsed.Seconds.ToString() + " segundos";
            Application.DoEvents();
        }


        int movesNumber = 0, labelIndex = 0;
        Timer timer;
        Stopwatch sw;

        private void shuffleButtons()
        {
            if (label2.Text != "0 segundos")
            {
                timer.Stop();
                sw.Stop();
                label2.Text = "0 segundos";

            }
           


            List<int> labelList = new List<int>(); // 1 , 2, 3 ,4 ,5, 6,7 ,8, white

            Random rand = new Random();
            foreach (Button btn in this.pnl.Controls) // == for(int i=0:i<9;i++)
            {
                while (labelList.Contains(labelIndex))
                    labelIndex = rand.Next(9); //asigna un numero entre 1 y 9

                btn.Text = (labelIndex == 0) ? "" : labelIndex + ""; //if index = 0  index="" else index + "" 

                btn.BackColor = (btn.Text == "") ? Color.White : Color.FromKnownColor(KnownColor.ControlLight); //if n =="" color to white else color normal
                labelList.Add(labelIndex); //se llena la lista con el index
            }

            movesNumber = 0;
            lblNoOfMoves.Text = "Movimientos : " + movesNumber;
        }
     

        private void swapLabel(Object sender, EventArgs e)
        {
            if (label2.Text == "0 segundos")
            {
                timer = new Timer();
                timer.Interval = (1000);
                timer.Tick += new EventHandler(timer_Tick);
                sw = new Stopwatch();
                timer.Start();
                sw.Start();
            }

            Button btn = (Button)sender;
            if (btn.Text == "")
                return;

            Button whiteBtn = null;
            foreach (Button bt in this.pnl.Controls)
            {
                if (bt.Text == "")
                {
                    whiteBtn = bt;
                    break;
                }
            }

            if (btn.TabIndex == (whiteBtn.TabIndex - 1) ||  btn.TabIndex == (whiteBtn.TabIndex - 3) || btn.TabIndex == (whiteBtn.TabIndex + 1) || btn.TabIndex == (whiteBtn.TabIndex + 3))
            {
                whiteBtn.BackColor = Color.FromKnownColor(KnownColor.ControlLight); //white pasa a color normal
                btn.BackColor = Color.White;// color notmal pas a white
                whiteBtn.Text = btn.Text; //swap
                btn.Text = ""; //swap
                movesNumber++;
                lblNoOfMoves.Text = "movimientos : " + movesNumber;
            }

            checkOrder();
        }

        private void checkOrder()
        {
            int index = 1;
            foreach (Button btn in this.pnl.Controls)
            {
                if (btn.Text != "" && Convert.ToInt16(btn.Text) != index)
                {
                    return;

                }

                index++;
            }
            timer.Stop();
            sw.Stop();
            MessageBox.Show("Has ganado en  " + movesNumber + " movimientos" );
            label2.Text = "completado en " + sw.Elapsed.Seconds.ToString() + " segundos";

        }

        private void createGame_Click(object sender, EventArgs e)
        {


            movesNumber = 0;
            labelIndex = 0;
            shuffleButtons();

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            shuffleButtons();
        }
    }

}
   