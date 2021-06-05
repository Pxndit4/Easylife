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
        public static int Insert(MDonorPartner ent)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {

                SqlCommand cmd = new SqlCommand("sp_DonorPartner_Ins", con);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IDonorCode", SqlDbType.VarChar).Value = ent.DonorCode;
                cmd.Parameters.Add("@IDonorName", SqlDbType.VarChar).Value = ent.DonorName;
                cmd.Parameters.Add("@IFundingPartner", SqlDbType.VarChar).Value = ent.FundingPartner;

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }

            return 0;
        }

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
