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
    public partial class frmThemPhong : Form
    {
        public frmThemPhong()
        {
            InitializeComponent();
            LoadFullRoomType();
        }

        private void LoadFullRoomType()
        {
            DataTable table = GetFullRoomType();
            ChangePrice(table);
            comboBoxRoomType.DataSource = table;
            comboBoxRoomType.DisplayMember = "Name";
            if (table.Rows.Count > 0)
                comboBoxRoomType.SelectedIndex = 0;
            txbPrice.DataBindings.Add("Text", comboBoxRoomType.DataSource, "price_New");
            txbLimitPerson.DataBindings.Add(new Binding("Text", comboBoxRoomType.DataSource, "limitPerson"));
        }
        private DataTable GetFullRoomType()
        {
            return LoaiPhongDAO.Instance.LoadFullRoomType();
        }
        private Phong GetRoomNow()
        {
            Phong room = new Phong();
            frmQuanLyNhanVien.Trim(new Bunifu.Framework.UI.BunifuMetroTextbox[] { txbNameRoom });
            room.Name = txbNameRoom.Text;
            int index = comboBoxRoomType.SelectedIndex;
            room.IdStatusRoom = 1;
            room.IdRoomType = (int)((DataTable)comboBoxRoomType.DataSource).Rows[index]["id"];
            return room;
        }
        private void InsertRoom()
        {
            if (!frmQuanLyKhachHang.CheckFillInText(new Control[] { txbNameRoom, comboBoxRoomType }))
            {
                MessageBox.Show("Không được để trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                Phong roomNow = GetRoomNow();
                if (PhongDAO.Instance.InsertRoom(roomNow))
                {
                    txbNameRoom.Text = string.Empty;
                    MessageBox.Show("Thêm Thành Công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Phòng này đã tồn tại(Trùng mã số phòng)", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            catch
            {
                MessageBox.Show("Lỗi không thêm được phòng này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChangePrice(DataTable table)
        {
            table.Columns.Add("price_New", typeof(string));
            for (int i = 0; i < table.Rows.Count; i++)
            {
                table.Rows[i]["price_New"] = ((int)table.Rows[i]["price"]).ToString("C0", CultureInfo.CreateSpecificCulture("vi-VN"));
            }
            table.Columns.Remove("price");
        }

        private void btnClose__Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn thêm phòng mới?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (result == DialogResult.OK)
                InsertRoom();
        }
    }
}
