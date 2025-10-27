namespace Laboratorio122
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblTitulo = new Label();
            lblNota1 = new Label();
            lblNota2 = new Label();
            lblNota3 = new Label();
            lblPromedioTotal = new Label();
            btnPromedio = new Button();
            btnBorrar = new Button();
            btnSalir = new Button();
            txtNota1 = new TextBox();
            txtNota2 = new TextBox();
            txtNota3 = new TextBox();
            txtResul = new TextBox();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(106, 40);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(170, 20);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Calculador de Promedio";
            // 
            // lblNota1
            // 
            lblNota1.AutoSize = true;
            lblNota1.Location = new Point(31, 88);
            lblNota1.Name = "lblNota1";
            lblNota1.Size = new Size(65, 20);
            lblNota1.TabIndex = 1;
            lblNota1.Text = "Nota N1";
            // 
            // lblNota2
            // 
            lblNota2.AutoSize = true;
            lblNota2.Location = new Point(31, 156);
            lblNota2.Name = "lblNota2";
            lblNota2.Size = new Size(65, 20);
            lblNota2.TabIndex = 2;
            lblNota2.Text = "Nota N2";
            // 
            // lblNota3
            // 
            lblNota3.AutoSize = true;
            lblNota3.Location = new Point(31, 226);
            lblNota3.Name = "lblNota3";
            lblNota3.Size = new Size(65, 20);
            lblNota3.TabIndex = 3;
            lblNota3.Text = "Nota N3";
            // 
            // lblPromedioTotal
            // 
            lblPromedioTotal.AutoSize = true;
            lblPromedioTotal.Location = new Point(31, 347);
            lblPromedioTotal.Name = "lblPromedioTotal";
            lblPromedioTotal.Size = new Size(111, 20);
            lblPromedioTotal.TabIndex = 4;
            lblPromedioTotal.Text = "Promedio Total";
            // 
            // btnPromedio
            // 
            btnPromedio.Location = new Point(12, 295);
            btnPromedio.Name = "btnPromedio";
            btnPromedio.Size = new Size(94, 29);
            btnPromedio.TabIndex = 5;
            btnPromedio.Text = "Promedio";
            btnPromedio.UseVisualStyleBackColor = true;
            btnPromedio.Click += btnPromedio_Click;
            // 
            // btnBorrar
            // 
            btnBorrar.Location = new Point(147, 295);
            btnBorrar.Name = "btnBorrar";
            btnBorrar.Size = new Size(94, 29);
            btnBorrar.TabIndex = 6;
            btnBorrar.Text = "Borrar ";
            btnBorrar.UseVisualStyleBackColor = true;
            btnBorrar.Click += btnBorrar_Click;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(283, 295);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(94, 29);
            btnSalir.TabIndex = 7;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // txtNota1
            // 
            txtNota1.Location = new Point(167, 81);
            txtNota1.Name = "txtNota1";
            txtNota1.Size = new Size(125, 27);
            txtNota1.TabIndex = 8;
            txtNota1.TextChanged += textBox1_TextChanged;
            // 
            // txtNota2
            // 
            txtNota2.Location = new Point(167, 149);
            txtNota2.Name = "txtNota2";
            txtNota2.Size = new Size(125, 27);
            txtNota2.TabIndex = 9;
            txtNota2.TextChanged += textBox2_TextChanged;
            // 
            // txtNota3
            // 
            txtNota3.Location = new Point(167, 219);
            txtNota3.Name = "txtNota3";
            txtNota3.Size = new Size(125, 27);
            txtNota3.TabIndex = 10;
            txtNota3.TextChanged += textBox3_TextChanged;
            // 
            // txtResul
            // 
            txtResul.Location = new Point(167, 340);
            txtResul.Name = "txtResul";
            txtResul.Size = new Size(125, 27);
            txtResul.TabIndex = 11;
            txtResul.TextChanged += textBox4_TextChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(389, 409);
            Controls.Add(txtResul);
            Controls.Add(txtNota3);
            Controls.Add(txtNota2);
            Controls.Add(txtNota1);
            Controls.Add(btnSalir);
            Controls.Add(btnBorrar);
            Controls.Add(btnPromedio);
            Controls.Add(lblPromedioTotal);
            Controls.Add(lblNota3);
            Controls.Add(lblNota2);
            Controls.Add(lblNota1);
            Controls.Add(lblTitulo);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitulo;
        private Label lblNota1;
        private Label lblNota2;
        private Label lblNota3;
        private Label lblPromedioTotal;
        private Button btnPromedio;
        private Button btnBorrar;
        private Button btnSalir;
        private TextBox txtNota1;
        private TextBox txtNota2;
        private TextBox txtNota3;
        private TextBox txtResul;
    }
}
