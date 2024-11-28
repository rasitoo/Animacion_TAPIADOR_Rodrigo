namespace Animacion_TAPIADOR_Rodrigo;

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class AnimatedForm : Form
{
    private Timer timer;
    private Random random;
    private Color colorForma;
    private int x, y; // Coordenadas de la figura
    private int xMovimiento = 4, yMovimiento = 4; // Pixeles que se mueve por tick
    private int tamanoForma = 50;

    public AnimatedForm()
    {
        this.Text = "Animación";
        this.ClientSize = new Size(500, 500);
        this.BackColor = Color.White;

        timer = new Timer();
        timer.Interval = 16; // Frecuencia de actualización
        timer.Tick += Timer_Tick;
        timer.Start();

        random = new Random();
        colorForma = Color.Blue;
        this.DoubleBuffered = true;

        this.MouseClick += AnimatedForm_MouseClick;
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
        // Añade los pixeles que se mueve
        x += xMovimiento;
        y += yMovimiento;

        // Rebote en los bordes
        if (x <= 0 || x + tamanoForma >= this.ClientSize.Width)
            xMovimiento = -xMovimiento;
        if (y <= 0 || y + tamanoForma >= this.ClientSize.Height)
            yMovimiento = -yMovimiento;

        this.Invalidate(); // Redibuja

    }

    private void AnimatedForm_MouseClick(object sender, MouseEventArgs e)
    {
        // Cambia el color de la figura a uno aleatorio
        colorForma = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
    }

    protected override void OnPaint(PaintEventArgs g)
    {
        base.OnPaint(g);

        g.Graphics.SmoothingMode = SmoothingMode.AntiAlias; //suavizado de bordes

        using (SolidBrush brush = new SolidBrush(colorForma))
        {
            g.Graphics.FillEllipse(brush, x, y, tamanoForma, tamanoForma);
        }
    }

}
