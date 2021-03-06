using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Data;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.DataAccess
{
    public class DAProject
    {
        public static List<int> TotalsProjects() {

            List<int> lisQuery = new List<int>();

            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
                {
                    SqlCommand cmd = new SqlCommand("sp_Project_Lis_Totals", con);
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lisQuery.Add(Convert.ToInt32(reader["Projects"]));
                            lisQuery.Add(Convert.ToInt32(reader["Countries"]));
                        }
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                return lisQuery;
            }

            return lisQuery;
        }

        public static List<String> YearLis(MProjectFinancials ent, ref int Val)
        {
            List<string> lisQuery = new List<string>();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ProjectFinancials_YearLis", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IProjectId", SqlDbType.Int).Value = ent.ProjectId;
                    cmd.Parameters.Add("@IDeparmentCode", SqlDbType.VarChar).Value = ent.DeparmentCode;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    Val = 1;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lisQuery.Add(Convert.ToString(reader["Year"]));
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
        public static List<String> GetFlags(ref int Val)
        {
            List<string> lisQuery = new List<string>();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_Project_GetFlags", con);
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    Val = 1;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lisQuery.Add(Convert.ToString(Constant.S3Server + reader["Flag"]));
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

        public static int Insert(MProject ent)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_Project_Ins", con);
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IProjectId", SqlDbType.VarChar).Value = ent.ProjectId;
                    cmd.Parameters.Add("@IDepartment", SqlDbType.VarChar).Value = ent.Department;
                    cmd.Parameters.Add("@IPProjectCode", SqlDbType.VarChar).Value = ent.ProjectCode;
                    cmd.Parameters.Add("@IDescription", SqlDbType.VarChar).Value = ent.Description;
                    cmd.Parameters.Add("@IType", SqlDbType.VarChar).Value = ent.Type;

                    cmd.Parameters.Add("@IEffectiveStatus", SqlDbType.VarChar).Value = ent.EffectiveStatus;
                    cmd.Parameters.Add("@IStatusEffDate", SqlDbType.Int).Value = ent.StatusEffDate;
                    cmd.Parameters.Add("@IStatusEffSeq", SqlDbType.Int).Value = ent.StatusEffSeq;

                    cmd.Parameters.Add("@IStatus", SqlDbType.VarChar).Value = ent.Status;

                    cmd.Parameters.Add("@IStatusDescription", SqlDbType.VarChar).Value = ent.StatusDescription;


                    cmd.Parameters.Add("@IStartDate", SqlDbType.Int).Value = ent.StartDate;
                    cmd.Parameters.Add("@IEndDate", SqlDbType.Int).Value = ent.EndDate;
                    cmd.Parameters.Add("@ITitle", SqlDbType.VarChar).Value = ent.Title;
                    cmd.Parameters.Add("@IAwardId", SqlDbType.VarChar).Value = ent.AwardId;
                    cmd.Parameters.Add("@IAwardStatus", SqlDbType.VarChar).Value = ent.AwardStatus;

                    con.Open();

                    cmd.ExecuteNonQuery();

                    con.Close();
                }
                catch (Exception ex)
                {
                    return 2;
                }
            }

            return 0;
        }

        public static List<MProject> RandomLis(BaseRequest baseRequest, ref int Val)
        {
            List<MProject> lisQuery = new List<MProject>();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_Project_RandomLis", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@ILanguage", SqlDbType.VarChar).Value = baseRequest.Language;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    Val = 0;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MProject entRow = new MProject();
                            entRow.ProjectId = Convert.ToInt32(reader["ProjectId"]);
                            entRow.Title = Convert.ToString(reader["Title"]);
                            entRow.Image = Constant.S3Server + Convert.ToString(reader["Image"]);
                            entRow.Video = Constant.S3Server + Convert.ToString(reader["Video"]);
                            entRow.Country =  Convert.ToString(reader["Country"]);
                            entRow.Department = Convert.ToString(reader["DeparmentCode"]);
                            lisQuery.Add(entRow);
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

        public static MProject GetDetails(MProject ent, ref int val)
        {
            MProject result = new MProject();

            val = 1;

            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_Project_Sel", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IProjectId", SqlDbType.Int).Value = ent.ProjectId;
                    cmd.Parameters.Add("@IDeparmentCode", SqlDbType.VarChar).Value = ent.DeparmentCode;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            result.ProjectId = Convert.ToInt32(reader["ProjectId"]);
                            result.Title = Convert.ToString(reader["Title"]);
                            result.ProgramName = Convert.ToString(reader["ProgramName"]);
                            result.Description = Convert.ToString(reader["Projectdetails"]);
                            result.DonorName = Convert.ToString(reader["DonorName"]);
                            result.DonorDescription = Convert.ToString(reader["DonorDescription"]);
                            result.Status = Convert.ToString(reader["Status"]);
                            result.Country = Convert.ToString(reader["Country"]);
                            result.Sector = Convert.ToString(reader["Sector"]);
                            result.SDG = Convert.ToString(reader["SDG"]);
                            result.AprovalDate = Convert.ToString(reader["AprovalDate"]);
                            result.TaskManager = Convert.ToString(reader["TaskManager"]);
                            result.StartDateDet = Convert.ToString(reader["StartDate"]);
                            result.EndDateDet = Convert.ToString(reader["EndDate"]);
                            result.Advance = Convert.ToInt32(reader["Advance"]);
                            result.Donation = Convert.ToBoolean(reader["Donation"]);
                            result.TotalBudget = Convert.ToDecimal(reader["TotalBudget"]);
                            result.TotalExpenditure = Convert.ToDecimal(reader["TotalExpenditure"]);
                            result.Flag = (Convert.ToString(reader["Flag"]).Equals("")) ? "" : Constant.S3Server + Convert.ToString(reader["Flag"]);
                            result.GifLoad = Constant.S3Server + Convert.ToString(reader["GifLoad"]);
                            result.GifLoadApp = Constant.S3Server + Convert.ToString(reader["GifLoadApp"]);
                            result.Image = (Convert.ToString(reader["Image"]).Equals("")) ? "" : Constant.S3Server + Convert.ToString(reader["Image"]);
                        }

                        val = 0;
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    val = 2;
                }
            }

            return result;
        }

        public static MProject Get(MProject ent)
        {
            MProject result = new MProject();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_Project_Get", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IProjectId", SqlDbType.Int).Value = ent.ProjectId;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    //            Val = 1;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            result.ProjectId = Convert.ToInt32(reader["ProjectId"]);
                            result.ProjectCode = Convert.ToString(reader["ProjectCode"]);
                            result.Title = Convert.ToString(reader["Title"]);
                            result.Type = Convert.ToString(reader["Type"]);
                            result.Description = Convert.ToString(reader["Description"]);
                            result.StartDate = Convert.ToInt32(reader["StartDate"]);
                            result.EndDate = Convert.ToInt32(reader["EndDate"]);
                            result.Status = Convert.ToString(reader["Status"]);
                            result.Department = Convert.ToString(reader["Department"]);
                            result.EffectiveStatus = Convert.ToString(reader["EffectiveStatus"]);
                            result.StatusEffDate = Convert.ToInt32(reader["StatusEffDate"]);
                            result.StatusEffSeq = Convert.ToInt32(reader["StatusEffSeq"]);
                            result.StatusDescription = Convert.ToString(reader["StatusDescription"]);
                            result.AwardId = Convert.ToString(reader["AwardId"]);
                            result.AwardStatus = Convert.ToString(reader["AwardStatus"]);
                            result.Image = (Convert.ToString(reader["Image"]).Equals("")) ? "" : Convert.ToString(reader["Image"]);
                            result.Video = (Convert.ToString(reader["Video"]).Equals("")) ? "" : Convert.ToString(reader["Video"]);
                            result.IsVisible = Convert.ToBoolean(reader["IsVisible"]);
                            result.Donation = Convert.ToBoolean(reader["Donation"]);


                            //                  Val = 0;
                        }
                    }
                    con.Close();
                }

                catch (Exception ex)
                {
                    //        Val = 2;
                }
            }
            return result;
        }


        public static List<MProject> ListProjectCodeExclusions(MProject ent)
        {
            List<MProject> lisQuery = new List<MProject>();

            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ProjectExclusions_Fil", con);
                    cmd.Parameters.Add("@IProjectCode", SqlDbType.VarChar).Value = ent.ProjectCode;
                    cmd.Parameters.Add("@IStarDate", SqlDbType.VarChar).Value = ent.StartDate;
                    cmd.Parameters.Add("@IEndDate", SqlDbType.VarChar).Value = ent.EndDate;
                    cmd.Parameters.Add("@ITitle", SqlDbType.VarChar).Value = ent.Title;
                    cmd.Parameters.Add("@IEffectiveStatus", SqlDbType.VarChar).Value = ent.EffectiveStatus;
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MProject entRow = new MProject();
                            entRow.ProjectId = Convert.ToInt32(reader["ProjectId"]);
                            entRow.ProjectCode = Convert.ToString(reader["ProjectCode"]);
                            entRow.Title = Convert.ToString(reader["Title"]);
                            //entRow.Type = Convert.ToString(reader["Type"]);
                            lisQuery.Add(entRow);
                        }
                    }
                    con.Close();
                }

                catch (Exception ex)
                {
                    lisQuery = new List<MProject>();
                }
            }
            return lisQuery;
        }


        public static List<MProject> List(MProject ent, Session session)
        {
            List<MProject> lisQuery = new List<MProject>();

            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("pr_Project_Lis", con);
                    cmd.Parameters.Add("@IUserId", SqlDbType.VarChar).Value = session.UserId;
                    cmd.Parameters.Add("@IProjectCode", SqlDbType.VarChar).Value = ent.ProjectCode;
                    cmd.Parameters.Add("@IStarDate", SqlDbType.VarChar).Value = ent.StartDate;
                    cmd.Parameters.Add("@IEndDate", SqlDbType.VarChar).Value = ent.EndDate;
                    cmd.Parameters.Add("@ITitle", SqlDbType.VarChar).Value = ent.Title;
                    cmd.Parameters.Add("@IEffectiveStatus", SqlDbType.VarChar).Value = ent.EffectiveStatus;
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MProject entRow = new MProject();
                            entRow.ProjectId = Convert.ToInt32(reader["ProjectId"]);
                            entRow.ProjectCode = Convert.ToString(reader["ProjectCode"]);
                            entRow.Title = Convert.ToString(reader["Title"]);
                            entRow.Type = Convert.ToString(reader["Type"]);
                            entRow.Description = Convert.ToString(reader["Description"]);
                            entRow.StartDate = Convert.ToInt32(reader["StartDate"]);
                            entRow.EndDate = Convert.ToInt32(reader["EndDate"]);
                            entRow.Status = Convert.ToString(reader["Status"]);
                            entRow.Department = Convert.ToString(reader["Department"]);
                            entRow.EffectiveStatus = Convert.ToString(reader["EffectiveStatus"]);
                            entRow.StatusEffDate = Convert.ToInt32(reader["StatusEffDate"]);
                            entRow.StatusEffSeq = Convert.ToInt32(reader["StatusEffSeq"]);
                            entRow.AwardId = Convert.ToString(reader["AwardId"]);
                            entRow.AwardStatus = Convert.ToString(reader["AwardStatus"]);
                            entRow.StatusDescription = Convert.ToString(reader["StatusDescription"]);
                            entRow.IsVisible = Convert.ToBoolean(reader["IsVisible"]);
                            entRow.Donation = Convert.ToBoolean(reader["Donation"]);
                            lisQuery.Add(entRow);
                        }
                    }
                    con.Close();
                }

                catch (Exception ex)
                {
                    lisQuery = new List<MProject>();
                }
            }
            return lisQuery;
        }

        public static List<MProject> ListScroll()
        {
            List<MProject> lisQuery = new List<MProject>();

            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_Project_Lis_Scroll", con);
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MProject entRow = new MProject();
                            entRow.ProjectId = Convert.ToInt32(reader["ProjectId"]);
                            entRow.Title = Convert.ToString(reader["Title"]);
                            entRow.Description = Convert.ToString(reader["Description"]);
                            entRow.Donation = Convert.ToBoolean(reader["Donation"]);
                            entRow.Image = (Convert.ToString(reader["Image"]).Equals("")) ? "" : Constant.S3Server + Convert.ToString(reader["Image"]);
                            entRow.Video = (Convert.ToString(reader["Video"]).Equals("")) ? "" : Constant.S3Server + Convert.ToString(reader["Video"]);
                            entRow.Advance = Convert.ToInt32(reader["Advance"]);
                            entRow.GifLoad = Constant.S3Server + Convert.ToString(reader["GifLoad"]);
                            entRow.Country = Convert.ToString(reader["Country"]);
                            entRow.Flag = (Convert.ToString(reader["Flag"]).Equals("")) ? "" : Constant.S3Server + Convert.ToString(reader["Flag"]);
                            entRow.Department = Convert.ToString(reader["DeparmentCode"]);
                            entRow.StartDateStr = Convert.ToString(reader["StartDate"]);
                            entRow.EndDateStr = Convert.ToString(reader["EndDate"]);
                            lisQuery.Add(entRow);
                        }
                    }
                    con.Close();
                }

                catch (Exception ex)
                {
                    lisQuery = new List<MProject>();
                }
            }
            return lisQuery;
        }

        public static List<MProject> ListGroupbyCountry(MProject ent)
        {
            List<MProject> lisQuery = new List<MProject>();

            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_Project_Lis_GroupByCountry", con);
                    cmd.CommandTimeout = 0;

                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MProject entRow = new MProject();
                            entRow.CountryId = Convert.ToInt32(reader["CountryId"]);
                            entRow.Quantity = Convert.ToInt32(reader["Quanty"]);
                            entRow.Flag = (Convert.ToString(reader["Flag"]).Equals("")) ? "" : Constant.S3Server + Convert.ToString(reader["Flag"]);
                            entRow.Longitude = Convert.ToString(reader["Longitude"]);
                            entRow.Latitude = Convert.ToString(reader["Latitude"]);
                            lisQuery.Add(entRow);
                        }
                    }
                    con.Close();
                }

                catch (Exception ex)
                {
                    lisQuery = new List<MProject>();
                }
            }
            return lisQuery;
        }

        public static List<MProject> ListbyCountry(MProject ent)
        {
            List<MProject> lisQuery = new List<MProject>();

            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_Project_Lis_FilterByCountry", con);
                    cmd.CommandTimeout = 0;

                    cmd.Parameters.Add("@ICountryId", SqlDbType.VarChar).Value = ent.CountryId;

                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MProject entRow = new MProject();
                            entRow.ProjectId = Convert.ToInt32(reader["ProjectId"]);
                            entRow.Department = Convert.ToString(reader["DeparmentCode"]);
                            entRow.Title = Convert.ToString(reader["Title"]);
                            entRow.Description = Convert.ToString(reader["Projectdetails"]);
                            entRow.Image = (Convert.ToString(reader["Image"]).Equals("")) ? "" : Constant.S3Server + Convert.ToString(reader["Image"]);
                            entRow.Flag = (Convert.ToString(reader["Flag"]).Equals("")) ? "" : Constant.S3Server + Convert.ToString(reader["Flag"]);

                            entRow.Donation = Convert.ToBoolean(reader["Donation"]);

                            entRow.Longitude = Convert.ToString(reader["Longitude"]);
                            entRow.Latitude = Convert.ToString(reader["Latitude"]);

                            entRow.StartDateStr = Convert.ToString(reader["StartDate"]);
                            entRow.EndDateStr = Convert.ToString(reader["EndDate"]);
                            entRow.Advance = Convert.ToInt32(reader["Advance"]);

                            entRow.TotalBudget = Convert.ToDecimal(reader["Budget"]);
                            entRow.TotalExpenditure = Convert.ToDecimal(reader["Expenditure"]);

                            lisQuery.Add(entRow);
                        }
                    }
                    con.Close();
                }

                catch (Exception ex)
                {
                    lisQuery = new List<MProject>();
                }
            }
            return lisQuery;
        }

        public static List<MProject> ListFilter(MProject ent)
        {
            List<MProject> lisQuery = new List<MProject>();

            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_Project_Lis_Filter", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IContinents", SqlDbType.VarChar).Value = ent.Continents;
                    cmd.Parameters.Add("@ICountries", SqlDbType.VarChar).Value = ent.Countries;
                    cmd.Parameters.Add("@ITitle", SqlDbType.VarChar).Value = ent.Title;
                    cmd.Parameters.Add("@IAnio", SqlDbType.VarChar).Value = ent.Anio;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MProject entRow = new MProject();
                            entRow.ProjectId = Convert.ToInt32(reader["ProjectId"]);
                            entRow.Department = Convert.ToString(reader["DeparmentCode"]);
                            entRow.Title = Convert.ToString(reader["Title"]);
                            entRow.Description = Convert.ToString(reader["Projectdetails"]);
                            entRow.Image = (Convert.ToString(reader["Image"]).Equals("")) ? "" : Constant.S3Server + Convert.ToString(reader["Image"]);
                            entRow.Flag = (Convert.ToString(reader["Flag"]).Equals("")) ? "" : Constant.S3Server + Convert.ToString(reader["Flag"]);

                            entRow.Donation = Convert.ToBoolean(reader["Donation"]);

                            entRow.Longitude = Convert.ToString(reader["Longitude"]);
                            entRow.Latitude = Convert.ToString(reader["Latitude"]);

                            entRow.StartDateStr = Convert.ToString(reader["StartDate"]);
                            entRow.EndDateStr = Convert.ToString(reader["EndDate"]);
                            entRow.Advance = Convert.ToInt32(reader["Advance"]);

                            entRow.TotalBudget = Convert.ToDecimal(reader["Budget"]);
                            entRow.TotalExpenditure = Convert.ToDecimal(reader["Expenditure"]);
                            entRow.Country = Convert.ToString(reader["Country"]);
                            lisQuery.Add(entRow);
                        }
                    }
                    con.Close();
                }

                catch (Exception ex)
                {
                    lisQuery = new List<MProject>();
                }
            }
            return lisQuery;
        }

        public static int Update(MProject ent)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("pr_Project_Upd", con);
                    cmd.Parameters.Add("@IProjectId", SqlDbType.Int).Value = ent.ProjectId;
                    cmd.Parameters.Add("@IImage", SqlDbType.VarChar).Value = ent.Image;
                    cmd.Parameters.Add("@IVideo", SqlDbType.VarChar).Value = ent.Video;
                    cmd.Parameters.Add("@IIsVisible", SqlDbType.Int).Value = ent.IsVisible;
                    cmd.Parameters.Add("@IDonation", SqlDbType.Int).Value = ent.Donation;


                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    return ent.ProjectId;
                }

                catch (Exception ex)
                {

                    return ent.ProjectId = 0;

                }
            }

        }

        public static MProjectFinancials GetFinancialsByYear(MProjectFinancials ent, ref int Val)
        {
            MProjectFinancials lisQuery = new MProjectFinancials();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ProjectFinancial_LisByYear", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IProjectId", SqlDbType.Int).Value = ent.ProjectId;
                    cmd.Parameters.Add("@IYear", SqlDbType.VarChar).Value = ent.Year;
                    cmd.Parameters.Add("@IDeparmentCode", SqlDbType.VarChar).Value = ent.DeparmentCode;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    Val = 0;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            lisQuery.Budget = Convert.ToDecimal(reader["Budget"]);
                            lisQuery.Expenditure = Convert.ToDecimal(reader["Expenditure"]);
                            lisQuery.GifLoadPresupuesto = Constant.S3Server + Convert.ToString(reader["GifLoadPresupuesto"]);
                            lisQuery.GifLoadGasto = Constant.S3Server + Convert.ToString(reader["GifLoadGasto"]);
                            lisQuery.AdvanceBudget = Convert.ToDecimal(reader["AdvanceBudget"]);
                            lisQuery.AdvanceExpenditure = Convert.ToDecimal(reader["AdvanceExpenditure"]);
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

        public static MProjectFinancials GetFinancials(MProjectFinancials ent, ref int Val)
        {
            MProjectFinancials lisQuery = new MProjectFinancials();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ProjectFinancial_LisTotals", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IProjectId", SqlDbType.Int).Value = ent.ProjectId;
                    cmd.Parameters.Add("@IDeparmentCode", SqlDbType.VarChar).Value = ent.DeparmentCode;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    Val = 0;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            lisQuery.Budget = Convert.ToDecimal(reader["Budget"]);
                            lisQuery.Expenditure = Convert.ToDecimal(reader["Expenditure"]);
                            lisQuery.GifLoadPresupuesto = Constant.S3Server + Convert.ToString(reader["GifLoadPresupuesto"]);
                            lisQuery.GifLoadGasto = Constant.S3Server + Convert.ToString(reader["GifLoadGasto"]);
                            lisQuery.AdvanceBudget = Convert.ToDecimal(reader["AdvanceBudget"]);
                            lisQuery.AdvanceExpenditure = Convert.ToDecimal(reader["AdvanceExpenditure"]);
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
