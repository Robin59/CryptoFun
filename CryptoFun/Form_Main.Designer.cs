namespace CryptoFun
{
    partial class Main
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBox_Algo_Choice = new System.Windows.Forms.ComboBox();
            this.textBox_In = new System.Windows.Forms.TextBox();
            this.textBox_Out = new System.Windows.Forms.TextBox();
            this.button_Encrypt = new System.Windows.Forms.Button();
            this.button_Decrypt = new System.Windows.Forms.Button();
            this.textBox_Key = new System.Windows.Forms.TextBox();
            this.textBox_Additional_Imput = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // comboBox_Algo_Choice
            // 
            this.comboBox_Algo_Choice.FormattingEnabled = true;
            this.comboBox_Algo_Choice.Location = new System.Drawing.Point(32, 27);
            this.comboBox_Algo_Choice.Name = "comboBox_Algo_Choice";
            this.comboBox_Algo_Choice.Size = new System.Drawing.Size(121, 21);
            this.comboBox_Algo_Choice.TabIndex = 0;
            this.comboBox_Algo_Choice.SelectedValueChanged += new System.EventHandler(this.comboBox_Algo_Choice_SelectedValueChanged);
            // 
            // textBox_In
            // 
            this.textBox_In.Location = new System.Drawing.Point(32, 89);
            this.textBox_In.Multiline = true;
            this.textBox_In.Name = "textBox_In";
            this.textBox_In.Size = new System.Drawing.Size(100, 85);
            this.textBox_In.TabIndex = 1;
            // 
            // textBox_Out
            // 
            this.textBox_Out.Location = new System.Drawing.Point(169, 89);
            this.textBox_Out.Multiline = true;
            this.textBox_Out.Name = "textBox_Out";
            this.textBox_Out.Size = new System.Drawing.Size(100, 85);
            this.textBox_Out.TabIndex = 2;
            // 
            // button_Encrypt
            // 
            this.button_Encrypt.Location = new System.Drawing.Point(32, 206);
            this.button_Encrypt.Name = "button_Encrypt";
            this.button_Encrypt.Size = new System.Drawing.Size(75, 23);
            this.button_Encrypt.TabIndex = 3;
            this.button_Encrypt.Text = "Encrypt";
            this.button_Encrypt.UseVisualStyleBackColor = true;
            this.button_Encrypt.Click += new System.EventHandler(this.button_Encrypt_Click);
            // 
            // button_Decrypt
            // 
            this.button_Decrypt.Location = new System.Drawing.Point(169, 206);
            this.button_Decrypt.Name = "button_Decrypt";
            this.button_Decrypt.Size = new System.Drawing.Size(75, 23);
            this.button_Decrypt.TabIndex = 4;
            this.button_Decrypt.Text = "Decrypt";
            this.button_Decrypt.UseVisualStyleBackColor = true;
            this.button_Decrypt.Click += new System.EventHandler(this.button_Decrypt_Click);
            // 
            // textBox_Key
            // 
            this.textBox_Key.Location = new System.Drawing.Point(169, 27);
            this.textBox_Key.Name = "textBox_Key";
            this.textBox_Key.Size = new System.Drawing.Size(100, 20);
            this.textBox_Key.TabIndex = 5;
            // 
            // textBox_Additional_Imput
            // 
            this.textBox_Additional_Imput.Location = new System.Drawing.Point(169, 53);
            this.textBox_Additional_Imput.Name = "textBox_Additional_Imput";
            this.textBox_Additional_Imput.Size = new System.Drawing.Size(100, 20);
            this.textBox_Additional_Imput.TabIndex = 6;
            this.textBox_Additional_Imput.Visible = false;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.textBox_Additional_Imput);
            this.Controls.Add(this.textBox_Key);
            this.Controls.Add(this.button_Decrypt);
            this.Controls.Add(this.button_Encrypt);
            this.Controls.Add(this.textBox_Out);
            this.Controls.Add(this.textBox_In);
            this.Controls.Add(this.comboBox_Algo_Choice);
            this.Name = "Main";
            this.Text = "Crypto fun";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_Algo_Choice;
        private System.Windows.Forms.TextBox textBox_In;
        private System.Windows.Forms.TextBox textBox_Out;
        private System.Windows.Forms.Button button_Encrypt;
        private System.Windows.Forms.Button button_Decrypt;
        private System.Windows.Forms.TextBox textBox_Key;
        private System.Windows.Forms.TextBox textBox_Additional_Imput;
    }
}

