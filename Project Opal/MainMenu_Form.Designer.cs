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
            this.lblLastShift = new System.Windows.Forms.Label();
            this.lblLastShiftInformation = new System.Windows.Forms.Label();
            this.btnCloseReviewShifts = new System.Windows.Forms.Button();
            this.btnMoreShiftInformation = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnClock
            // 
            this.btnClock.Location = new System.Drawing.Point(24, 12);
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
            this.btnReview.Click += new System.EventHandler(this.btnReview_Click);
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
            // lblLastShift
            // 
            this.lblLastShift.AutoSize = true;
            this.lblLastShift.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastShift.Location = new System.Drawing.Point(96, 144);
            this.lblLastShift.Name = "lblLastShift";
            this.lblLastShift.Size = new System.Drawing.Size(91, 13);
            this.lblLastShift.TabIndex = 3;
            this.lblLastShift.Text = "Your Last Shift";
            this.lblLastShift.Visible = false;
            // 
            // lblLastShiftInformation
            // 
            this.lblLastShiftInformation.AutoSize = true;
            this.lblLastShiftInformation.Location = new System.Drawing.Point(8, 176);
            this.lblLastShiftInformation.Name = "lblLastShiftInformation";
            this.lblLastShiftInformation.Size = new System.Drawing.Size(110, 13);
            this.lblLastShiftInformation.TabIndex = 4;
            this.lblLastShiftInformation.Text = "lblLastShiftInformation";
            this.lblLastShiftInformation.Visible = false;
            // 
            // btnCloseReviewShifts
            // 
            this.btnCloseReviewShifts.Location = new System.Drawing.Point(248, 136);
            this.btnCloseReviewShifts.Name = "btnCloseReviewShifts";
            this.btnCloseReviewShifts.Size = new System.Drawing.Size(27, 24);
            this.btnCloseReviewShifts.TabIndex = 5;
            this.btnCloseReviewShifts.Text = "X";
            this.btnCloseReviewShifts.UseVisualStyleBackColor = true;
            this.btnCloseReviewShifts.Visible = false;
            this.btnCloseReviewShifts.Click += new System.EventHandler(this.btnCloseReviewShifts_Click);
            // 
            // btnMoreShiftInformation
            // 
            this.btnMoreShiftInformation.Location = new System.Drawing.Point(200, 200);
            this.btnMoreShiftInformation.Name = "btnMoreShiftInformation";
            this.btnMoreShiftInformation.Size = new System.Drawing.Size(75, 23);
            this.btnMoreShiftInformation.TabIndex = 6;
            this.btnMoreShiftInformation.Text = "More...";
            this.btnMoreShiftInformation.UseVisualStyleBackColor = true;
            this.btnMoreShiftInformation.Visible = false;
            this.btnMoreShiftInformation.Click += new System.EventHandler(this.btnMoreShiftInformation_Click);
            // 
            // MainMenu_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 230);
            this.Controls.Add(this.btnMoreShiftInformation);
            this.Controls.Add(this.btnCloseReviewShifts);
            this.Controls.Add(this.lblLastShiftInformation);
            this.Controls.Add(this.lblLastShift);
            this.Controls.Add(this.lblShiftInformation);
            this.Controls.Add(this.btnReview);
            this.Controls.Add(this.btnClock);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainMenu_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main Menu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClock;
        private System.Windows.Forms.Button btnReview;
        private System.Windows.Forms.Label lblShiftInformation;
        private System.Windows.Forms.Label lblLastShift;
        private System.Windows.Forms.Label lblLastShiftInformation;
        private System.Windows.Forms.Button btnCloseReviewShifts;
        private System.Windows.Forms.Button btnMoreShiftInformation;
    }
}