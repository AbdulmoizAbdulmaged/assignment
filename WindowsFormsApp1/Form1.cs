using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net.Http;
using System.Text.Json;



namespace WindowsFormsApp1
{
    public partial class User : Form
    {
        public User()
        {
            InitializeComponent();
        }

        public object ListView1 { get; private set; }
       

        private void button1_Click_1(object sender, EventArgs e)
        {
            //save data to text file
            saveDataToTextFile();
           
        }

       

       
       

        private void User_Load(object sender, EventArgs e)
        {
            // read from text file
            readUserInfoFromTextFile();
           
        }

        

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //get selected item
            getSelectedItem();
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

            //push to snapchat
            pushToSnapchat();

           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            addUser();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //remove an item
            removeUser();
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //edit user
            editUser();
        }






        //..............................................
        public void pushToSnapchat()
        {
            //push the user's info to snapchat api
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                try
                {
                    var postData = new PostData // create user info object
                    {
                        UserName = listView1.Items[i].SubItems[1].Text,
                        Email = listView1.Items[i].SubItems[2].Text,
                        Password = listView1.Items[i].SubItems[3].Text


                    };


                    //passing the user info object to the snapchat api


                    var client = new HttpClient();  //create http client object
                    client.BaseAddress = new Uri("https://api.snapchat.com"); // provide api url, replace with actual snapchat url
                    var json = JsonSerializer.Serialize(postData); // convert user info to json format
                    var content = new StringContent(json, Encoding.UTF8, "application/json"); // add encoding type
                    var response = client.PostAsync("/adplatform", content).Result; // send post request to api end point


                    if (response.IsSuccessStatusCode) // check the reponse status
                    {
                        var responseContent = response.Content.ReadAsStringAsync().Result; // get the respone from api
                        var postResponse = JsonSerializer.Deserialize<PostResponse>(responseContent); // convert from json to c# object

                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode);
                    }


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }

        }
        //.................................
        public void getSelectedItem()
        {

            // Check if an item is selected
            if (listView1.SelectedItems.Count > 0)
            {
                // Get the index of the selected item
                UserId.UserIndex = listView1.SelectedItems[0].Index;
                textBox1.Text = listView1.SelectedItems[0].SubItems[0].Text;
                textBox2.Text = listView1.SelectedItems[0].SubItems[1].Text;
                textBox3.Text = listView1.SelectedItems[0].SubItems[2].Text;
                textBox4.Text = listView1.SelectedItems[0].SubItems[3].Text;


            }
        }
        //............................................
        public void removeUser()
        {
            //remove an item
            try
            {
                listView1.Items.RemoveAt(UserId.UserIndex);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        //..............................................
        public void editUser()
        {
            //edit user
            try

            {
                listView1.SelectedItems[0].SubItems[0].Text = textBox1.Text;
                listView1.SelectedItems[0].SubItems[1].Text = textBox2.Text;
                listView1.SelectedItems[0].SubItems[2].Text = textBox3.Text;
                listView1.SelectedItems[0].SubItems[3].Text = textBox4.Text;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        //.................................................
        public void readUserInfoFromTextFile()
        {
            try
            {   // read the stored data from text file to listview
                var fileLines = File.ReadAllLines(@"UserInfo.txt");
                for (int i = 0; i + 3 < fileLines.Length; i += 4)
                {
                    listView1.Items.Add(
                        new ListViewItem(new[]
                        {
                fileLines[i],
                fileLines[i + 1],
                fileLines[i + 2],
                fileLines[i + 3],

                        }));
                }
                // Subscribe to the SelectedIndexChanged event
                listView1.SelectedIndexChanged += ListView1_SelectedIndexChanged;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        //...........................................
        public void saveDataToTextFile()
        {
            try
            {
                //save users info to text file
                using (StreamWriter sw = new StreamWriter("UserInfo.txt"))
                {
                    for (int i = 0; i < listView1.Items.Count; i++)
                    {
                        sw.WriteLine(listView1.Items[i].SubItems[0].Text);
                        sw.WriteLine(listView1.Items[i].SubItems[1].Text);
                        sw.WriteLine(listView1.Items[i].SubItems[2].Text);
                        sw.WriteLine(listView1.Items[i].SubItems[3].Text);
                    }



                }
                MessageBox.Show("All User's Info have been saved successfully..");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
        //............................................
        public void addUser()
        {
            try
            {
                //add single user info
                ListViewItem item = new ListViewItem(textBox1.Text);
                item.SubItems.Add(textBox2.Text);
                item.SubItems.Add(textBox3.Text);
                item.SubItems.Add(textBox4.Text);
                listView1.Items.Add(item);
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
    }
    
   




