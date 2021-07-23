using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Data;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.DataAccess
{
    public class DADonorStripe
    {
        public static int Confirm(MDonorStripe ent, BaseRequest baseRequest)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_StripePayment_Confirm", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IPaymentId", SqlDbType.VarChar).Value = ent.PaymentId;
                    cmd.Parameters.Add("@IDonationId", SqlDbType.VarChar).Value = ent.DonationId;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    return 0;
                }

                catch (SqlException ex)
                {
                    return 2;
                }
            }
        }

        public static int Create(string PaymentId, string ClientSecret, Int32 donorId)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_StripePayment_Create", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IPaymentId", SqlDbType.VarChar).Value = PaymentId;
                    cmd.Parameters.Add("@IClientSecret", SqlDbType.VarChar).Value = ClientSecret;
                    cmd.Parameters.Add("@IdonorId", SqlDbType.VarChar).Value = donorId;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    return 0;
                }

                catch (SqlException ex)
                {
                    return 2;
                }
            }
        }

        public static int Cancel(string PaymentId)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_StripePayment_Cancel", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IPaymentId", SqlDbType.VarChar).Value = PaymentId;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    return 0;
                }

                catch (SqlException ex)
                {
                    return 2;
                }
            }
        }
    }
}
