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
    public partial class frmDoiPhong : Form
    {
        int idRoom, idReceiveRoom;
        public frmDoiPhong(int _idRoom, int _idReceiveRoom)
        {
            InitializeComponent();
            idRoom = _idRoom;
            idReceiveRoom = _idReceiveRoom;
            LoadListRoomType();
            LoadRoomTypeInfo(_idRoom);
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
        public void LoadRoomTypeInfo(int idRoom)
        {
            CultureInfo cultureInfo = new CultureInfo("vi-vn");
            LoaiPhong roomType = LoaiPhongDAO.Instance.GetRoomTypeByIdRoom(idRoom);
            txbLimitPerson.Text = roomType.LimitPerson.ToString();
            txbPrice.Text = roomType.Price.ToString("c", cultureInfo);
            txbRoomTypeName.Text = roomType.Name;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cbRoomType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txbRoomTypeName.Text = (cbRoomType.SelectedItem as LoaiPhong).Name;
            LoadEmptyRoom((cbRoomType.SelectedItem as LoaiPhong).Id);
            LoadRoomTypeInfo((cbRoom.SelectedItem as Phong).Id);
        }

        private void cbRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            txbRoomName.Text = (cbRoom.SelectedItem as Phong).Name;
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            //Phải cập nhật trạng thái của phòng cũ
            PhongDAO.Instance.UpdateStatusRoom(idRoom);
            NhanPhongDAO.Instance.UpdateReceiveRoom(idReceiveRoom, (cbRoom.SelectedItem as Phong).Id);
            MessageBox.Show("Đổi phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnClose__Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
