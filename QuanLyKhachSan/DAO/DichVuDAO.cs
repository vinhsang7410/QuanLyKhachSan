using QuanLyKhachSan.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.DAO
{
    public class DichVuDAO
    {
        private static DichVuDAO instance;
        public List<DichVu> GetServices(int idServiceType)
        {
            List<DichVu> services = new List<DichVu>();
            string query = "USP_LoadServiceByServiceType @idServiceType";
            DataTable dataTable = DataProvider.Instance.ExecuteQuery(query, new object[] { idServiceType });
            foreach (DataRow item in dataTable.Rows)
            {
                DichVu service = new DichVu(item);
                services.Add(service);
            }
            return services;
        }
        internal bool InsertService(string name, int idtype, int price)
        {
            string query = "USP_InsertService @name , @idServiceType , @price";
            return DataProvider.Instance.ExecuteNoneQuery(query, new object[]
            {
                name, idtype, price
            }) > 0;
        }
        internal bool InsertService(DichVu serviceNow)
        {
            return InsertService(serviceNow.Name, serviceNow.IdServiceType, serviceNow.Price);
        }
        internal bool UpdateService(int id, string name, int idServiceType, int price)
        {
            string query = "USP_UpdateService @id , @name , @idServiceType , @price";
            return DataProvider.Instance.ExecuteNoneQuery(query, new object[] { id, name, idServiceType, price }) > 0;
        }
        internal bool UpdateService(DichVu serviceNow, DichVu servicePre)
        {
            return UpdateService(serviceNow.Id, serviceNow.Name, serviceNow.IdServiceType, serviceNow.Price);
        }
        internal DataTable LoadFullService()
        {
            string query = "USP_LoadFullService";
            return DataProvider.Instance.ExecuteQuery(query);
        }
        internal DataTable Search(string name, int id)
        {
            string query = "USP_SearchService @string , @int";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { name, id });
        }
        public static DichVuDAO Instance
        {
            get { if (instance == null) instance = new DichVuDAO(); return instance; }
            private set => instance = value;
        }
    }
}
