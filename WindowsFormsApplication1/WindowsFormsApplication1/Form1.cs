using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Speech.Synthesis;
using System.Text;

namespace WindowsFormsApplication1
{
    
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Assistance"].ConnectionString);
        int ename = 0;
        int notepad = 0, joke = 1,joke_start = 0;
        int evalue = 0;
        int ct = 0;
        int save = 0;
        int selected = 0;
        int text_cut = 0;
        int sze = 0;
        int i = 0;
        string filename = "";
        string textfile = "";
        string event_name = "";
        string event_value = "";
        public Form1(string sr)
        {
            InitializeComponent();
            label2.Text = "";
        }
        SpeechSynthesizer speechSynthesizerObj;
        private void Form1_Load(object sender, EventArgs e)
        {
            
              
            speechSynthesizerObj = new SpeechSynthesizer();
            
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            
            speechSynthesizerObj.Dispose();
            SpeechSynthesizer sp = new SpeechSynthesizer();
            string time = DateTime.Now.ToString("hh:mm:ss tt");           
            string s = "Select * from DATA  ";
            SqlDataAdapter da = new SqlDataAdapter(s, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            string count = ds.Tables[0].Rows.Count.ToString();
            
            if (save == 1)
            {
                //StreamWriter sw = new StreamWriter(filename);
                System.IO.File.WriteAllText(filename, textfile);
                textfile = "";
                save = 0;                
                test1();
            }
            if (ds.Tables[0].Rows.Count > 0)
            {                                
                string data = ds.Tables[0].Rows[0][1].ToString();
                string[] ssizes = data.Split(' ', '\t');
                bool a = Array.Exists(ssizes, element => element == "are");
                bool b = Array.Exists(ssizes, element => element == "you");
                sze = ssizes.Length;
                //MessageBox.Show(sze.ToString());
                if (ssizes[0] == "open" && sze > 1)
                {
                    //MessageBox.Show(sze);
                    if (ssizes[1] == "file" && ssizes[2] == "explorer")
                    {
                        sp.SpeakAsync("Opening File Explorer");
                        test1();
                        System.Diagnostics.Process.Start("explorer.exe");
                    }
                    else if (ssizes[1] == "Paint")
                    {
                        sp.SpeakAsync("Opening Paint");
                        test1();
                        System.Diagnostics.Process.Start("mspaint.exe");
                    }
                    else if (ssizes[1].Contains("www") || ssizes[1].Contains(".com"))
                    {
                        sp.SpeakAsync("Opening URL");
                        test1();
                        System.Diagnostics.Process.Start("chrome.exe", "http://" + ssizes[1] + "");
                    }
                    else if (ssizes[1] == "browser")
                    {
                        sp.SpeakAsync("Opening Chrome");
                        test1();
                        System.Diagnostics.Process.Start("chrome.exe");
                    }
                    else if (ssizes[1] == "Word")
                    {
                        sp.SpeakAsync("Opening Word");
                        test1();
                        System.Diagnostics.Process.Start("winword.exe");
                    }
                    else if (ssizes[1] == "PowerPoint")
                    {
                        sp.SpeakAsync("Opening Power Point");
                        test1();
                        System.Diagnostics.Process.Start("powerpnt.exe");
                    }
                    else if (ssizes[1] == "Excel")
                    {
                        sp.SpeakAsync("Opening Excel");
                        test1();
                        System.Diagnostics.Process.Start("excel.exe");
                    }

                    else if (ssizes[1] == "calculator")
                    {
                        sp.SpeakAsync("Opening Calculator");
                        test1();
                        System.Diagnostics.Process.Start("calc.exe");
                    }
                    else if (ssizes[1] == "new" || ssizes[1] == "New")
                    {
                        if (ssizes[2] == "window" || ssizes[2] == "Window" || ssizes[2] == "Tab" || ssizes[2] == "tab")
                        {
                            sp.SpeakAsync("Opening new tab");
                            SendKeys.Send("^t");
                            test1();
                        }
                        else
                        {
                            MessageBox.Show("Something Error");
                        }
                    }
                    else
                    {
                        sp.SpeakAsync("No such application found");
                        test1();
                    }
                }
                else if ((ssizes[0] == "close" || ssizes[0] == "Close") && sze == 2)
                {
                    //MessageBox.Show(sze.ToString());
                    if (ssizes[1] == "File" || ssizes[1] == "file")
                    {
                        if (ssizes[2] == "explorer" || ssizes[2] == "Explorer")
                        {
                            sp.SpeakAsync("Closing File Explorer");
                            Process[] processes = Process.GetProcessesByName("explorer");
                            foreach (Process process in processes)
                            {
                                process.Kill();
                            }
                            test1();
                        }
                    }
                    else if (ssizes[1] == "notepad" || ssizes[1] == "Notepad")
                    {
                        if (notepad == 1)
                        {
                            notepad = 0;
                        }
                        sp.SpeakAsync("Closing Notepad");
                        Process[] processes = Process.GetProcessesByName("notepad");
                        foreach (Process process in processes)
                        {
                            process.Kill();
                        }
                        test1();
                    }

                    else if (ssizes[1] == "Paint" || ssizes[1] == "paint")
                    {
                        sp.SpeakAsync("Closing Paint");
                        Process[] processes1 = Process.GetProcessesByName("mspaint");
                        foreach (Process process in processes1)
                        {
                            process.Kill();
                        }
                        test1();
                    }
                    else if (ssizes[1] == "browser" || ssizes[1] == "Browser")
                    {

                        Process[] processes = Process.GetProcessesByName("chrome");
                        foreach (Process process in processes)
                        {
                            process.Kill();
                        }
                        test1();
                        //System.Diagnostics.Process.Start("chrome.exe");
                    }
                    else if (ssizes[1] == "Word" || ssizes[1] == "word")
                    {

                        sp.SpeakAsync("Closing Word");
                        //label1.Text = "Closing Word";
                        Process[] processes = Process.GetProcessesByName("winword");
                        foreach (Process process in processes)
                        {
                            process.Kill();
                        }
                        //processes[0].Kill();                        
                        test1();
                        //System.Diagnostics.Process.Start("winword.exe");
                    }
                    else if (ssizes[1] == "PowerPoint" || ssizes[1] == "powerpoint")
                    {
                        sp.SpeakAsync("Closing PowerPoint");
                        //label1.Text = "Closing Power Point";
                        Process[] processes = Process.GetProcessesByName("powerpnt");
                        foreach (Process process in processes)
                        {
                            process.Kill();
                        }
                        test1();
                        //System.Diagnostics.Process.Start("powerpnt.exe");
                    }
                    else if (ssizes[1] == "Excel" || ssizes[1] == "excel")
                    {
                        sp.SpeakAsync("Closing Excel");
                        //label1.Text = "Closing Excel";
                        Process[] processes = Process.GetProcessesByName("excel");
                        foreach (Process process in processes)
                        {
                            process.Kill();
                        }
                        test1();
                        //System.Diagnostics.Process.Start("excel.exe");
                    }

                    else if (ssizes[1] == "calculator" || ssizes[1] == "Calculator")
                    {
                        sp.SpeakAsync("Closing Calculator");
                        //label1.Text = "Closing Calculator";
                        Process[] processes = Process.GetProcessesByName("calc");
                        foreach (Process process in processes)
                        {
                            process.Kill();
                        }
                        test1();
                        //System.Diagnostics.Process.Start("calc.exe");
                    }
                    else if (ssizes[1] == "tab" || ssizes[1] == "Tab" || ssizes[1] == "Window" || ssizes[1] == "window")
                    {
                        sp.SpeakAsync("Closing current tab");
                        SendKeys.Send("^w");
                        test1();
                    }
                    else
                    {
                        sp.SpeakAsync("No such Application is opened or Found");
                        //label1.Text = "No such application is opened or Found";
                        test1();
                        //System.Diagnostics.Process.Start("" + ssizes[1] + ".exe");

                    }
                }
                else if (ssizes[0] == "Hi" || ssizes[0] == "Hello" || ssizes[0] == "hello" || ssizes[0] == "hi" || ssizes[0] == "hey" || data == "Hey")
                {
                    sp.SpeakAsync("Hello, I am Akamaii");
                    test1();
                }
                else if (ssizes[0] == "Who" || ssizes[0] == "who" && a == true && b == true)
                {
                    sp.SpeakAsync("I am Akamaii, A Virtual Assistant");
                    test1();
                }
                else if (ssizes[0] == "How" || ssizes[0] == "how" || ssizes[0]=="how's" && sze > 2)
                {
                    if (ssizes[1] == "are" && ssizes[2] == "you")
                    {
                        sp.SpeakAsync("I am cool, smart and getting better in my future");
                        test1();
                    }
                    else if (ssizes[1] == "the" && ssizes[2] == "Josh")
                    {
                        sp.SpeakAsync("High Sir!");
                        //label1.Text = "High Sir!";
                        test1();
                    }
                    else if (ssizes[1] == "is" && ssizes[2] == "the" && ssizes[3] == "Josh" || ssizes[3] == "josh")
                    {
                        sp.SpeakAsync("High Sir!");
                        //label1.Text = "High Sir!";
                        test1();
                    }
                    else
                    {
                        test1();
                    }

                }
                else if (ssizes[0] == "I" || ssizes[0] == "i")
                {
                    if (ssizes[1] == "Love" || ssizes[1] == "love")
                    {
                        if (ssizes[2] == "You" || ssizes[2] == "you")
                        {
                            sp.SpeakAsync("I Love You Three thousand sir");
                            test1();
                        }
                    }
                }
                else if (ssizes[0] == "Tell" || ssizes[0] == "tell")
                {
                    if (ssizes[1] == "me" || ssizes[1] == "Me")
                    {
                        if (ssizes[2] == "a" || ssizes[2] == "a")
                        {
                            if (ssizes[3] == "joke" || ssizes[3] == "Joke")
                            {
                                if (joke == 1 && joke_start == 0)
                                {
                                    timer1.Enabled = false;
                                    sp.Rate = -1;
                                    sp.SpeakAsync("Ok sir");
                                    sp.SpeakAsync("this might make you Laugh");
                                    sp.SpeakAsync("There is a boy named Bhavya and a very beautiful Girl");
                                    sp.SpeakAsync("But the Joke is that they both are a Couple");
                                    sp.SpeakAsync("he he he he he he");
                                    joke_start = 1;
                                    joke = 2;
                                    timer1.Enabled = true;
                                    test1();
                                }
                                else if (joke == 2)
                                {
                                    timer1.Enabled = false;
                                    sp.Rate = -1;
                                    sp.SpeakAsync("Ok sir");
                                    sp.SpeakAsync("this is a good one");
                                    sp.SpeakAsync("In this election of Lok Sabha");
                                    sp.SpeakAsync("Congress will win the election and Rahul gandhi will be the Prime Minister");
                                    sp.SpeakAsync("he he he he he he");
                                    joke = 3;
                                    timer1.Enabled = true;
                                    test1();
                                }
                                else if (joke == 3)
                                {
                                    timer1.Enabled = false;
                                    sp.Rate = -1;
                                    sp.SpeakAsync("Ok sir");
                                    sp.SpeakAsync("It is regarding Cricket I hope you Like It");
                                    sp.SpeakAsync("In this World Cup");
                                    sp.SpeakAsync("Pakistan will Beat India by ten wickets");
                                    sp.SpeakAsync("he he he he he he");
                                    joke = 4;
                                    timer1.Enabled = true;
                                    test1();
                                }
                                else if (joke == 4)
                                {
                                    timer1.Enabled = false;
                                    sp.Rate = -1;
                                    sp.SpeakAsync("Ok sir");
                                    sp.SpeakAsync("One More regarding Cricket");
                                    sp.SpeakAsync("In this IPL season");
                                    sp.SpeakAsync("RCB will be the winner");
                                    sp.SpeakAsync("and Kohli will have the orange cap");
                                    sp.SpeakAsync("he he he he he he");
                                    joke = 1;
                                    joke_start = 1;
                                    timer1.Enabled = true;
                                    test1();
                                }                                
                                else if (joke_start == 1)
                                {
                                    timer1.Enabled = false;
                                    sp.Rate = -1;
                                    sp.SpeakAsync("Sorry sir");
                                    sp.SpeakAsync("But I am out of jokes");                                    
                                    timer1.Enabled = true;
                                    test1();
                                }                                    
                            }
                        }
                    }
                }
                else if (ssizes[0] == "One" || ssizes[0] == "one")
                {
                    if (ssizes[1] == "more" || ssizes[1] == "More" && joke_start == 1)
                    {
                            if (joke == 2)
                            {
                                timer1.Enabled = false;
                                sp.Rate = -1;
                                sp.SpeakAsync("Ok sir");
                                sp.SpeakAsync("this is a good one");
                                sp.SpeakAsync("In this election of Lok Sabha");
                                sp.SpeakAsync("Congress will win the election and Rahul gandhi will be the Prime Minister");
                                sp.SpeakAsync("he he he he he he");
                                joke = 3;
                                timer1.Enabled = true;
                                test1();
                            }
                            else if (joke == 3)
                            {
                                timer1.Enabled = false;
                                sp.Rate = -1;
                                sp.SpeakAsync("Ok sir");
                                sp.SpeakAsync("It is regarding Cricket I hope you Like It");
                                sp.SpeakAsync("In this World Cup");
                                sp.SpeakAsync("Pakistan will Beat India by ten wickets");
                                sp.SpeakAsync("he he he he he he");
                                joke = 4;
                                timer1.Enabled = true;
                                test1();
                            }
                            else if (joke == 4)
                            {
                                timer1.Enabled = false;
                                sp.Rate = -1;
                                sp.SpeakAsync("Ok sir");
                                sp.SpeakAsync("One More regarding Cricket");
                                sp.SpeakAsync("In this IPL season");
                                sp.SpeakAsync("RCB will be the winner and Kohli will have the orange cap");
                                sp.SpeakAsync("he he he he he he");
                                joke = 1;
                                timer1.Enabled = true;
                                test1();
                            }
                            else if (joke == 1)
                            {
                                timer1.Enabled = false;
                                sp.Rate = -1;
                                sp.SpeakAsync("Sorry sir");
                                sp.SpeakAsync("But I am out of jokes");                                
                                joke_start = 1;
                                timer1.Enabled = true;
                                test1();
                            }
                    }
                    
                                            
                }
                else if (ssizes[0] == "Who" || ssizes[0] == "who" && sze > 2)
                {
                    if (ssizes[1] == "made" && ssizes[2] == "you")
                    {
                        //SpeechSynthesizer sp = new SpeechSynthesizer();
                        //sp.SelectVoiceByHints(VoiceGender.Female);
                        sp.SpeakAsync("I am made by Bhavya Huzaifah deep");
                        test1();
                    }                    
                }
                else if (ssizes[0] == "What" || ssizes[0] == "what" && sze > 3)
                {
                    if (ssizes[1] == "Operation" || ssizes[1] == "operation" && ssizes[2] == "can" || ssizes[2] == "Can" && ssizes[3] == "you" || ssizes[3] == "You" && ssizes[4] == "do" || ssizes[4] == "Do")
                    {
                        sp.SpeakAsync("You can refer the command Guide");
                        test1();
                    }
                }
                else if (ssizes[0] == "scroll" || ssizes[0] == "Scroll")
                {
                    if (ssizes[1] == "up" || ssizes[1] == "Up")
                    {
                        sp.SpeakAsync("Scrolling Up");
                        SendKeys.Send("{PGUP}");
                    }
                    else if (ssizes[1] == "down" || ssizes[1] == "Down")
                    {
                        sp.SpeakAsync("Scrolling Down");
                        SendKeys.Send("{PGDN}");
                    }
                    test1();
                }
                else if ((ssizes[0] == "create" || ssizes[0] == "Create") && ssizes[1] == "folder")
                {
                    sp.SpeakAsync("Creating Folder " + ssizes[2]);                    
                    test1();
                    string foldername = ssizes[2];
                    if (!Directory.Exists(@"C:\Users\admin\Desktop\" + foldername))
                    {
                        Directory.CreateDirectory(@"C:\Users\admin\Desktop\" + foldername);
                    }
                }
                else    if ((ssizes[0] == "When" || ssizes[0] == "when" || ssizes[0] == "Where" || ssizes[0] == "where" || ssizes[0]=="what") )
                {
                    if (ssizes[1] == "is" || ssizes[1] == "Is")
                    {
                        test1();
                        if (ssizes[2] == "my" || ssizes[2] == "My")
                        {
                            if (ssizes[3] != "")
                            {
                                timer1.Enabled = false;
                                string s1 = "Select * from Events  ";
                                SqlDataAdapter da2 = new SqlDataAdapter(s1, con);
                                DataSet ds2 = new DataSet();
                                da2.Fill(ds2, "Events");
                                for (int f = 0; f < ds2.Tables["Events"].Rows.Count; f++)
                                {
                                    if (ds2.Tables["Events"].Rows.Count > 0)
                                    {
                                        string data2 = ds2.Tables["Events"].Rows[f][0].ToString();
                                        string data3 = ds2.Tables["Events"].Rows[f][1].ToString();                                         
                                        string ssizes2 = String.Join(" ",ssizes);                                       
                                        if (ssizes2.Contains(data2)) 
                                        {
                                            sp.SpeakAsync("You Said Your " + data2 + " is at" + data3);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    timer1.Enabled = true;
                    i = 0;
                    test1();
                }
                else if (ssizes[0] == "remember" || ssizes[0] == "Remember")
                {
                    if (ssizes[1] == "event" || ssizes[1] == "Event")
                    {
                        sp.SpeakAsync("What is Event Name");
                        ename = 1;
                        test1();
                    }
                }
                else if (ename == 1)
                {
                    ename = 0;
                    if (ssizes[0] != "")
                    {
                        event_name = data;                        
                        string ins = "insert into Events (name, value) values ('" + event_name + "', '')";
                        SqlConnection con3 = new SqlConnection(ConfigurationManager.ConnectionStrings["Assistance"].ConnectionString);
                        SqlCommand cmd = new SqlCommand(ins, con3);
                        con3.Open();
                        cmd.ExecuteNonQuery();
                        con3.Close();
                        //insert into database
                        sp.SpeakAsync("What you want to remember");
                        evalue = 1;
                    }
                    test1();
                }
                else if (evalue == 1)
                {
                    evalue = 0;
                    if (ssizes[0] != "")
                    {
                        event_value = data;
                        //MessageBox.Show(event_value + ssizes[0]);
                        string ins = "update Events set Value = '" + event_value + "' where Name = '" + event_name + "'";
                        SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["Assistance"].ConnectionString);
                        SqlCommand cmd = new SqlCommand(ins, con2);
                        con2.Open();
                        cmd.ExecuteNonQuery();
                        con2.Close();
                        //insert into database
                        sp.SpeakAsync("Ok I Will Remember");
                    }
                    test1();
                }

                else if (ssizes[0] == "search")
                {
                    string text = "";
                    string otext = "";
                    List<string> list = new List<string>(ssizes);
                    list.RemoveAt(0);
                    foreach (string arr in list)
                    {
                        text += arr + "%20";
                        otext += arr + " ";
                    }
                    sp.SpeakAsync("Searching " + otext + "");
                    //label1.Text = "Searching " + otext + "";
                    test1();
                    System.Diagnostics.Process.Start("chrome.exe", "http://www.google.com/search?q=" + text + "");
                }
                else if (ssizes[0] == "calculate")
                {
                    string text = "";
                    string otext = "";
                    List<string> list = new List<string>(ssizes);
                    list.RemoveAt(0);
                    text += "calculate";
                    foreach (string arr in list)
                    {
                        text += "%20" + arr + "%20";
                        otext += arr + " ";
                    }
                    sp.SpeakAsync("Searching " + otext + "");
                    //label1.Text = "Searching " + otext + "";
                    test1();
                    System.Diagnostics.Process.Start("chrome.exe", "http://www.google.com/search?q=" + text + "");
                }
                else if (notepad == 1)
                {

                    if (ct == 0)
                    {
                        System.Diagnostics.Process.Start("notepad.exe", filename);
                        ct = 1;
                    }
                    if (ssizes[0] == "save" || ssizes[0] == "Save")
                    {
                        save = 1;
                        /*MessageBox.Show(textfile);
                        StreamWriter sw = new StreamWriter(filename);
                        sw.WriteLine(textfile);
                        textfile = "";*/
                        sp.SpeakAsync("File succesfully saved");
                        test1();
                    }
                    else if (ssizes[0] == "move" || ssizes[0] == "Move")
                    {
                        if (ssizes[1] == "left" || ssizes[1] == "Left")
                        {
                            SendKeys.Send("^{LEFT}");
                        }
                        else if (ssizes[1] == "right" || ssizes[1] == "write")
                        {
                            SendKeys.Send("^{RIGHT}");
                        }
                        else if (ssizes[1] == "up")
                        {
                            SendKeys.Send("^{UP}");
                        }
                        else if (ssizes[1] == "down")
                        {
                            SendKeys.Send("^{DOWN}");
                        }
                    }
                    else if (ssizes[0] == "select" || ssizes[0] == "Select")
                    {
                        if (ssizes[1] == "left")
                        {
                            SendKeys.Send("+^LEFT");
                        }
                        else if (ssizes[1] == "right" || ssizes[1] == "write")
                        {
                            SendKeys.Send("+^RIGHT");
                        }
                        else if (ssizes[1] == "all" || ssizes[1] == "everything" || ssizes[1]=="Everything")
                        {
                            SendKeys.Send("^a");
                        }
                        selected = 1;
                    }
                    else if (ssizes[0] == "delete" || ssizes[0] == "Delete")
                    {
                        SendKeys.Send("^{BS}");
                    }
                    else if (ssizes[0] == "Next" || ssizes[0] == "next")
                    {
                        if (ssizes[1] == "Line" || ssizes[1] == "line")
                        {
                            SendKeys.Send("{END}");
                            SendKeys.Send("{ENTER}");
                        }
                    }
                    else if (ssizes[0] == "cut" && selected == 1)
                    {
                        SendKeys.Send("^c");
                        text_cut = 1;
                    }
                    else if (ssizes[0] == "paste" && text_cut == 1)
                    {
                        SendKeys.Send("^v");
                        text_cut = 0;
                    }
                    else if (ssizes[0] == "Speak" || ssizes[0] == "speak")
                    {
                        timer1.Enabled = false;
                        sp.SpeakAsync(textfile);
                        timer1.Enabled = true;
                        test1();
                    }
                    else
                    {
                        for (int i = 0; i < ssizes.Length; i++)
                        {
                            textfile += ssizes[i] + " ";
                            SendKeys.Send(ssizes[i] + " ");
                        }
                        selected = 0;
                    }
                    test1();
                }
                else if (ssizes[0] == "right" || ssizes[0] == "write" && notepad == 0)
                {

                    int word_count = ssizes.Length;

                    FileInfo fi;
                    if (word_count == 1)
                    {
                        filename = @"D:\SpeechEditor\TextFile.txt";
                        FileStream stream = File.Create(filename);
                        stream.Close();
                    }
                    else
                    {
                        filename = @"D:\SpeechEditor\" + ssizes[1] + ".txt";
                        fi = new FileInfo(filename);
                        FileStream fs = fi.Create();
                        fs.Close();
                    }
                    notepad = 1;
                    sp.SpeakAsync("Start Speaking");
                    test1();                  
                }
                else if (ssizes[0] == "shut" || ssizes[0] == "yes" || ssizes[0] == "Yes" || ssizes[0] == "No" || ssizes[0] == "no")
                {
                    if (ssizes[0] == "shut")
                    {
                        sp.SpeakAsync("Do You want to shutdown");
                        if (ssizes[1] == "down")
                        {
                            ssizes[0] = "shutdown";
                            test1();
                        }
                    }
                    if (ssizes[0] == "yes" || ssizes[0] == "Yes" || ssizes[0] == "No" || ssizes[0] == "no")
                    {


                        //DialogResult dresult;
                        // if (ssizes[1] == "down" || ssizes[1] == "Down")
                        //{
                        //   dresult = MessageBox.Show("Are you sure, you want to shutdown the computer ", "Alert", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        // }
                        if (ssizes[0] == "yes" || ssizes[0] == "Yes")
                        {
                            sp.SpeakAsync("Shutting Down");

                            ProcessStartInfo startinfo = new ProcessStartInfo("shutdown.exe", "-s");
                            Process.Start(startinfo);
                        }
                        else if (ssizes[0] == "no" || ssizes[0] == "No")
                        {
                            sp.SpeakAsync("Shut Down Cancel");
                            test1();
                        }
                    }
                }
                else if (ssizes[0] == "shutdown" || ssizes[0] == "yes" || ssizes[0] == "Yes" || ssizes[0] == "No" || ssizes[0] == "no")
                {
                    if (ssizes[0] == "shutdown")
                        sp.SpeakAsync("Do you want to shutdown");
                    test1();
                    /*DialogResult dresult = MessageBox.Show("Are you sure, you want to shutdown the computer ", "Alert", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dresult == DialogResult.OK)
                    {
                        ProcessStartInfo startinfo = new ProcessStartInfo("shutdown.exe", "-s");
                        Process.Start(startinfo);
                    }*/
                    if (ssizes[0] == "yes" || ssizes[0] == "Yes")
                    {
                        sp.SpeakAsync("Shutting Down");
                        string del = "delete from Data";
                        SqlCommand cmd = new SqlCommand(del, con);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        timer1.Enabled = false;
                        ProcessStartInfo startinfo = new ProcessStartInfo("shutdown.exe", "-s");
                        Process.Start(startinfo);
                    }
                    else if (ssizes[0] == "no" || ssizes[0] == "No")
                    {
                        sp.SpeakAsync("Shut Down Cancel");
                        test1();
                    }
                }
                else if (ssizes[0] == "restart" || ssizes[0] == "Restart" || ssizes[0] == "yes" || ssizes[0] == "Yes" || ssizes[0] == "No" || ssizes[0] == "no")
                {
                    if (ssizes[0] == "restart" || ssizes[0] == "Restart")
                        sp.SpeakAsync("Do you want to restart");
                    test1();
                    /*DialogResult dresult = MessageBox.Show("Are you sure, you want to shutdown the computer ", "Alert", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dresult == DialogResult.OK)
                    {
                        ProcessStartInfo startinfo = new ProcessStartInfo("shutdown.exe", "-s");
                        Process.Start(startinfo);
                    }*/
                    if (ssizes[0] == "yes" || ssizes[0] == "Yes")
                    {
                        sp.SpeakAsync("PC Restarting");
                        ProcessStartInfo startinfo = new ProcessStartInfo("shutdown.exe", "-r");
                        Process.Start(startinfo);
                    }
                    else if (ssizes[0] == "no" || ssizes[0] == "No")
                    {
                        sp.SpeakAsync("Restart Cancel");
                        test1();
                    }
                }

                else if (ssizes[0] == "Hello" || ssizes[0] == "hello")
                {
                    if (ssizes[1] == "Jarvis" || ssizes[1] == "jarvis")
                    {
                        if (WindowState == FormWindowState.Minimized)
                        {
                            WindowState = FormWindowState.Normal;
                        }
                        else
                        {
                            /*TopMost = true;
                            Focus();
                            BringToFront();
                            TopMost = false;*/
                        }
                        label1.Text = "Hello Sir";
                        test1();
                    }
                }
                else
                {
                    //label1.Text = "Failure";
                    pictureBox1.Load("D:\\Final_Year_Project\\Changes\\Windows Assistance\\WindowsFormsApplication1\\WindowsFormsApplication1\\Failure.gif");
                    string del = "delete from Data";
                    SqlCommand cmd = new SqlCommand(del, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            else
            {
                label1.Text = "Listening...";
                pictureBox1.Load("D:\\Final_Year_Project\\Changes\\Windows Assistance\\WindowsFormsApplication1\\WindowsFormsApplication1\\Listening.gif");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
           
        }
         private void test1()
        {
            //label1.Text = "Processing.....";
            pictureBox1.Load("D:\\Final_Year_Project\\Changes\\Windows Assistance\\WindowsFormsApplication1\\WindowsFormsApplication1\\Processing.gif");
            string del = "delete from Data";
            SqlCommand cmd = new SqlCommand(del, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
   
}