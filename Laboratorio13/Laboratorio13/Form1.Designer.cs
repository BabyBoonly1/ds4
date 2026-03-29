namespace Laboratorio13
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
            btnSwitch = new Button();
            listDBO = new ListBox();
            SuspendLayout();
            // 
            // btnSwitch
            // 
            btnSwitch.Location = new Point(266, 65);
            btnSwitch.Name = "btnSwitch";
            btnSwitch.Size = new Size(216, 92);
            btnSwitch.TabIndex = 0;
            btnSwitch.Text = "Conectar y desconectar de SQL Server";
            btnSwitch.UseVisualStyleBackColor = true;
            btnSwitch.Click += btnSwitch_Click;
            // 
            // listDBO
            // 
            listDBO.FormattingEnabled = true;
            listDBO.Location = new Point(266, 182);
            listDBO.Name = "listDBO";
            listDBO.Size = new Size(216, 204);
            listDBO.TabIndex = 1;
            listDBO.SelectedIndexChanged += listDBO_SelectedIndexChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(listDBO);
            Controls.Add(btnSwitch);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button btnSwitch;
        private ListBox listDBO;
    }
}
