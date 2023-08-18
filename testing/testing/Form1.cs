namespace testing
{
    public partial class Form1 : Form
    {
        private int maxTitleLength = 25;
        private string titleHold = "error with titleHold";
        private string longNoteHold = "error with textHold";
        private int currentYear = DateTime.Now.Year;
        private int currentMonth = DateTime.Now.Month;
        private int yearHold = 2000;
        private int monthHold = 1;
        private int dayHold = 1;
        private object taskSender;
        private string timeHold1 = "error with timeHold1";
        private string timeHold2 = "error with timeHold2";
        private Button taskHold;
        private Color colourHold = Color.Orange;
        private CheckBox tempColourCheckBox;
        private int startingDay, startingDayCountdown, daysInMonth, dayCount;
        private string usernameHold = "userTest3";
        private string oldFileName = "error with oldFileName";

        public Form1()
        {
            InitializeComponent();
            buttonHome.Click += buttonHome_Click;
            buttonTimetable.Click += buttonTimetable_Click;
            buttonCalendar.Click += buttonCalendar_Click;
            buttonSettings.Click += buttonSettings_Click;

            //Adds years to ComboBox and selects current year
            for (int i = 1990; i <= 3000; i++)
            {
                toolStripComboBoxYear.Items.Add(i.ToString());
            }
            toolStripComboBoxYear.SelectedItem = (currentYear.ToString());
            toolStripComboBoxMonth.SelectedIndex = (currentMonth) - 1;

            calendarChange(currentYear, currentMonth);
            pullFromFiles(currentYear, currentMonth);
        }




        //home button
        private void buttonHome_Click(object sender, EventArgs e)
        {
            //home button
            tabControl1.SelectedTab = tabPage1;
        }

        //timetable button
        private void buttonTimetable_Click(object sender, EventArgs e)
        {
            //make this go to timetable tab
            tabControl1.SelectedTab = tabPage2;
        }

        private void buttonCalendar_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage3;
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage4;
        }

        //Should have 4 references: colourOrangeCheckBox, colourPurpleCheckBox, colourGreenCheckBox, colourBlueCheckBox and it should be when CheckedChanged
        private void colourCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //This function is used by all of the color checkboxes,
            //If a colour is to be added, make sure this is in the events

            tempColourCheckBox = (CheckBox)sender;

            //Holds the color to change later
            colourHold = tempColourCheckBox.BackColor;
            changeEditTaskTheme(tempColourCheckBox.BackColor);

            //Instead of the usual check appearance, checkboxes get a border when checked
            tempColourCheckBox.FlatAppearance.BorderSize = 2;

            foreach (CheckBox c in this.editTaskPanel.Controls.OfType<CheckBox>())
            {
                if (c != tempColourCheckBox)
                {
                    //Only one checkbox can be pressed at a time
                    c.Checked = false;
                    c.FlatAppearance.BorderSize = 0;
                }
            }
        }

        //Should have 1 reference: saveButton.click
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (editTaskTitleLabel.Text != "")
            {
                editTaskPanel.Visible = false;

                //Save changes to data base (Colour, Date, Time, Title etc) here

                //Color may need to be a string - check if database takes colors

                //To be used specifically when taking colours from the database and applying them to tasks (buttons) in the calendar
                //eg: under the column "colour", in the row "English Essay", the string reads orange- this data is then taken and input here along with a button,
                //this is repeated to create a calendar

                yearHold = editTaskDatePicker.Value.Year;
                monthHold = editTaskDatePicker.Value.Month;
                dayHold = editTaskDatePicker.Value.Day;
                timeHold1 = editTaskTimePicker1.Value.ToString("h:mm tt");
                timeHold2 = editTaskTimePicker2.Value.ToString("h:mm tt");
                longNoteHold = editTaskNoteTextbox.Text;
                titleHold = editTaskTitleLabel.Text;

                //Probably a better way to do this

                if (taskSender == toolStripButtonNewTask)
                {
                    if (currentYear == yearHold && currentMonth == monthHold)
                    {
                        addTask(titleHold, yearHold, monthHold, dayHold, timeHold1, timeHold2, colourHold);
                        taskSender = saveButton;
                    }
                    createFile(usernameHold, titleHold, longNoteHold, yearHold, monthHold, dayHold, timeHold1, timeHold2, colourHold);
                }
                else
                {
                    if (currentYear == yearHold && currentMonth == monthHold)
                    {
                        taskHold.Text = titleHold + "\n" + timeHold1 + " to " + timeHold2;
                        //Edit Task
                    }
                    editFile(usernameHold, titleHold, oldFileName, longNoteHold, yearHold, currentYear, monthHold, currentMonth, dayHold, timeHold1, timeHold2, colourHold);
                }
            }
            else
            {
                MessageBox.Show("Must have a title.");
            }
        }

        //Should have 1 reference: editTaskTitleTextBox.TextChanged 
        private void editTaskTitleTextBox_TextChanged(object sender, EventArgs e)
        {
            if (editTaskTitleTextBox.TextLength == maxTitleLength - 1)
            {
                titleHold = editTaskTitleTextBox.Text;
            }
            if (editTaskTitleTextBox.Text.Length >= maxTitleLength)
            {
                editTaskTitleTextBox.Text = titleHold;
                MessageBox.Show("Maximum letters reached");
            }

            editTaskTitleLabel.Text = editTaskTitleTextBox.Text;
        }

        //Should have 0 references, not in use currently
        private void changePopupTaskTheme(Color color)
        {
            //Should change Popup Task Theme when hovering a button/task and READ the color of the button/task

            if (color == Color.Orange)
            {
                popupTaskPanel.BackColor = Color.Bisque;
                popupTaskNoteTextBox.BackColor = Color.Bisque;
            }
            else if (color == Color.Green)
            {
                popupTaskPanel.BackColor = Color.LightGreen;
                popupTaskNoteTextBox.BackColor = Color.LightGreen;
            }
            else if (color == Color.Blue)
            {
                popupTaskPanel.BackColor = Color.LightBlue;
                popupTaskNoteTextBox.BackColor = Color.LightBlue;
            }
            else if (color == Color.Purple)
            {
                popupTaskPanel.BackColor = Color.Thistle;
                popupTaskNoteTextBox.BackColor = Color.Thistle;
            }
            else
            {
                MessageBox.Show("Error when changing Popup Task Theme");
            }
        }
        //Should have 1 reference inside colourCheckBox_CheckedChanged (Not an event)
        private void changeEditTaskTheme(Color color)
        {
            //To be used to change the theme of the "Edit Task" Panel
            //Used when changing checkboxes and opening the Edit Task Panel in the future

            if (color == Color.Orange)
            {
                //May try and simplify later, but currently haven't found a way to do this easily
                editTaskPanel.BackColor = Color.Bisque;
                saveButton.BackColor = Color.Orange;
                deleteButton.BackColor = Color.Orange;
            }
            else if (color == Color.Green)
            {
                editTaskPanel.BackColor = Color.LightGreen;
                saveButton.BackColor = Color.Green;
                deleteButton.BackColor = Color.Green;
            }
            else if (color == Color.Blue)
            {
                editTaskPanel.BackColor = Color.LightBlue;
                saveButton.BackColor = Color.Blue;
                deleteButton.BackColor = Color.Blue;
            }
            else if (color == Color.Purple)
            {
                editTaskPanel.BackColor = Color.Thistle;
                saveButton.BackColor = Color.Purple;
                deleteButton.BackColor = Color.Purple;
            }
            else
            {
                MessageBox.Show("Error when changing Edit Task Theme");
            }
            //Remember to "check" the corresponding checkbox when making code that opens previously created tasks



        }
        //Log in code
        int SendLogInRequest()
        {

            //if (something (criteria for logging in))
            if (true)
            {
                return 1;
            }

            return 0;
        }
        //Should have 1 reference: loginButton.Click
        private void loginButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Login button was clicked");

            //if the log in request works. 
            if (SendLogInRequest() == 1)
            {
                loginPanel.Visible = false;
            }
            else
            {
                Console.WriteLine("Login request failed");
            }
            return;
        }
        //Should have 1 reference: exitButton.Click
        private void exitButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Exit button was clicked");
            this.Close();
        }

        //Should have 1 reference: toolStripButtonNewTask.Click
        private void toolStripButtonNewTask_Click(object sender, EventArgs e)
        {
            colourOrangeCheckBox.Checked = true;
            editTaskTitleTextBox.Text = "";
            editTaskTitleLabel.Text = "Task";

            //Change to year & month currently selected
            editTaskDatePicker.Value = DateTime.Now;
            editTaskTimePicker1.Value = DateTime.Now;
            editTaskTimePicker2.Value = DateTime.Now;
            editTaskNoteTextbox.Text = "";

            //Move to designer
            //editTaskPanel.Location = new Point(452, 280);
            editTaskPanel.Visible = true;

            taskSender = toolStripButtonNewTask;
        }
        //Should have 2 references at the top and inside month_year_Changed
        private void calendarChange(int year, int month)
        {
            if (year >= 1990 && year <= 3000 && month >= 1 && month <= 12)
            {
                startingDay = (int)new DateTime(year, month, 1).DayOfWeek;
                startingDayCountdown = startingDay;
                daysInMonth = DateTime.DaysInMonth(year, month);
            }
            else
            {
                MessageBox.Show("Could not retrive month/year");
                startingDayCountdown = 2;
                daysInMonth = 2;
            }

            dayCount = 1;

            foreach (Panel panels in this.flowLayoutPanelCalendar.Controls.OfType<Panel>())
            {
                if (startingDayCountdown == 0)
                {
                    if (dayCount <= daysInMonth)
                    {
                        panels.BackColor = Color.LightSteelBlue;

                        //There is only one in each, so maybe change this
                        foreach (Label label in panels.Controls.OfType<Label>())
                        {
                            label.Text = "" + dayCount;
                            label.Font = new Font("Segoe UI", 13, FontStyle.Bold);
                        }
                        dayCount++;
                    }
                    else
                    {
                        panels.BackColor = Color.White;
                        foreach (Label label in panels.Controls.OfType<Label>())
                        {
                            label.Text = "";
                        }
                    }
                }
                else
                {
                    panels.BackColor = Color.White;
                    foreach (Label label in panels.Controls.OfType<Label>())
                    {
                        label.Text = "";
                    }
                    //Could make it include the last months dates but I think it'd be kinda confusing to code
                    startingDayCountdown--;
                }
            }
            foreach (Panel panels in this.flowLayoutPanelCalendar.Controls.OfType<Panel>())
            {
                var buttons = panels.Controls.OfType<Button>().ToArray();
                foreach (Button button in buttons)
                {
                    panels.Controls.Remove(button);
                    button.Dispose();
                }
            }
            currentYear = year;
            currentMonth = month;
        }

        //Should have 1 reference: editTaskPanel.VisibleChanged
        private void editTaskPanel_VisibleChanged(object sender, EventArgs e)
        {
            if (editTaskPanel.Visible)
            {
                foreach (Control control in Controls)
                {
                    if (control != editTaskPanel)
                    {
                        control.Enabled = false;
                    }
                }
            }
            else
            {
                foreach (Control control in Controls)
                {
                    control.Enabled = true;
                }
            }
        }

        //Should have 2 references: toolStripComboBoxYear.SelectedIndexChanged and toolStripComboBoxMonth.SelectedIndexChanged
        private void month_year_Changed(object sender, EventArgs e)
        {
            //Checks whether input is from user
            if (((ToolStripComboBox)sender).Focused)
            {
                int year, month;
                bool okay = Int32.TryParse(toolStripComboBoxYear.Text, out year);
                month = toolStripComboBoxMonth.SelectedIndex + 1;
                if (okay)
                {
                    if (currentYear != year || currentMonth != month)
                    {
                        calendarChange(year, month);
                        pullFromFiles(year, month);
                    }
                }
                else
                {
                    MessageBox.Show("Error Occured - not able to parse year");
                }
            }
        }

        //Should have 2 references inside saveButton_Click and pullFromFiles
        private void addTask(string title, int year, int month, int day, string time1, string time2, Color color)
        {
            //need to add input for long note string
            int count = 0;
            int temp = day + startingDay;
            //MessageBox.Show("startingDay" + startingDay + "day:" + day + "temp:" + temp);
            string panelNameChange = "panelCalendarDay" + (temp);
            foreach (Button b in flowLayoutPanelCalendar.Controls[panelNameChange].Controls.OfType<Button>())
            {
                count++;
            }
            if (count > 2)
            {
                //try get [label], if it exists do nothing, else make a label that reads "..."
                MessageBox.Show("Cannot currently enter more than 2 tasks into a panel");
            }
            else
            {
                Point point;
                if (count == 1)
                {
                    point = new Point(30, 10 + (10 * count));
                }
                else
                {
                    point = new Point(30, 10);
                }
                Button button = buttonToAdd(year, month, day, time1, time2, color, flowLayoutPanelCalendar.Controls[panelNameChange].Size, point);
                button.Text = title + "\n" + time1 + " to " + time2;
                taskHold = button;
                this.flowLayoutPanelCalendar.Controls[panelNameChange].Controls.Add(button);
            }
        }
        //Should have 1 reference inside addTask (directly above this)
        private Button buttonToAdd(int year, int month, int day, string time1, string time2, Color colour, Size size, Point point)
        {
            Button button = new Button();
            button.BackColor = colour;
            button.Size = new System.Drawing.Size((size.Width - 10), (size.Height - 40));
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Location = point;
            button.TextAlign = ContentAlignment.TopLeft;
            button.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            button.ForeColor = Color.White;
            button.Click += new System.EventHandler(task_Click);
            button.Anchor = AnchorStyles.Top;
            return button;
        }
        //Should have 1 reference: deleteButton.Click
        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (taskSender != toolStripButtonNewTask)
            {
                Controls.Remove(taskHold);
                taskHold.Dispose();

                deleteFile(usernameHold, titleHold, currentYear, currentMonth);

                editTaskPanel.Visible = false;
            }
        }

        //Should have 2 references: deleteButton_Click and editFile
        public static async Task deleteFile(string username, string title, int year, int month)
        {
            if (username != null && title != null && year >= 1990 && month >= 1)
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\" + username;

                path = Path.Combine(path, year.ToString());
                if (Directory.Exists(path))
                {
                    path = Path.Combine(path, month.ToString());
                    if (Directory.Exists(path))
                    {
                        path = Path.Combine(path, title);
                        if (File.Exists(path))
                        {
                            File.Delete(path);
                            //Maybe I should delete the folders too?
                        }
                        else
                        {
                            MessageBox.Show("Path doesn't exist + " + path);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Month path doesn't exist: " + month);
                    }
                }
                else
                {
                    MessageBox.Show("Year path doesn't exist: " + path);
                }
            }
            else
            {
                MessageBox.Show("deleteFile did not recieve correct values");
            }
        }

        //Should have 1 reference inside buttonToAdd (directly above this)
        private void task_Click(object sender, EventArgs e)
        {
            taskHold = (Button)sender;
            taskSender = taskHold;
            editTaskPanel.Visible = true;
            string[] titleSplit = taskHold.Text.Split('\n');
            string[] timeSplit = titleSplit[1].Split(" to ");
            editTaskTitleTextBox.Text = titleSplit[0];
            editTaskTimePicker1.Text = timeSplit[0];
            editTaskTimePicker2.Text = timeSplit[1];
            string stringColour = taskHold.BackColor.ToString();
            int length = stringColour.IndexOf("]") - stringColour.IndexOf("[") - 1;
            stringColour = stringColour.Substring(stringColour.IndexOf("[") + 1, length);
            string colourPicker = "colour" + stringColour + "CheckBox";
            CheckBox tempCheckBox = (CheckBox)this.editTaskPanel.Controls[colourPicker];
            tempCheckBox.Checked = true;
            titleHold = editTaskTitleTextBox.Text;
            oldFileName = editTaskTitleLabel.Text;

            pullFromFile(currentYear, currentMonth);
        }
        public static async Task editFile(string username, string title, string oldTitle, string longNote, int year, int oldYear, int month, int oldMonth, int day, string time1, string time2, Color colour)
        {
            //May be a way to find out what changed exactly but it would be hard to input into an old file
            //Instead maybe that could be used for the button label changing

            if (username != null && title != null && year >= 1990 && month >= 1 && day >= 1 && time1 != null && time2 != null)
            {
                deleteFile(username, oldTitle, oldYear, oldMonth);

                createFile(username, title, longNote, year, month, day, time1, time2, colour);
            }
            else
            {
                MessageBox.Show("editFile did not recieve correct values");
            }
        }
        //Should have 1 reference: task_Click
        public void pullFromFile(int chosenYear, int chosenMonth)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\" + usernameHold;

            path = Path.Combine(path, chosenYear.ToString());
            if (Directory.Exists(path))
            {
                path = Path.Combine(path, chosenMonth.ToString());
                if (Directory.Exists(path))
                {
                    try
                    {
                        path = Path.Combine(path, oldFileName);
                        string line = File.ReadAllText(path);

                        string[] info = line.Split('-');

                        editTaskDatePicker.Value = new DateTime(chosenYear, chosenMonth, int.Parse(info[1]));

                        if (info.Length < 5)
                        {
                            editTaskNoteTextbox.Text = "";
                        }
                        else
                        {
                            editTaskNoteTextbox.Text = info[5];
                            //Used if the user put a '-' in their longNote
                            if (info.Length > 5)
                            {
                                for (int i = 6; i < info.Length; i++)
                                {
                                    editTaskNoteTextbox.Text += "-" + info[i];
                                }
                            }
                        }
                    }
                    catch (DirectoryNotFoundException) { }
                }
            }
        }
        //Should have 1 reference: editTaskNoteTextbox.TextChanged
        private void editTaskNoteTextbox_TextChanged(object sender, EventArgs e)
        {
            longNoteHold = editTaskNoteTextbox.Text;
            if (editTaskNoteTextbox.Text.Length > 40)
            {
                MessageBox.Show("Word limit has been reached");
                editTaskNoteTextbox.Text = longNoteHold;
            }
        }

        //Should have 1 reference inside saveButton_Click
        public static async Task createFile(string username, string title, string longNote, int year, int month, int day, string time1, string time2, Color colour)
        {
            if (username != null && title != null && year >= 1990 && month >= 1 && day >= 1 && time1 != null && time2 != null)
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\" + username;

                string stringColour = colour.ToString();
                int length = stringColour.IndexOf("]") - stringColour.IndexOf("[") - 1;
                stringColour = stringColour.Substring(stringColour.IndexOf("[") + 1, length);

                string inputToFile = title + "-" + day + "-" + time1 + "-" + time2 + "-" + stringColour + "-" + longNote;

                path = Path.Combine(path, year.ToString());
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path = Path.Combine(path, month.ToString());
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path = Path.Combine(path, title);
                if (!File.Exists(path))
                {
                    using StreamWriter file = new(path);
                    await file.WriteLineAsync(inputToFile);
                    //await file.WriteLineAsync(EncryptDecrypt.Encrypt(inputToFile, Key));
                    //Will need to be encrypted/decrypted later!
                }
                else
                {
                    string tempPath = path;
                    for (int i = 1; File.Exists(tempPath); i++)
                    {
                        tempPath = path + i;
                    }
                    using StreamWriter file = new(tempPath);
                    await file.WriteLineAsync(inputToFile);
                }
            }
            else
            {
                MessageBox.Show("Cannot input incorrect values into createFile");
            }
        }

        //Should have 2 references inside month_year_changed and inside Form1
        public void pullFromFiles(int chosenYear, int chosenMonth)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\" + usernameHold;

            path = Path.Combine(path, chosenYear.ToString());
            if (Directory.Exists(path))
            {
                path = Path.Combine(path, chosenMonth.ToString());
                if (Directory.Exists(path))
                {
                    try
                    {
                        string[] files = Directory.GetFiles(path);

                        foreach (string file in files)
                        {
                            //file is the address of the file!!
                            string line = File.ReadAllText(file);

                            string[] info = line.Split('-');
                            //string[] date = info[1].Split('/');

                            //MessageBox.Show(date[0] + date[1] + date[2]);

                            //int spaceLocation1 = date[2].IndexOf(' ');
                            int year = chosenYear;
                            int month = chosenMonth;
                            int day = int.Parse(info[1]);
                            //string time1 = date[2].Substring(spaceLocation1);
                            //int spaceLocation2 = info[1].IndexOf(' ');

                            string time1 = info[2];
                            string time2 = info[3];
                            //string time2 = info[2].Substring(spaceLocation2);

                            //string title = Path.GetFileName(file);
                            string title = info[0];

                            //MessageBox.Show("title" + title + " year:" + year + " month:" + month + " day:" + day + " time1:" + time1 + " time2" + time2);

                            //Note: info[5] is the long note

                            string longNote;

                            if (info.Length < 5)
                            {
                                longNote = "";
                            }
                            else
                            {
                                longNote = info[5];
                                //Used if the user put a '-' in their longNote
                                if (info.Length > 5)
                                {
                                    for (int i = 6; i < info.Length; i++)
                                    {
                                        longNote += "-" + info[i];
                                    }
                                }
                            }

                            addTask(title, year, month, day, time1, time2, Color.FromName(info[4]));
                        }
                    }
                    catch (DirectoryNotFoundException) { }
                }
            }
        }


        //make timetable textboxes editable
        //Should have 1 reference: buttonEditTimetable.Click
        private void buttonEditTimetable_Click(object sender, EventArgs e)
        {
            TextBox[] textBoxes = new TextBox[]
            {
                textBoxTTMnP1Sub, textBoxTTMnP2Sub, textBoxTTMnP3Sub, textBoxTTMnP4Sub, textBoxTTMnP5Sub, textBoxTTMnP6Sub,
                textBoxTTTuP1Sub, textBoxTTTuP2Sub, textBoxTTTuP3Sub, textBoxTTTuP4Sub, textBoxTTTuP5Sub, textBoxTTTuP6Sub,
                textBoxTTWedP1Sub, textBoxTTWedP2Sub, textBoxTTWedP3Sub, textBoxTTWedP4Sub, textBoxTTWedP5Sub, textBoxTTWedP6Sub,
                textBoxTTThuP1Sub, textBoxTTThuP2Sub, textBoxTTThuP3Sub, textBoxTTThuP4Sub, textBoxTTThuP5Sub, textBoxTTThuP6Sub,
                textBoxTTFriP1Sub, textBoxTTFriP2Sub, textBoxTTFriP3Sub, textBoxTTFriP4Sub, textBoxTTFriP5Sub, textBoxTTFriP6Sub
            };

            foreach (TextBox textBox in textBoxes)
            {
                textBox.ReadOnly = false;
            }

            buttonTimetableClear.Visible = true;
            buttonTimetableEditDone.Visible = true;
        }

        //Should have 1 reference: buttonTimetableClear.Click
        private void buttonTimetableClear_Click(object sender, EventArgs e)
        {
            TextBox[] textBoxes = new TextBox[]
            {
                textBoxTTMnP1Sub, textBoxTTMnP2Sub, textBoxTTMnP3Sub, textBoxTTMnP4Sub, textBoxTTMnP5Sub, textBoxTTMnP6Sub,
                textBoxTTTuP1Sub, textBoxTTTuP2Sub, textBoxTTTuP3Sub, textBoxTTTuP4Sub, textBoxTTTuP5Sub, textBoxTTTuP6Sub,
                textBoxTTWedP1Sub, textBoxTTWedP2Sub, textBoxTTWedP3Sub, textBoxTTWedP4Sub, textBoxTTWedP5Sub, textBoxTTWedP6Sub,
                textBoxTTThuP1Sub, textBoxTTThuP2Sub, textBoxTTThuP3Sub, textBoxTTThuP4Sub, textBoxTTThuP5Sub, textBoxTTThuP6Sub,
                textBoxTTFriP1Sub, textBoxTTFriP2Sub, textBoxTTFriP3Sub, textBoxTTFriP4Sub, textBoxTTFriP5Sub, textBoxTTFriP6Sub
            };

            foreach (TextBox textBox in textBoxes)
            {
                textBox.Clear();
            }
        }
        //Should have 1 reference: buttonTimetableEditDone.Click

        private void buttonTimetableEditDone_Click(object sender, EventArgs e)
        {
            TextBox[] textBoxes = new TextBox[]
            {
                textBoxTTMnP1Sub, textBoxTTMnP2Sub, textBoxTTMnP3Sub, textBoxTTMnP4Sub, textBoxTTMnP5Sub, textBoxTTMnP6Sub,
                textBoxTTTuP1Sub, textBoxTTTuP2Sub, textBoxTTTuP3Sub, textBoxTTTuP4Sub, textBoxTTTuP5Sub, textBoxTTTuP6Sub,
                textBoxTTWedP1Sub, textBoxTTWedP2Sub, textBoxTTWedP3Sub, textBoxTTWedP4Sub, textBoxTTWedP5Sub, textBoxTTWedP6Sub,
                textBoxTTThuP1Sub, textBoxTTThuP2Sub, textBoxTTThuP3Sub, textBoxTTThuP4Sub, textBoxTTThuP5Sub, textBoxTTThuP6Sub,
                textBoxTTFriP1Sub, textBoxTTFriP2Sub, textBoxTTFriP3Sub, textBoxTTFriP4Sub, textBoxTTFriP5Sub, textBoxTTFriP6Sub
            };

            foreach (TextBox textBox in textBoxes)
            {
                textBox.ReadOnly = true;
            }

            buttonTimetableClear.Visible = false;
            buttonTimetableEditDone.Visible = false;


            //update home page subjects

            int dateCurrent = (int)DateTime.Now.DayOfWeek;

            updateLabelHomePageSub1To6(dateCurrent);


        }


        //Should have 2 references inside Form1_Load1 and buttonTimetableEditDone_Click
        private void updateLabelHomePageSub1To6(int day)
        {
            switch (day)
            {
                case 1: //Monday
                    labelHomePageSub1.Text = textBoxTTMnP1Sub.Text;
                    labelHomePageSub2.Text = textBoxTTMnP2Sub.Text;
                    labelHomePageSub3.Text = textBoxTTMnP3Sub.Text;
                    labelHomePageSub4.Text = textBoxTTMnP4Sub.Text;
                    labelHomePageSub5.Text = textBoxTTMnP5Sub.Text;
                    labelHomePageSub6.Text = textBoxTTMnP6Sub.Text;
                    break;

                case 2:
                    labelHomePageSub1.Text = textBoxTTTuP1Sub.Text;
                    labelHomePageSub2.Text = textBoxTTTuP2Sub.Text;
                    labelHomePageSub3.Text = textBoxTTTuP3Sub.Text;
                    labelHomePageSub4.Text = textBoxTTTuP4Sub.Text;
                    labelHomePageSub5.Text = textBoxTTTuP5Sub.Text;
                    labelHomePageSub6.Text = textBoxTTTuP6Sub.Text;
                    break;

                case 3:
                    labelHomePageSub1.Text = textBoxTTWedP1Sub.Text;
                    labelHomePageSub2.Text = textBoxTTWedP2Sub.Text;
                    labelHomePageSub3.Text = textBoxTTWedP3Sub.Text;
                    labelHomePageSub4.Text = textBoxTTWedP4Sub.Text;
                    labelHomePageSub5.Text = textBoxTTWedP5Sub.Text;
                    labelHomePageSub6.Text = textBoxTTWedP6Sub.Text;
                    break;

                case 4:
                    labelHomePageSub1.Text = textBoxTTThuP1Sub.Text;
                    labelHomePageSub2.Text = textBoxTTThuP2Sub.Text;
                    labelHomePageSub3.Text = textBoxTTThuP3Sub.Text;
                    labelHomePageSub4.Text = textBoxTTThuP4Sub.Text;
                    labelHomePageSub5.Text = textBoxTTThuP5Sub.Text;
                    labelHomePageSub6.Text = textBoxTTThuP6Sub.Text;
                    break;

                case 5:
                    labelHomePageSub1.Text = textBoxTTFriP1Sub.Text;
                    labelHomePageSub2.Text = textBoxTTFriP2Sub.Text;
                    labelHomePageSub3.Text = textBoxTTFriP3Sub.Text;
                    labelHomePageSub4.Text = textBoxTTFriP4Sub.Text;
                    labelHomePageSub5.Text = textBoxTTFriP5Sub.Text;
                    labelHomePageSub6.Text = textBoxTTFriP6Sub.Text;
                    break;

                default:
                    labelHomePageSub1.Text = "NO SCHOOL";
                    labelHomePageSub2.Text = "NO SCHOOL";
                    labelHomePageSub3.Text = "NO SCHOOL";
                    labelHomePageSub4.Text = "NO SCHOOL";
                    labelHomePageSub5.Text = "NO SCHOOL";
                    labelHomePageSub6.Text = "NO SCHOOL";
                    break;
            }
        }

    }
}