using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.VisualBasic.Devices;
using Microsoft.VisualBasic.FileIO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data.Common;
using System.Collections;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace ExamSeatingSystem
{
    public partial class AssignmentPage : Form
    {
        public AssignmentPage()
        {
            InitializeComponent();

        }

        private readonly string connectionString = "Data Source=TARUNJOSHI\\SQLEXPRESS;Initial Catalog=ExamCell;Integrated Security=True;";

        private void AddClassRoom_Click(object sender, EventArgs e)
        {
            AddRoom();
            GetClassroomData();
            roomFilter.DataSource = GetRooms();
            roomFilter.DisplayMember = "room_number";
            roomFilter.Text = null;
        }

        private DataTable GetRooms()
        {
            string query = "SELECT distinct room_number FROM classroom where isEmpty = 1";
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        dataTable.Load(reader);
                    }
                }
            }

            return dataTable;
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
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string deleteQuery = "DELETE from classroom where room_number = @RoomNumber";
                using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, con))
                {
                    deleteCmd.Parameters.AddWithValue("@RoomNumber", roomNumber);
                    deleteCmd.ExecuteNonQuery();
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
            programFilter.DataSource = GetProgramNames();
            programFilter.DisplayMember = "program_name";
            programFilter.Text = null;


            semesterFilter.Enabled = false;
            semesterFilter.Text = null;
            label3.ForeColor = Color.Gray;


            courseFilter.Enabled = false;
            courseFilter.Text = null;
            label4.ForeColor = Color.Gray;

            roomFilter.DataSource = GetRooms();
            roomFilter.DisplayMember = "room_number";
            roomFilter.Text = null;

            CountStudentsByDetails();
            GetClassroomData();
        }

        private DataTable GetProgramNames()
        {
            string query = "SELECT distinct program_name FROM ProgramHasCourse";
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        dataTable.Load(reader);
                    }
                }
            }

            return dataTable;
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
        Dictionary<string, HashSet<string>> PSCHashSet = new Dictionary<string, HashSet<string>>();
        private void GetStudentCount_Click(object sender, EventArgs e)
        {
            HashSet<string> hs = new HashSet<string>();
            hs.Add(programFilter.Text);
            hs.Add(semesterFilter.Text);
            hs.Add(courseFilter.Text);
            string k = programFilter.Text + semesterFilter.Text + courseFilter.Text;
            if (!PSCHashSet.ContainsKey(k))
            {
                PSCHashSet.Add(k, hs);
            }
            else
            {
                MessageBox.Show("Entered value already exists");
            }
            CountStudentsByDetails();
            textBox4.DataSource = GetSerial();
            textBox4.DisplayMember = "serial_number";
            textBox4.Text = null;
        }

        private DataTable GetSerial()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("serial_number", typeof(string));
            //dt.Rows.Add("1");

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    if (Convert.ToInt32(row.Cells[7].Value) > 0)
                    {
                        string serial = (Convert.ToInt32(row.Cells[0].Value)).ToString();
                        dt.Rows.Add(serial);
                    }
                }
            }
            return dt;
        }

        private void CountStudentsByDetails()
        {
            DataTable combinedDataTable = new DataTable();
            HashSet<List<string>> PSCList = new HashSet<List<string>>();
            foreach (KeyValuePair<string, HashSet<string>> entry in PSCHashSet)
            {
                string key = entry.Key;
                HashSet<string> hs = entry.Value;
                PSCList.Add(hs.ToList());
            }
            foreach (List<string> L in PSCList)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
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
            }
        }

        string programByRoomsBenchName;
        string programBySeatNumber;
        Boolean stop = false;
        private void assign_Click(object sender, EventArgs e)
        {
            try
            {
                List<long> studentsList = null;
                string roomNumber = roomFilter.Text.ToString();
                // Iterate through each row in the DataGridView
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    // Skip the last row if it's the new row for entering data
                    if (!row.IsNewRow)
                    {
                        if (row.Cells["SerialNumber"].Value.ToString() == textBox4.Text.ToString())
                        {
                            string program = null;
                            string course = null;
                            foreach (DataGridViewCell cell in row.Cells)
                            {
                                int semester = Convert.ToInt32(row.Cells[1].Value);
                                program = (string)row.Cells[2].Value;
                                course = (string)row.Cells[3].Value;
                                studentsList = GetStudentsFromDB(program, semester, course);
                            }
                            GenerateBlockNumber(roomNumber, program, course);
                        }
                    }
                }
                List<string> benchesList = GetBenchesFromDB(roomNumber);
                Dictionary<string, long> studentOnBench = new Dictionary<string, long>();
                int loopIteration = 0;
                if (studentsList == null || benchesList == null)
                {
                    MessageBox.Show("No students or benches available for assignment.");
                    return;
                }

                for (int i = 0; i < loopIteration; i++)
                {
                    if (benchesList[i][0] == 'B')
                    {
                        GetProgramBySeatNumber(studentsList[i]);
                        GetProgramByRoomsBenchName(roomNumber, benchesList[i]);
                        if (stop)
                        {
                            break;
                        }
                        if (programByRoomsBenchName == programBySeatNumber)
                        {
                            studentsList.Remove(i);
                        }
                        else
                        {
                            studentOnBench.Add(benchesList[i], studentsList[i]);
                        }
                    }
                    else
                    {
                        studentOnBench.Add(benchesList[i], studentsList[i]);
                    }
                }


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
            UpdateUI();
        }

        private void UpdateUI()
        {

            // Count students and refresh classroom data
            CountStudentsByDetails();
            GetClassroomData();

            // Update roomFilter dropdown
            roomFilter.DataSource = GetRooms();
            roomFilter.DisplayMember = "room_number";
            roomFilter.Text = null;

            // Update textBox4 dropdown
            textBox4.DataSource = GetSerial();
            textBox4.DisplayMember = "serial_number";
            textBox4.Text = null;

        }
        private void GetProgramByRoomsBenchName(string roomNumber, string benchName)
        {
            benchName = benchName.Replace('B', 'A');
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string selectQuery = "SELECT phc.program_name FROM StudentSeatInClassroom ssc JOIN StudentEnrollsProgramInYear sep ON ssc.roll_number = sep.roll_number JOIN ProgramHasCourse phc ON sep.ProgCour_ID = phc.ProgCour_ID WHERE ssc.room_number = @RoomNumber AND ssc.bench_Name = @BenchName;";
                using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                {
                    cmd.Parameters.AddWithValue("@RoomNumber", roomNumber);
                    cmd.Parameters.AddWithValue("@BenchName", benchName);
                    stop = false;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            stop = true;
                        }
                        while (reader.Read())
                        {
                            programByRoomsBenchName = reader.GetString(0);
                        }
                    }
                }
            }
        }

        private void GetProgramBySeatNumber(long seatNumber)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT DISTINCT phc.program_name FROM StudentEnrollsProgramInYear sep JOIN ProgramHasCourse phc ON sep.ProgCour_ID = phc.ProgCour_ID WHERE sep.roll_number = @SeatNumber;";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@SeatNumber", seatNumber);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            programBySeatNumber = reader.GetString(0);
                        }
                    }
                }
            }
        }

        List<string> roomNumberList = new List<string>();
        List<string> programNameList = new List<string>();
        List<string> courseNameList = new List<string>();
        private void GenerateBlockNumber(string roomNumber, string program, string course)
        {
            roomNumberList.Add(roomNumber);
            programNameList.Add(program);
            courseNameList.Add(course);
            int blockPart1 = roomNumberList.IndexOf(roomNumber) + 1;
            int blockPart2 = programNameList.IndexOf(program) + 1;
            int blockPart3 = courseNameList.IndexOf(course) + 65;
            label11.Text = $"{blockPart1}{blockPart2}{(char)blockPart3}";
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

        private List<string> GetBenchesFromDB(string roomNumber)
        {
            List<string> benchesList = new List<string>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = $"SELECT * FROM classroom WHERE room_number = @RoomNumber AND isEmpty = 1 ORDER BY LEFT(bench_name, 1), TRY_CONVERT(int, SUBSTRING(bench_name, 2, LEN(bench_name))) ASC;";

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
                        cmd.Parameters.AddWithValue("@RoomNumber", roomFilter.Text);
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
                        updateCommand.Parameters.AddWithValue("@RoomNumber", roomFilter.Text);
                        updateCommand.Parameters.AddWithValue("@BenchName", kvp.Key);
                        updateCommand.ExecuteNonQuery();
                    }
                }
            }
        }

        private void insertCSVtoDB_Click(object sender, EventArgs e)
        {
            Task.Run(() => InsertProgrammeSemCourseData(@"C:\Users\tarun\Downloads\New Text Document.csv"));
            //InsertProgrammeSemCourseData(@"C:\Users\admin\Downloads\New Text Document.csv");
            //MessageBox.Show("Successfully Inserted Data");
        }

        private void InsertProgrammeSemCourseData(string csvFilePath)
        {
            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(csvFilePath))
                {
                    MessageBox.Show("Started, Please perform only adding room's till I am working");
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
                MessageBox.Show("Successfully Inserted Data, Now can fetch students data");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting data from CSV into SQL Server: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        ArrayList dataList = new ArrayList();
        HashSet<string> roomNumberHashSet = new HashSet<string>();
        private void WillGiveLater2()
        {
            dataList = new ArrayList();
            roomNumberHashSet = new HashSet<string>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string selectQuery = @"
        SELECT
    sic.room_number,
    sic.block_number,
    sic.bench_name,
    sic.roll_number,
    phc.program_name
FROM
    StudentSeatInClassroom sic
INNER JOIN
    StudentEnrollsProgramInYear sep ON sic.roll_number = sep.roll_number
INNER JOIN
    ProgramHasCourse phc ON sep.ProgCour_ID = phc.ProgCour_ID
ORDER BY
    sic.room_number,
    sic.block_number,
    LEFT(bench_name, 1),
    TRY_CONVERT(int, SUBSTRING(sic.bench_name, 2, LEN(sic.bench_name))) ASC;";
                using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Create a dictionary to store row data
                            Dictionary<string, object> row = new Dictionary<string, object>();
                            roomNumberHashSet.Add(reader.GetString(0));
                            // Add each column to the dictionary
                            row["room_number"] = reader.GetString(0);
                            row["block_number"] = reader.GetString(1);
                            row["bench_name"] = reader.GetString(2);
                            row["roll_number"] = reader.GetInt64(3);
                            row["program_name"] = reader.GetString(4);

                            // Add the dictionary to the ArrayList
                            dataList.Add(row);
                        }
                    }
                }
            }
            PrintAbsentPDF();
            PrintNoticePDF();
            PrintClassroomPDF();
            PrintAttendancePDF();
        }

        private void PrintNoticePDF()
        {
            Boolean first = true;
            FileMode fm;
            if (first)
            {
                fm = FileMode.Create;
                first = false;
            }
            else
            {
                fm = FileMode.Append;
            }

            string filename = "notice.pdf";
            string filePath = Path.Combine("C://Tarun_java//", filename);

            try
            {
                using (FileStream fs = new FileStream(filePath, fm))
                {
                    Document document = new Document();
                    PdfWriter.GetInstance(document, fs);
                    document.Open();

                    // document.NewPage();
                    Paragraph heading = new Paragraph("Seating Arrangement");
                    heading.Alignment = Element.ALIGN_CENTER;
                    document.Add(heading);

                    Paragraph spacing = new Paragraph("\n");
                    spacing.SpacingAfter = 10f; // Adjust spacing as needed
                    document.Add(spacing);


                    // Set the width percentage of the table
                    float tableWidthPercentage = 110f;
                    PdfPTable table = new PdfPTable(6);
                    table.WidthPercentage = tableWidthPercentage;

                    // Calculate the available width based on the page size and margins
                    float availableWidth = document.PageSize.Width - (document.LeftMargin + document.RightMargin);

                    // Calculate the width of each column based on the available width and the specified percentage
                    float[] columnWidths = new float[] {
                availableWidth * 0.08f, // Room number 15% of the available width for each column
                availableWidth * 0.08f, // Block number 15% of the available width for each column
                availableWidth * 0.21f, // Min roll number 21% of the available width for each column
                availableWidth * 0.19f, // Max roll number 17% of the available width for each column
                availableWidth * 0.3f,  // Program name 30% of the available width for each column
                availableWidth * 0.09f   // Total count 10% of the available width for each column
            };

                    table.SetWidths(columnWidths);

                    // Add column headers
                    table.AddCell("Room");
                    table.AddCell("Block");
                    table.AddCell("FromSeatNumber");
                    table.AddCell("ToSeatNumber");
                    table.AddCell("Program");
                    table.AddCell("Total");

                    string query = @"
        SELECT 
            s.room_number,
            s.block_number,
            p.program_name,
            MIN(e.roll_number) AS min_roll_number,
            MAX(e.roll_number) AS max_roll_number,
            COUNT(e.roll_number) AS roll_number_count
        FROM 
            StudentSeatInClassroom s
        INNER JOIN 
            StudentEnrollsProgramInYear e ON s.roll_number = e.roll_number
        INNER JOIN 
            ProgramHasCourse pc ON e.ProgCour_ID = pc.ProgCour_ID
        INNER JOIN 
            Program p ON pc.program_name = p.program_name
        GROUP BY 
            s.room_number,
            s.block_number,
            p.program_name";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    table.AddCell(reader["room_number"].ToString());
                                    table.AddCell(reader["block_number"].ToString());
                                    table.AddCell(reader["min_roll_number"].ToString());
                                    table.AddCell(reader["max_roll_number"].ToString());


                                    table.AddCell(reader["program_name"].ToString());

                                    table.AddCell(reader["roll_number_count"].ToString());
                                }
                            }
                        }
                    }

                    // Add the table to the document
                    document.Add(table);

                    document.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintAbsentPDF()
        {

            FileMode fm = FileMode.Create;
            string filename = "absent.pdf";

            // Provide the full file path
            string filePath = Path.Combine("C://Tarun_java//", filename);
            try
            {
                using (FileStream fs = new FileStream(filePath, fm))
                {
                    Document document = new Document();
                    PdfWriter.GetInstance(document, fs);
                    document.Open();

                    foreach (string room in roomNumberHashSet)
                    {
                        Dictionary<string, int> getProgramForRoom = new Dictionary<string, int>();
                        // Create a new page for each room
                        document.NewPage();
                        Paragraph headingRoomNumber = new Paragraph("Room Number: " + room);
                        headingRoomNumber.Alignment = Element.ALIGN_CENTER;
                        document.Add(headingRoomNumber);
                        // Create a table with 3 columns
                        PdfPTable table = new PdfPTable(4);

                        // Add column headers
                        table.AddCell(new PdfPCell(new Phrase("Seat Nos. Allocated")) { Colspan = 2 });
                        table.AddCell(new PdfPCell(new Phrase("Seat Nos. of the Absentees")) { Colspan = 2 });

                        // Add column headers
                        table.AddCell("Sr. No.");
                        table.AddCell("Seat no.");
                        table.AddCell("Sr. No.");
                        table.AddCell("Seat no.");
                        int serial = 1;
                        foreach (Dictionary<string, object> row in dataList)
                        {
                            // Add data to the table if it belongs to the current room
                            if (row["room_number"].ToString() == room)
                            {
                                if (!getProgramForRoom.ContainsKey(row["program_name"].ToString()))
                                {
                                    getProgramForRoom.Add(row["program_name"].ToString(), 1);
                                    Paragraph headingProgramName = new Paragraph("ProgramName: " + row["program_name"]);
                                    headingProgramName.Alignment = Element.ALIGN_CENTER;
                                    document.Add(headingProgramName);
                                }
                                table.AddCell(serial.ToString());
                                table.AddCell(row["roll_number"].ToString());
                                table.AddCell(serial++.ToString());
                                table.AddCell("               ");
                            }
                        }

                        // Add the table to the document
                        document.Add(table);
                        PdfPTable additionalTable = CreateAdditionalTable();
                        document.Add(additionalTable);
                    }

                    document.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private PdfPTable CreateAdditionalTable()
        {
            // Create a table with 3 columns and 3 rows
            PdfPTable additionalTable = new PdfPTable(3);
            additionalTable.WidthPercentage = 100; // Set table width to 100% of available width
            additionalTable.DefaultCell.Border = PdfPCell.NO_BORDER; // Remove default cell border

            // Add column titles
            PdfPCell titleCell = new PdfPCell(new Phrase(" "));
            titleCell.HorizontalAlignment = Element.ALIGN_CENTER;
            titleCell.BackgroundColor = BaseColor.LIGHT_GRAY; // Set background color for title cell
            titleCell.Border = PdfPCell.BOTTOM_BORDER; // Add bottom border to the title cell
            additionalTable.AddCell(titleCell);
            additionalTable.AddCell(new PdfPCell(new Phrase("Junior")) { BackgroundColor = BaseColor.LIGHT_GRAY, Border = PdfPCell.BOTTOM_BORDER });
            additionalTable.AddCell(new PdfPCell(new Phrase("Senior")) { BackgroundColor = BaseColor.LIGHT_GRAY, Border = PdfPCell.BOTTOM_BORDER });

            // Add rows with empty cells
            additionalTable.AddCell(new PdfPCell(new Phrase("Name")) { Border = PdfPCell.NO_BORDER }); // Row: Name
            additionalTable.AddCell(new PdfPCell(new Phrase(" "))); // Empty cell
            additionalTable.AddCell(new PdfPCell(new Phrase(" "))); // Empty cell

            additionalTable.AddCell(new PdfPCell(new Phrase("Signature")) { Border = PdfPCell.NO_BORDER }); // Row: Signature
            additionalTable.AddCell(new PdfPCell(new Phrase(" "))); // Empty cell
            additionalTable.AddCell(new PdfPCell(new Phrase(" "))); // Empty cell

            additionalTable.AddCell(new PdfPCell(new Phrase("Date")) { Border = PdfPCell.NO_BORDER }); // Row: Date
            additionalTable.AddCell(new PdfPCell(new Phrase(" "))); // Empty cell
            additionalTable.AddCell(new PdfPCell(new Phrase(" "))); // Empty cell

            return additionalTable;

        }

        private void PrintAttendancePDF()
        {
            FileMode fm = FileMode.Create;
            string filename = "attendance.pdf";
            string filePath = Path.Combine("C://Tarun_java//", filename);
            try
            {
                using (FileStream fs = new FileStream(filePath, fm))
                {
                    Document document = new Document();
                    PdfWriter.GetInstance(document, fs);
                    document.Open();

                    foreach (string room in roomNumberHashSet)
                    {
                        Dictionary<string, int> getProgramForRoom = new Dictionary<string, int>();
                        // Create a new page for each room
                        document.NewPage();
                        Paragraph headingRoomNumber = new Paragraph("Room Number: " + room);
                        headingRoomNumber.Alignment = Element.ALIGN_CENTER;
                        document.Add(headingRoomNumber);
                        // Create a table with 3 columns
                        PdfPTable table = new PdfPTable(3);

                        // Add column headers
                        table.AddCell("Sr. No.");
                        table.AddCell("Seat no.");
                        table.AddCell("Signature");
                        int serial = 1;
                        foreach (Dictionary<string, object> row in dataList)
                        {
                            // Add data to the table if it belongs to the current room
                            if (row["room_number"].ToString() == room)
                            {
                                if (!getProgramForRoom.ContainsKey(row["program_name"].ToString()))
                                {
                                    getProgramForRoom.Add(row["program_name"].ToString(), 1);
                                    Paragraph headingProgramName = new Paragraph("ProgramName: " + row["program_name"]);
                                    headingProgramName.Alignment = Element.ALIGN_CENTER;
                                    document.Add(headingProgramName);
                                }
                                table.AddCell(serial++.ToString());
                                table.AddCell(row["roll_number"].ToString());
                                table.AddCell("               ");
                            }
                        }

                        // Add the table to the document
                        document.Add(table);
                    }

                    document.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
        private void PrintClassroomPDF()
        {
            FileMode fm = FileMode.Create;
            string filename = "classroomcopy.pdf";

            // Provide the full file path
            string filePath = Path.Combine("C://Tarun_java//", filename);
            try
            {
                using (FileStream fs = new FileStream(filePath, fm))
                {
                    Document document = new Document();
                    PdfWriter.GetInstance(document, fs);
                    document.Open();

                    foreach (string room in roomNumberHashSet)
                    {
                        Dictionary<string, int> getProgramForRoom = new Dictionary<string, int>();
                        // Create a new page for each room
                        document.NewPage();

                        Paragraph headingRoomNumber = new Paragraph("Room Number: " + room);
                        headingRoomNumber.Alignment = Element.ALIGN_CENTER;
                        document.Add(headingRoomNumber);

                        // Create a table with 4 columns
                        //PdfPTable table = new PdfPTable(4);
                        PdfPTable table = new PdfPTable(3);

                        // Add column headers
                        //table.AddCell("Room Number");
                        table.AddCell("Bench Name");
                        table.AddCell("Roll Number");
                        table.AddCell("Program Name");

                        foreach (Dictionary<string, object> row in dataList)
                        {
                            // Add data to the table if it belongs to the current room
                            if (row["room_number"].ToString() == room)
                            {
                                if (!getProgramForRoom.ContainsKey(row["program_name"].ToString()))
                                {
                                    getProgramForRoom.Add(row["program_name"].ToString(), 1);
                                    Paragraph headingProgramName = new Paragraph("ProgramName: " + row["program_name"]);
                                    headingProgramName.Alignment = Element.ALIGN_CENTER;
                                    document.Add(headingProgramName);
                                }
                                //table.AddCell(row["room_number"].ToString());
                                table.AddCell(row["bench_name"].ToString());
                                table.AddCell(row["roll_number"].ToString());
                                table.AddCell(row["program_name"].ToString());
                            }
                        }

                        // Add the table to the document
                        document.Add(table);
                    }

                    document.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void done_Click(object sender, EventArgs e)
        {
            WillGiveLater2();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string, HashSet<string>> set in PSCHashSet)
            {
                HashSet<string> hs = set.Value;

                // Check if the set has at least three elements
                if (hs.Count >= 3)
                {
                    // Convert the set to a list and access the first three elements
                    List<string> list = hs.ToList();
                    MessageBox.Show(list[0] + list[1] + list[2]);
                }
                else
                {
                    // Handle cases where the set doesn't have enough elements
                    MessageBox.Show("Set does not contain enough elements.");
                }
            }


        }

        private void semesterFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (semesterFilter.SelectedItem != null)
                {

                    semesterFilter.Text = semesterFilter.Text.ToString();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Sem: " + ex.Message);
            }
            finally
            {

                courseFilter.Enabled = true;
                courseFilter.DataSource = GetCourseNames(semesterFilter.Text, programFilter.Text);
                courseFilter.DisplayMember = "course_name";
                courseFilter.Text = null;
                label4.ForeColor = Color.Black;
            }
        }

        private DataTable GetCourseNames(string semester, string program)
        {

            string query = "SELECT course_name FROM ProgramHasCourse WHERE semester_number = @semester AND program_name = @program";

            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@semester", semester);
                        command.Parameters.AddWithValue("@program", program);

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dataTable.Load(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving course names: " + ex.Message);
            }

            return dataTable;
        }

        private void programFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            semesterFilter.Enabled = true;
            semesterFilter.DataSource = GetSemester();
            semesterFilter.DisplayMember = "semester_number";
            semesterFilter.Text = null;
            label4.ForeColor = Color.Gray;
            courseFilter.Enabled = false;
            label3.ForeColor = Color.Black;
            try
            {
                if (programFilter.SelectedItem != null)
                {
                    programFilter.Text = programFilter.SelectedItem.ToString();
                }
                GetSemester();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private DataTable GetSemester()
        {
            string query = "select * from Semester";
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        dataTable.Load(reader);
                    }
                }
            }
            return dataTable;
        }

        private void courseFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (courseFilter.SelectedItem != null)
                {
                    courseFilter.Text = courseFilter.SelectedItem.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void textBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (roomFilter.SelectedItem != null)
                {
                    roomFilter.Text = roomFilter.SelectedItem.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void textBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox4.SelectedItem != null)
                {
                    textBox4.Text = textBox4.SelectedItem.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            WillGiveLater2();
            PrintNoticePDF();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            WillGiveLater2();
            PrintAbsentPDF();
        }

        private void button2_Click(object sender, EventArgs e)
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
            CountStudentsByDetails();
            GetClassroomData();
            PSCHashSet.Clear();
            dataGridView1.DataSource = null;
            MessageBox.Show("Success");
        }

        private void signUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Shared.OpenSignUp = true;
            Login login = new Login();
            login.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WillGiveLater2();
            PrintClassroomPDF();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            WillGiveLater2();
            PrintAttendancePDF();
        }
    }
}
