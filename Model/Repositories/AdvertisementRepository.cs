using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Model.PocoEntity;
using Model.DataInterfaces;

namespace Model.Repositories
{
    public class AdvertisementRepository: IAdvertisementRepository
    {
        string connectionString = ConfigurationManager.ConnectionStrings["AdvConnection"].ConnectionString;

        public List<Advertisement> GetAll()
        {
            List<Advertisement> result = new List<Advertisement>();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                result = db.Query<Advertisement>("Select Id, [Description], MaintenanceTime, [Lt] = Location.Lat, [Ln] = Location.Long, [Type], Height, Width, MonthlyCost " +
                    "FROM Advertisement").ToList();
            }
            return result;
        }        
             
        public Advertisement Add(Advertisement adv)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                int advId = db.Query<int>("AdvInsert", adv, commandType: CommandType.StoredProcedure).FirstOrDefault();
                adv.Id = advId;
            }
            return adv;
        }

        public  void Update(Advertisement adv)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                db.Execute("AdvUpdate", adv, commandType: CommandType.StoredProcedure);
            }
        }

        public void Remove(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Advertisement WHERE Id = @id";
                db.Execute(sqlQuery, new { id });
            }
        }
    }
}
