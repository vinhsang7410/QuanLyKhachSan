using QuanLyKhachSan.DAO;
using QuanLyKhachSan.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyKhachSan
{
    public partial class frmDatPhong : Form
    {
        public frmDatPhong()
        {
            InitializeComponent();
            LoadData();
        }

        public void LoadData()
        {
            LoadRoomType();
            LoadCustomerType();
            LoadDate();
            LoadDays();
            LoadListBookRoom();
        }
        public void LoadRoomType()
        {
            cbRoomType.DataSource = LoaiPhongDAO.Instance.LoadListRoomType();
            cbRoomType.DisplayMember = "Name";
        }
        public void LoadRoomTypeInfo(int id)
        {
            LoaiPhong roomType = LoaiPhongDAO.Instance.LoadRoomTypeInfo(id);
            txbRoomTypeID.Text = roomType.Id.ToString();
            txbRoomTypeName.Text = roomType.Name;
            CultureInfo cultureInfo = new CultureInfo("vi-vn");
            txbPrice.Text = roomType.Price.ToString("c0", cultureInfo);
            txbAmountPeople.Text = roomType.LimitPerson.ToString();
        }
        public void LoadDate()
        {
            dpkDateOfBirth.Value = new DateTime(1998, 4, 6);
            dpkDateCheckIn.Value = DateTime.Now;
            dpkDateCheckOut.Value = DateTime.Now.AddDays(1);
        }
        public void LoadDays()
        {
            txbDays.Text = (dpkDateCheckOut.Value.Date - dpkDateCheckIn.Value.Date).Days.ToString();
        }
        public void LoadCustomerType()
        {
            cbCustomerType.DataSource = LoaiKhachHangDAO.Instance.LoadListCustomerType();
            cbCustomerType.DisplayMember = "Name";
        }
        public bool IsIdCardExists(string idCard)
        {
            return KhachHangDAO.Instance.IsIdCardExists(idCard);
        }
        public void InsertCustomer(string idCard, string name, int idCustomerType, DateTime dateofBirth, string address, int phonenumber, string sex, string nationality)
        {
            KhachHangDAO.Instance.InsertCustomer(idCard, name, idCustomerType, dateofBirth, address, phonenumber, sex, nationality);
        }
        public void GetInfoByIdCard(string idCard)
        {
            KhachHang customer = KhachHangDAO.Instance.GetInfoByIdCard(idCard);
            txbIDCard.Text = customer.IdCard.ToString();
            txbFullName.Text = customer.Name;
            txbAddress.Text = customer.Address;
            dpkDateOfBirth.Value = customer.DateOfBirth;
            cbSex.Text = customer.Sex;
            txbPhoneNumber.Text = customer.PhoneNumber.ToString();
            cbNationality.Text = customer.Nationality;
            cbCustomerType.Text = LoaiKhachHangDAO.Instance.GetNameByIdCard(idCard);
        }
        public void InsertBookRoom(int idCustomer, int idRoomType, DateTime datecheckin, DateTime datecheckout, DateTime datebookroom)
        {
            DatPhongDAO.Instance.InsertBookRoom(idCustomer, idRoomType, datecheckin, datecheckout, datebookroom);
        }
        public int GetCurrentIDBookRoom(DateTime dateTime)
        {
            return DatPhongDAO.Instance.GetCurrentIDBookRoom(dateTime);
        }
        public void LoadListBookRoom()
        {
            dataGridViewBookRoom.DataSource = DatPhongDAO.Instance.LoadListBookRoom(DateTime.Now.Date);
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            int idBookRoom = (int)dataGridViewBookRoom.SelectedRows[0].Cells[0].Value;
            string idCard = dataGridViewBookRoom.SelectedRows[0].Cells[2].Value.ToString();
            frmChiTietDatPhong f = new frmChiTietDatPhong(idBookRoom, idCard);
            f.ShowDialog();
            Show();
            LoadListBookRoom();
        }

        private void btnClose__Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txbDays_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void cbRoomType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadRoomTypeInfo((cbRoomType.SelectedItem as LoaiPhong).Id);
        }

        private void dpkDateCheckIn_onValueChanged(object sender, EventArgs e)
        {
            if (dpkDateCheckIn.Value <= DateTime.Now)
                LoadDate();
            if (dpkDateCheckOut.Value <= dpkDateCheckIn.Value)
                LoadDate();
            LoadDays();
        }

        private void dpkDateCheckOut_onValueChanged(object sender, EventArgs e)
        {
            if (dpkDateCheckOut.Value < DateTime.Now)
                LoadDate();
            if (dpkDateCheckOut.Value <= dpkDateCheckIn.Value)
                LoadDate();
            LoadDays();
        }

        private void txbIDCardSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
            if (e.KeyChar == 13)
                btnSearch_Click(sender, null);
        }

        private void txbIDCard_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txbIDCardSearch.Text != String.Empty)
            {
                if (IsIdCardExists(txbIDCardSearch.Text))
                    GetInfoByIdCard(txbIDCardSearch.Text);
                else
                    MessageBox.Show("Thẻ căn cước/ CMND không tồn tại.\nVui lòng nhập lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void ClearData()
        {
            txbIDCardSearch.Text = txbIDCard.Text = txbFullName.Text = txbAddress.Text = txbPhoneNumber.Text = cbNationality.Text = String.Empty;
            LoadDate();
        }

        private void btnBookRoom_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn đặt phòng không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (txbIDCard.Text != String.Empty && txbFullName.Text != String.Empty && txbAddress.Text != String.Empty && txbPhoneNumber.Text != String.Empty && cbNationality.Text != String.Empty)
                {
                    if (!IsIdCardExists(txbIDCard.Text))
                    {
                        int idCustomerType = (cbCustomerType.SelectedItem as LoaiKhachHang).Id;
                        InsertCustomer(txbIDCard.Text, txbFullName.Text, idCustomerType, dpkDateOfBirth.Value, txbAddress.Text, int.Parse(txbPhoneNumber.Text), cbSex.Text, cbNationality.Text);
                    }
                    InsertBookRoom(KhachHangDAO.Instance.GetInfoByIdCard(txbIDCard.Text).Id, (cbRoomType.SelectedItem as LoaiPhong).Id, dpkDateCheckIn.Value, dpkDateCheckOut.Value, DateTime.Now);
                    MessageBox.Show("Đặt phòng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearData();
                    LoadListBookRoom();
                    if (bunifuCheckbox1.Checked)
                    {
                        this.Hide();
                        frmNhanPhong fReceiveRoom = new frmNhanPhong(GetCurrentIDBookRoom(DateTime.Now.Date));
                        fReceiveRoom.ShowDialog();
                    }
                }
                else
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        private void txbPhoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        
    }
}
