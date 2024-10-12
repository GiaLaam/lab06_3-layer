using lab06_DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab06_3_layer
{
    public partial class RegisterMajorForm : Form
    {
        private Student student;

        public RegisterMajorForm(Student student)
        {
            
        }
        public RegisterMajorForm()
        {
            InitializeComponent();
        }

        private void RegisterMajorForm_Load(object sender, EventArgs e)
        {

        }
    }
}
