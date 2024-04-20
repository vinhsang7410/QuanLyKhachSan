using System.Data;

namespace QuanLyKhachSan.DAO
{
    public class BaoCaoDAO
    {
        #region Constructor & properties
        private static BaoCaoDAO instance;
        public static BaoCaoDAO Instance { get { if (instance == null) instance = new BaoCaoDAO(); return instance; } }
        private BaoCaoDAO()
        {

        }
        #endregion

        #region Method
        public DataTable LoadFullReport(int month, int year)
        {
            string query = "USP_LoadFullReport @month , @year";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { month, year });
        }

        public bool InsertReport(int idBill)
        {
            return DataProvider.Instance.ExecuteNoneQuery("USP_InsertReport @idBill", new object[] { idBill }) > 0;
        }
        #endregion
    }
}
