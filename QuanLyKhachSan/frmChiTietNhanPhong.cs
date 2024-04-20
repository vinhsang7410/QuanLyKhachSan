using QuanLyKhachSan.DAO;
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
    public partial class frmChiTietNhanPhong : Form
    {
        int idReceiveRoom;
        public frmChiTietNhanPhong(int _idReceiRoom)
        {
            InitializeComponent();
            idReceiveRoom = _idReceiRoom;
            ShowReceiveRoom(_idReceiRoom);
            ShowCustomers(_idReceiRoom);
        }

        public void ShowReceiveRoom(int idReceiveRoom)
        {
            DataRow data = NhanPhongDAO.Instance.ShowReceiveRoom(idReceiveRoom).Rows[0];
            txbIDReceiveRoom.Text = ((int)data["Mã nhận phòng"]).ToString();
            txbRoomName.Text = data["Tên phòng"].ToString();
            txbDateCheckIn.Text = ((DateTime)data["Ngày nhận"]).ToString().Split(' ')[0];
            txbDateCheckOut.Text = ((DateTime)data["Ngày trả"]).ToString().Split(' ')[0];
        }
        public void ShowCustomers(int idReceiveRoom)
        {
            dataGridView.DataSource = NhanPhongDAO.Instance.ShowCusomers(idReceiveRoom);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnClose__Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            frmThemThongTinKhachHang f = new frmThemThongTinKhachHang();
            f.ShowDialog();
            Show();
            if (frmThemThongTinKhachHang.ListIdCustomer.Count > 0)
                foreach (var item in frmThemThongTinKhachHang.ListIdCustomer)
                {
                    ChiTietNhanPhongDAO.Instance.InsertReceiveRoomDetails(idReceiveRoom, item);
                }
            ShowCustomers(idReceiveRoom);
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            string idCard = dataGridView.SelectedRows[0].Cells[1].Value.ToString();
            int idCustomer = KhachHangDAO.Instance.GetInfoByIdCard(idCard).Id;
            if (idCustomer != KhachHangDAO.Instance.GetIDCustomerFromBookRoom(idReceiveRoom))
            {
                ChiTietNhanPhongDAO.Instance.DeleteReceiveRoomDetails(idReceiveRoom, idCustomer);
                MessageBox.Show("Xóa khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ShowCustomers(idReceiveRoom);
            }
            else
                MessageBox.Show("Không thể xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {
            string idCard = dataGridView.SelectedRows[0].Cells[1].Value.ToString();
            int idCustomer = KhachHangDAO.Instance.GetInfoByIdCard(idCard).Id;
            frmCapNhatThongTinKhachHang f = new frmCapNhatThongTinKhachHang(idCard);
            f.ShowDialog();
            Show();
            ShowCustomers(idReceiveRoom);
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            frmDoiPhong f = new frmDoiPhong(PhongDAO.Instance.GetIdRoomFromReceiveRoom(idReceiveRoom), idReceiveRoom);
            f.ShowDialog();
            Show();
            ShowReceiveRoom(idReceiveRoom);
        }
    }
}
