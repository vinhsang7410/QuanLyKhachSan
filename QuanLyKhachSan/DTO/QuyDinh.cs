using System.Data;

namespace QuanLyKhachSan.DTO
{
    public class QuyDinh
    {
        private string name;
        private double value;
        private string describe;
        public QuyDinh(string name, double value, string describe)
        {
            this.Name = name;
            this.Value = value;
            this.Describe = describe;
        }
        public QuyDinh(DataRow row)
        {
            this.Value = (double)row["Value"];
            this.Name = row["Name"] as string;
            this.Describe = row["Describe"] as string;
        }

        public bool Equals(QuyDinh parameterPre)
        {
            if (parameterPre == null)
                return false;
            if (this.value != parameterPre.value) return false;
            if (this.describe != parameterPre.describe) return false;
            return true;
        }

        public string Name { get => name; set => name = value; }
        public double Value { get => value; set => this.value = value; }
        public string Describe { get => describe; set => describe = value; }
    }
}
