using Selim.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JsonToDart
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // copy btn
            try
            {
                Clipboard.SetText(dartTextBox.Text);
            }
            catch { }
        }

        private void convertBtn_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(jsonTextBox.Text)) return;
            //
            try
            {
                var jp = new JsonParser();
                var json = jp.Parse(jsonTextBox.Text);

                if (!(json is JsonObject))
                {
                    MessageBox.Show(this, "must be json object");
                    return;
                }
                //
                var jsonObject = json as JsonObject;

                var className = string.IsNullOrWhiteSpace(classNameTextBox.Text) ? "RootClass"
                    : classNameTextBox.Text;

                var generator = new DartClassGenerator();

                dartTextBox.Text = generator.generateDartClass(jsonObject, className);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new Form2().ShowDialog(this);
        }
    }
}
