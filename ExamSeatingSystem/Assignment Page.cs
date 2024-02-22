﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.VisualBasic.FileIO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace ExamSeatingSystem
{
    public partial class AssignmentPage : Form
    {
        public AssignmentPage()
        {
            InitializeComponent();
        }

        private readonly string connectionString = "Data Source=TARUNJOSHI\\SQLEXPRESS;Initial Catalog=examcell;Integrated Security=True";

        private void AddClassRoom_Click(object sender, EventArgs e)
        {
            AddRoom();
            GetClassroomData();
        }

        private void AddRoom()
        {
            string roomNumber = txtRoomNumber.Text;
            int capacity;
            if (string.IsNullOrEmpty(roomNumber))
            {
                MessageBox.Show("Please Enter Room Number. e.g. 501");
                return;
            }
            else
            {
                if (!int.TryParse(txtCapacity.Text, out capacity))
                {
                    MessageBox.Show("Invalid capacity. Please enter a valid number.");
                    return;
                }
            }

            if (!checkBox1.Checked)
            {
                InitializeClassroomSeats(roomNumber, capacity, "A");
            }
            else
            {
                InitializeClassroomSeats(roomNumber, capacity / 2, "A");
                InitializeClassroomSeats(roomNumber, capacity / 2, "B");
            }

            // Clear textboxes after adding data
            txtRoomNumber.Clear();
            txtCapacity.Clear();
        }

        private void InitializeClassroomSeats(string roomNumber, int capacity, string name)
        {
            for (int i = 1; i <= capacity; i++)
            {
                string benchName = name + i;
                try
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        string query = "INSERT INTO classroom (room_number, bench_name) VALUES (@RoomNumber, @BenchName)";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@RoomNumber", roomNumber);
                            cmd.Parameters.AddWithValue("@BenchName", benchName);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private void AssignmentPage_Load(object sender, EventArgs e)
        {
            GetClassroomData();
        }

        private void GetClassroomData()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT room_number as RoomNumber, COUNT(DISTINCT bench_name) AS Capacity, SUM(CASE WHEN isEmpty = 1 THEN 1 ELSE 0 END) AS RemainingCapacity FROM classroom GROUP BY room_number;";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataAdapter ad = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    ad.Fill(dt);
                    dataGridView2.DataSource = dt;
                }
            }
        }
        HashSet<List<string>> PSC = new HashSet<List<string>>();
        private void GetStudentCount_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            list.Add(programFilter.Text);
            list.Add(semesterFilter.Text);
            list.Add(courseFilter.Text);
            PSC.Add(list);
            combinedDataTable = new DataTable();
            foreach (List<string> L in PSC)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    CountStudentsByDetails(L, con);
                }
            }
        }

        // Define a DataTable at class level to hold combined results
        private DataTable combinedDataTable = new DataTable();
        private void CountStudentsByDetails(List<string> L, SqlConnection con)
        {
            string programFilter = L[0];
            string courseFilter = L[2];
            string semesterFilter = L[1];
            string query = "SELECT phc.semester_number AS Sem, phc.program_name AS Program, phc.course_name AS Course, MIN(s.roll_number) AS FromSeat, MAX(s.roll_number) AS ToSeat, COUNT(s.roll_number) AS TotalStudents, SUM(CASE WHEN s.isAssigned = 0 THEN 1 ELSE 0 END) AS UnAssigned FROM ProgramHasCourse AS phc INNER JOIN StudentEnrollsProgramInYear AS sep ON phc.ProgCour_ID = sep.ProgCour_ID INNER JOIN Student AS s ON sep.roll_number = s.roll_number WHERE phc.program_name = @Program AND phc.semester_number = @Semester AND phc.course_name = @Course GROUP BY phc.program_name, phc.course_name, phc.semester_number;";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Program", programFilter);
                cmd.Parameters.AddWithValue("@Course", courseFilter);
                cmd.Parameters.AddWithValue("@Semester", semesterFilter);
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                ad.Fill(dt);

                // Add a new column for the serial number
                DataColumn serialNumberColumn = new DataColumn("SerialNumber", typeof(int));
                serialNumberColumn.AutoIncrement = true;
                serialNumberColumn.AutoIncrementSeed = 1;
                serialNumberColumn.AutoIncrementStep = 1;
                dt.Columns.Add(serialNumberColumn);

                // Rearrange columns if you want the serial number to be at a specific position
                dt.Columns["SerialNumber"].SetOrdinal(0);

                //Merge the new DataTable with the combinedDataTable
                combinedDataTable.Merge(dt);
                dataGridView1.DataSource = combinedDataTable; // Assign combinedDataTable to the DataGridView
                int serialNumber = 1;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        row.Cells["SerialNumber"].Value = serialNumber++;
                    }
                }
            }
        }

        private void assign_Click(object sender, EventArgs e)
        {
            List<long> studentsList = null;
            // Iterate through each row in the DataGridView
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // Skip the last row if it's the new row for entering data
                if (!row.IsNewRow)
                {
                    if (row.Cells["SerialNumber"].Value.ToString() == textBox4.Text.ToString())
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            int semester = (int)row.Cells[1].Value;
                            string program = (string)row.Cells[2].Value;
                            string course = (string)row.Cells[3].Value;
                            studentsList = GetStudentsFromDB(program, semester, course);
                        }
                    }
                }
            }
            string roomNumber = textBox1.Text.ToString();
            List<string> benchesList = GetBenchFromDB(roomNumber);
            Dictionary<string, long> studentOnBench = new Dictionary<string, long>();
            int loopIteration = studentsList.Count < benchesList.Count ? studentsList.Count : benchesList.Count;
            for (int i = 0; i < loopIteration; i++)
            {
                studentOnBench.Add(benchesList[i], studentsList[i]);
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    AssignStudents(studentOnBench);
                    MessageBox.Show("Success");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
            GetClassroomData();
        }

        private List<long> GetStudentsFromDB(string program, int semester, string course)
        {
            List<long> studentsList = new List<long>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = $"SELECT phc.program_name, phc.semester_number, phc.course_name, sep.roll_number FROM ProgramHasCourse AS phc INNER JOIN StudentEnrollsProgramInYear AS sep ON phc.ProgCour_ID = sep.ProgCour_ID INNER JOIN Student AS s ON sep.roll_number = s.roll_number WHERE phc.program_name = @Program AND phc.semester_number = @Semester AND phc.course_name = @Course AND s.isAssigned = 0 AND s.isActive = 1;";

                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@Program", program);
                    command.Parameters.AddWithValue("@Semester", semester);
                    command.Parameters.AddWithValue("@Course", course);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            long seatNumber = reader.GetInt64(3);
                            studentsList.Add(seatNumber);
                        }
                    }
                }
            }
            return studentsList;
        }

        private List<string> GetBenchFromDB(string roomNumber)
        {
            List<string> benchesList = new List<string>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = $"SELECT * FROM classroom WHERE room_number = @RoomNumber AND isEmpty = 1";

                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@RoomNumber", roomNumber);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string benchName = reader.GetString(1);
                            benchesList.Add(benchName);
                        }
                    }
                }
            }
            return benchesList;
        }

        private void AssignStudents(Dictionary<string, long> studentOnBench)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                foreach (KeyValuePair<string, long> kvp in studentOnBench)
                {
                    string query = "INSERT INTO StudentSeatInClassroom(room_number, roll_number, bench_name, block_number) VALUES (@RoomNumber, @SeatNumber, @BenchName, @BlockNumber);";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@RoomNumber", textBox1.Text);
                        cmd.Parameters.AddWithValue("@SeatNumber", kvp.Value);
                        cmd.Parameters.AddWithValue("@BenchName", kvp.Key);
                        cmd.Parameters.AddWithValue("@BlockNumber", label11.Text);
                        cmd.ExecuteNonQuery();
                    }
                    string updateStudentQuery = "Update Student SET isAssigned = 1 where roll_number = @SeatNumber";
                    using (SqlCommand updateCmd = new SqlCommand(updateStudentQuery, con))
                    {
                        updateCmd.Parameters.AddWithValue("@SeatNumber", kvp.Value);
                        updateCmd.ExecuteNonQuery();
                    }

                    string updateBenchQuery = "UPDATE classroom SET isEmpty = 0 WHERE bench_name = @BenchName AND room_number = @RoomNumber;";
                    using (SqlCommand updateCommand = new SqlCommand(updateBenchQuery, con))
                    {
                        updateCommand.Parameters.AddWithValue("@RoomNumber", textBox1.Text);
                        updateCommand.Parameters.AddWithValue("@BenchName", kvp.Key);
                        updateCommand.ExecuteNonQuery();
                    }
                }
            }
        }

        private void insertCSVtoDB_Click(object sender, EventArgs e)
        {
            InsertProgrammeSemCourseData(@"C:\Users\tarun\Downloads\New Text Document.csv");
            MessageBox.Show("Successfully Inserted Data");
        }

        private void InsertProgrammeSemCourseData(string csvFilePath)
        {
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(csvFilePath))
                {
                    MessageBox.Show("Start");
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = true;

                    // Skip header line if it exists
                    if (!csvReader.EndOfData)
                    {
                        csvReader.ReadLine(); // Skip header line
                    }

                    using (SqlConnection dbConnection = new SqlConnection(connectionString))
                    {
                        dbConnection.Open();

                        while (!csvReader.EndOfData)
                        {
                            string[] fields = csvReader.ReadFields();

                            // Check if all fields exist
                            if (fields.Length < 3)
                            {
                                continue; // Skip line if not enough fields
                            }
                            string programmeName = fields[0];
                            int semesterNumber;
                            if (!int.TryParse(fields[1], out semesterNumber))
                            {
                                continue; // Skip line if semester number is invalid
                            }
                            string courseName = fields[2];

                            // Check if Programme already exists
                            string checkProgrammeQuery = "SELECT COUNT(*) FROM Program WHERE program_name = @ProgrammeName";
                            using (SqlCommand checkProgrammeCommand = new SqlCommand(checkProgrammeQuery, dbConnection))
                            {
                                checkProgrammeCommand.Parameters.AddWithValue("@ProgrammeName", programmeName);
                                int programmeCount = (int)checkProgrammeCommand.ExecuteScalar();
                                if (programmeCount == 0)
                                {
                                    // Insert Programme
                                    string insertProgrammeQuery = "INSERT INTO Program (program_name) VALUES (@ProgrammeName)";
                                    using (SqlCommand insertProgrammeCommand = new SqlCommand(insertProgrammeQuery, dbConnection))
                                    {
                                        insertProgrammeCommand.Parameters.AddWithValue("@ProgrammeName", programmeName);
                                        insertProgrammeCommand.ExecuteNonQuery();
                                    }
                                }
                            }

                            // Check if Semester already exists
                            string checkSemesterQuery = "SELECT COUNT(*) FROM Semester WHERE semester_number = @SemesterNumber";
                            using (SqlCommand checkSemesterCommand = new SqlCommand(checkSemesterQuery, dbConnection))
                            {
                                checkSemesterCommand.Parameters.AddWithValue("@SemesterNumber", semesterNumber);
                                int semesterCount = (int)checkSemesterCommand.ExecuteScalar();
                                if (semesterCount == 0)
                                {
                                    // Insert Semester
                                    string insertSemesterQuery = "INSERT INTO Semester (semester_number) VALUES (@SemesterNumber)";
                                    using (SqlCommand insertSemesterCommand = new SqlCommand(insertSemesterQuery, dbConnection))
                                    {
                                        insertSemesterCommand.Parameters.AddWithValue("@SemesterNumber", semesterNumber);
                                        insertSemesterCommand.ExecuteNonQuery();
                                    }

                                }
                            }

                            // Check if Course already exists
                            string checkCourseQuery = "SELECT COUNT(*) FROM Course WHERE course_name = @CourseName";
                            using (SqlCommand checkCourseCommand = new SqlCommand(checkCourseQuery, dbConnection))
                            {
                                checkCourseCommand.Parameters.AddWithValue("@CourseName", courseName);
                                int courseCount = (int)checkCourseCommand.ExecuteScalar();
                                if (courseCount == 0)
                                {
                                    // Insert Course
                                    string insertCourseQuery = "INSERT INTO Course (course_name) VALUES (@CourseName)";
                                    using (SqlCommand insertCourseCommand = new SqlCommand(insertCourseQuery, dbConnection))
                                    {
                                        insertCourseCommand.Parameters.AddWithValue("@CourseName", courseName);
                                        insertCourseCommand.ExecuteNonQuery();
                                    }
                                }
                            }

                            // Check if ProgrammeHasCourses already exists
                            string checkProgrammeHasCoursesQuery = "SELECT COUNT(*) FROM ProgramHasCourse WHERE program_name = @ProgrammeName AND semester_number = @SemesterNumber AND course_name = @CourseName";
                            using (SqlCommand checkProgrammeHasCoursesCommand = new SqlCommand(checkProgrammeHasCoursesQuery, dbConnection))
                            {
                                checkProgrammeHasCoursesCommand.Parameters.AddWithValue("@ProgrammeName", programmeName);
                                checkProgrammeHasCoursesCommand.Parameters.AddWithValue("@SemesterNumber", semesterNumber);
                                checkProgrammeHasCoursesCommand.Parameters.AddWithValue("@CourseName", courseName);
                                int programmeHasCoursesCount = (int)checkProgrammeHasCoursesCommand.ExecuteScalar();
                                if (programmeHasCoursesCount == 0)
                                {
                                    // Insert ProgrammeHasCourses
                                    string insertProgrammeHasCoursesQuery = "INSERT INTO ProgramHasCourse (program_name, semester_number, course_name) VALUES (@ProgrammeName, @SemesterNumber, @CourseName)";
                                    using (SqlCommand insertProgrammeHasCoursesCommand = new SqlCommand(insertProgrammeHasCoursesQuery, dbConnection))
                                    {
                                        insertProgrammeHasCoursesCommand.Parameters.AddWithValue("@ProgrammeName", programmeName);
                                        insertProgrammeHasCoursesCommand.Parameters.AddWithValue("@SemesterNumber", semesterNumber);
                                        insertProgrammeHasCoursesCommand.Parameters.AddWithValue("@CourseName", courseName);
                                        insertProgrammeHasCoursesCommand.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting data from CSV into SQL Server: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void done_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string updateStudentQuery = "UPDATE Student SET isAssigned = 0;";
                using (SqlCommand updateCmd = new SqlCommand(updateStudentQuery, con))
                {
                    updateCmd.ExecuteNonQuery();
                }

                string updateBenchQuery = "UPDATE classroom SET isEmpty = 1;";
                using (SqlCommand updateCommand = new SqlCommand(updateBenchQuery, con))
                {
                    updateCommand.ExecuteNonQuery();
                } 

                string deleteSSICQuery = "delete from StudentSeatInClassroom;";
                using (SqlCommand deleteCommand = new SqlCommand(deleteSSICQuery, con))
                {
                    deleteCommand.ExecuteNonQuery();
                }

                string deleteClassroomQuery = "delete from classroom;";
                using (SqlCommand deleteCommand = new SqlCommand(deleteClassroomQuery, con))
                {
                    deleteCommand.ExecuteNonQuery();
                }

            }
            MessageBox.Show("Success");
        }
    }
}
