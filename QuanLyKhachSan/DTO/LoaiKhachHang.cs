using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.DTO
{
    public class LoaiKhachHang
    {
        private int id;
        private string name;
        public LoaiKhachHang(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public LoaiKhachHang(DataRow row)
        {
            Id = (int)row["id"];
            Name = row["Name"].ToString();
        }
        public override bool Equals(object obj)
        {
            return Equals(obj as LoaiKhachHang);
        }
        public bool Equals(LoaiKhachHang customerTypePre)
        {
            if (customerTypePre == null) return false;
            return (this.name == customerTypePre.name);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
    }
}
