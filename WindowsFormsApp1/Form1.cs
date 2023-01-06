using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Sah : Form
    {
        Image img;
        Graphics g;
        JocSah joc;
        int dx,dy;
        bool apasat;
        Piesa piesa;

        public Sah()
        {
            InitializeComponent();
        }

        private void p_Click(object sender, EventArgs e)
        {

        }

        private void iesireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void despreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Aplicatie realizata de Robert Dănilă");
        }

        private void deseneazaTablaDeSahToolStripMenuItem_Click(object sender, EventArgs e)
        {
            joc.jocNou();
            p.Refresh();
        }

    

        private void p_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(img, 0, 0);
        }

        private void Sah_Load(object sender, EventArgs e)
        {
            img = new Bitmap(p.Width, p.Height);
            g = Graphics.FromImage(img);
            joc = new JocSah(p.Width, p.Height, g);
            apasat = false;

        }

        private void p_MouseMove(object sender, MouseEventArgs e)
        {
            if (apasat)
            {
                piesa.X = e.X - dx;
                piesa.Y = e.Y - dy;
                joc.deseneaza();
                p.Refresh();
            
            }
        }

        private void p_MouseUp(object sender, MouseEventArgs e)
        { 
            p.Cursor = Cursors.Default;
            if (apasat)
            {
                joc.aseazaPiesa(piesa, e.X, e.Y);
                joc.deseneaza();
                p.Refresh();
            }
            apasat = false;
        }


        private void p_MouseDown(object sender, MouseEventArgs e)
        {
            piesa = joc.daPiesa(e.X, e.Y);
            if (piesa != null)
            {
                apasat = true;
                p.Cursor = Cursors.Hand;
                dx = e.X - piesa.X;
                dy = e.Y - piesa.Y;

            }
        }
    }
}
