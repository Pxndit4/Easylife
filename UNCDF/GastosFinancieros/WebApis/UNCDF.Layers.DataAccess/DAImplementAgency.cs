using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Data;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.DataAccess
{
    public class DAImplementAgency
    {
        public static int Insert(MImplementAgency ent)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                

                SqlCommand cmd = new SqlCommand("sp_ImplementAgency_Ins", con);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IImplementAgencyCode", SqlDbType.VarChar).Value = ent.ImplementAgencyCode;
                cmd.Parameters.Add("@IDescription", SqlDbType.VarChar).Value = ent.Description;
                cmd.Parameters.Add("@IShortDescription", SqlDbType.VarChar).Value = ent.ShortDescription;

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }

            return 0;
        }

        public static int InsertAll(List<MImplementAgency> entList)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                DataTable table = new DataTable();
                table.TableName = "TData";
                //table.Columns.Add("Id", typeof(int));
                table.Columns.Add("Campo1Int", typeof(int));
                table.Columns.Add("Campo2Int", typeof(int));
                table.Columns.Add("Campo3Int", typeof(int));
                table.Columns.Add("Campo4Int", typeof(int));
                table.Columns.Add("Campo5Int", typeof(int));
                table.Columns.Add("Campo6Int", typeof(int));
                table.Columns.Add("Campo7Varchar", typeof(string));
                table.Columns.Add("Campo8Varchar", typeof(string));
                table.Columns.Add("Campo9Varchar", typeof(string));
                table.Columns.Add("Campo11Varchar", typeof(string));
                table.Columns.Add("Campo12Varchar", typeof(string));
                table.Columns.Add("Campo13Varchar", typeof(string));

                foreach (var item in entList)
                {
                    DataRow row = table.NewRow();
                    row["Campo7Varchar"] = item.ImplementAgencyCode;
                    row["Campo8Varchar"] = item.Description;
                    row["Campo9Varchar"] = item.ShortDescription;
                    table.Rows.Add(row);
                }

                SqlCommand cmd = new SqlCommand("sp_ImplementAgency_InsAll", con);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;

                var parameter = cmd.CreateParameter();
                parameter.TypeName = "TData";
                parameter.Value = table;
                parameter.ParameterName = "@IList";
                cmd.Parameters.Add(parameter);

                
                //cmd.Parameters.Add("@IImplementAgencyCode", SqlDbType.VarChar).Value = ent.ImplementAgencyCode;
                //cmd.Parameters.Add("@IDescription", SqlDbType.VarChar).Value = ent.Description;
                //cmd.Parameters.Add("@IShortDescription", SqlDbType.VarChar).Value = ent.ShortDescription;

                
                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();
            }

            return 0;
        }

        public static List<MImplementAgency> List()
        {
            List<MImplementAgency> lisQuery = new List<MImplementAgency>();

            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ImplementAgency_Lis", con);
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MImplementAgency entRow = new MImplementAgency();
                            entRow.ImplementAgencyId = Convert.ToInt32(reader["ImplementAgencyId"]);
                            entRow.ImplementAgencyCode = Convert.ToString(reader["ImplementAgencyCode"]);
                            entRow.Description = Convert.ToString(reader["Description"]);
                            entRow.ShortDescription = Convert.ToString(reader["ShortDescription"]);
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
