using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Data;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.DataAccess
{
    public class DASubscribers
    {
        public static int Insert(MSubscribers ent, ref int Val)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;

                    SqlCommand cmd = new SqlCommand("sp_Subscribers_Ins", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IEmail", SqlDbType.VarChar).Value = ent.Email;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    Val = 0;

                    return 1;
                }

                catch (Exception ex)
                {
                    Val = 2;

                    return 0;

                }
            }
        }
    }
}
