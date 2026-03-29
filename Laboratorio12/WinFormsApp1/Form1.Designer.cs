namespace WinFormsApp1
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
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            btnSemi = new Button();
            btnArea = new Button();
            btnBorrar = new Button();
            btnSalir = new Button();
            txtLadoA = new TextBox();
            txtLadoB = new TextBox();
            txtLadoC = new TextBox();
            txtResulSemi = new TextBox();
            txtResulArea = new TextBox();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 78);
            label2.Name = "label2";
            label2.Size = new Size(151, 20);
            label2.TabIndex = 1;
            label2.Text = "Longitud del Lado A :";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 125);
            label3.Name = "label3";
            label3.Size = new Size(154, 20);
            label3.TabIndex = 2;
            label3.Text = "Longitud del Lado B : ";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 176);
            label4.Name = "label4";
            label4.Size = new Size(150, 20);
            label4.TabIndex = 3;
            label4.Text = "Longitud del Lado C :";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 294);
            label5.Name = "label5";
            label5.Size = new Size(167, 20);
            label5.TabIndex = 4;
            label5.Text = "Calcular Semi Perimetro";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 335);
            label6.Name = "label6";
            label6.Size = new Size(131, 20);
            label6.TabIndex = 5;
            label6.Text = "Área del Triángulo";
            // 
            // btnSemi
            // 
            btnSemi.Location = new Point(12, 229);
            btnSemi.Name = "btnSemi";
            btnSemi.Size = new Size(119, 29);
            btnSemi.TabIndex = 6;
            btnSemi.Text = "Semi Perimetro";
            btnSemi.UseVisualStyleBackColor = true;
            btnSemi.Click += btnSemi_Click;
            // 
            // btnArea
            // 
            btnArea.Location = new Point(163, 229);
            btnArea.Name = "btnArea";
            btnArea.Size = new Size(94, 29);
            btnArea.TabIndex = 7;
            btnArea.Text = "Área";
            btnArea.UseVisualStyleBackColor = true;
            btnArea.Click += btnArea_Click;
            // 
            // btnBorrar
            // 
            btnBorrar.Location = new Point(297, 229);
            btnBorrar.Name = "btnBorrar";
            btnBorrar.Size = new Size(94, 29);
            btnBorrar.TabIndex = 8;
            btnBorrar.Text = "Borrar";
            btnBorrar.UseVisualStyleBackColor = true;
            btnBorrar.Click += btnBorrar_Click;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(441, 229);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(94, 29);
            btnSalir.TabIndex = 9;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = true;
            btnSalir.Click += btnSalir_Click;
            // 
            // txtLadoA
            // 
            txtLadoA.Location = new Point(205, 75);
            txtLadoA.Name = "txtLadoA";
            txtLadoA.Size = new Size(125, 27);
            txtLadoA.TabIndex = 10;
            // 
            // txtLadoB
            // 
            txtLadoB.Location = new Point(205, 122);
            txtLadoB.Name = "txtLadoB";
            txtLadoB.Size = new Size(125, 27);
            txtLadoB.TabIndex = 11;
            // 
            // txtLadoC
            // 
            txtLadoC.Location = new Point(205, 173);
            txtLadoC.Name = "txtLadoC";
            txtLadoC.Size = new Size(125, 27);
            txtLadoC.TabIndex = 12;
            // 
            // txtResulSemi
            // 
            txtResulSemi.Location = new Point(205, 291);
            txtResulSemi.Name = "txtResulSemi";
            txtResulSemi.Size = new Size(125, 27);
            txtResulSemi.TabIndex = 13;
            // 
            // txtResulArea
            // 
            txtResulArea.Location = new Point(205, 335);
            txtResulArea.Name = "txtResulArea";
            txtResulArea.Size = new Size(125, 27);
            txtResulArea.TabIndex = 14;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(547, 450);
            Controls.Add(txtResulArea);
            Controls.Add(txtResulSemi);
            Controls.Add(txtLadoC);
            Controls.Add(txtLadoB);
            Controls.Add(txtLadoA);
            Controls.Add(btnSalir);
            Controls.Add(btnBorrar);
            Controls.Add(btnArea);
            Controls.Add(btnSemi);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Button btnSemi;
        private Button btnArea;
        private Button btnBorrar;
        private Button btnSalir;
        private TextBox txtLadoA;
        private TextBox txtLadoB;
        private TextBox txtLadoC;
        private TextBox txtResulSemi;
        private TextBox txtResulArea;
    }
}
