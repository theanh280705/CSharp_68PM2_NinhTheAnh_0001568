using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public class Form3 : Form
    {
        DataTable dt = DataStorage.LopHoc;
        DataTable dtSinhVien = new DataTable();

        DataGridView dgvSinhVien = new DataGridView();

        DataGridView dgv = new DataGridView();
        TextBox txtMaLop = new TextBox();
        TextBox txtTenLop = new TextBox();

        Button btnThem = new Button();
        Button btnSua = new Button();
        Button btnXoa = new Button();

        public Form3()
        {
            Text = "Quản lý lớp học";
            Size = new Size(1000, 600);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.WhiteSmoke;

            Label lblTitle = new Label()
            {
                Text = "QUẢN LÝ LỚP HỌC",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(20, 10)
            };

            Controls.Add(lblTitle);

            Label lbl1 = new Label()
            {
                Text = "Mã lớp",
                Location = new Point(20, 70)
            };

            txtMaLop.Location = new Point(20, 100);

            Label lbl2 = new Label()
            {
                Text = "Tên lớp",
                Location = new Point(20, 140)
            };

            txtTenLop.Location = new Point(20, 170);

            btnThem.Text = "Thêm";
            btnThem.Location = new Point(20, 230);

            btnSua.Text = "Sửa";
            btnSua.Location = new Point(130, 230);

            btnXoa.Text = "Xóa";
            btnXoa.Location = new Point(240, 230);

            btnThem.Size = new Size(100, 40);
            btnSua.Size = new Size(100, 40);
            btnXoa.Size = new Size(100, 40);

            btnThem.BackColor = Color.DodgerBlue;
            btnSua.BackColor = Color.SeaGreen;
            btnXoa.BackColor = Color.IndianRed;

            btnThem.ForeColor = Color.White;
            btnSua.ForeColor = Color.White;
            btnXoa.ForeColor = Color.White;

            btnThem.FlatStyle = FlatStyle.Flat;
            btnSua.FlatStyle = FlatStyle.Flat;
            btnXoa.FlatStyle = FlatStyle.Flat;

            dgv.Location = new Point(350, 60);
            dgv.Size = new Size(600, 180);
            dgvSinhVien.Location = new Point(20, 320);
            Label lblSV = new Label()
            {
                Text = "Danh sách sinh viên thuộc lớp",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(20, 280)
            };

            Controls.Add(lblSV);
            dgvSinhVien.Size = new Size(930, 250);


            Controls.Add(lbl1);
            Controls.Add(txtMaLop);

            Controls.Add(lbl2);
            Controls.Add(txtTenLop);

            Controls.Add(btnThem);
            Controls.Add(btnSua);
            Controls.Add(btnXoa);

            Controls.Add(dgv);
            Controls.Add(dgvSinhVien);

            dtSinhVien.Columns.Add("Mã SV");
            dtSinhVien.Columns.Add("Họ Tên");
            dtSinhVien.Columns.Add("Lớp");

            dtSinhVien.Rows.Add("1", "Hieu", "68PM1");
            dtSinhVien.Rows.Add("2", "Nguyễn Văn B", "68PM2");
            dtSinhVien.Rows.Add("3", "Trần Văn C", "68PM2");
            dtSinhVien.Rows.Add("4", "Lê Văn D", "68PM1");

            dgv.DataSource = dt;

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgv.RowHeadersVisible = false;

            dgv.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvSinhVien.AutoSizeColumnsMode =
    DataGridViewAutoSizeColumnsMode.Fill;

            dgvSinhVien.RowHeadersVisible = false;

            dgvSinhVien.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgv.CellClick += dgv_CellClick;
            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtMaLop.Text =
                    dgv.Rows[e.RowIndex].Cells[0].Value.ToString();

                txtTenLop.Text =
                    dgv.Rows[e.RowIndex].Cells[1].Value.ToString();

                string maLop =
                    dgv.Rows[e.RowIndex].Cells[0].Value.ToString();

                DataView dv = new DataView(dtSinhVien);

                dv.RowFilter = $"[Lớp] = '{maLop}'";

                dgvSinhVien.DataSource = dv;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            dt.Rows.Add(txtMaLop.Text, txtTenLop.Text);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentRow != null)
            {
                dgv.CurrentRow.Cells[0].Value = txtMaLop.Text;
                dgv.CurrentRow.Cells[1].Value = txtTenLop.Text;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentRow != null)
            {
                dgv.Rows.RemoveAt(dgv.CurrentRow.Index);
            }
        }
    }
}