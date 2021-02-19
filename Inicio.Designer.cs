namespace Toyota_WPP_Sender_W
{
    partial class Form_Inicio
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
            this.components = new System.ComponentModel.Container();
            this.timerClicker = new System.Windows.Forms.Timer(this.components);
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridNumbers = new System.Windows.Forms.DataGridView();
            this.timerAtualizarNumeros = new System.Windows.Forms.Timer(this.components);
            this.contactUpdater = new System.Windows.Forms.Timer(this.components);
            this.formInicioBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridNumbers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.formInicioBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(15, 28);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(311, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 6;
            // 
            // progressBar2
            // 
            this.progressBar2.Location = new System.Drawing.Point(15, 81);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(311, 23);
            this.progressBar2.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Proxima Operação";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Tempo de Espera";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.progressBar2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(13, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(344, 125);
            this.panel1.TabIndex = 10;
            // 
            // dataGridNumbers
            // 
            this.dataGridNumbers.AllowUserToAddRows = false;
            this.dataGridNumbers.AllowUserToDeleteRows = false;
            this.dataGridNumbers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridNumbers.Location = new System.Drawing.Point(13, 143);
            this.dataGridNumbers.Name = "dataGridNumbers";
            this.dataGridNumbers.ReadOnly = true;
            this.dataGridNumbers.Size = new System.Drawing.Size(344, 182);
            this.dataGridNumbers.TabIndex = 11;
            // 
            // timerAtualizarNumeros
            // 
            this.timerAtualizarNumeros.Enabled = true;
            this.timerAtualizarNumeros.Interval = 200;
            this.timerAtualizarNumeros.Tick += new System.EventHandler(this.timerAtualizarNumeros_Tick);
            // 
            // contactUpdater
            // 
            this.contactUpdater.Enabled = true;
            this.contactUpdater.Interval = 2000;
            this.contactUpdater.Tick += new System.EventHandler(this.contactUpdater_Tick);
            // 
            // formInicioBindingSource
            // 
            this.formInicioBindingSource.DataSource = typeof(Toyota_WPP_Sender_W.Form_Inicio);
            // 
            // Form_Inicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 337);
            this.Controls.Add(this.dataGridNumbers);
            this.Controls.Add(this.panel1);
            this.MaximumSize = new System.Drawing.Size(385, 376);
            this.MinimumSize = new System.Drawing.Size(385, 376);
            this.Name = "Form_Inicio";
            this.Text = "Toyota WhatsAPP vW";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridNumbers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.formInicioBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timerClicker;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ProgressBar progressBar2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridNumbers;
        private System.Windows.Forms.BindingSource formInicioBindingSource;
        private System.Windows.Forms.Timer timerAtualizarNumeros;
        private System.Windows.Forms.Timer contactUpdater;
    }
}

