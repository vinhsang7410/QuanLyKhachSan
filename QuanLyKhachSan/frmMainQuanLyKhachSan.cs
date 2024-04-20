using QuanLyKhachSan.DAO;
using QuanLyKhachSan.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyKhachSan
{
    public partial class frmMainQuanLyKhachSan : Form
    {
        private string userName;
        public frmMainQuanLyKhachSan(string userName)
        {
            this.userName = userName;
            InitializeComponent();
            fLoad();
        }

        public bool IsAdmin()
        {
            return LoaiTaiKhoanDAO.Instance.GetStaffTypeByUserName(userName).Id == 1;
        }
        void fLoad()
        {

            panelLeft.Width = 177;

        }
        private bool CheckAccess(string nameform)
        {
            return QuyenTruyCapDAO.Instance.CheckAccess(userName, nameform);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                Application.Exit();
        }

        private void btnNavigationPanel_Click(object sender, EventArgs e)
        {
            if (panelLeft.Width == 42)
            {
                panelLeft.Width = 177;
                panelRight.Width = 939;
                this.Width = 1116;
            }
            else
            {
                panelLeft.Width = 42;
                panelRight.Width = 807;
                this.Width = 981;
            }
        }

        private void titleBookRoom_Click(object sender, EventArgs e)
        {
            if (CheckAccess("fBookRoom"))
            {
                Hide();
                frmDatPhong f = new frmDatPhong();
                f.ShowDialog();
                Show();
            }
            else MessageBox.Show("Bạn không quyền truy cập.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmDangNhap login = new frmDangNhap();
            login.ShowDialog();
        }

        private void titleRecieveRoom_Click(object sender, EventArgs e)
        {
            if (CheckAccess("fReceiveRoom"))
            {
                this.Hide();
                frmNhanPhong f = new frmNhanPhong();
                f.ShowDialog();
                this.Show();
            }
            else MessageBox.Show("Bạn không quyền truy cập.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void titleSendRoom_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmSuDungDichVuVaThanhToan f = new frmSuDungDichVuVaThanhToan(userName);
            f.ShowDialog();
            this.Show();
        }

        private void titlePay_Click(object sender, EventArgs e)
        {
            if (CheckAccess("fUseService"))
            {
                this.Hide();
                frmSuDungDichVuVaThanhToan f = new frmSuDungDichVuVaThanhToan(userName);
                f.ShowDialog();
                this.Show();
            }
            else MessageBox.Show("Bạn không quyền truy cập.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void titleManageRoom_Click(object sender, EventArgs e)
        {
            if (CheckAccess("froom"))
            {
                this.Hide();
                frmQuanLyPhong fProfile = new frmQuanLyPhong();
                fProfile.ShowDialog();
                this.Show();
            }
            else
                MessageBox.Show("Bạn không quyền truy cập.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnAccountProfile_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmThongTinCaNhan fProfile = new frmThongTinCaNhan(userName);
            fProfile.ShowDialog();
            this.Show();
        }

        private void metroTile17_Click(object sender, EventArgs e)
        {
            if (CheckAccess("fcustomer"))
            {
                this.Hide();
                frmQuanLyKhachHang customer = new frmQuanLyKhachHang();
                customer.ShowDialog();
                this.Show();
            }
            else
                MessageBox.Show("Bạn không quyền truy cập.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void metroTile13_Click(object sender, EventArgs e)
        {
            if (CheckAccess("fparameter"))
            {
                this.Hide();
                frmQuyDinh parameter = new frmQuyDinh();
                parameter.ShowDialog();
                this.Show();
            }
            else
                MessageBox.Show("Bạn không quyền truy cập.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void metroTile8_Click(object sender, EventArgs e)
        {
            if (CheckAccess("fstaff"))
            {
                this.Hide();
                frmQuanLyNhanVien fProfile = new frmQuanLyNhanVien();
                fProfile.ShowDialog();
                this.Show();
            }
            else
                MessageBox.Show("Bạn không quyền truy cập.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void metroTile2_Click(object sender, EventArgs e)
        {
            if (CheckAccess("fservice"))
            {
                this.Hide();
                frmQuanLyDichVu fProfile = new frmQuanLyDichVu();
                fProfile.ShowDialog();
                this.Show();
            }
            else
                MessageBox.Show("Bạn không quyền truy cập.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnIntroduce_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmGioiThieu fAbout = new frmGioiThieu();
            fAbout.ShowDialog();
            this.Show();
        }

        private void title_Click(object sender, EventArgs e)
        {
            if (CheckAccess("freport"))
            {
                this.Hide();
                frmBaoCaoDoanhThu fAbout = new frmBaoCaoDoanhThu();
                fAbout.ShowDialog();
                this.Show();
            }
            else
                MessageBox.Show("Bạn không quyền truy cập.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void metroTile16_Click(object sender, EventArgs e)
        {
            if (CheckAccess("fBill"))
            {
                this.Hide();
                frmQuanLyHoaDon fAbout = new frmQuanLyHoaDon();
                fAbout.ShowDialog();
                this.Show();
            }
            else
                MessageBox.Show("Bạn không quyền truy cập.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void panelRight_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
