using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Data;

namespace UNCDF.Layers.DataAccess
{
    public class DAAplication
    {
        public static int ValidateAplicationToken(string Token)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                int val = 0;
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_Aplication_Val", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IToken", SqlDbType.VarChar).Value = Token;
                    cmd.Parameters.Add("@OVal", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    val = Convert.ToInt32(cmd.Parameters["@OVal"].Value);
                    con.Close();
                }

                catch (Exception ex)
                {
                    val = 2;
                }

                return val;
            }
        }
    }
}
