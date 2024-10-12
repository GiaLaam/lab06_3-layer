using lab06_BUS;
using lab06_DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace lab06_3_layer
{
    public partial class Form1 : Form
    {
        private readonly StudentService studentService = new StudentService();
        private readonly FacultyService facultyService = new FacultyService();
        private readonly AvatarService avatarService = new AvatarService();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                setGridViewStyle(dataGridView1);
                var listFacultys = facultyService.GetAll();
                var listStudent = studentService.GetAll();
                FillFacultyCBB(listFacultys);
                BindGrid(listStudent);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void BindGrid(List<Student> listStudent)
        {
            dataGridView1.Rows.Clear();
            foreach (var item in listStudent)
            {
                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells[0].Value = item.StudentID;
                dataGridView1.Rows[index].Cells[1].Value = item.FullName;
                if (item.Faculty != null)
                    dataGridView1.Rows[index].Cells[2].Value = item.Faculty.FacultyName;
                dataGridView1.Rows[index].Cells[3].Value = item.AverageScore + "";
                if (item.MajorID != null)
                    dataGridView1.Rows[index].Cells[4].Value = item.Major.Name + "";
                ShowAvatar(item.Avatar);
            }
        }

        private void ShowAvatar(string ImageName)
        {
            if (string.IsNullOrEmpty(ImageName))
            {
                ptBox.Image = null;
            }
            else
            {
                string parentDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
                string imagePath = Path.Combine(parentDirectory, "Igames", ImageName);
                ptBox.Image = Image.FromFile(imagePath);
                ptBox.Refresh();
            }
        }

        private void FillFacultyCBB(List<Faculty> listFacultys)
        {
            listFacultys.Insert(0, new Faculty());
            this.comboBox.DataSource = listFacultys;
            this.comboBox.DisplayMember = "FacultyName";
            this.comboBox.ValueMember = "FacultyID";

        }

        private void setGridViewStyle(DataGridView dgview)
        {
            dgview.BorderStyle = BorderStyle.None;
            dgview.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgview.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
            dgview.BackgroundColor = Color.White;
            dgview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnptBox_Click(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtMa.Text.Trim(), out int studentIdInt))
                {
                    MessageBox.Show("Vui lòng nhập ID sinh viên hợp lệ.");
                    return;
                }
                string fullName = txtName.Text.Trim();
                if (string.IsNullOrEmpty(fullName))
                {
                    MessageBox.Show("Tên sinh viên không được để trống.");
                    return;
                }

                if (!float.TryParse(txtDiem.Text.Trim(), out float averageScore))
                {
                    MessageBox.Show("Vui lòng nhập điểm trung bình hợp lệ.");
                    return;
                }
                Faculty selectedFaculty = comboBox.SelectedItem as Faculty;
                if (selectedFaculty == null)
                {
                    MessageBox.Show("Vui lòng chọn khoa cho sinh viên.");
                    return;
                }
                Student newStudent = new Student
                {
                    StudentID = studentIdInt,
                    FullName = fullName,
                    AverageScore = averageScore,
                    FacultyID = selectedFaculty.FacultyID 
                };
                studentService.AddStudent(newStudent);
                MessageBox.Show("Thêm sinh viên thành công.");
                BindGrid(studentService.GetAll());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void chkUnregisterMajor_CheckedChanged_CheckedChanged(object sender, EventArgs e)
        {
            var listStudents = new List<Student>();
            if (this.chkUnregisterMajor.Checked)
                listStudents = studentService.GetAllHasNoMajor();
            else
                listStudents = studentService.GetAll();
            BindGrid(listStudents);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtMa.Text.Trim(), out int studentIdInt))
                {
                    MessageBox.Show("Vui lòng nhập ID sinh viên hợp lệ.");
                    return;
                }
                studentService.RemoveStudent(studentIdInt);

                MessageBox.Show("Xóa sinh viên thành công.");

                BindGrid(studentService.GetAll());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void dataGridView(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Kiểm tra rằng click vào một hàng hợp lệ
            {
                // Lấy thông tin sinh viên từ DataGridView
                var studentId = (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value; // Giả sử ID sinh viên ở cột đầu tiên
                var student = studentService.FinbyId(studentId);
                using (var registerMajorForm = new RegisterMajorForm(student))
                {
                    if (registerMajorForm.ShowDialog() == DialogResult.OK)
                    {
                        MessageBox.Show("Đăng ký chuyên ngành thành công.");
                        // Cập nhật lại danh sách sinh viên
                        BindGrid(studentService.GetAll());
                    }
                }
            }
        }

        private void ptBox_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                ptBox.Image = Image.FromFile(open.FileName);
                this.Text = open.FileName;
            }
        }
    }
}
