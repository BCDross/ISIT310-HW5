using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISIT310_HW5
{
    public partial class Form : System.Windows.Forms.Form
    {
        BirdsDataContext birdsCollections = new BirdsDataContext();

        public Form()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            DataLoad();
        }

        private void DataLoad()
        {
            var birds = from myBirdCounts in birdsCollections.BirdCounts
                orderby myBirdCounts.BirdID ascending
                select new
                {
                    myBirdCounts.CountID,
                    myBirdCounts.Bird.Name,
                    myBirdCounts.CountDate,
                    myBirdCounts.Counted,
                    myBirdCounts.Bird.Description
                };

            dataGridView1.DataSource = birds;
        }

        private void btnUpdateCount_Click(object sender, EventArgs e)
        {
            int selectedCountID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["CountID"].Value);

            var selectedBirdCount = (from myBirdCounts in birdsCollections.BirdCounts
                where myBirdCounts.CountID == selectedCountID
                select myBirdCounts).Single();

            int newCount = Convert.ToInt32(textBox1.Text);

            selectedBirdCount.Counted = newCount;
            try
            {
                birdsCollections.SubmitChanges();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating row: " + ex.Message);
            }
            DataLoad();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int selectedCountID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["CountID"].Value);

            var selectedBirdCount = (from myBirdCounts in birdsCollections.BirdCounts
                                     where myBirdCounts.CountID == selectedCountID
                                     select myBirdCounts).Single();

            textBox1.Text = selectedBirdCount.Counted.ToString();
        }
    }
}
