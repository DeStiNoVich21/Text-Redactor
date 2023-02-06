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
using Microsoft.WindowsAPICodePack.Dialogs;
namespace WindowsFormsApp11
{
    public partial class Form1 : Form
    {
        // path of the chosen folder
        private string path = @"C:/Users/User/source/repos/WindowsFormsApp11/WindowsFormsApp11/Documents/";
        private int singleorfolder = 0;
        // value nneded to correctly change the colormod
        private int f = 0;
        public Form1()
        {
            InitializeComponent();
            try
            {
                DirectoryInfo d = new DirectoryInfo(path); //Assuming Test is your Folder

                FileInfo[] Files = d.GetFiles("*.txt"); //Getting Text files
               
                    foreach (FileInfo file in Files)
                    {

                    if (file.Exists)
                    {
                        extendedTreeView1.Nodes.Add(file.Name);
                    }
                       
                    }
         
            } 
            catch(IOException ex)
            {
                MessageBox.Show(ex.ToString(),"Error",MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) ;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
          
        }

        private async void guna2Button1_Click(object sender, EventArgs e)
        {
            
        }

        private void extendedTreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            
            try
            {
                if (singleorfolder == 1)
                {
                        // opening the chosen txt file
                    richTextBox1.Text = "";
                    richTextBox1.Text = File.ReadAllText(extendedTreeView1.SelectedNode.Text);               
                }
                else
                {
                    // opening the chosen txt file
                    richTextBox1.Text = "";
                    richTextBox1.Text = File.ReadAllText(path + "/" + extendedTreeView1.SelectedNode.Text);
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

        }
        private void saver()
        {

        }
        private async void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            try
            {
                Stream myStream;
                SaveFileDialog savas = new SaveFileDialog();
                savas.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                savas.FilterIndex = 2;
                savas.RestoreDirectory = true;

                if (savas.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter writer = new StreamWriter(savas.FileName.ToString(), false))
                    { 
                        await writer.WriteLineAsync(richTextBox1.Text);
                    }
                }
            }
            catch
            {

            }
        }

        private void openFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (singleorfolder ==1)
            { 
            //singleorfolder = singleorfolder * 0;
                extendedTreeView1.Nodes.Clear();
            }
            try
            {
                // opening file dialog
                CommonOpenFileDialog opg = new CommonOpenFileDialog();
                opg.IsFolderPicker = true;
                opg.ShowDialog();
              

                DirectoryInfo d = new DirectoryInfo(opg.FileName.ToString()); //Assuming Test is your Folder

                FileInfo[] Files = d.GetFiles(); //Getting Text files
                path = opg.FileName.ToString();
                //clearing and re-adding the treeview
                extendedTreeView1.Nodes.Clear();
                foreach (FileInfo file in Files)
                {

                    if (file.Exists)
                    { 

                        extendedTreeView1.Nodes.Add(file.Name);
                    }

                }

            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            // functionality of working with separate files
            if (singleorfolder == 0)
            {
                singleorfolder = singleorfolder+ 1;
                extendedTreeView1.Nodes.Clear();
            }
            try
            {
                // opening the chosen txt file

                richTextBox1.Text = "";
                CommonOpenFileDialog com = new CommonOpenFileDialog();
                com.ShowDialog();
                extendedTreeView1.Nodes.Add(com.FileName.ToString());
                richTextBox1.Text = File.ReadAllText(com.FileName.ToString());
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private async void guna2Button2_Click(object sender, EventArgs e)
        {
           
        }

        private async void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // works if the only separete element is opened
                if (singleorfolder == 1)
                {
                    string pathe = extendedTreeView1.SelectedNode.Text;
                    // полная перезапись файла 
                    if (File.Exists(pathe))
                    {

                        using (StreamWriter writer = new StreamWriter(pathe, false))
                        {

                            await writer.WriteLineAsync(richTextBox1.Text);
                        }
                    }
                    else
                    {
                        MessageBox.Show("The File doesnt exist", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }


                }
                // Works if you work with folder
                else
                {

                    string pathe = path + "/" + extendedTreeView1.SelectedNode.Text;
                    // saving already existing file  by REwriting it
                    if (File.Exists(pathe))
                    {

                        using (StreamWriter writer = new StreamWriter(pathe, false))
                        {

                            await writer.WriteLineAsync(richTextBox1.Text);
                        }
                        MessageBox.Show("Successfully saved", "Success");
                    }
                    else
                    {
                        MessageBox.Show("The File doesnt exist", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void fontSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
         
        }

        private void lightModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (f == 0)
            {
                richTextBox1.BackColor = Color.Black;
                richTextBox1.ForeColor = Color.White;
                extendedTreeView1.ForeColor = Color.White;
                extendedTreeView1.BackColor = Color.Black;
                ColorModChange.Text = "Light Mod";
                f = 1;
            }
            else
            {
                richTextBox1.BackColor = Color.White;
                richTextBox1.ForeColor = Color.Black;
                extendedTreeView1.ForeColor = Color.Black;
                extendedTreeView1.BackColor = Color.White;
                ColorModChange.Text = "Black Mod";
                f = 0;
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void supportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("I really appreciate that you like my app", "Thank you");
        }
    }
}
