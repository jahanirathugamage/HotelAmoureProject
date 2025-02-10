namespace hotelreservationsystem1
{
    partial class PaymentPortal
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
            this.close = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cardHolderNameInput = new System.Windows.Forms.TextBox();
            this.cardNumberInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cvcInput = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.makePaymentBtn = new System.Windows.Forms.Button();
            this.expiryDateMonthInput = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.expiryDateYearInput = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // close
            // 
            this.close.AutoSize = true;
            this.close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.close.Font = new System.Drawing.Font("Gadugi", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.close.Location = new System.Drawing.Point(530, 23);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(25, 28);
            this.close.TabIndex = 51;
            this.close.Text = "x";
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Gadugi", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(35, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(218, 34);
            this.label4.TabIndex = 95;
            this.label4.Text = "Payment Portal";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label14.Font = new System.Drawing.Font("Gadugi", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label14.Location = new System.Drawing.Point(37, 123);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(279, 19);
            this.label14.TabIndex = 104;
            this.label14.Text = "Enter the card holder name on the card";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Gadugi", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(37, 93);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(167, 21);
            this.label10.TabIndex = 103;
            this.label10.Text = "Card Holder Name";
            // 
            // cardHolderNameInput
            // 
            this.cardHolderNameInput.Location = new System.Drawing.Point(41, 154);
            this.cardHolderNameInput.Name = "cardHolderNameInput";
            this.cardHolderNameInput.Size = new System.Drawing.Size(463, 26);
            this.cardHolderNameInput.TabIndex = 105;
            this.cardHolderNameInput.TextChanged += new System.EventHandler(this.cardHolderNameInput_TextChanged);
            // 
            // cardNumberInput
            // 
            this.cardNumberInput.Location = new System.Drawing.Point(41, 266);
            this.cardNumberInput.Name = "cardNumberInput";
            this.cardNumberInput.Size = new System.Drawing.Size(463, 26);
            this.cardNumberInput.TabIndex = 108;
            this.cardNumberInput.TextChanged += new System.EventHandler(this.cardNumberInput_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label1.Font = new System.Drawing.Font("Gadugi", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label1.Location = new System.Drawing.Point(37, 235);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(305, 19);
            this.label1.TabIndex = 107;
            this.label1.Text = "Enter the 16-digit card number on the card";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Gadugi", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(37, 205);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 21);
            this.label2.TabIndex = 106;
            this.label2.Text = "Card Number";
            // 
            // cvcInput
            // 
            this.cvcInput.Location = new System.Drawing.Point(112, 384);
            this.cvcInput.Name = "cvcInput";
            this.cvcInput.Size = new System.Drawing.Size(80, 26);
            this.cvcInput.TabIndex = 111;
            this.cvcInput.TextChanged += new System.EventHandler(this.cvcInput_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label3.Font = new System.Drawing.Font("Gadugi", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label3.Location = new System.Drawing.Point(245, 378);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(184, 38);
            this.label3.TabIndex = 110;
            this.label3.Text = "Enter the 3-digit number \r\nat the back of the card";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Gadugi", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(37, 386);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 21);
            this.label5.TabIndex = 109;
            this.label5.Text = "CVC";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label6.Font = new System.Drawing.Font("Gadugi", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label6.Location = new System.Drawing.Point(383, 317);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(128, 38);
            this.label6.TabIndex = 113;
            this.label6.Text = "Enter the expiry \r\ndate as mm/yyyy";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Gadugi", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(37, 325);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(108, 21);
            this.label7.TabIndex = 112;
            this.label7.Text = "Expiry Date";
            // 
            // makePaymentBtn
            // 
            this.makePaymentBtn.Location = new System.Drawing.Point(41, 459);
            this.makePaymentBtn.Name = "makePaymentBtn";
            this.makePaymentBtn.Size = new System.Drawing.Size(462, 42);
            this.makePaymentBtn.TabIndex = 122;
            this.makePaymentBtn.Text = "Pay Now";
            this.makePaymentBtn.UseVisualStyleBackColor = true;
            this.makePaymentBtn.Click += new System.EventHandler(this.makePaymentBtn_Click);
            // 
            // expiryDateMonthInput
            // 
            this.expiryDateMonthInput.Location = new System.Drawing.Point(171, 323);
            this.expiryDateMonthInput.Name = "expiryDateMonthInput";
            this.expiryDateMonthInput.Size = new System.Drawing.Size(58, 26);
            this.expiryDateMonthInput.TabIndex = 123;
            this.expiryDateMonthInput.TextChanged += new System.EventHandler(this.expiryDateMonthInput_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Gadugi", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(241, 323);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(21, 26);
            this.label9.TabIndex = 124;
            this.label9.Text = "/";
            // 
            // expiryDateYearInput
            // 
            this.expiryDateYearInput.Location = new System.Drawing.Point(268, 323);
            this.expiryDateYearInput.Name = "expiryDateYearInput";
            this.expiryDateYearInput.Size = new System.Drawing.Size(90, 26);
            this.expiryDateYearInput.TabIndex = 125;
            this.expiryDateYearInput.TextChanged += new System.EventHandler(this.expiryDateYearInput_TextChanged);
            // 
            // PaymentPortal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(582, 555);
            this.Controls.Add(this.expiryDateYearInput);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.expiryDateMonthInput);
            this.Controls.Add(this.makePaymentBtn);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cvcInput);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cardNumberInput);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cardHolderNameInput);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.close);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PaymentPortal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PaymentPortal";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label close;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox cardHolderNameInput;
        private System.Windows.Forms.TextBox cardNumberInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox cvcInput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button makePaymentBtn;
        private System.Windows.Forms.TextBox expiryDateMonthInput;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox expiryDateYearInput;
    }
}