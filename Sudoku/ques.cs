using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku
{
    public partial class ques : Form
    {
        int[,] ans = new int[9, 9];
        int[,] problem = new int[9, 9];
        PictureBox[,] pb = new PictureBox[9, 9];
        public ques()
        {
            InitializeComponent();
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Owner.Show();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            for (int column = 0; column < tableLayoutPanel1.ColumnCount; column++)
            {
                for (int row = 0; row < tableLayoutPanel1.RowStyles.Count; row++)
                {
                    pb[column, row] = new System.Windows.Forms.PictureBox();
                    pb[column, row].BorderStyle = BorderStyle.FixedSingle;
                    pb[column, row].Width = 41;
                    pb[column, row].Height = 41;
                    tableLayoutPanel1.Controls.Add(pb[column, row]);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Sudoku su = new Sudoku(ref ans);
            su.question_bulid(1, ref problem);
            print_sudoku(problem);
        }
        public void print_sudoku(int[,] problem)
        {
            int i = 0;
            int j = 0;
            for (i = 0; i < 9; i++)
            {
                for (j = 0; j < 9; j++)
                {
                    if (problem[i, j] == 0)
                        pb[i, j].Image = null;

                    if (problem[i, j] == 1)
                        pb[i, j].Image = imageList2.Images[0];

                    if (problem[i, j] == 2)
                        pb[i, j].Image = imageList2.Images[1];

                    if (problem[i, j] == 3)
                        pb[i, j].Image = imageList2.Images[2];

                    if (problem[i, j] == 4)
                        pb[i, j].Image = imageList2.Images[3];

                    if (problem[i, j] == 5)
                        pb[i, j].Image = imageList2.Images[4];

                    if (problem[i, j] == 6)
                        pb[i, j].Image = imageList2.Images[5];

                    if (problem[i, j] == 7)
                        pb[i, j].Image = imageList2.Images[6];

                    if (problem[i, j] == 8)
                        pb[i, j].Image = imageList2.Images[7];

                    if (problem[i, j] == 9)
                        pb[i, j].Image = imageList2.Images[8];

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Sudoku su = new Sudoku(ref ans);
            su.question_bulid(2, ref problem);
            print_sudoku(problem);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Sudoku su = new Sudoku(ref ans);
            su.question_bulid(3, ref problem);
            print_sudoku(problem);
        }

        private void button4_Click(object sender, EventArgs e   )
        {
            Sudoku su = new Sudoku(ref ans);
            su.question_solve();
            print_sudoku(ans);
        }
    }
}
