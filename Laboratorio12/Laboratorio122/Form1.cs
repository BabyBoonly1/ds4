using System;

namespace Laboratorio122
{
    public partial class Form1 : Form
    {
        CalcularPromedio calcular = new CalcularPromedio();
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnPromedio_Click(object sender, EventArgs e)
        {
            try
            {
                double n1 = Convert.ToDouble(txtNota1.Text);
                double n2 = Convert.ToDouble(txtNota2.Text);
                double n3 = Convert.ToDouble(txtNota3.Text);

                double promedio = calcular.Promedio(n1, n2, n3);
                txtResul.Text = Convert.ToString(promedio);
            }
            catch
            {
                MessageBox.Show("Ingrese valores numéricos válidos para las notas.", "Error de entrada");
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            txtNota1.Text = txtNota2.Text = txtNota3.Text = txtResul.Text = string.Empty;
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

public class CalcularPromedio
{
    public double Promedio(double n1, double n2, double n3)
    {
        return (n1 + n2 + n3) / 3;
    }
}
