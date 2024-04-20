using QuanLyKhachSan.DTO;
using System.Collections.Generic;
using System.Data;

namespace QuanLyKhachSan.DAO
{
    public class LoaiKhachHangDAO
    {
        private static LoaiKhachHangDAO instance;

        #region Method
        private LoaiKhachHangDAO() { }
        public List<LoaiKhachHang> LoadListCustomerType()
        {
            string query = "select * from CustomerType";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<LoaiKhachHang> listCustomerType = new List<LoaiKhachHang>();
            foreach (DataRow item in data.Rows)
            {
                LoaiKhachHang CustomerType = new LoaiKhachHang(item);
                listCustomerType.Add(CustomerType);
            }
            return listCustomerType;
        }
        public string GetNameByIdCard(string idCard)
        {
            string query = "USP_GetCustomerTypeNameByIdCard @idCard";
            DataRow dataRow = DataProvider.Instance.ExecuteQuery(query, new object[] { idCard }).Rows[0];
            return dataRow["Name"].ToString();
        }
        internal bool UpdateCustomerType(LoaiKhachHang customerTypeNow)
        {
            string query = "USP_UpdateCustomerType @id , @name";
            return DataProvider.Instance.ExecuteNoneQuery(query, new object[] { customerTypeNow.Id, customerTypeNow.Name }) > 0;
        }

        internal DataTable LoadFullCustomerType()
        {
            return DataProvider.Instance.ExecuteQuery("USP_LoadFullCustomerType");
        }
        #endregion
        public static LoaiKhachHangDAO Instance
        {
            get { if (instance == null) instance = new LoaiKhachHangDAO(); return instance; }
            private set => instance = value;
        }
    }
}
