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
    public partial class frmThemThongTinKhachHang : Form
    {
        private static List<int> listIdCustomer;

        public static List<int> ListIdCustomer { get => listIdCustomer; set => listIdCustomer = value; }

        public frmThemThongTinKhachHang()
        {
            InitializeComponent();
            LoadCustomerType();
            ListIdCustomer = new List<int>();
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
        public void ClearData()
        {
            txbIDCardSearch.Text = txbIDCard.Text = txbFullName.Text = txbAddress.Text = txbPhoneNumber.Text = cbNationality.Text = String.Empty;
        }
        public void AddIdCustomer(int idCustomer)
        {
            if (ListIdCustomer.Count != 0)
            {
                bool check = false;
                foreach (int item in ListIdCustomer)
                {
                    if (item == idCustomer)
                    {
                        check = true;
                        break;
                    }
                }
                if (!check) ListIdCustomer.Add(idCustomer);
            }
            else
                ListIdCustomer.Add(idCustomer);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            if (txbFullName.Text != string.Empty && txbIDCard.Text != string.Empty && txbAddress.Text != string.Empty && cbNationality.Text != string.Empty && txbPhoneNumber.Text != string.Empty)
            {
                if (!IsIdCardExists(txbIDCard.Text))
                {
                    int idCustomerType = (cbCustomerType.SelectedItem as LoaiKhachHang).Id;
                    InsertCustomer(txbIDCard.Text, txbFullName.Text, idCustomerType, dpkDateOfBirth.Value, txbAddress.Text, int.Parse(txbPhoneNumber.Text), cbSex.Text, cbNationality.Text);
                }
                MessageBox.Show("Thêm khách hàng thành công.", "Thông báo.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                AddIdCustomer(KhachHangDAO.Instance.GetInfoByIdCard(txbIDCard.Text).Id);
                ClearData();
            }
            else
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnClose__Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            ClearData();
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

        private void txbIDCardSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txbIDCard_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txbPhoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }
    }
}
