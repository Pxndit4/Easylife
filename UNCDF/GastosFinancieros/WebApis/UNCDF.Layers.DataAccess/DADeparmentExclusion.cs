using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Data;
using UNCDF.Layers.Model;


namespace UNCDF.Layers.DataAccess
{
    public class DADeparmentExclusion
    {
        public static int Insert(MDeparmentExclusion ent)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("sp_DeparmentExclusion_Ins", con);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IDeparmentCode", SqlDbType.VarChar).Value = ent.DeparmentCode;

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }

            return 0;
        }
        public static int Delete(MDeparmentExclusion ent, ref int Val)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;

                    SqlCommand cmd = new SqlCommand("sp_DeparmentExclusion_Del", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IDeparmentCode", SqlDbType.VarChar).Value = ent.DeparmentCode;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    Val = 0;

                    return 1;//succes
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
