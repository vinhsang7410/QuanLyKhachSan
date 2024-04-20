using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.DTO
{
    public class ChiTietNhanPhong
    {
        private int idReceiveRoom;
        private int idCustomerOther;
        public ChiTietNhanPhong(DataRow row)
        {
            IdReceiveRoom = (int)row["idReceiveRoom"];
            IdCustomerOther = (int)row["idCustomerOther"];
        }
        public int IdReceiveRoom { get => idReceiveRoom; set => idReceiveRoom = value; }
        public int IdCustomerOther { get => idCustomerOther; set => idCustomerOther = value; }
    }
}
