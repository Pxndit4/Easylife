using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Data;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.DataAccess
{
    public class DADonation
    {
       
        public static List<MDonation> Lis(MDonation ent, ref int Val)
        {
            List<MDonation> lisQuery = new List<MDonation>();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_Donation_Lis", con);
                    cmd.Parameters.Add("@IDonorId", SqlDbType.Int).Value = ent.DonorId;
                    cmd.Parameters.Add("@IFirstName", SqlDbType.VarChar).Value = ent.FirstName;
                    cmd.Parameters.Add("@ILastName", SqlDbType.VarChar).Value = ent.LastName;
                    cmd.Parameters.Add("@IStartDate", SqlDbType.Decimal).Value = ent.StartDate;
                    cmd.Parameters.Add("@IEndDate", SqlDbType.Decimal).Value = ent.EndDate;
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MDonation entRow = new MDonation();
                            entRow.DonationId = Convert.ToInt32(reader["DonationId"]);
                            entRow.FirstName = Convert.ToString(reader["FirstName"]);
                            entRow.LastName = Convert.ToString(reader["LastName"]);
                            entRow.Amount = Convert.ToDecimal(reader["Amount"]);
                            entRow.Date = Convert.ToInt32(reader["Date"]);
                            lisQuery.Add(entRow);

                            Val = 0;
                        }
                    }
                    con.Close();
                }

                catch (Exception ex)
                {
                    Val = 2;
                }
            }
            return lisQuery;
        }

    }
}
