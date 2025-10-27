using System;
namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        CalculoTriangulo calculos = new CalculoTriangulo();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSemi_Click(object sender, EventArgs e)
        {
            try
            {
                double a = Convert.ToDouble(txtLadoA.Text);
                double b = Convert.ToDouble(txtLadoB.Text);
                double c = Convert.ToDouble(txtLadoC.Text);

                double semi = calculos.SemiPerimetro(a, b, c);
                txtResulSemi.Text = Convert.ToString(semi);
            }
            catch
            {
                MessageBox.Show("Introduzca valores númericos validos");
            }
        }

        private void btnArea_Click(object sender, EventArgs e)
        {
            try
            {
                double a = Convert.ToDouble(txtLadoA.Text);
                double b = Convert.ToDouble(txtLadoB.Text);
                double c = Convert.ToDouble(txtLadoC.Text);

                double area = calculos.Area(a, b, c);
                txtResulArea.Text = Convert.ToString(area);
            }
            catch
            {
                MessageBox.Show("Introduzca valores númericos validos");
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            txtLadoA.Text = txtLadoB.Text = txtLadoC.Text = string.Empty;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

public class CalculoTriangulo
{
    
    public double SemiPerimetro(double a, double b, double c)
    {
        return (a + b + c) / 2;
    }

    public double Area(double a, double b, double c)
    {
        return a + b + c;
    }


}
