using System;
using Microsoft.Data.SqlClient;
using UNCDF.Layers.Model;
using System.Data;
using System.Collections.Generic;

namespace UNCDF.Layers.DataAccess
{
    public class DALogLoad
    {
        public static int  Insert(MLogLoad ent, ref int Val)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    Val = 1;

                    SqlCommand cmd = new SqlCommand("sp_LogLoad_Ins", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@TypeParamId", SqlDbType.Int).Value = ent.TypeParamId;
                    cmd.Parameters.Add("@IUserId", SqlDbType.Int).Value = ent.UserId;
                    cmd.Parameters.Add("@ITotalCorrectRecords", SqlDbType.Int).Value = ent.TotalCorrectRecords;
                    cmd.Parameters.Add("@ITotalBadRecords", SqlDbType.Int).Value = ent.TotalBadRecords;
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

        public static List<MLogLoad> List(MLogLoad ent, ref int Val)
        {
            List<MLogLoad> lisQuery = new List<MLogLoad>();

            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_LogLoad_Lis", con);
                    cmd.Parameters.Add("@ITypeParamId", SqlDbType.Int).Value = ent.TypeParamId;
                    cmd.Parameters.Add("@IEndDate", SqlDbType.Int).Value = ent.EndDate;
                    cmd.Parameters.Add("@IStartDate", SqlDbType.Int).Value = ent.StartDate;
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MLogLoad entRow = new MLogLoad();
                            entRow.LogloadId = Convert.ToInt32(reader["LogloadId"]);
                            entRow.TypeParamId = Convert.ToInt32(reader["TypeParamId"]);
                            entRow.Code = Convert.ToString(reader["Code"]);
                            entRow.DescriptionParam = Convert.ToString(reader["Description"]);
                            entRow.LoadingDate = Convert.ToString(reader["LoadingDate"]);
                            
                            entRow.UserId = Convert.ToInt32(reader["UserId"]);
                            entRow.NameUser = Convert.ToString(reader["Name"]);
                            entRow.TotalCorrectRecords = Convert.ToInt32(reader["TotalCorrectRecords"]);
                            entRow.TotalBadRecords = Convert.ToInt32(reader["TotalBadRecords"]);
                            entRow.Total = Convert.ToInt32(reader["Total"]);

                            lisQuery.Add(entRow);
                        }
                    }
                    con.Close();
                }

                catch (Exception ex)
                {
                    lisQuery = new List<MLogLoad>();
                }
            }
            return lisQuery;
        }


    }
}
