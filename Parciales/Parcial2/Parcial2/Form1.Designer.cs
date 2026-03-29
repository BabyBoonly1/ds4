namespace Parcial2
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
            btnYardas = new Button();
            btnMetros = new Button();
            label1 = new Label();
            label2 = new Label();
            txtMetros1 = new TextBox();
            txtYardas2 = new TextBox();
            txtYardas1 = new TextBox();
            txtMetros2 = new TextBox();
            Historial = new ListBox();
            label3 = new Label();
            btnMostrar = new Button();
            btnBorrar = new Button();
            btnSalir = new Button();
            SuspendLayout();
            // 
            // btnYardas
            // 
            btnYardas.Location = new Point(292, 59);
            btnYardas.Name = "btnYardas";
            btnYardas.Size = new Size(115, 29);
            btnYardas.TabIndex = 0;
            btnYardas.Text = "A Pulgadas ->";
            btnYardas.UseVisualStyleBackColor = true;
            btnYardas.Click += btnYardas_Click;
            // 
            // btnMetros
            // 
            btnMetros.Location = new Point(292, 120);
            btnMetros.Name = "btnMetros";
            btnMetros.Size = new Size(103, 29);
            btnMetros.TabIndex = 1;
            btnMetros.Text = "A Metros ->";
            btnMetros.UseVisualStyleBackColor = true;
            btnMetros.Click += btnMetros_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(35, 64);
            label1.Name = "label1";
            label1.Size = new Size(62, 20);
            label1.TabIndex = 2;
            label1.Text = "Metros :";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(35, 125);
            label2.Name = "label2";
            label2.Size = new Size(76, 20);
            label2.TabIndex = 3;
            label2.Text = "Pulgadas :";
            // 
            // txtMetros1
            // 
            txtMetros1.Location = new Point(125, 61);
            txtMetros1.Name = "txtMetros1";
            txtMetros1.Size = new Size(125, 27);
            txtMetros1.TabIndex = 4;
            // 
            // txtYardas2
            // 
            txtYardas2.Location = new Point(125, 122);
            txtYardas2.Name = "txtYardas2";
            txtYardas2.Size = new Size(125, 27);
            txtYardas2.TabIndex = 5;
            // 
            // txtYardas1
            // 
            txtYardas1.Location = new Point(440, 60);
            txtYardas1.Name = "txtYardas1";
            txtYardas1.Size = new Size(125, 27);
            txtYardas1.TabIndex = 6;
            // 
            // txtMetros2
            // 
            txtMetros2.Location = new Point(440, 122);
            txtMetros2.Name = "txtMetros2";
            txtMetros2.Size = new Size(125, 27);
            txtMetros2.TabIndex = 7;
            // 
            // Historial
            // 
            Historial.FormattingEnabled = true;
            Historial.Location = new Point(35, 214);
            Historial.Name = "Historial";
            Historial.Size = new Size(351, 104);
            Historial.TabIndex = 8;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(35, 182);
            label3.Name = "label3";
            label3.Size = new Size(72, 20);
            label3.TabIndex = 9;
            label3.Text = "Historial :";
            // 
            // btnMostrar
            // 
            btnMostrar.Location = new Point(113, 178);
            btnMostrar.Name = "btnMostrar";
            btnMostrar.Size = new Size(94, 29);
            btnMostrar.TabIndex = 10;
            btnMostrar.Text = "Mostrar";
            btnMostrar.UseVisualStyleBackColor = true;
            btnMostrar.Click += btnMostrar_Click;
            // 
            // btnBorrar
            // 
            btnBorrar.Location = new Point(292, 178);
            btnBorrar.Name = "btnBorrar";
            btnBorrar.Size = new Size(94, 29);
            btnBorrar.TabIndex = 11;
            btnBorrar.Text = "Borrar";
            btnBorrar.UseVisualStyleBackColor = true;
            btnBorrar.Click += btnBorrar_Click;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(471, 178);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(94, 29);
            btnSalir.TabIndex = 12;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(626, 343);
            Controls.Add(btnSalir);
            Controls.Add(btnBorrar);
            Controls.Add(btnMostrar);
            Controls.Add(label3);
            Controls.Add(Historial);
            Controls.Add(txtMetros2);
            Controls.Add(txtYardas1);
            Controls.Add(txtYardas2);
            Controls.Add(txtMetros1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnMetros);
            Controls.Add(btnYardas);
            Name = "Form1";
            Text = "Conversor de Medidas";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnYardas;
        private Button btnMetros;
        private Label label1;
        private Label label2;
        private TextBox txtMetros1;
        private TextBox txtYardas2;
        private TextBox txtYardas1;
        private TextBox txtMetros2;
        private ListBox Historial;
        private Label label3;
        private Button btnMostrar;
        private Button btnBorrar;
        private Button btnSalir;
    }
}
