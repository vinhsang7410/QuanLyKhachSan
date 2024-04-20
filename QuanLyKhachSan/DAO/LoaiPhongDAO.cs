using QuanLyKhachSan.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.DAO
{
    public class LoaiPhongDAO
    {
        private static LoaiPhongDAO instance;
        #region Method
        internal DataTable LoadFullRoomType()
        {
            return DataProvider.Instance.ExecuteQuery("USP_LoadFullRoomType");
        }
        internal bool InsertRoomType(string name, int price, int limitPerson)
        {
            string query = "USP_InsertRoomType @name , @price , @limitPerson";
            return DataProvider.Instance.ExecuteNoneQuery(query, new object[] { name, price, limitPerson }) > 0;

        }
        internal bool InsertRoomType(LoaiPhong roomTypeNow)
        {
            return InsertRoomType(roomTypeNow.Name, roomTypeNow.Price, roomTypeNow.LimitPerson);
        }
        internal bool UpdateRoomType(LoaiPhong roomNow, LoaiPhong roomPre)
        {
            string query = "USP_UpdateRoomType @id , @name , @price , @limitPerson";
            return DataProvider.Instance.ExecuteNoneQuery(query, new object[] { roomNow.Id, roomNow.Name, roomNow.Price, roomNow.LimitPerson }) > 0;
        }
        internal DataTable Search(string text, int id)
        {
            string query = "USP_SearchRoomType @string , @id";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { text, id });
        }
        internal int GetMaxPersonByRoomType(int idRoomType)
        {
            string query = "USP_GetMaxPersonByRoomType @idRoomType";
            DataRow data = DataProvider.Instance.ExecuteQuery(query, new object[] { idRoomType }).Rows[0];
            return Convert.ToInt32((double)data["Value"]);
        }
        public static LoaiPhongDAO Instance
        {
            get { if (instance == null) instance = new LoaiPhongDAO(); return instance; }
            private set => instance = value;
        }
        public LoaiPhongDAO() { }
        public LoaiPhong LoadRoomTypeInfo(int id)
        {
            string query = "USP_RoomTypeInfo @id";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { id });
            LoaiPhong roomType = new LoaiPhong(data.Rows[0]);
            return roomType;
        }
        public List<LoaiPhong> LoadListRoomType()
        {
            string query = "select * from RoomType";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            List<LoaiPhong> listRoomType = new List<LoaiPhong>();
            foreach (DataRow item in data.Rows)
            {
                LoaiPhong roomType = new LoaiPhong(item);
                listRoomType.Add(roomType);
            }
            return listRoomType;
        }
        public LoaiPhong GetRoomTypeByIdRoom(int idRoom)
        {
            string query = "USP_GetRoomTypeByIdRoom @idRoom";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { idRoom });
            LoaiPhong roomType = new LoaiPhong(data.Rows[0]);
            return roomType;
        }
        public LoaiPhong GetRoomTypeByIdBookRoom(int idBookRoom)
        {
            string query = "USP_GetRoomTypeByIdBookRoom @idBookRoom";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { idBookRoom });
            LoaiPhong roomType = new LoaiPhong(data.Rows[0]);
            return roomType;
        }
        #endregion
    }
}
