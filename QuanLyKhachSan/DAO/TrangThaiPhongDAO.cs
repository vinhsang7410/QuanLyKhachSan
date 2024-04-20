using QuanLyKhachSan.DTO;
using System.Data;

namespace QuanLyKhachSan.DAO
{
    public class TrangThaiPhongDAO
    {
        #region Properties & Constructor
        private static TrangThaiPhongDAO instance;
        private TrangThaiPhongDAO() { }
        public static TrangThaiPhongDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new TrangThaiPhongDAO();
                return instance;
            }
            private set => instance = value;
        }

        #endregion

        #region Method
        internal bool UpdateStatusRoom(int id, string name)
        {
            string query = "exec USP_UpdateStatusRoom @id , @name";
            return DataProvider.Instance.ExecuteNoneQuery(query, new object[] { id, name }) > 0;
        }
        internal bool UpdateStatusRoom(TrangThaiPhong statusRoomNow)
        {
            return UpdateStatusRoom(statusRoomNow.Id, statusRoomNow.Name);
        }
        internal DataTable LoadFullStatusRoom()
        {
            return DataProvider.Instance.ExecuteQuery("USP_LoadFullStatusRoom");
        }
        #endregion
    }
}
