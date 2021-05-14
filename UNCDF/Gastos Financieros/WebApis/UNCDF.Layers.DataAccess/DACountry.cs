using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using UNCDF.Layers.Models;

namespace UNCDF.Layers.DataAccess
{
    public class DACountry
    {
        public static MCountry Select(MCountry ent, ref int Val)
        {
            MCountry result = new MCountry();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_Country_Sel", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@ICountryId", SqlDbType.Int).Value = ent.CountryId;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    Val = 1;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.CountryId = Convert.ToInt32(reader["CountryId"]);
                            result.ContinentId = Convert.ToInt32(reader["ContinentId"]);
                            result.Description = Convert.ToString(reader["Description"]);
                            result.Flag = Convert.ToString(reader["Flag"]);
                            result.Status = Convert.ToInt32(reader["Status"]);
                            result.Prefix = Convert.ToString(reader["Prefix"]);

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
            return result;
        }
    }
}
