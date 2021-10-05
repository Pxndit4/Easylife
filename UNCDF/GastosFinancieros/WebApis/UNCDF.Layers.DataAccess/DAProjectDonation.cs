using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.DataAccess
{
    public class DAProjectDonation
    {
        public static List<MProjectDonation> List(MDonor ent, BaseRequest baseRequest)
        {
            List<MProjectDonation> lisQuery = new List<MProjectDonation>();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ProjectDonation_Lis", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IDonorId", SqlDbType.VarChar).Value = ent.DonorId;
                    cmd.Parameters.Add("@ILanguage", SqlDbType.VarChar).Value = baseRequest.Language;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MProjectDonation entRow = new MProjectDonation();
                            entRow.DonationId = Convert.ToInt32(reader["DonationId"]);
                            entRow.Date = Convert.ToString(reader["Date"]);
                            entRow.Amount = Convert.ToDecimal(reader["Amount"]);
                            entRow.Certificate = Path.Combine(Constant.S3Server, "certificates") + "/" + Convert.ToString(reader["DonorCertificate"]);
                            entRow.Title = Convert.ToString(reader["Project"]);
                            lisQuery.Add(entRow);
                        }
                    }
                    con.Close();
                }

                catch (SqlException ex)
                {

                }
            }
            return lisQuery;
        }
    }
}
