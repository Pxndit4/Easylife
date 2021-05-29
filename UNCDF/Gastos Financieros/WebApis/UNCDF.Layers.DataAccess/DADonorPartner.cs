using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Data;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.DataAccess
{
    public class DADonorPartner
    {
        public static List<MDonorPartner> List()
        {
            List<MDonorPartner> lisQuery = new List<MDonorPartner>();

            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_DonorPartner_Lis", con);
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MDonorPartner entRow = new MDonorPartner();
                            entRow.DonorPartnerId = Convert.ToInt32(reader["DonorPartnerId"]);
                            entRow.DonorCode = Convert.ToString(reader["DonorCode"]);
                            entRow.DonorName = Convert.ToString(reader["DonorName"]);
                            lisQuery.Add(entRow);
                        }
                    }

                    con.Close();
                }
                catch (Exception ex)
                {
                    lisQuery = null;
                }
            }

            return lisQuery;
        }
    }
}
