using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku
{
    public partial class solve : Form
    {
        PictureBox[,] pb = new PictureBox[9,9];
        int[,] Sudoku = new int[9, 9];
        bool flag = false;
        Image pic;
        public solve()
        {
            InitializeComponent();

        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Owner.Show();
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            int i = 0;
            for(i = 82; i <= 90; i++)
            {
                (Controls["pictureBox" + i.ToString()] as PictureBox).Image = imageList1.Images[i - 82];
            }

            for (int column = 0; column < tableLayoutPanel1.ColumnCount; column++)
            {
                for (int row = 0; row < tableLayoutPanel1.RowStyles.Count; row++)
                {
                    pb[column,row] = new System.Windows.Forms.PictureBox();
                    pb[column,row].BorderStyle = BorderStyle.FixedSingle;
                    pb[column,row].Width = 41;
                    pb[column,row].Height = 41;
                    pb[column,row].Click += all_click;
                    tableLayoutPanel1.Controls.Add(pb[column,row]);
                }
            }
        }

        private void Form2_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void all_click(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;

            if (flag)
            {
                pb.Image = pic;
            }
                
        }

        private void pictureBox82_Click(object sender, EventArgs e)
        {
            flag = false;
            pic = imageList2.Images[0];
            flag = true;
        }
        private void pictureBox83_Click(object sender, EventArgs e)
        {
            flag = false;
            pic = imageList2.Images[1];
            flag = true;
        }

        private void pictureBox84_Click(object sender, EventArgs e)
        {
            flag = false;
            pic = imageList2.Images[2];
            flag = true;
        }

        private void pictureBox86_Click(object sender, EventArgs e)
        {
            flag = false;
            pic = imageList2.Images[4];
            flag = true;
        }

        private void pictureBox85_Click(object sender, EventArgs e)
        {
            flag = false;
            pic = imageList2.Images[3];
            flag = true;
        }

        private void pictureBox87_Click(object sender, EventArgs e)
        {
            flag = false;
            pic = imageList2.Images[5];
            flag = true;
        }

        private void pictureBox88_Click(object sender, EventArgs e)
        {
            flag = false;
            pic = imageList2.Images[6];
            flag = true;
        }
        private void pictureBox89_Click(object sender, EventArgs e)
        {
            flag = false;
            pic = imageList2.Images[7];
            flag = true;
        }
        private void pictureBox90_Click(object sender, EventArgs e)
        {
            flag = false;
            pic = imageList2.Images[8];
            flag = true;
        }

        private void pictureBox91_Click(object sender, EventArgs e)
        {
            flag = false;
            pic = null;
            flag = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = 0;
            int j = 0;
            
            for (i = 0; i < 9; i++)
            {
                for (j = 0; j < 9; j++)
                {
                    if (pb[i,j].Image == null)
                    {
                        Sudoku[i,j] = 0;
                        continue;
                    }
                    if (Same(pb[i, j].Image, imageList2.Images[0]))
                        Sudoku[i, j] = 1;
                    if (Same(pb[i, j].Image, imageList2.Images[1]))
                        Sudoku[i, j] = 2;
                    if (Same(pb[i, j].Image, imageList2.Images[2]))
                        Sudoku[i, j] = 3;
                    if (Same(pb[i, j].Image, imageList2.Images[3]))
                        Sudoku[i, j] = 4;
                    if (Same(pb[i, j].Image, imageList2.Images[4]))
                        Sudoku[i, j] = 5;
                    if (Same(pb[i, j].Image, imageList2.Images[5]))
                        Sudoku[i, j] = 6;
                    if (Same(pb[i, j].Image, imageList2.Images[6]))
                        Sudoku[i, j] = 7;
                    if (Same(pb[i, j].Image, imageList2.Images[7]))
                        Sudoku[i, j] = 8;
                    if (Same(pb[i, j].Image, imageList2.Images[8]))
                        Sudoku[i, j] = 9;
                }
            }

            Sudoku su = new Sudoku(ref Sudoku);
            su.question_solve();
            for (i = 0; i < 9; i++)
            {
                for (j = 0; j < 9; j++)
                {
                    if (Sudoku[i, j] == 0)
                        pb[i,j].Image = null;

                    if (Sudoku[i, j] == 1)
                        pb[i,j].Image = imageList2.Images[0];

                    if (Sudoku[i, j] == 2)
                        pb[i, j].Image = imageList2.Images[1];

                    if (Sudoku[i, j] == 3)
                        pb[i, j].Image = imageList2.Images[2];

                    if (Sudoku[i, j] == 4)
                        pb[i, j].Image = imageList2.Images[3];

                    if (Sudoku[i, j] == 5)
                        pb[i, j].Image = imageList2.Images[4];

                    if (Sudoku[i, j] == 6)
                        pb[i, j].Image = imageList2.Images[5];

                    if (Sudoku[i, j] == 7)
                        pb[i, j].Image = imageList2.Images[6];

                    if (Sudoku[i, j] == 8)
                        pb[i, j].Image = imageList2.Images[7];

                    if (Sudoku[i, j] == 9)
                        pb[i, j].Image = imageList2.Images[8];

                }
            }


        }

        private bool Same(Image image1, Image image2)

        {
            MemoryStream ms1 = new MemoryStream();
            MemoryStream ms2 = new MemoryStream();
            image1.Save(ms1, System.Drawing.Imaging.ImageFormat.Bmp);
            image2.Save(ms2, System.Drawing.Imaging.ImageFormat.Bmp);
            byte[] im1 = ms1.GetBuffer();
            byte[] im2 = ms2.GetBuffer();
            if (im1.Length != im2.Length)
                return false;
            else
            {
                for (int i = 0; i < im1.Length; i++)
                    if (im1[i] != im2[i])
                        return false;
            }
            return true;
        }

    }
}
