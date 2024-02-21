using System;
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

namespace ExamSeatingSystem
{
    public partial class AssignmentPage : Form
    {
        public AssignmentPage()
        {
            InitializeComponent();
        }

        private readonly string connectionString = "Data Source=TARUNJOSHI\\SQLEXPRESS;Initial Catalog=ExamCell;Integrated Security=True";

        private void AddClassRoom_Click(object sender, EventArgs e)
        {
            AddRoom();
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

            // Add data to dataGridView2;
            dataGridView2.Rows.Add(roomNumber, capacity, capacity);

            // Clear textboxes after adding data
            txtRoomNumber.Clear();
            txtCapacity.Clear();
            //classrooms = getClassRoomDataFromDataGridView();
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
            // Create a TextBox column
            DataGridViewTextBoxColumn roomNumbCol = new DataGridViewTextBoxColumn();
            roomNumbCol.Name = "Room Number";

            DataGridViewTextBoxColumn capacityCol = new DataGridViewTextBoxColumn();
            capacityCol.Name = "Capacity";

            DataGridViewTextBoxColumn remainCapacityCol = new DataGridViewTextBoxColumn();
            remainCapacityCol.Name = "Remaining Capacity";

            // Add the column to the DataGridView
            dataGridView2.Columns.Add(roomNumbCol);
            dataGridView2.Columns.Add(capacityCol);
            dataGridView2.Columns.Add(remainCapacityCol);
        }

        List<string> SelectedProgram = new List<string>();

        private void GetStudentCount_Click(object sender, EventArgs e)
        {
            SelectedProgram.Add(textBox2.Text);
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                CountStudentsByDetails(con);
            }
        }


        // Define a DataTable at class level to hold combined results
        private DataTable combinedDataTable = new DataTable();
        private void CountStudentsByDetails(SqlConnection con)
        {
            string programFilter = textBox2.Text;
            string courseFilter = "Modern Computer Architecture";
            string semesterFilter = "1";
            string query = "SELECT phc.semester_number AS Sem, phc.program_name AS Program, phc.course_name AS Course, MIN(s.roll_number) AS FromSeat, MAX(s.roll_number) AS ToSeat, COUNT(s.roll_number) AS TotalStudents, COUNT(s.isAssigned) AS UnAssigned FROM ProgramHasCourse AS phc INNER JOIN StudentEnrollsProgramInYear AS sep ON phc.ProgCour_ID = sep.ProgCour_ID INNER JOIN Student AS s ON sep.roll_number = s.roll_number WHERE phc.program_name = @Program AND phc.semester_number = @Semester AND phc.course_name = @Course AND s.isAssigned = 0 GROUP BY phc.program_name, phc.course_name, phc.semester_number;";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Program", programFilter);
                cmd.Parameters.AddWithValue("@Course", courseFilter);
                cmd.Parameters.AddWithValue("@Semester", semesterFilter);
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                ad.Fill(dt);
                //Merge the new DataTable with the combinedDataTable
                combinedDataTable.Merge(dt);
                dataGridView1.DataSource = combinedDataTable; // Assign combinedDataTable to the DataGridView
            }
        }

        private void assign_Click(object sender, EventArgs e)
        {
            // Iterate through each row in the DataGridView
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // Skip the last row if it's the new row for entering data
                if (!row.IsNewRow)
                {
                    if (row.Cells[0].Value.ToString() == textBox4.Text.ToString())
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            int semester = (int)row.Cells[1].Value;
                            string program = (string)row.Cells[2].Value;
                            string course = (string)row.Cells[3].Value;
                            CreateDictWithBenchAndStud(program, semester, course);
                        }
                    }
                }
            }
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    AssignStudents(textBox4.Text);

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void CreateDictWithBenchAndStud(string program, int semester, string course)
        {
            Dictionary<string, long> BenchWithStud = new Dictionary<string, long>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = $"SELECT phc.program_name, phc.semester_number, phc.course_name, sep.roll_number FROM ProgramHasCourse AS phc INNER JOIN StudentEnrollsProgramInYear AS sep ON phc.ProgCour_ID = sep.ProgCour_ID INNER JOIN Student AS s ON sep.roll_number = s.roll_number WHERE phc.program_name = @Program AND phc.semester_number = @Semester AND phc.course_name = @Course AND s.isAssigned = 0 AND s.isActive = 1;";

                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@Program", program);
                    command.Parameters.AddWithValue("@Semester", semester);
                    command.Parameters.AddWithValue("@Course", course);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        int studentassigned = 0;
                        string classroomNumber = null;
                        while (reader.Read())
                        {
                            long seatNumber = reader.GetInt64(3);
                            foreach (var classroom in classrooms)
                            {
                                if (classroom.Key == textBox1.Text)
                                {
                                    classroomNumber = classroom.Key;
                                    if (!blockNumber.ContainsKey(classroom.Key))
                                    {
                                        blockNumber.Add(classroom.Key, program);

                                    }
                                    //blockNumber.Add(program);
                                    foreach (var bench in classroom.Value)
                                    {
                                        if (bench.Count < 2)
                                        {
                                            //Console.WriteLine(CompareWithBenchsFirstStudent(course, bench));
                                            var studentTuple = (rollNumber, course);
                                            bench.Add(studentTuple);
                                            assigned = true;
                                            studentassigned++;
                                            break;
                                        }
                                    }
                                    if (assigned)
                                    {
                                        AssignList.Add(rollNumber);
                                        break;
                                    }
                                }
                            }
                            if (!assigned)
                            {
                                //Instead of adding it in document i am first storing them in dictionary
                                Dictionary<long, string> UnAssigned = new Dictionary<long, string>();
                                UnAssigned.Add(rollNumber, course);
                                //document.Add(new Paragraph($"Unable to assign student {rollNumber} of {course}"));
                            }

                        }
                        ChangeDataGridView2(classroomNumber, studentassigned);
                    }

                    using (SqlCommand updateCmd = new SqlCommand("UPDATE Student SET assigned = 1 WHERE seat_number = @RollNumber", con))
                    {
                        foreach (long rollNumber in AssignList)
                        {
                            // Clear the parameters collection before adding new parameters
                            updateCmd.Parameters.Clear();
                            // Add the parameter for the current roll number
                            updateCmd.Parameters.AddWithValue("@RollNumber", rollNumber);
                            updateCmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        private void AssignStudents(string program)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "INSERT INTO StudentSeatInClassroom(room_number, roll_number, bench_name, block_number) VALUES (@RoomNumber, @SeatNumber, @BenchName, @BlockNumber);";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@RoomNumber", textBox1.Text);
                    cmd.Parameters.AddWithValue("@SeatNumber", 101);
                    cmd.Parameters.AddWithValue("@BenchName", "A1");
                    cmd.Parameters.AddWithValue("@BlockNumber", label1.Text);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void insertCSVtoDB_Click(object sender, EventArgs e)
        {
            InsertProgrammeSemCourseData(@"C:\Users\Pulin\Desktop\Project\New Text Document.csv");
        }





        private void InsertProgrammeSemCourseData(string csvFilePath)
        {
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(csvFilePath))
                {
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = true;

                    // Skip header line if it exists
                    if (!csvReader.EndOfData)
                    {
                        csvReader.ReadLine(); // Skip header line
                    }

                    using (SqlConnection dbConnection = new SqlConnection(@"Data Source=Short-Feet\SQLEXPRESS; Initial Catalog=ExamCell; Integrated Security=SSPI;"))
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
    }
}
