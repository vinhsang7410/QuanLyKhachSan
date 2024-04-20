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
    public partial class frmThemLoaiDichVu : Form
    {
        public frmThemLoaiDichVu()
        {
            InitializeComponent();
        }

        private LoaiDichVu GetServiceTypeNow()
        {
            LoaiDichVu serviceType = new LoaiDichVu();
            serviceType.Name = txbName.Text.Trim();
            return serviceType;
        }
        private void InsertServiceType()
        {
            if (frmQuanLyKhachHang.CheckFillInText(new Control[] { txbName }))
            {
                try
                {
                    LoaiDichVu serviceTypeNow = GetServiceTypeNow();
                    if (LoaiDichVuDAO.Instance.InsertServiceType(serviceTypeNow))
                    {
                        MessageBox.Show("Thêm thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txbName.Text = string.Empty;
                    }
                    else
                        MessageBox.Show("Lỗi nhập dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch
                {
                    MessageBox.Show("Lỗi loại dịch vụ đã có", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void btnClose__Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn thêm mới loại dịch vụ không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (result == DialogResult.OK)
                InsertServiceType();
        }
    }
}
