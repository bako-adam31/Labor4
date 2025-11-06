namespace miniprojekt_1
{
    partial class AddKeywordForm
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
            this.txtNewKeyword = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtNewKeyword
            // 
            this.txtNewKeyword.Location = new System.Drawing.Point(30, 59);
            this.txtNewKeyword.Name = "txtNewKeyword";
            this.txtNewKeyword.Size = new System.Drawing.Size(516, 31);
            this.txtNewKeyword.TabIndex = 0;
            this.txtNewKeyword.TextChanged += new System.EventHandler(this.txtNewKeyword_TextChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(30, 112);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(194, 54);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Hozzáadás";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // AddKeywordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1237, 758);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtNewKeyword);
            this.Name = "AddKeywordForm";
            this.Text = "AddKeywordForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNewKeyword;
        private System.Windows.Forms.Button btnAdd;
    }
}