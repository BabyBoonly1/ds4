namespace Laboratorio12
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
            btnCalcular = new Button();
            btnBorrador = new Button();
            btnSalir = new Button();
            lblTiempo = new Label();
            lblVelocidad = new Label();
            lblResul = new Label();
            txtTiempo = new TextBox();
            txtVelocidad = new TextBox();
            txtResul = new TextBox();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Location = new Point(280, 49);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(174, 20);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Calculadora de Distancia";
            lblTitulo.Click += label1_Click;
            // 
            // btnCalcular
            // 
            btnCalcular.BackColor = SystemColors.ActiveCaption;
            btnCalcular.Location = new Point(155, 262);
            btnCalcular.Name = "btnCalcular";
            btnCalcular.Size = new Size(94, 29);
            btnCalcular.TabIndex = 1;
            btnCalcular.Text = "calcular";
            btnCalcular.UseVisualStyleBackColor = false;
            btnCalcular.Click += btnCalcular_Click;
            // 
            // btnBorrador
            // 
            btnBorrador.BackColor = SystemColors.ActiveCaption;
            btnBorrador.Location = new Point(327, 262);
            btnBorrador.Name = "btnBorrador";
            btnBorrador.Size = new Size(94, 29);
            btnBorrador.TabIndex = 2;
            btnBorrador.Text = "borrar";
            btnBorrador.UseVisualStyleBackColor = false;
            btnBorrador.Click += btnBorrador_Click;
            // 
            // btnSalir
            // 
            btnSalir.AllowDrop = true;
            btnSalir.BackColor = SystemColors.ActiveCaption;
            btnSalir.Location = new Point(485, 262);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(94, 29);
            btnSalir.TabIndex = 3;
            btnSalir.Text = "salir";
            btnSalir.UseVisualStyleBackColor = false;
            btnSalir.Click += btnSalir_Click;
            // 
            // lblTiempo
            // 
            lblTiempo.AutoSize = true;
            lblTiempo.Location = new Point(155, 114);
            lblTiempo.Name = "lblTiempo";
            lblTiempo.Size = new Size(60, 20);
            lblTiempo.TabIndex = 4;
            lblTiempo.Text = "Tiempo";
            // 
            // lblVelocidad
            // 
            lblVelocidad.AutoSize = true;
            lblVelocidad.Location = new Point(155, 180);
            lblVelocidad.Name = "lblVelocidad";
            lblVelocidad.Size = new Size(75, 20);
            lblVelocidad.TabIndex = 5;
            lblVelocidad.Text = "Velocidad";
            lblVelocidad.Click += label3_Click;
            // 
            // lblResul
            // 
            lblResul.AutoSize = true;
            lblResul.Location = new Point(127, 338);
            lblResul.Name = "lblResul";
            lblResul.Size = new Size(145, 20);
            lblResul.TabIndex = 6;
            lblResul.Text = "Distancia Recorrida: ";
            lblResul.Click += label4_Click;
            // 
            // txtTiempo
            // 
            txtTiempo.Location = new Point(311, 107);
            txtTiempo.Name = "txtTiempo";
            txtTiempo.Size = new Size(110, 27);
            txtTiempo.TabIndex = 7;
            txtTiempo.TextChanged += textBox1_TextChanged;
            // 
            // txtVelocidad
            // 
            txtVelocidad.Location = new Point(311, 177);
            txtVelocidad.Name = "txtVelocidad";
            txtVelocidad.Size = new Size(110, 27);
            txtVelocidad.TabIndex = 8;
            txtVelocidad.TextChanged += lblVelocidad_TextChanged;
            // 
            // txtResul
            // 
            txtResul.Location = new Point(311, 331);
            txtResul.Name = "txtResul";
            txtResul.ReadOnly = true;
            txtResul.Size = new Size(110, 27);
            txtResul.TabIndex = 9;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(727, 418);
            Controls.Add(txtResul);
            Controls.Add(txtVelocidad);
            Controls.Add(txtTiempo);
            Controls.Add(lblResul);
            Controls.Add(lblVelocidad);
            Controls.Add(lblTiempo);
            Controls.Add(btnSalir);
            Controls.Add(btnBorrador);
            Controls.Add(btnCalcular);
            Controls.Add(lblTitulo);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitulo;
        private Button btnCalcular;
        private Button btnBorrador;
        private Button btnSalir;
        private Label lblTiempo;
        private Label lblVelocidad;
        private Label lblResul;
        private TextBox txtTiempo;
        private TextBox txtVelocidad;
        private TextBox txtResul;
    }
}
