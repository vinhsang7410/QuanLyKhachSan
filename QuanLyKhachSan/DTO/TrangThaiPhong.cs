using System.Data;

namespace QuanLyKhachSan.DTO
{
    public class TrangThaiPhong
    {
        #region Properties
        private int id;
        private string name;
        public string Name { get => name; set => name = value; }
        public int Id { get => id; set => id = value; }
        #endregion

        #region Constructor
        public TrangThaiPhong() { }
        public TrangThaiPhong(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
        public TrangThaiPhong(DataRow row)
        {
            this.Id = (int)row["ID"];
            this.Name = row["Name"].ToString();
        }
        #endregion

        #region Method
        public bool Equals(TrangThaiPhong statusRoomPre)
        {
            return this.name == statusRoomPre.name;
        }
        #endregion
    }
}
