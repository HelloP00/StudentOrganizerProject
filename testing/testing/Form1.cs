namespace testing
{
    public partial class Form1 : Form
    {
        private int maxTitleLength = 25;
        private string titleHold = "error";
        private Button taskHold;
        private Color colourHold = Color.Red;
        private CheckBox tempColourCheckBox;
        public Form1()
        {
            InitializeComponent();
            button1.Click += button1_Click;
            button2.Click += button2_Click;
            button3.Click += button3_Click;
            button4.Click += button4_Click;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label5.Text = DateTime.Now.ToLongDateString();
        }


        //home button
        private void button1_Click(object sender, EventArgs e)
        {
            //home button
            tabControl1.SelectedTab = tabPage1;
        }

        //timetable button
        private void button2_Click(object sender, EventArgs e)
        {
            //make this go to timetable tab
            tabControl1.SelectedTab = tabPage2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage3;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage4;
        }

        private void taskButton_Click(object sender, EventArgs e)
        {
            //Sets temp button so that the program knows which task is being viewed (this system may be changed)
            //It probably won't always be "taskButton"
            taskHold = taskButton;

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
            editTaskPanel.Visible = false;

            //Temporary until database
            popupTaskTitleLabel.Text = editTaskTitleLabel.Text;
            taskButton.Text = editTaskTitleLabel.Text;

            //Temporary until database
            popupTaskDateLabel.Text = editTaskDatePicker.Text;
            popupTaskTimeLabel.Text = editTaskTimePicker.Text;

            popupTaskNoteTextBox.Text = editTaskNoteTextbox.Text;

            //Save changes to data base (Colour, Date, Time, Title etc) here

            //Color may need to be a string - check if database takes colors

            //To be used specifically when taking colours from the database and applying them to tasks (buttons) in the calendar
            //eg: under the column "colour", in the row "English Essay", the string reads orange- this data is then taken and input here along with a button,
            //this is repeated to create a calendar
            taskHold.BackColor = colourHold;

            //Temporary until database
            changePopupTaskTheme(colourHold);
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
    }
}