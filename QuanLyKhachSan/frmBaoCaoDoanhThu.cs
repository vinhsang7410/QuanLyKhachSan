using QuanLyKhachSan.DAO;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace QuanLyKhachSan
{
    public partial class frmBaoCaoDoanhThu : Form
    {
        private int month = 1;
        private int year = 1990;
        public frmBaoCaoDoanhThu()
        {
            InitializeComponent();
            dataGridReport.Font = new System.Drawing.Font("Segoe UI", 9.75F);
        }

        #region Load
        private void LoadFullReport(int month, int year)
        {
            this.month = month;
            this.year = year;
            DataTable table = GetFulReport(month, year);
            BindingSource source = new BindingSource();
            ChangePrice(table);
            source.DataSource = table;
            dataGridReport.DataSource = source;
            bindingReport.BindingSource = source;
            DrawChart(source);
            GC.Collect();
        }
        #endregion

        #region Click
        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadFullReport(int.Parse(comboBoxMonth.Text), (int)(numericYear.Value));
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            saveReport.FileName = "Doanh thu tháng " + month + '-' + year;
            if (saveReport.ShowDialog() == DialogResult.Cancel)
                return;
            else
            {
                bool check;
                try
                {
                    switch (saveReport.FilterIndex)
                    {
                        case 2:
                            check = ExportToExcel.Instance.Export(dataGridReport, saveReport.FileName, ModeExportToExcel.XLSX);
                            break;
                        case 3:
                            check = ExportToExcel.Instance.Export(dataGridReport, saveReport.FileName, ModeExportToExcel.PDF);
                            break;
                        default:
                            check = ExportToExcel.Instance.Export(dataGridReport, saveReport.FileName, ModeExportToExcel.XLS);
                            break;
                    }
                    if (check)
                        MessageBox.Show("Xuất thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Lỗi xuất thất bại", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch
                {
                    MessageBox.Show("Lỗi (Cần cài đặt Office)", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        #region Data
        private DataTable GetFulReport(int month, int year)
        {
            return BaoCaoDAO.Instance.LoadFullReport(month, year);
        }
        #endregion

        #region Chart
        private void DrawChart(BindingSource source)
        {
            chartReport.DataSource = source;
            chartReport.DataBind();
            foreach (DataPoint item in chartReport.Series[0].Points)
            {
                if (item.YValues[0] == 0)
                {
                    item.Label = " ";
                }
            }
        }
        #endregion

        #region Change Price
        private void ChangePrice(DataTable table)
        {
            table.Columns.Add("value_New", typeof(string));
            table.Columns.Add("rate_New", typeof(string));
            int sum = 0;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                int node = ((int)table.Rows[i]["value"]);
                table.Rows[i]["value_New"] = node.ToString("C0", CultureInfo.CreateSpecificCulture("vi-VN"));
                table.Rows[i]["rate_New"] = (((double)table.Rows[i]["rate"]) / 100).ToString("#0.##%");
                sum += node;
            }
            table.Columns.Remove("value");
            DataRow row = table.NewRow();
            table.Columns["value_new"].ColumnName = "value";
            row["value"] = sum.ToString("C0", CultureInfo.CreateSpecificCulture("vi-VN"));
            table.Rows.Add(row);
        }
        #endregion

        #region Form
        private void FReport_Load(object sender, EventArgs e)
        {
            LoadFullReport(DateTime.Now.Month, DateTime.Now.Year);
            comboBoxMonth.Text = DateTime.Now.Month.ToString();
            numericYear.Value = DateTime.Now.Year;
        }
        #endregion

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
