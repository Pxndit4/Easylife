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

        public static MDonation Select(MDonation ent, ref int Val)
        {
            MDonation entRow = new MDonation();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_Donation_Sel", con);
                    cmd.Parameters.Add("@IDonationId", SqlDbType.Int).Value = ent.DonationId;
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    Val = 1;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            entRow.DonationId = Convert.ToInt32(reader["DonationId"]);
                            entRow.DonorId = Convert.ToInt32(reader["DonorId"]);
                            entRow.Date = Convert.ToDecimal(reader["Date"]);
                            entRow.PaymentType = Convert.ToString(reader["PaymentType"]);
                            entRow.Amount = Convert.ToDecimal(reader["Amount"]);
                            entRow.Code = Convert.ToString(reader["Code"]);
                            entRow.Status = Convert.ToInt32(reader["Status"]);
                            entRow.Certificate = Convert.ToString(reader["Certificate"]);
                            entRow.Email = Convert.ToString(reader["Email"]);
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
            return entRow;
        }

        public static int Insert(MDonation ent)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_Donation_Ins", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IDonorId", SqlDbType.Int).Value = ent.DonorId;
                    cmd.Parameters.Add("@IDate", SqlDbType.VarChar).Value = ent.Date;
                    cmd.Parameters.Add("@IAmount", SqlDbType.Decimal).Value = ent.Amount;
                    cmd.Parameters.Add("@IPaymentType", SqlDbType.VarChar).Value = ent.PaymentType;
                    cmd.Parameters.Add("@IProjectId", SqlDbType.Int).Value = ent.ProjectId;
                    cmd.Parameters.Add("@OCodeVar", SqlDbType.VarChar, 20).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@ODonationIdInt", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    ent.DonationId = Convert.ToInt32(cmd.Parameters["@ODonationIdInt"].Value);
                    ent.Code = cmd.Parameters["@OCodeVar"].Value.ToString();

                    con.Close();

                    return 0;
                }
                catch (Exception ex)
                {
                    return 2;
                }
            }
        }

        public static int Update(MDonation ent, BaseRequest baseRequest)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_Donation_Upd", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IDonationId", SqlDbType.Int).Value = ent.DonationId;
                    cmd.Parameters.Add("@ICertificate", SqlDbType.VarChar).Value = ent.Certificate;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();

                    con.Close();

                    return 0;
                }
                catch (Exception ex)
                {
                    return 2;
                }
            }
        }

        public static List<MDonation> Lis(MDonation ent, ref int Val)
        {
            List<MDonation> lisQuery = new List<MDonation>();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_Donation_Lis", con);
                    //cmd.Parameters.Add("@IDonorId", SqlDbType.Int).Value = ent.DonorId;
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

        public static List<decimal> GetTotals()
        {
            List<decimal> totales = new List<decimal>();

            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("sp_Donation_GetTotals", con);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        totales.Add(Convert.ToDecimal(reader["Total"]));
                        totales.Add(Convert.ToDecimal(reader["Comidas"]));
                    }
                }
                con.Close();
            }

            return totales;
        }
    }
}
