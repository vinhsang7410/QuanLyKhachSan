using QuanLyKhachSan.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.DAO
{
    public class LoaiDichVuDAO
    {
        private static LoaiDichVuDAO instance;

        #region  Method
        internal bool InsertServiceType(string name)
        {
            string query = "USP_InsertServiceType @name";
            return DataProvider.Instance.ExecuteNoneQuery(query, new object[] { name }) > 0;
        }
        internal bool InsertServiceType(LoaiDichVu serviceTypeNow)
        {
            return InsertServiceType(serviceTypeNow.Name);
        }
        internal bool UpdateServiceType(int id, string name)
        {
            string query = "USP_UpdateServiceType @id , @name";
            return DataProvider.Instance.ExecuteNoneQuery(query, new object[] { id, name }) > 0;
        }
        internal bool UpdateServiceType(LoaiDichVu serviceTypeNow)
        {
            return UpdateServiceType(serviceTypeNow.Id, serviceTypeNow.Name);
        }
        internal DataTable LoadFullServiceType()
        {
            string query = "USP_LoadFullServiceType";
            return DataProvider.Instance.ExecuteQuery(query);
        }
        internal DataTable Search(string name, int id)
        {
            string query = "USP_SearchServiceType @string , @int";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { name, id });
        }

        public static LoaiDichVuDAO Instance
        {
            get { if (instance == null) instance = new LoaiDichVuDAO(); return instance; }
            private set => instance = value;
        }

        public List<LoaiDichVu> GetServiceTypes()
        {
            string query = "select * from ServiceType";
            List<LoaiDichVu> serviceTypes = new List<LoaiDichVu>();
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                LoaiDichVu serviceType = new LoaiDichVu(item);
                serviceTypes.Add(serviceType);
            }
            return serviceTypes;
        }
        #endregion
    }
}
