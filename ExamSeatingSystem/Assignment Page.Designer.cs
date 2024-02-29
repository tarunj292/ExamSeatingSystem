namespace ExamSeatingSystem
{
    partial class AssignmentPage
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
            this.button5 = new System.Windows.Forms.Button();
            this.done = new System.Windows.Forms.Button();
            this.assign = new System.Windows.Forms.Button();
            this.insertCSVtoDB = new System.Windows.Forms.Button();
            this.AddClassRoom = new System.Windows.Forms.Button();
            this.GetStudentCount = new System.Windows.Forms.Button();
            this.txtCapacity = new System.Windows.Forms.TextBox();
            this.txtRoomNumber = new System.Windows.Forms.TextBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button3 = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.programFilter = new System.Windows.Forms.ComboBox();
            this.courseFilter = new System.Windows.Forms.ComboBox();
            this.semesterFilter = new System.Windows.Forms.ComboBox();
            this.roomFilter = new System.Windows.Forms.ComboBox();
            this.textBox4 = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Mongolian Baiti", 11.25F);
            this.button5.Location = new System.Drawing.Point(21, 662);
            this.button5.Margin = new System.Windows.Forms.Padding(4);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(304, 28);
            this.button5.TabIndex = 54;
            this.button5.Text = "Seating Arrangement Notice board";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // done
            // 
            this.done.AutoSize = true;
            this.done.BackColor = System.Drawing.Color.Red;
            this.done.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Bold);
            this.done.ForeColor = System.Drawing.Color.LimeGreen;
            this.done.Location = new System.Drawing.Point(1124, 569);
            this.done.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.done.Name = "done";
            this.done.Size = new System.Drawing.Size(273, 167);
            this.done.TabIndex = 63;
            this.done.Text = "Done";
            this.done.UseVisualStyleBackColor = false;
            this.done.Click += new System.EventHandler(this.done_Click);
            // 
            // assign
            // 
            this.assign.AutoSize = true;
            this.assign.Location = new System.Drawing.Point(1414, 510);
            this.assign.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.assign.Name = "assign";
            this.assign.Size = new System.Drawing.Size(133, 28);
            this.assign.TabIndex = 62;
            this.assign.Text = "Assign";
            this.assign.UseVisualStyleBackColor = true;
            this.assign.Click += new System.EventHandler(this.assign_Click);
            // 
            // insertCSVtoDB
            // 
            this.insertCSVtoDB.AutoSize = true;
            this.insertCSVtoDB.Location = new System.Drawing.Point(1154, 446);
            this.insertCSVtoDB.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.insertCSVtoDB.Name = "insertCSVtoDB";
            this.insertCSVtoDB.Size = new System.Drawing.Size(192, 28);
            this.insertCSVtoDB.TabIndex = 61;
            this.insertCSVtoDB.Text = "InsertDataFromCSVtoDB";
            this.insertCSVtoDB.UseVisualStyleBackColor = true;
            this.insertCSVtoDB.Click += new System.EventHandler(this.insertCSVtoDB_Click);
            // 
            // AddClassRoom
            // 
            this.AddClassRoom.AutoSize = true;
            this.AddClassRoom.Location = new System.Drawing.Point(733, 352);
            this.AddClassRoom.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AddClassRoom.Name = "AddClassRoom";
            this.AddClassRoom.Size = new System.Drawing.Size(89, 28);
            this.AddClassRoom.TabIndex = 60;
            this.AddClassRoom.Text = "Add Room";
            this.AddClassRoom.UseVisualStyleBackColor = true;
            this.AddClassRoom.Click += new System.EventHandler(this.AddClassRoom_Click);
            // 
            // GetStudentCount
            // 
            this.GetStudentCount.AutoSize = true;
            this.GetStudentCount.Location = new System.Drawing.Point(427, 526);
            this.GetStudentCount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.GetStudentCount.Name = "GetStudentCount";
            this.GetStudentCount.Size = new System.Drawing.Size(220, 28);
            this.GetStudentCount.TabIndex = 59;
            this.GetStudentCount.Text = "Get Student Count With Details";
            this.GetStudentCount.UseVisualStyleBackColor = true;
            this.GetStudentCount.Click += new System.EventHandler(this.GetStudentCount_Click);
            // 
            // txtCapacity
            // 
            this.txtCapacity.Location = new System.Drawing.Point(397, 352);
            this.txtCapacity.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtCapacity.Name = "txtCapacity";
            this.txtCapacity.Size = new System.Drawing.Size(160, 22);
            this.txtCapacity.TabIndex = 58;
            // 
            // txtRoomNumber
            // 
            this.txtRoomNumber.Location = new System.Drawing.Point(199, 351);
            this.txtRoomNumber.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtRoomNumber.Name = "txtRoomNumber";
            this.txtRoomNumber.Size = new System.Drawing.Size(160, 22);
            this.txtRoomNumber.TabIndex = 57;
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(1125, 10);
            this.dataGridView2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.RowTemplate.Height = 24;
            this.dataGridView2.Size = new System.Drawing.Size(488, 319);
            this.dataGridView2.TabIndex = 56;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 10);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1025, 319);
            this.dataGridView1.TabIndex = 39;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Mongolian Baiti", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(877, 661);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(159, 28);
            this.button3.TabIndex = 55;
            this.button3.Text = "Absent Report";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Mongolian Baiti", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(1275, 384);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 25);
            this.label11.TabIndex = 51;
            this.label11.Text = "1A";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Mongolian Baiti", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(670, 515);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 25);
            this.label8.TabIndex = 43;
            this.label8.Text = "room";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Mongolian Baiti", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(1091, 384);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(111, 25);
            this.label6.TabIndex = 41;
            this.label6.Text = "Block No";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Mongolian Baiti", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(16, 416);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(169, 25);
            this.label5.TabIndex = 40;
            this.label5.Text = "Program Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Mongolian Baiti", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(16, 523);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(154, 25);
            this.label4.TabIndex = 38;
            this.label4.Text = "Course Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Mongolian Baiti", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(17, 463);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 25);
            this.label3.TabIndex = 37;
            this.label3.Text = "Semester";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Mongolian Baiti", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(944, 515);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(200, 25);
            this.label2.TabIndex = 36;
            this.label2.Text = "Programme Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Mongolian Baiti", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 348);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 25);
            this.label1.TabIndex = 34;
            this.label1.Text = "Room No";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(564, 359);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(120, 20);
            this.checkBox1.TabIndex = 64;
            this.checkBox1.Text = "Two Per Bench";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // programFilter
            // 
            this.programFilter.DisplayMember = "program_name";
            this.programFilter.FormattingEnabled = true;
            this.programFilter.Location = new System.Drawing.Point(219, 418);
            this.programFilter.Margin = new System.Windows.Forms.Padding(4);
            this.programFilter.Name = "programFilter";
            this.programFilter.Size = new System.Drawing.Size(496, 24);
            this.programFilter.TabIndex = 70;
            this.programFilter.SelectedIndexChanged += new System.EventHandler(this.programFilter_SelectedIndexChanged);
            // 
            // courseFilter
            // 
            this.courseFilter.FormattingEnabled = true;
            this.courseFilter.Location = new System.Drawing.Point(191, 524);
            this.courseFilter.Margin = new System.Windows.Forms.Padding(4);
            this.courseFilter.Name = "courseFilter";
            this.courseFilter.Size = new System.Drawing.Size(160, 24);
            this.courseFilter.TabIndex = 69;
            this.courseFilter.SelectedIndexChanged += new System.EventHandler(this.courseFilter_SelectedIndexChanged);
            // 
            // semesterFilter
            // 
            this.semesterFilter.Font = new System.Drawing.Font("Mongolian Baiti", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.semesterFilter.FormattingEnabled = true;
            this.semesterFilter.Location = new System.Drawing.Point(199, 466);
            this.semesterFilter.Margin = new System.Windows.Forms.Padding(4);
            this.semesterFilter.Name = "semesterFilter";
            this.semesterFilter.Size = new System.Drawing.Size(160, 28);
            this.semesterFilter.TabIndex = 68;
            this.semesterFilter.SelectedIndexChanged += new System.EventHandler(this.semesterFilter_SelectedIndexChanged);
            // 
            // roomFilter
            // 
            this.roomFilter.Font = new System.Drawing.Font("Mongolian Baiti", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.roomFilter.FormattingEnabled = true;
            this.roomFilter.Location = new System.Drawing.Point(762, 509);
            this.roomFilter.Margin = new System.Windows.Forms.Padding(4);
            this.roomFilter.Name = "roomFilter";
            this.roomFilter.Size = new System.Drawing.Size(160, 28);
            this.roomFilter.TabIndex = 71;
            this.roomFilter.SelectedIndexChanged += new System.EventHandler(this.textBox1_SelectedIndexChanged);
            // 
            // textBox4
            // 
            this.textBox4.Font = new System.Drawing.Font("Mongolian Baiti", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox4.FormattingEnabled = true;
            this.textBox4.Location = new System.Drawing.Point(1156, 512);
            this.textBox4.Margin = new System.Windows.Forms.Padding(4);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(160, 28);
            this.textBox4.TabIndex = 72;
            this.textBox4.SelectedIndexChanged += new System.EventHandler(this.textBox4_SelectedIndexChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(562, 637);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(95, 23);
            this.button2.TabIndex = 74;
            this.button2.Text = "Completed";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // AssignmentPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1660, 746);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.roomFilter);
            this.Controls.Add(this.programFilter);
            this.Controls.Add(this.courseFilter);
            this.Controls.Add(this.semesterFilter);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.done);
            this.Controls.Add(this.assign);
            this.Controls.Add(this.insertCSVtoDB);
            this.Controls.Add(this.AddClassRoom);
            this.Controls.Add(this.GetStudentCount);
            this.Controls.Add(this.txtCapacity);
            this.Controls.Add(this.txtRoomNumber);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "AssignmentPage";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.AssignmentPage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button done;
        private System.Windows.Forms.Button assign;
        private System.Windows.Forms.Button insertCSVtoDB;
        private System.Windows.Forms.Button AddClassRoom;
        private System.Windows.Forms.Button GetStudentCount;
        private System.Windows.Forms.TextBox txtCapacity;
        private System.Windows.Forms.TextBox txtRoomNumber;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ComboBox programFilter;
        private System.Windows.Forms.ComboBox courseFilter;
        private System.Windows.Forms.ComboBox semesterFilter;
        private System.Windows.Forms.ComboBox roomFilter;
        private System.Windows.Forms.ComboBox textBox4;
        private System.Windows.Forms.Button button2;
    }
}

