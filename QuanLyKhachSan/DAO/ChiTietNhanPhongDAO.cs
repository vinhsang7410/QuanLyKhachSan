using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.DAO
{
    public class ChiTietNhanPhongDAO
    {
        private static ChiTietNhanPhongDAO instance;
        private ChiTietNhanPhongDAO() { }
        public bool InsertReceiveRoomDetails(int idReceiveRoom, int idCustomerOther)
        {
            string query = "InsertReceiveRoomDetails @idReceiveRoom , @idCustomer";
            int count = DataProvider.Instance.ExecuteNoneQuery(query, new object[] { idReceiveRoom, idCustomerOther });
            return count > 0;
        }
        public bool DeleteReceiveRoomDetails(int idReceiveRoom, int idCustomerOther)
        {
            string query = "USP_DeleteReceiveRoomDetails @idReceiveRoom , @idCustomer";
            int count = DataProvider.Instance.ExecuteNoneQuery(query, new object[] { idReceiveRoom, idCustomerOther });
            return count > 0;
        }
        public static ChiTietNhanPhongDAO Instance
        {
            get { if (instance == null) instance = new ChiTietNhanPhongDAO(); return instance; }
            private set => instance = value;
        }
    }
}
