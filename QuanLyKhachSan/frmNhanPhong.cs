﻿using QuanLyKhachSan.DAO;
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
    public partial class frmNhanPhong : Form
    {
        List<int> ListIDCustomer = new List<int>();
        int IDBookRoom = -1;
        DateTime dateCheckIn;
        public frmNhanPhong(int idBookRoom)
        {
            IDBookRoom = idBookRoom;
            InitializeComponent();
            LoadData();
            ShowBookRoomInfo(IDBookRoom);
        }

        public frmNhanPhong()
        {
            InitializeComponent();
            LoadData();
        }

        public void LoadData()
        {
            LoadListRoomType();
            LoadReceiveRoomInfo();
        }
        public void LoadListRoomType()
        {
            List<LoaiPhong> rooms = LoaiPhongDAO.Instance.LoadListRoomType();
            cbRoomType.DataSource = rooms;
            cbRoomType.DisplayMember = "Name";
        }
        public void LoadEmptyRoom(int idRoomType)
        {
            List<Phong> rooms = PhongDAO.Instance.LoadEmptyRoom(idRoomType);
            cbRoom.DataSource = rooms;
            cbRoom.DisplayMember = "Name";
        }
        public bool IsIDBookRoomExists(int idBookRoom)
        {
            return DatPhongDAO.Instance.IsIDBookRoomExists(idBookRoom);
        }
        public void ShowBookRoomInfo(int idBookRoom)
        {
            DataRow dataRow = DatPhongDAO.Instance.ShowBookRoomInfo(idBookRoom);
            txbFullName.Text = dataRow["FullName"].ToString();
            txbIDCard.Text = dataRow["IDCard"].ToString();
            txbRoomTypeName.Text = dataRow["RoomTypeName"].ToString();
            cbRoomType.Text = dataRow["RoomTypeName"].ToString();//*
            txbDateCheckIn.Text = dataRow["DateCheckIn"].ToString().Split(' ')[0];
            dateCheckIn = (DateTime)dataRow["DateCheckIn"];
            txbDateCheckOut.Text = dataRow["DateCheckOut"].ToString().Split(' ')[0];
            txbAmountPeople.Text = dataRow["LimitPerson"].ToString();
            txbPrice.Text = dataRow["Price"].ToString();
        }
        public bool InsertReceiveRoom(int idBookRoom, int idRoom)
        {
            return NhanPhongDAO.Instance.InsertReceiveRoom(idBookRoom, idRoom);
        }
        public bool InsertReceiveRoomDetails(int idReceiveRoom, int idCustomerOther)
        {
            return ChiTietNhanPhongDAO.Instance.InsertReceiveRoomDetails(idReceiveRoom, idCustomerOther);
        }
        public void LoadReceiveRoomInfo()
        {
            dataGridViewReceiveRoom.DataSource = NhanPhongDAO.Instance.LoadReceiveRoomInfo();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose__Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbRoomType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txbRoomTypeName.Text = (cbRoomType.SelectedItem as LoaiPhong).Name;
            LoadEmptyRoom((cbRoomType.SelectedItem as LoaiPhong).Id);
        }

        private void cbRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            txbRoomName.Text = cbRoom.Text;
        }

        private void txbIDBookRoom_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
            if (e.KeyChar == 13)
                btnSearch_Click(sender, null);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txbIDBookRoom.Text != string.Empty)
            {
                if (IsIDBookRoomExists(int.Parse(txbIDBookRoom.Text)))
                {
                    btnSearch.Tag = txbIDBookRoom.Text;
                    ShowBookRoomInfo(int.Parse(txbIDBookRoom.Text));
                }
                else
                    MessageBox.Show("Mã đặt phòng không tồn tại.\nVui lòng nhập lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txbIDBookRoom.Text = string.Empty;
            }
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            if (txbRoomName.Text != string.Empty && txbRoomTypeName.Text != string.Empty && txbFullName.Text != string.Empty && txbIDCard.Text != string.Empty && txbDateCheckIn.Text != string.Empty && txbDateCheckOut.Text != string.Empty && txbAmountPeople.Text != string.Empty && txbPrice.Text != string.Empty)
            {
                frmThemThongTinKhachHang fAddCustomerInfo = new frmThemThongTinKhachHang();
                fAddCustomerInfo.ShowDialog();
                this.Show();
            }
            else
                MessageBox.Show("Vui lòng nhập lại đầy đủ thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnReceiveRoom_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn nhận phòng không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (txbRoomName.Text != string.Empty && txbRoomTypeName.Text != string.Empty && txbFullName.Text != string.Empty && txbIDCard.Text != string.Empty && txbDateCheckIn.Text != string.Empty && txbDateCheckOut.Text != string.Empty && txbAmountPeople.Text != string.Empty && txbPrice.Text != string.Empty)
                {
                    if (dateCheckIn == DateTime.Now.Date)
                    {
                        int idBookRoom;
                        if (IDBookRoom != -1) idBookRoom = IDBookRoom;
                        else idBookRoom = int.Parse(btnSearch.Tag.ToString());
                        int idRoom = (cbRoom.SelectedItem as Phong).Id;
                        if (InsertReceiveRoom(idBookRoom, idRoom))
                        {
                            if (frmThemThongTinKhachHang.ListIdCustomer != null)
                            {
                                foreach (int item in frmThemThongTinKhachHang.ListIdCustomer)
                                {
                                    if (item != int.Parse(txbIDCard.Text))
                                        InsertReceiveRoomDetails(NhanPhongDAO.Instance.GetIDCurrent(), item);
                                }
                            }
                            MessageBox.Show("Nhận phòng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadEmptyRoom((cbRoomType.SelectedItem as LoaiPhong).Id);
                        }
                        else
                            MessageBox.Show("Tạo phiếu nhận phòng thất bại.\nVui lòng nhập lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        MessageBox.Show("Ngày nhận phòng không hợp lệ.\nVui lòng nhập lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ClearData();
                    LoadReceiveRoomInfo();
                }
                else
                    MessageBox.Show("Vui lòng nhập lại đầy đủ thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void ClearData()
        {
            txbFullName.Text = txbIDCard.Text = txbRoomTypeName.Text = txbDateCheckIn.Text = txbDateCheckOut.Text = txbAmountPeople.Text = txbPrice.Text = string.Empty;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            frmChiTietNhanPhong f = new frmChiTietNhanPhong((int)dataGridViewReceiveRoom.SelectedRows[0].Cells[0].Value);
            f.ShowDialog();
            Show();
            LoadReceiveRoomInfo();
        }
    }
}
