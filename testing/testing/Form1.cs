namespace testing
{
    public partial class Form1 : Form
    {
        private int maxTitleLength = 25;
        private string titleHold = "error with titleHold";
        private string textHold = "error with textHold";
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
            toolStripComboBoxYear.SelectedItem = (DateTime.Now.Year.ToString());
            toolStripComboBoxMonth.SelectedIndex = (DateTime.Now.Month) - 1;

            calendarChange(DateTime.Now.Year, DateTime.Now.Month);
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            //update home page subject
            int dateCurrent = (int)DateTime.Now.DayOfWeek;

            updateLabelHomePageSub1To6(dateCurrent);
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

        private void taskButton_Click(object sender, EventArgs e)
        {
            //Sets temp button so that the program knows which task is being viewed (this system may be changed)
            //It probably won't always be "taskButton"
            //taskHold = taskButton;

            //Clickable task which reveals task editing panel and hides task panel
            popupTaskPanel.Visible = false;
            editTaskPanel.Visible = true;
        }

        private void taskButton_MouseHover(object sender, EventArgs e)
        {
            //Hover over task to reveal task editing panel
            if (editTaskPanel.Visible == false)
            {
                popupTaskPanel.Visible = true;
            }
        }

        private void taskButton_MouseLeave(object sender, EventArgs e)
        {
            popupTaskPanel.Visible = false;
        }

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

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (editTaskTitleLabel.Text != "")
            {
                editTaskPanel.Visible = false;

                //Temporary until database
                //popupTaskTitleLabel.Text = editTaskTitleLabel.Text;
                //taskButton.Text = editTaskTitleLabel.Text;

                //Temporary until database
                //popupTaskDateLabel.Text = editTaskDatePicker.Text;
                //popupTaskTimeLabel.Text = editTaskTimePicker.Text;

                //popupTaskNoteTextBox.Text = editTaskNoteTextbox.Text;

                //Save changes to data base (Colour, Date, Time, Title etc) here

                //Color may need to be a string - check if database takes colors

                //To be used specifically when taking colours from the database and applying them to tasks (buttons) in the calendar
                //eg: under the column "colour", in the row "English Essay", the string reads orange- this data is then taken and input here along with a button,
                //this is repeated to create a calendar
                //taskHold.BackColor = colourHold;

                //Temporary until database
                //changePopupTaskTheme(colourHold);

                yearHold = editTaskDatePicker.Value.Year;
                monthHold = editTaskDatePicker.Value.Month;
                dayHold = editTaskDatePicker.Value.Day;
                timeHold1 = editTaskTimePicker1.Value.ToString();
                timeHold2 = editTaskTimePicker2.Value.ToString();
                textHold = editTaskNoteTextbox.Text;
                titleHold = editTaskTitleLabel.Text;

                //Check if year == current year & month == current month
                //If true then use a addtask function
                if (taskSender == toolStripButtonNewTask)
                {
                    addTask(yearHold, monthHold, dayHold, timeHold1, timeHold2, colourHold);
                    taskSender = saveButton;
                }
                taskHold.Text = titleHold;

                foreach (Panel panel in flowLayoutPanelCalendar.Controls.OfType<Panel>())
                {
                    panel.Enabled = true;
                }
                toolStripButtonNewTask.Enabled = true;
            }
            else
            {
                MessageBox.Show("Must have a title.");
            }
        }

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
        private void exitButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Exit button was clicked");
            this.Close();
        }
        private void toolStripButtonNewTask_Click(object sender, EventArgs e)
        {
            editTaskPanel.Visible = true;
            taskSender = toolStripButtonNewTask;
        }
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
                        panels.BackColor = Color.LightBlue;

                        //There is only one in each, so maybe change this
                        foreach (Label label in panels.Controls.OfType<Label>())
                        {
                            label.Text = "" + dayCount;
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
                    //Also make it unable to be pressed
                    //Could make it include the last months dates but I think it'd be kinda confusing to code
                    startingDayCountdown--;
                }
            }
            foreach (Panel panels in this.flowLayoutPanelCalendar.Controls.OfType<Panel>())
            {
                foreach (Button button in panels.Controls.OfType<Button>())
                {
                    panels.Controls.Remove(button);
                }
            }
            currentYear = year;
            currentMonth = month;
        }
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
                    calendarChange(year, month);
                }
                else
                {
                    MessageBox.Show("Error Occured - not able to parse year");
                }
            }
        }

        private void addTask(int year, int month, int day, string time1, string time2, Color color)
        {
            int temp = day + startingDay;
            //MessageBox.Show("startingDay" + startingDay + "day:" + day + "temp:" + temp);
            string panelNameChange = "panelCalendarDay" + (temp);
            Point point = new Point(10, 10);
            Button button = buttonToAdd(year, month, day, time1, time2, color, flowLayoutPanelCalendar.Controls[panelNameChange].Size, point);
            taskHold = button;
            this.flowLayoutPanelCalendar.Controls[panelNameChange].Controls.Add(button);
        }
        private Button buttonToAdd(int year, int month, int day, string time1, string time2, Color color, Size size, Point point)
        {
            Button button = new Button();
            button.BackColor = color;
            //Instead of worring about size too much, use margins - top,left,right
            button.Size = new System.Drawing.Size((size.Width - 2), (size.Height - 10));
            //button.Size = new System.Drawing.Size(30, 20);
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Location = point;
            button.TextAlign = ContentAlignment.TopLeft;
            button.Font = new Font(button1.Font, FontStyle.Bold);
            button.ForeColor = Color.White;
            button.Click += new System.EventHandler(task_Click);
            return button;
        }
        private void task_Click(object sender, EventArgs e)
        {
            taskHold = (Button)sender;
            editTaskPanel.Visible = true;
        }

        //make timetable textboxes editable
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