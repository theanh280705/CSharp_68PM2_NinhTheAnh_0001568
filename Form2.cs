using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form2 : Form
    {
        // Khai báo các thành phần giao diện
        private MenuStrip menuStrip;
        private Label lblTitleInfo, lblMaSV, lblHoTen, lblNgaySinh, lblGioiTinh, lblLop, lblTimKiem, lblPhanTrang;
        private TextBox txtMaSV, txtHoTen, txtTimKiem;
        private DateTimePicker dtpNgaySinh;
        private ComboBox cboGioiTinh, cboLop;
        private Button btnThem, btnSua, btnXoa, btnLamMoi, btnTim;
        private Button btnFirst, btnPrev, btnNext, btnLast;
        private DataGridView dgvSinhVien;
        private Panel panelLeft, panelRight, panelPhanTrang;

        public Form2()
        {
            // Cấu hình Form2 chính
            this.Text = "Quản Lý Sinh Viên";
            this.Size = new Size(1120, 760);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Segoe UI", 10);
            this.BackColor = Color.FromArgb(240, 240, 240);

            // Gọi hàm tự dựng giao diện thay cho InitializeComponent mặc định
            BuildCustomUI();

            // Đổ dữ liệu mẫu vào bảng
            LoadMockData();
        }

        private void BuildCustomUI()
        {
            // 1. MenuThanh điều hướng (MenuStrip)
            menuStrip = new MenuStrip();
            menuStrip.Items.Add("Quản Lý Sinh Viên");
            menuStrip.Items.Add("Quản Lý Lớp Học");
            ToolStripMenuItem menuDangXuat = new ToolStripMenuItem("Đăng xuất") { ForeColor = Color.Red };
            menuStrip.Items.Add(menuDangXuat);
            this.Controls.Add(menuStrip);

            // 2. Panel Trái - Nhập thông tin sinh viên
            panelLeft = new Panel
            {
                Location = new Point(15, 45),
                Size = new Size(330, 650),
                BackColor = Color.FromArgb(245, 245, 245)
            };
            this.Controls.Add(panelLeft);

            lblTitleInfo = new Label { Text = "Thông tin sinh viên", Font = new Font("Segoe UI", 11, FontStyle.Bold), Location = new Point(10, 10), AutoSize = true };

            lblMaSV = new Label { Text = "Mã sinh viên:", Location = new Point(10, 50), AutoSize = true };
            txtMaSV = new TextBox { Location = new Point(10, 75), Size = new Size(300, 27) };

            lblHoTen = new Label { Text = "Họ và tên:", Location = new Point(10, 115), AutoSize = true };
            txtHoTen = new TextBox { Location = new Point(10, 140), Size = new Size(300, 27) };

            lblNgaySinh = new Label { Text = "Ngày sinh:", Location = new Point(10, 185), AutoSize = true };
            dtpNgaySinh = new DateTimePicker { Location = new Point(10, 210), Size = new Size(300, 27), Format = DateTimePickerFormat.Short };

            lblGioiTinh = new Label { Text = "Giới tính:", Location = new Point(10, 255), AutoSize = true };
            cboGioiTinh = new ComboBox { Location = new Point(10, 280), Size = new Size(300, 27), DropDownStyle = ComboBoxStyle.DropDownList };
            cboGioiTinh.Items.AddRange(new string[] { "Nam", "Nữ" });

            lblLop = new Label { Text = "Lớp:", Location = new Point(10, 325), AutoSize = true };
            cboLop = new ComboBox { Location = new Point(10, 350), Size = new Size(300, 27), DropDownStyle = ComboBoxStyle.DropDownList };
            cboLop.Items.AddRange(new string[] { "68PM1 - Lớp 68PM1", "68PM2 - Lớp 68PM2" });

            // 4 Nút Thêm, Sửa, Xóa, Làm mới
            btnThem = new Button { Text = "Thêm", Location = new Point(10, 430), Size = new Size(140, 45), BackColor = Color.FromArgb(52, 152, 219), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnSua = new Button { Text = "Sửa", Location = new Point(170, 430), Size = new Size(140, 45), BackColor = Color.FromArgb(46, 204, 113), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnXoa = new Button { Text = "Xóa", Location = new Point(10, 490), Size = new Size(140, 45), BackColor = Color.FromArgb(231, 76, 60), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnLamMoi = new Button { Text = "Làm mới", Location = new Point(170, 490), Size = new Size(140, 45), BackColor = Color.FromArgb(127, 140, 141), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };

            panelLeft.Controls.AddRange(new Control[] {
                lblTitleInfo, lblMaSV, txtMaSV, lblHoTen, txtHoTen,
                lblNgaySinh, dtpNgaySinh, lblGioiTinh, cboGioiTinh,
                lblLop, cboLop, btnThem, btnSua, btnXoa, btnLamMoi
            });

            // 3. Panel Phải - Thanh tìm kiếm và DataGridView
            panelRight = new Panel
            {
                Location = new Point(360, 45),
                Size = new Size(720, 650),
                BackColor = Color.White
            };
            this.Controls.Add(panelRight);

            lblTimKiem = new Label { Text = "Tìm kiếm (Tên / Mã SV / Lớp):", Location = new Point(10, 10), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            txtTimKiem = new TextBox { Location = new Point(10, 35), Size = new Size(420, 27) };
            btnTim = new Button { Text = "Tìm", Location = new Point(440, 33), Size = new Size(130, 30), BackColor = Color.FromArgb(44, 62, 80), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };

            // DataGridView hiển thị danh sách
            dgvSinhVien = new DataGridView
            {
                Location = new Point(10, 80),
                Size = new Size(700, 470),
                BackgroundColor = Color.White,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            // 4. Thanh phân trang dưới bảng
            panelPhanTrang = new Panel { Location = new Point(10, 560), Size = new Size(700, 50) };
            btnFirst = new Button { Text = "<<", Location = new Point(10, 10), Size = new Size(60, 30) };
            btnPrev = new Button { Text = "<", Location = new Point(75, 10), Size = new Size(60, 30) };
            lblPhanTrang = new Label { Text = "Trang 1/1  |  3 bản ghi", Location = new Point(290, 15), AutoSize = true };
            btnNext = new Button { Text = ">", Location = new Point(565, 10), Size = new Size(60, 30) };
            btnLast = new Button { Text = ">>", Location = new Point(630, 10), Size = new Size(60, 30) };

            panelPhanTrang.Controls.AddRange(new Control[] { btnFirst, btnPrev, lblPhanTrang, btnNext, btnLast });

            panelRight.Controls.AddRange(new Control[] { lblTimKiem, txtTimKiem, btnTim, dgvSinhVien, panelPhanTrang });
        }

        private void LoadMockData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Mã SV");
            dt.Columns.Add("Họ và Tên");
            dt.Columns.Add("Giới Tính");
            dt.Columns.Add("Ngày Sinh");
            dt.Columns.Add("Lớp");

            dt.Rows.Add("1", "hieu", "Nam", "11/03/2026", "68PM1");
            dt.Rows.Add("2", "Nguyễn Văn B", "Nam", "11/03/2026", "68PM2");
            dt.Rows.Add("3", "Trần Văn C", "Nam", "21/03/2026", "68PM2");

            dgvSinhVien.DataSource = dt;
        }
    }
}