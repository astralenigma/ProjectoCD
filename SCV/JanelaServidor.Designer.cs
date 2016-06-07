namespace SCV
{
    partial class JanelaServidor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Start = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.carregar = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.porVotar = new System.Windows.Forms.Label();
            this.reprovados = new System.Windows.Forms.Label();
            this.aprovados = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Start
            // 
            this.Start.Location = new System.Drawing.Point(73, 241);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(267, 23);
            this.Start.TabIndex = 0;
            this.Start.Text = "Começar votação";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(61, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Documento a aprovar";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // carregar
            // 
            this.carregar.Location = new System.Drawing.Point(304, 59);
            this.carregar.Name = "carregar";
            this.carregar.Size = new System.Drawing.Size(75, 23);
            this.carregar.TabIndex = 3;
            this.carregar.Text = "Carregar documento";
            this.carregar.UseVisualStyleBackColor = true;
            this.carregar.Click += new System.EventHandler(this.carregar_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(177, 61);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(121, 20);
            this.textBox1.TabIndex = 4;
            // 
            // label8
            // 
            this.porVotar.AutoSize = true;
            this.porVotar.Location = new System.Drawing.Point(269, 139);
            this.porVotar.Name = "label8";
            this.porVotar.Size = new System.Drawing.Size(80, 13);
            this.porVotar.TabIndex = 17;
            this.porVotar.Text = "Não votaram=0";
            // 
            // label7
            // 
            this.reprovados.AutoSize = true;
            this.reprovados.Location = new System.Drawing.Point(175, 139);
            this.reprovados.Name = "label7";
            this.reprovados.Size = new System.Drawing.Size(77, 13);
            this.reprovados.TabIndex = 16;
            this.reprovados.Text = "Reprovados=0";
            // 
            // label6
            // 
            this.aprovados.AutoSize = true;
            this.aprovados.Location = new System.Drawing.Point(91, 139);
            this.aprovados.Name = "label6";
            this.aprovados.Size = new System.Drawing.Size(70, 13);
            this.aprovados.TabIndex = 15;
            this.aprovados.Text = "Aprovados=0";
            // 
            // JanelaServidor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 291);
            this.Controls.Add(this.porVotar);
            this.Controls.Add(this.reprovados);
            this.Controls.Add(this.aprovados);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.carregar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Start);
            this.Name = "JanelaServidor";
            this.Text = "JanelaServidor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button carregar;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label aprovados;
        private System.Windows.Forms.Label reprovados;
        private System.Windows.Forms.Label porVotar;
    }
}