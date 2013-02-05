namespace Project_Opal
{
    partial class MainMenu_Form
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
            this.btnClock = new System.Windows.Forms.Button();
            this.btnReview = new System.Windows.Forms.Button();
            this.lblShiftInformation = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnClock
            // 
            this.btnClock.Location = new System.Drawing.Point(24, 16);
            this.btnClock.Name = "btnClock";
            this.btnClock.Size = new System.Drawing.Size(232, 64);
            this.btnClock.TabIndex = 0;
            this.btnClock.Text = "btnClock";
            this.btnClock.UseVisualStyleBackColor = true;
            this.btnClock.Click += new System.EventHandler(this.btnClock_Click);
            // 
            // btnReview
            // 
            this.btnReview.Location = new System.Drawing.Point(48, 136);
            this.btnReview.Name = "btnReview";
            this.btnReview.Size = new System.Drawing.Size(184, 32);
            this.btnReview.TabIndex = 1;
            this.btnReview.Text = "Review Shifts";
            this.btnReview.UseVisualStyleBackColor = true;
            // 
            // lblShiftInformation
            // 
            this.lblShiftInformation.Location = new System.Drawing.Point(24, 96);
            this.lblShiftInformation.Name = "lblShiftInformation";
            this.lblShiftInformation.Size = new System.Drawing.Size(232, 32);
            this.lblShiftInformation.TabIndex = 2;
            this.lblShiftInformation.Text = "lblShiftInformation";
            this.lblShiftInformation.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // MainMenu_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 185);
            this.Controls.Add(this.lblShiftInformation);
            this.Controls.Add(this.btnReview);
            this.Controls.Add(this.btnClock);
            this.Name = "MainMenu_Form";
            this.Text = "Main Menu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClock;
        private System.Windows.Forms.Button btnReview;
        private System.Windows.Forms.Label lblShiftInformation;
    }
}