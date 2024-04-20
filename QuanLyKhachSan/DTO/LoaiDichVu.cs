using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.DTO
{
    public class LoaiDichVu
    {
        private int id;
        private string name;
        public LoaiDichVu() { }
        public LoaiDichVu(DataRow dataRow)
        {
            Id = (int)dataRow["id"];
            Name = dataRow["name"].ToString();
        }
        public bool Equals(LoaiDichVu serviceTypePre)
        {
            return this.name == serviceTypePre.name;
        }
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
    }
}
