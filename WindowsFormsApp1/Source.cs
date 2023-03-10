using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

abstract class Piesa
{
    protected int x, y, w, h;
    protected Image img;



    public int X
    {
        get { return x; }
        set { this.x = value; }
    }
    public int Y
    {
        get { return y; }
        set { this.y = value; }
    }
    public int W
    {
        get { return w; }
        set { this.w = value; }
    }
    public int H
    {
        get { return h; }
        set { this.h = value; }
    }
    public bool este_deasupra(int x, int y)
    {
        if (x < this.x) return false;
        if (x > this.x + w) return false;
        if (y < this.y) return false;
        if (y > this.y + h) return false;
        return true;
    }
    public void deseneaza(Graphics g)
    {
        g.DrawImage(img, x, y, w, h);
    }
}
class PionA : Piesa
{
    public PionA(int x, int y, int w, int h)
    {
        this.x = x;
        this.y = y;
        this.w = w;
        this.h = h;
        this.img = Image.FromFile("pion.gif");
    }
}
class PionN : Piesa
{
    public PionN(int x, int y, int w, int h)
    {
        this.x = x;
        this.y = y;
        this.w = w;
        this.h = h;
        this.img = Image.FromFile("pionN.gif");
    }
}
class RegeA : Piesa
{
    public RegeA(int x, int y, int w, int h)
    {
        this.x = x;
        this.y = y;
        this.w = w;
        this.h = h;
        this.img = Image.FromFile("rege.png");
    }
}

class JocSah
{
    int w, h, l;
    Graphics g;
    List<Piesa> piese;
    public JocSah(int w,int h, Graphics g)
    {
        this.w = w;
        this.h = h;
        this.g = g;
        l = w / 10;
        piese = new List<Piesa>();
        for(int i = 0; i < 8; i++)
        {
            piese.Add(new PionA(0, 0, l / 2, l * 2 / 3));
            
        }
        for (int i = 0; i < 8; i++)
        {
            piese.Add(new PionN(0, 0, l / 2, l * 2 / 3));
        }
        piese.Add(new RegeA(0, 0, l * 2 / 3, l * 2 / 3));
    }

    public void aseazaPiesa(Piesa p,int x,int y)
    {
        int lin = y / l, col=x/l;
        p.X = col * l + l / 2 - p.W / 2;
        p.Y = lin * l + l / 2 - p.H / 2;
    }

    void aseazaPiese()
    {
        for(int i = 0; i < 8; i++)
        {
            aseazaPiesa(piese[i],l+i*l,2*l+l/2);
        }
        for(int i = 0; i < 8; i++)
        {
            aseazaPiesa(piese[8 + i], l + i * l, 7*l+ l/2);
        }
        aseazaPiesa(piese[16], l + 4 * l, l + l / 2);
    }
    void deseneaza_tabla_sah()
    {
        g.Clear(Color.White);
        Pen creion = new Pen(Color.Black, 2);
        Brush col1 = Brushes.Chocolate, col2 = Brushes.SandyBrown;
        System.Drawing.Font f = new Font("Times New Roman", 12);
        StringFormat sf = new StringFormat();
        sf.Alignment = StringAlignment.Center;
        sf.LineAlignment = StringAlignment.Center;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                g.FillRectangle((i + j) % 2 == 0 ? col1 : col2, l + i * l, l + j * l, l, l);
            }
        }
        for (int i = 0; i <= 8; i++)
        {

            g.DrawLine(creion, l, l + i * l, 9 * l, l + i * l);
            g.DrawLine(creion, l + i * l, l, l + i * l, 9 * l);

        }
        for (int i = 0; i < 8; i++)
        {
            g.DrawString((i + 1).ToString(), f, Brushes.Black, new Rectangle(l / 2, l + i * l, l / 2, l), sf);
        }
        for (int i = 0; i < 8; i++)
        {
            g.DrawString(((char)(i + 'a')).ToString(), f, Brushes.Black, new Rectangle(l + i * l, 9 * l, l, l / 2), sf);
        }
    }

    public void deseneaza()
    {
        deseneaza_tabla_sah();
        foreach(Piesa p in piese)
        {
            p.deseneaza(g);
        }
    }

    public void jocNou()
    {
        aseazaPiese();
        deseneaza();
    }

    public Piesa daPiesa(int x, int y)
    {
        foreach(Piesa p in piese)
        {
            if (p.este_deasupra(x, y))
                return p;
          
        }
        return null;
    }
}
