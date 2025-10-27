using System;

namespace Laboratorio12
{
    public partial class Form1 : Form
    {
        CalculoDistancia calculo = new CalculoDistancia();
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblVelocidad_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            try
            {
                double v = double.Parse(txtVelocidad.Text);
                double t = double.Parse(txtTiempo.Text);
                double d = calculo.CalculDistancia(v, t);

                txtResul.Text = d.ToString() + "km";
            }
            catch 
            {
                MessageBox.Show("Ingresar valores numéricos validos");

            }
        }


        private void btnBorrador_Click(object sender, EventArgs e)
        {
            txtVelocidad.Text = txtTiempo.Text = txtResul.Text = string.Empty;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

public class CalculoDistancia
{
    public double CalculDistancia(double velocidad, double tiempo)
    {
        return velocidad * tiempo;
    }
}


