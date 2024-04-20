using QuanLyKhachSan.DTO;
using System.Data;

namespace QuanLyKhachSan.DAO
{
    public class LoaiTaiKhoanDAO
    {
        private static LoaiTaiKhoanDAO instance;

        #region Method
        internal DataTable LoadFullStaffType()
        {
            string query = "USP_LoadFullStaffType";
            return DataProvider.Instance.ExecuteQuery(query);
        }
        public LoaiTaiKhoan GetStaffTypeByUserName(string username)
        {
            string query = "USP_GetNameStaffTypeByUserName @username";
            LoaiTaiKhoan staffType = new LoaiTaiKhoan(DataProvider.Instance.ExecuteQuery(query, new object[] { username }).Rows[0]);
            return staffType;
        }
        internal bool Delete(int idStaffType)
        {
            string query = "USP_DeleteStaffType @id";
            return DataProvider.Instance.ExecuteNoneQuery(query, new object[] { idStaffType }) > 0;
        }
        internal bool Update(int idStaffType, string text)
        {
            string query = "USP_UpdateStaffType @id , @name";
            return DataProvider.Instance.ExecuteNoneQuery(query, new object[] { idStaffType, text }) > 0;
        }

        internal bool Insert(string text)
        {
            string query = "USP_InsertStaffType @name";
            return DataProvider.Instance.ExecuteNoneQuery(query, new object[] { text }) > 0;
        }
        #endregion

        public static LoaiTaiKhoanDAO Instance
        {
            get { if (instance == null) instance = new LoaiTaiKhoanDAO(); return instance; }
            private set => instance = value;
        }
    }
}
