using QuanLyKhachSan.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.DAO
{
    public class PhongDAO
    {
        private static PhongDAO instance;
        #region Method

        internal DataTable LoadFullRoom()
        {
            return DataProvider.Instance.ExecuteQuery("USP_LoadFullRoom");
        }
        internal bool InsertRoom(Phong roomNow)
        {
            return InsertRoom(roomNow.Name, roomNow.IdRoomType, roomNow.IdStatusRoom);
        }
        internal bool InsertRoom(string roomName, int idRoomType, int idStatusRoom)
        {
            string query = "USP_InsertRoom @nameRoom , @idRoomType , @idStatusRoom";
            return DataProvider.Instance.ExecuteNoneQuery(query, new object[] { roomName, idRoomType, idStatusRoom }) > 0;
        }
        internal bool UpdateCustomer(Phong roomNow)
        {
            string query = "USP_UpdateRoom  @id , @nameRoom , @idRoomType , @idStatusRoom";
            return DataProvider.Instance.ExecuteNoneQuery(query, new object[] { roomNow.Id, roomNow.Name, roomNow.IdRoomType, roomNow.IdStatusRoom }) > 0;
        }
        internal DataTable Search(string text, int id)
        {
            string query = "USP_SearchRoom @string , @id";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { text, id });
        }
        public List<Phong> LoadEmptyRoom(int idRoomType)
        {
            List<Phong> rooms = new List<Phong>();
            string query = "USP_LoadEmptyRoom @idRoomType";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { idRoomType });
            foreach (DataRow item in data.Rows)
            {
                Phong room = new Phong(item);
                rooms.Add(room);
            }
            return rooms;
        }
        public List<Phong> LoadListFullRoom()
        {
            string query = "USP_LoadListFullRoom @getToday";
            List<Phong> rooms = new List<Phong>();
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { DateTime.Now.Date });
            foreach (DataRow item in data.Rows)
            {
                Phong room = new Phong(item);
                rooms.Add(room);
            }
            return rooms;
        }
        public int GetPeoples(int idBill)
        {
            string query = "USP_GetPeoples @idBill";
            return (int)DataProvider.Instance.ExecuteScalar(query, new object[] { idBill }) + 1;
        }
        public int GetIdRoomFromReceiveRoom(int idReceiveRoom)
        {
            string query = "USP_GetIDRoomFromReceiveRoom @idReceiveRoom";
            return (int)DataProvider.Instance.ExecuteScalar(query, new object[] { idReceiveRoom });
        }
        public bool UpdateStatusRoom(int idRoom)
        {
            string query = "USP_UpdateStatusRoom @idRoom";
            return DataProvider.Instance.ExecuteNoneQuery(query, new object[] { idRoom }) > 0;
        }
        #endregion

        public static PhongDAO Instance
        {
            get { if (instance == null) instance = new PhongDAO(); return instance; }
            private set => instance = value;
        }
        private PhongDAO() { }
    }
}
