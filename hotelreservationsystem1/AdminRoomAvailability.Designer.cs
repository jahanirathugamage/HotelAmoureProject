namespace hotelreservationsystem1
{
    partial class AdminRoomAvailability
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.control_manageBtn = new System.Windows.Forms.Button();
            this.control_feedback = new System.Windows.Forms.Button();
            this.control_offersBtn = new System.Windows.Forms.Button();
            this.control_profileBtn = new System.Windows.Forms.Button();
            this.control_roomsBtn = new System.Windows.Forms.Button();
            this.home_homeBtn = new System.Windows.Forms.Button();
            this.close = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.AllRoomsTable = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.bedTypeMenu = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.viewTypeMenu = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.bedroomsMenu = new System.Windows.Forms.ComboBox();
            this.resultField = new System.Windows.Forms.Label();
            this.roomTypeMenu = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.room_searchBtn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.roomIDInput = new System.Windows.Forms.TextBox();
            this.startDateInput = new System.Windows.Forms.DateTimePicker();
            this.endDateInput = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.roomAvailableLabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AllRoomsTable)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.Controls.Add(this.control_manageBtn);
            this.panel1.Controls.Add(this.control_feedback);
            this.panel1.Controls.Add(this.control_offersBtn);
            this.panel1.Controls.Add(this.control_profileBtn);
            this.panel1.Controls.Add(this.control_roomsBtn);
            this.panel1.Controls.Add(this.home_homeBtn);
            this.panel1.Controls.Add(this.close);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1300, 84);
            this.panel1.TabIndex = 2;
            // 
            // control_manageBtn
            // 
            this.control_manageBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.control_manageBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.control_manageBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.control_manageBtn.Font = new System.Drawing.Font("Gadugi", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.control_manageBtn.Location = new System.Drawing.Point(726, 20);
            this.control_manageBtn.Name = "control_manageBtn";
            this.control_manageBtn.Size = new System.Drawing.Size(112, 42);
            this.control_manageBtn.TabIndex = 57;
            this.control_manageBtn.Text = "Manage";
            this.control_manageBtn.UseVisualStyleBackColor = true;
            this.control_manageBtn.Click += new System.EventHandler(this.home_manageBtn_Click);
            // 
            // control_feedback
            // 
            this.control_feedback.Cursor = System.Windows.Forms.Cursors.Hand;
            this.control_feedback.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.control_feedback.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.control_feedback.Font = new System.Drawing.Font("Gadugi", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.control_feedback.Location = new System.Drawing.Point(942, 20);
            this.control_feedback.Name = "control_feedback";
            this.control_feedback.Size = new System.Drawing.Size(130, 42);
            this.control_feedback.TabIndex = 55;
            this.control_feedback.Text = "Feedback";
            this.control_feedback.UseVisualStyleBackColor = true;
            this.control_feedback.Click += new System.EventHandler(this.home_feedback_Click);
            // 
            // control_offersBtn
            // 
            this.control_offersBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.control_offersBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.control_offersBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.control_offersBtn.Font = new System.Drawing.Font("Gadugi", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.control_offersBtn.Location = new System.Drawing.Point(844, 20);
            this.control_offersBtn.Name = "control_offersBtn";
            this.control_offersBtn.Size = new System.Drawing.Size(92, 42);
            this.control_offersBtn.TabIndex = 54;
            this.control_offersBtn.Text = "Offers";
            this.control_offersBtn.UseVisualStyleBackColor = true;
            this.control_offersBtn.Click += new System.EventHandler(this.home_offersBtn_Click);
            // 
            // control_profileBtn
            // 
            this.control_profileBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.control_profileBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.control_profileBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.control_profileBtn.Font = new System.Drawing.Font("Gadugi", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.control_profileBtn.Location = new System.Drawing.Point(1078, 20);
            this.control_profileBtn.Name = "control_profileBtn";
            this.control_profileBtn.Size = new System.Drawing.Size(105, 42);
            this.control_profileBtn.TabIndex = 53;
            this.control_profileBtn.Text = "Profile";
            this.control_profileBtn.UseVisualStyleBackColor = true;
            this.control_profileBtn.Click += new System.EventHandler(this.home_profileBtn_Click);
            // 
            // control_roomsBtn
            // 
            this.control_roomsBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.control_roomsBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.control_roomsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.control_roomsBtn.Font = new System.Drawing.Font("Gadugi", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.control_roomsBtn.Location = new System.Drawing.Point(628, 20);
            this.control_roomsBtn.Name = "control_roomsBtn";
            this.control_roomsBtn.Size = new System.Drawing.Size(92, 42);
            this.control_roomsBtn.TabIndex = 51;
            this.control_roomsBtn.Text = "Rooms";
            this.control_roomsBtn.UseVisualStyleBackColor = true;
            this.control_roomsBtn.Click += new System.EventHandler(this.home_roomsBtn_Click);
            // 
            // home_homeBtn
            // 
            this.home_homeBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.home_homeBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.home_homeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.home_homeBtn.Font = new System.Drawing.Font("Gadugi", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.home_homeBtn.Location = new System.Drawing.Point(530, 20);
            this.home_homeBtn.Name = "home_homeBtn";
            this.home_homeBtn.Size = new System.Drawing.Size(92, 42);
            this.home_homeBtn.TabIndex = 50;
            this.home_homeBtn.Text = "Home";
            this.home_homeBtn.UseVisualStyleBackColor = true;
            this.home_homeBtn.Click += new System.EventHandler(this.home_homeBtn_Click);
            // 
            // close
            // 
            this.close.AutoSize = true;
            this.close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.close.Font = new System.Drawing.Font("Gadugi", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.close.Location = new System.Drawing.Point(1220, 26);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(25, 28);
            this.close.TabIndex = 50;
            this.close.Text = "x";
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.Font = new System.Drawing.Font("Calisto MT", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(69, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 23);
            this.label1.TabIndex = 10;
            this.label1.Text = "Hotel";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label5.Font = new System.Drawing.Font("Calisto MT", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(44, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 32);
            this.label5.TabIndex = 9;
            this.label5.Text = "Amouré";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // AllRoomsTable
            // 
            this.AllRoomsTable.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.AllRoomsTable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.AllRoomsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AllRoomsTable.Location = new System.Drawing.Point(50, 491);
            this.AllRoomsTable.Name = "AllRoomsTable";
            this.AllRoomsTable.RowHeadersWidth = 62;
            this.AllRoomsTable.RowTemplate.Height = 28;
            this.AllRoomsTable.Size = new System.Drawing.Size(1178, 479);
            this.AllRoomsTable.TabIndex = 62;
            this.AllRoomsTable.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.AllRoomsTable_CellContentClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Gadugi", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(524, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(249, 34);
            this.label2.TabIndex = 60;
            this.label2.Text = "Room Availability";
            // 
            // panel6
            // 
            this.panel6.Location = new System.Drawing.Point(464, 976);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(200, 50);
            this.panel6.TabIndex = 94;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label9.Font = new System.Drawing.Font("Gadugi", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label9.Location = new System.Drawing.Point(224, 293);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(104, 19);
            this.label9.TabIndex = 120;
            this.label9.Text = "only standard";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Gadugi", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(126, 291);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(83, 21);
            this.label13.TabIndex = 119;
            this.label13.Text = "Bed Type";
            // 
            // bedTypeMenu
            // 
            this.bedTypeMenu.FormattingEnabled = true;
            this.bedTypeMenu.Location = new System.Drawing.Point(354, 288);
            this.bedTypeMenu.Name = "bedTypeMenu";
            this.bedTypeMenu.Size = new System.Drawing.Size(202, 28);
            this.bedTypeMenu.TabIndex = 118;
            this.bedTypeMenu.SelectedIndexChanged += new System.EventHandler(this.bedTypeMenu_SelectedIndexChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label16.Font = new System.Drawing.Font("Gadugi", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label16.Location = new System.Drawing.Point(224, 392);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(88, 19);
            this.label16.TabIndex = 117;
            this.label16.Text = "only deluxe";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label14.Font = new System.Drawing.Font("Gadugi", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label14.Location = new System.Drawing.Point(224, 342);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(80, 19);
            this.label14.TabIndex = 116;
            this.label14.Text = "only  suite";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Gadugi", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(126, 390);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(90, 21);
            this.label12.TabIndex = 113;
            this.label12.Text = "View Type";
            // 
            // viewTypeMenu
            // 
            this.viewTypeMenu.FormattingEnabled = true;
            this.viewTypeMenu.Location = new System.Drawing.Point(354, 388);
            this.viewTypeMenu.Name = "viewTypeMenu";
            this.viewTypeMenu.Size = new System.Drawing.Size(202, 28);
            this.viewTypeMenu.TabIndex = 112;
            this.viewTypeMenu.SelectedIndexChanged += new System.EventHandler(this.viewTypeMenu_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Gadugi", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(126, 340);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(92, 21);
            this.label10.TabIndex = 115;
            this.label10.Text = "Bedrooms";
            // 
            // bedroomsMenu
            // 
            this.bedroomsMenu.FormattingEnabled = true;
            this.bedroomsMenu.Location = new System.Drawing.Point(354, 338);
            this.bedroomsMenu.Name = "bedroomsMenu";
            this.bedroomsMenu.Size = new System.Drawing.Size(202, 28);
            this.bedroomsMenu.TabIndex = 114;
            this.bedroomsMenu.SelectedIndexChanged += new System.EventHandler(this.bedroomsMenu_SelectedIndexChanged);
            // 
            // resultField
            // 
            this.resultField.AutoSize = true;
            this.resultField.Font = new System.Drawing.Font("Gadugi", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resultField.Location = new System.Drawing.Point(126, 431);
            this.resultField.Name = "resultField";
            this.resultField.Size = new System.Drawing.Size(14, 21);
            this.resultField.TabIndex = 111;
            this.resultField.Text = ".";
            this.resultField.Visible = false;
            // 
            // roomTypeMenu
            // 
            this.roomTypeMenu.FormattingEnabled = true;
            this.roomTypeMenu.Location = new System.Drawing.Point(243, 229);
            this.roomTypeMenu.Name = "roomTypeMenu";
            this.roomTypeMenu.Size = new System.Drawing.Size(201, 28);
            this.roomTypeMenu.TabIndex = 110;
            this.roomTypeMenu.SelectedIndexChanged += new System.EventHandler(this.roomTypeMenu_SelectedIndexChanged_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Gadugi", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(126, 231);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 21);
            this.label3.TabIndex = 109;
            this.label3.Text = "Room Type";
            // 
            // room_searchBtn
            // 
            this.room_searchBtn.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.room_searchBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.room_searchBtn.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.room_searchBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.room_searchBtn.Font = new System.Drawing.Font("Gadugi", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.room_searchBtn.Location = new System.Drawing.Point(464, 220);
            this.room_searchBtn.Name = "room_searchBtn";
            this.room_searchBtn.Size = new System.Drawing.Size(92, 42);
            this.room_searchBtn.TabIndex = 108;
            this.room_searchBtn.Text = "Filter";
            this.room_searchBtn.UseVisualStyleBackColor = false;
            this.room_searchBtn.Click += new System.EventHandler(this.room_searchBtn_Click_1);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Gadugi", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(255, 173);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(141, 26);
            this.label4.TabIndex = 121;
            this.label4.Text = "Filter Rooms";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Gadugi", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(868, 173);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(192, 26);
            this.label6.TabIndex = 122;
            this.label6.Text = "Room Occupancy";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Gadugi", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(787, 231);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 21);
            this.label7.TabIndex = 123;
            this.label7.Text = "Room No";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Gadugi", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(787, 275);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 21);
            this.label8.TabIndex = 124;
            this.label8.Text = "Start Date";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Gadugi", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(787, 324);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(82, 21);
            this.label11.TabIndex = 125;
            this.label11.Text = "End Date";
            // 
            // roomIDInput
            // 
            this.roomIDInput.Location = new System.Drawing.Point(942, 226);
            this.roomIDInput.Name = "roomIDInput";
            this.roomIDInput.Size = new System.Drawing.Size(200, 26);
            this.roomIDInput.TabIndex = 126;
            this.roomIDInput.TextChanged += new System.EventHandler(this.roomIDInput_TextChanged);
            // 
            // startDateInput
            // 
            this.startDateInput.Location = new System.Drawing.Point(942, 271);
            this.startDateInput.Name = "startDateInput";
            this.startDateInput.Size = new System.Drawing.Size(200, 26);
            this.startDateInput.TabIndex = 127;
            this.startDateInput.ValueChanged += new System.EventHandler(this.startDateInput_ValueChanged);
            // 
            // endDateInput
            // 
            this.endDateInput.Location = new System.Drawing.Point(942, 320);
            this.endDateInput.Name = "endDateInput";
            this.endDateInput.Size = new System.Drawing.Size(200, 26);
            this.endDateInput.TabIndex = 128;
            this.endDateInput.ValueChanged += new System.EventHandler(this.endDateInput_ValueChanged);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Gadugi", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(791, 367);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(351, 44);
            this.button1.TabIndex = 129;
            this.button1.Text = "Check";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // roomAvailableLabel
            // 
            this.roomAvailableLabel.AutoSize = true;
            this.roomAvailableLabel.Font = new System.Drawing.Font("Gadugi", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.roomAvailableLabel.Location = new System.Drawing.Point(787, 431);
            this.roomAvailableLabel.Name = "roomAvailableLabel";
            this.roomAvailableLabel.Size = new System.Drawing.Size(14, 21);
            this.roomAvailableLabel.TabIndex = 130;
            this.roomAvailableLabel.Text = ".";
            this.roomAvailableLabel.Visible = false;
            // 
            // AdminRoomAvailability
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1288, 1055);
            this.Controls.Add(this.roomAvailableLabel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.endDateInput);
            this.Controls.Add(this.startDateInput);
            this.Controls.Add(this.roomIDInput);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.bedTypeMenu);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.viewTypeMenu);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.bedroomsMenu);
            this.Controls.Add(this.resultField);
            this.Controls.Add(this.roomTypeMenu);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.room_searchBtn);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.AllRoomsTable);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AdminRoomAvailability";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AdminRoomAvailability";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AllRoomsTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button control_feedback;
        private System.Windows.Forms.Button control_offersBtn;
        private System.Windows.Forms.Button control_profileBtn;
        private System.Windows.Forms.Button control_roomsBtn;
        private System.Windows.Forms.Button home_homeBtn;
        private System.Windows.Forms.Label close;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView AllRoomsTable;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button control_manageBtn;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox bedTypeMenu;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox viewTypeMenu;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox bedroomsMenu;
        private System.Windows.Forms.Label resultField;
        private System.Windows.Forms.ComboBox roomTypeMenu;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button room_searchBtn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox roomIDInput;
        private System.Windows.Forms.DateTimePicker startDateInput;
        private System.Windows.Forms.DateTimePicker endDateInput;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label roomAvailableLabel;
    }
}