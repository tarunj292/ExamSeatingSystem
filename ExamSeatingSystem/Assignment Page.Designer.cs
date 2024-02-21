﻿namespace ExamSeatingSystem
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.comboBox5 = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Mongolian Baiti", 11.25F);
            this.button5.Location = new System.Drawing.Point(86, 643);
            this.button5.Margin = new System.Windows.Forms.Padding(4);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(304, 28);
            this.button5.TabIndex = 54;
            this.button5.Text = "Seating Arrangement Notice board";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // done
            // 
            this.done.AutoSize = true;
            this.done.BackColor = System.Drawing.Color.Red;
            this.done.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Bold);
            this.done.ForeColor = System.Drawing.Color.LimeGreen;
            this.done.Location = new System.Drawing.Point(1124, 568);
            this.done.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.done.Name = "done";
            this.done.Size = new System.Drawing.Size(256, 167);
            this.done.TabIndex = 63;
            this.done.Text = "Done";
            this.done.UseVisualStyleBackColor = false;
            // 
            // assign
            // 
            this.assign.AutoSize = true;
            this.assign.Location = new System.Drawing.Point(1126, 523);
            this.assign.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.assign.Name = "assign";
            this.assign.Size = new System.Drawing.Size(133, 26);
            this.assign.TabIndex = 62;
            this.assign.Text = "Assign";
            this.assign.UseVisualStyleBackColor = true;
            this.assign.Click += new System.EventHandler(this.assign_Click);
            // 
            // insertCSVtoDB
            // 
            this.insertCSVtoDB.AutoSize = true;
            this.insertCSVtoDB.Location = new System.Drawing.Point(1148, 467);
            this.insertCSVtoDB.Name = "insertCSVtoDB";
            this.insertCSVtoDB.Size = new System.Drawing.Size(192, 26);
            this.insertCSVtoDB.TabIndex = 61;
            this.insertCSVtoDB.Text = "InsertDataFromCSVtoDB";
            this.insertCSVtoDB.UseVisualStyleBackColor = true;
            // 
            // AddClassRoom
            // 
            this.AddClassRoom.AutoSize = true;
            this.AddClassRoom.Location = new System.Drawing.Point(607, 352);
            this.AddClassRoom.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AddClassRoom.Name = "AddClassRoom";
            this.AddClassRoom.Size = new System.Drawing.Size(82, 26);
            this.AddClassRoom.TabIndex = 60;
            this.AddClassRoom.Text = "Add Room";
            this.AddClassRoom.UseVisualStyleBackColor = true;
            this.AddClassRoom.Click += new System.EventHandler(this.AddClassRoom_Click);
            // 
            // GetStudentCount
            // 
            this.GetStudentCount.AutoSize = true;
            this.GetStudentCount.Location = new System.Drawing.Point(380, 584);
            this.GetStudentCount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.GetStudentCount.Name = "GetStudentCount";
            this.GetStudentCount.Size = new System.Drawing.Size(197, 26);
            this.GetStudentCount.TabIndex = 59;
            this.GetStudentCount.Text = "Get Student Count With Details";
            this.GetStudentCount.UseVisualStyleBackColor = true;
            this.GetStudentCount.Click += new System.EventHandler(this.GetStudentCount_Click);
            // 
            // txtCapacity
            // 
            this.txtCapacity.Location = new System.Drawing.Point(398, 352);
            this.txtCapacity.Name = "txtCapacity";
            this.txtCapacity.Size = new System.Drawing.Size(160, 22);
            this.txtCapacity.TabIndex = 58;
            // 
            // txtRoomNumber
            // 
            this.txtRoomNumber.Location = new System.Drawing.Point(199, 351);
            this.txtRoomNumber.Name = "txtRoomNumber";
            this.txtRoomNumber.Size = new System.Drawing.Size(160, 22);
            this.txtRoomNumber.TabIndex = 57;
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(1126, 10);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.RowTemplate.Height = 24;
            this.dataGridView2.Size = new System.Drawing.Size(670, 319);
            this.dataGridView2.TabIndex = 56;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 10);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1025, 319);
            this.dataGridView1.TabIndex = 39;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Mongolian Baiti", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(804, 630);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(159, 28);
            this.button3.TabIndex = 55;
            this.button3.Text = "Absent Report";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.Location = new System.Drawing.Point(601, 643);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(133, 26);
            this.button1.TabIndex = 35;
            this.button1.Text = "Get Data by Details";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Mongolian Baiti", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(455, 687);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(304, 28);
            this.button2.TabIndex = 53;
            this.button2.Text = "Seating Arrangement Classroom";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // comboBox5
            // 
            this.comboBox5.Font = new System.Drawing.Font("Mongolian Baiti", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox5.FormattingEnabled = true;
            this.comboBox5.Items.AddRange(new object[] {
            "Sem 1",
            "Sem 2",
            "Sem 3",
            "Sem 4",
            "Sem 5",
            "Sem 6"});
            this.comboBox5.Location = new System.Drawing.Point(878, 464);
            this.comboBox5.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox5.Name = "comboBox5";
            this.comboBox5.Size = new System.Drawing.Size(188, 28);
            this.comboBox5.TabIndex = 52;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Mongolian Baiti", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(878, 348);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(82, 25);
            this.label11.TabIndex = 51;
            this.label11.Text = "label11";
            // 
            // textBox4
            // 
            this.textBox4.Font = new System.Drawing.Font("Mongolian Baiti", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox4.Location = new System.Drawing.Point(878, 524);
            this.textBox4.Margin = new System.Windows.Forms.Padding(4);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(188, 29);
            this.textBox4.TabIndex = 50;
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Mongolian Baiti", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(199, 583);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(160, 29);
            this.textBox2.TabIndex = 49;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Mongolian Baiti", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(464, 529);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(160, 29);
            this.textBox1.TabIndex = 48;
            // 
            // comboBox4
            // 
            this.comboBox4.Font = new System.Drawing.Font("Mongolian Baiti", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Items.AddRange(new object[] {
            "First Year (FY)",
            "Second Year (SY)",
            "Third Year (TY)",
            "Master\'s"});
            this.comboBox4.Location = new System.Drawing.Point(878, 405);
            this.comboBox4.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(188, 28);
            this.comboBox4.TabIndex = 47;
            // 
            // comboBox3
            // 
            this.comboBox3.Font = new System.Drawing.Font("Mongolian Baiti", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] {
            "Regular",
            "ATKT",
            "Supplementary"});
            this.comboBox3.Location = new System.Drawing.Point(199, 465);
            this.comboBox3.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(160, 28);
            this.comboBox3.TabIndex = 46;
            // 
            // comboBox2
            // 
            this.comboBox2.Font = new System.Drawing.Font("Mongolian Baiti", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(199, 405);
            this.comboBox2.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(160, 28);
            this.comboBox2.TabIndex = 45;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Mongolian Baiti", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(251, 524);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(92, 25);
            this.label9.TabIndex = 44;
            this.label9.Text = "End No";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Mongolian Baiti", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(375, 530);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 25);
            this.label8.TabIndex = 43;
            this.label8.Text = "room";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Mongolian Baiti", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(719, 405);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 25);
            this.label7.TabIndex = 42;
            this.label7.Text = "Class ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Mongolian Baiti", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(715, 348);
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
            this.label5.Location = new System.Drawing.Point(17, 582);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(154, 25);
            this.label5.TabIndex = 40;
            this.label5.Text = "Course Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Mongolian Baiti", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(17, 524);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 25);
            this.label4.TabIndex = 38;
            this.label4.Text = "Starting No";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Mongolian Baiti", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(17, 463);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(145, 25);
            this.label3.TabIndex = 37;
            this.label3.Text = "Examination";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Mongolian Baiti", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(648, 531);
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
            this.checkBox1.Location = new System.Drawing.Point(464, 405);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(120, 20);
            this.checkBox1.TabIndex = 64;
            this.checkBox1.Text = "Two Per Bench";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // AssignmentPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1870, 746);
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
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.comboBox5);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.comboBox4);
            this.Controls.Add(this.comboBox3);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
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
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox comboBox5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox comboBox4;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}

