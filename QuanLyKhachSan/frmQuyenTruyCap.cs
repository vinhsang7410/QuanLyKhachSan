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
    public partial class frmQuyenTruyCap : Form
    {
        private int idStaffType = -1;
        public frmQuyenTruyCap()
        {
            InitializeComponent();
            cbbStaffType.DisplayMember = "Name";
            LoadFullStaffType();
        }

        #region Load
        private void LoadFullStaffType()
        {
            cbbStaffType.DataSource = GetFullStaffType();
        }
        private void LoadAccess()
        {
            dataGridViewAccessNow.DataSource = GetFullAccessNow(idStaffType);
            dataGridViewAccessRest.DataSource = GetFullAccessRest(idStaffType);
        }
        #endregion

        #region Click

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {
            int count = dataGridViewAccessRest.SelectedRows.Count;
            for (int i = 0; i < count; i++)
            {
                int idJob = (int)dataGridViewAccessRest.SelectedRows[i].Cells[colIdRest.Name].Value;
                AccessInsert(idJob, idStaffType);
            }
            LoadAccess();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            int count = dataGridViewAccessNow.SelectedRows.Count;
            for (int i = 0; i < count; i++)
            {
                int idJob = (int)dataGridViewAccessNow.SelectedRows[i].Cells[colIdNow.Name].Value;
                AcccessDelete(idJob, idStaffType);
            }
            LoadAccess();
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            int count = dataGridViewAccessRest.RowCount;
            for (int i = 0; i < count; i++)
            {
                int idJob = (int)dataGridViewAccessRest.Rows[i].Cells[colIdRest.Name].Value;
                AccessInsert(idJob, idStaffType);
            }
            LoadAccess();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            int count = dataGridViewAccessNow.RowCount;
            for (int i = 0; i < count; i++)
            {
                int idJob = (int)dataGridViewAccessNow.Rows[i].Cells[colIdNow.Name].Value;
                AcccessDelete(idJob, idStaffType);
            }
            LoadAccess();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (cbbStaffType.SelectedIndex == -1) return;
            DialogResult result = MessageBox.Show("Bạn có muốn xoá loại nhân viên này không", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                if (LoaiTaiKhoanDAO.Instance.Delete(idStaffType))
                {
                    MessageBox.Show("Xoá thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Xoá thất bại, đã tồn tại nhân viên loại này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            LoadFullStaffType();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int index = cbbStaffType.SelectedIndex;
            new frmThemLoaiNhanVien(idStaffType, cbbStaffType.Text).ShowDialog();
            LoadFullStaffType();
            cbbStaffType.SelectedIndex = index;
        }

        private void btnInsertStaffType_Click(object sender, EventArgs e)
        {
            new frmThemLoaiNhanVien().ShowDialog();
            LoadFullStaffType();
            cbbStaffType.SelectedIndex = (cbbStaffType.DataSource as DataTable).Rows.Count - 1;
        }
        #endregion

        #region Get Data
        private DataTable GetFullStaffType()
        {
            return LoaiTaiKhoanDAO.Instance.LoadFullStaffType();
        }
        private DataTable GetFullAccessNow(int idStaffType)
        {
            return QuyenTruyCapDAO.Instance.GetFullAccessNow(idStaffType);
        }
        private DataTable GetFullAccessRest(int idStaffType)
        {
            return QuyenTruyCapDAO.Instance.GetFullAccessRest(idStaffType);
        }

        #endregion

        #region Change StaffType
        private void cbbStaffType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cbbStaffType.SelectedIndex;
            idStaffType = (int)((DataTable)cbbStaffType.DataSource).Rows[index]["id"];
            LoadAccess();
        }
        #endregion

        #region Method

        private void AccessInsert(int idJob, int idStaffType)
        {
            QuyenTruyCapDAO.Instance.Insert(idJob, idStaffType);
        }
        private void AcccessDelete(int idJob, int idStaffType)
        {
            QuyenTruyCapDAO.Instance.Delete(idJob, idStaffType);
        }
        #endregion

        private void frmQuyenTruyCap_Load(object sender, EventArgs e)
        {

        }
    }
}
