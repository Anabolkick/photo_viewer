using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace lab7_2
{
    public partial class Form1 : Form
    {
        private int nodeIndex;
        public Form1()
        {
            InitializeComponent();
            nodeIndex = 0;
        }


        private void open_file_btn_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image(*.PNG;*.JPG;*.JPEG;*.BMP;*.GIF;)|*.PNG;*.JPG*.JPEG;*.BMP;*.GIF;";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var filePath = openFileDialog1.FileName;
                var name = filePath.Substring(filePath.LastIndexOf('\\') + 1);
                pictureBox1.Image = Image.FromFile(filePath);
                selected_ph_label.Text = $"Обране фото: {Environment.NewLine} {name}";
            }
        }

        private void open_folder_btn_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                var files = Directory.GetFiles(folderBrowserDialog1.SelectedPath);
                var dir_name = folderBrowserDialog1.SelectedPath.Substring(folderBrowserDialog1.SelectedPath.LastIndexOf('\\') + 1);
                treeView1.Nodes.Add(dir_name, dir_name);
                foreach (string file in files)
                {
                    if (file.EndsWith(".jpeg") || file.EndsWith(".jpg") || file.EndsWith(".png"))
                    {
                        var name = file.Substring(file.LastIndexOf('\\') + 1);
                        treeView1.Nodes[nodeIndex].Nodes.Add(file, name);
                    }
                }
                if (treeView1.Nodes[nodeIndex].Nodes[0] != null)
                {
                    treeView1.SelectedNode = treeView1.Nodes[nodeIndex].Nodes[0];
                    pictureBox1.Image = Image.FromFile(treeView1.SelectedNode.Name);
                }
                nodeIndex++;
            }
        }

        private void prev_btn_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode.PrevNode != null)
            {
                treeView1.SelectedNode = treeView1.SelectedNode.PrevNode;
                pictureBox1.Image = Image.FromFile(treeView1.SelectedNode.Name);
            }
        }

        private void next_btn_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode.NextNode != null)
            {
                treeView1.SelectedNode = treeView1.SelectedNode.NextNode;
                pictureBox1.Image = Image.FromFile(treeView1.SelectedNode.Name);
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                pictureBox1.Image = Image.FromFile(treeView1.SelectedNode.Name);
                selected_ph_label.Text = $"Обране фото: {Environment.NewLine} {treeView1.SelectedNode.Text}";
            }
            catch
            {
                if (treeView1.SelectedNode.Nodes[0] != null)
                {
                    pictureBox1.Image = Image.FromFile(treeView1.SelectedNode.Nodes[0].Name);
                    selected_ph_label.Text = $"Обране фото: {Environment.NewLine} {treeView1.SelectedNode.Nodes[0].Text}";
                }
            }
        }

    }
}
