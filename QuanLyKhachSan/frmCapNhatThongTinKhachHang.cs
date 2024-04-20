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
    public partial class frmCapNhatThongTinKhachHang : Form
    {
        string idCard;
        public frmCapNhatThongTinKhachHang(string _idCard)
        {
            InitializeComponent();
            idCard = _idCard;
            LoadCustomerType();
            LoadCustomerInfo(_idCard);
        }

        public void LoadCustomerType()
        {
            cbCustomerType.DataSource = LoaiKhachHangDAO.Instance.LoadListCustomerType();
            cbCustomerType.DisplayMember = "Name";
        }
        public void LoadCustomerInfo(string idCard)
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
        public void UpdateCustomer()
        {
            int idCustomerType = (cbCustomerType.SelectedItem as LoaiKhachHang).Id;
            KhachHangDAO.Instance.UpdateCustomer(KhachHangDAO.Instance.GetInfoByIdCard(idCard).Id, txbFullName.Text, txbIDCard.Text, idCustomerType, int.Parse(txbPhoneNumber.Text), dpkDateOfBirth.Value, txbAddress.Text, cbSex.Text, cbNationality.Text);
        }
        public void ClearData()
        {
            txbIDCard.Text = txbFullName.Text = txbAddress.Text = txbPhoneNumber.Text = cbNationality.Text = String.Empty;
        }
        public bool IsIdCardExists(string idCard)
        {
            return KhachHangDAO.Instance.IsIdCardExists(idCard);
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            if (txbFullName.Text != string.Empty && txbIDCard.Text != string.Empty && txbAddress.Text != string.Empty && cbNationality.Text != string.Empty && txbPhoneNumber.Text != string.Empty)
            {
                //Kiểm tra IDCard có trùng không
                if (!IsIdCardExists(txbIDCard.Text) || txbIDCard.Text == idCard)
                {
                    UpdateCustomer();
                    MessageBox.Show("Cập nhật thông tin khách hàng thành công.", "Thông báo.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearData();
                    LoadCustomerInfo(idCard);
                }
                else
                    MessageBox.Show("Thẻ căn cước/ CMND không hợp lệ.\nVui lòng nhập lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            ClearData();
        }

        private void txbPhoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }


    }
}
