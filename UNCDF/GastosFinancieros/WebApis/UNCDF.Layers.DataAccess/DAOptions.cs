using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.DataAccess
{
    public class DAOptions
    {
        public static List<MOptions> Lis(MOptions ent, ref int Val)
        {
            List<MOptions> lisQuery = new List<MOptions>();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_Options_Listar", con);
                    cmd.Parameters.Add("@IProfileId", SqlDbType.VarChar).Value = ent.ProfileId;
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    Val = 1;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MOptions entRow = new MOptions();
                            entRow.OptionId = Convert.ToInt32(reader["OptionId"]);
                            entRow.IdFather = Convert.ToInt32(reader["IdFather"]);
                            entRow.Action = Convert.ToInt32(reader["Action"]);
                            entRow.Title = Convert.ToString(reader["Title"]);
                            entRow.Link = Convert.ToString(reader["Link"]);
                            entRow.Orders = Convert.ToInt32(reader["Orders"]);
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


        public static List<MOptions> Sel(MOptions ent, ref int Val)
        {
            List<MOptions> lisQuery = new List<MOptions>();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ProfileOptions_Sel", con);
                    cmd.Parameters.Add("@IProfileId", SqlDbType.VarChar).Value = ent.ProfileId;
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    Val = 1;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MOptions entRow = new MOptions();
                            entRow.ProfileId = Convert.ToInt32(reader["ProfileId"]);
                            entRow.OptionId = Convert.ToInt32(reader["OptionId"]);
                            entRow.FlagActive = Convert.ToInt32(reader["FlagActive"]);
                            entRow.Title = Convert.ToString(reader["Title"]);
                            entRow.TitleSubModule = Convert.ToString(reader["TitleSubModule"]);
                            entRow.TitleModule = Convert.ToString(reader["TitleModule"]);
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
