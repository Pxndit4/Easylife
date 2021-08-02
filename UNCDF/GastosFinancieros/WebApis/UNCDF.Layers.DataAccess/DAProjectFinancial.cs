using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Data;
using UNCDF.Layers.Model;

namespace UNCDF.Layers.DataAccess
{
    public class DAProjectFinancial
    {
        public static int Insert(MProjectFinancials ent)
        {
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ProjectFinancial_Ins", con);
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IProjectId", SqlDbType.Int).Value =        ent.ProjectId;
                    cmd.Parameters.Add("@IBudgetPeriod", SqlDbType.VarChar).Value =         ent.Year;
                    cmd.Parameters.Add("@IOperUnit", SqlDbType.VarChar).Value =             ent.OperUnit;
                    cmd.Parameters.Add("@IDepartment", SqlDbType.VarChar).Value =           ent.DeparmentCode;
                    cmd.Parameters.Add("@IPProjectCode", SqlDbType.VarChar).Value =         ent.ProjectCode;
                    cmd.Parameters.Add("@IDescProject", SqlDbType.VarChar).Value =          ent.DescrProject;
                    cmd.Parameters.Add("@IProjectManager", SqlDbType.VarChar).Value =       ent.ProjectManager;
                    cmd.Parameters.Add("@IImplementAgencyCode", SqlDbType.VarChar).Value =  ent.ImplementAgencyCode;
                    cmd.Parameters.Add("@IShortDesc", SqlDbType.VarChar).Value =            ent.ShortDesc;
                    cmd.Parameters.Add("@IFundCode", SqlDbType.VarChar).Value =             ent.FundCode;
                    cmd.Parameters.Add("@IDescrFund", SqlDbType.VarChar).Value =            ent.DescrFund;
                    cmd.Parameters.Add("@IBudget", SqlDbType.Decimal).Value =               ent.Budget;
                    cmd.Parameters.Add("@IPreEncumbrance", SqlDbType.Decimal).Value =       ent.PreEncumbrance;
                    cmd.Parameters.Add("@IEncumbrance", SqlDbType.Decimal).Value =          ent.Encumbrance;
                    cmd.Parameters.Add("@IDisbursement", SqlDbType.Decimal).Value =         ent.Disbursement;
                    cmd.Parameters.Add("@IExpenditure", SqlDbType.Decimal).Value =          ent.Expenditure;
                    cmd.Parameters.Add("@IBalance", SqlDbType.Decimal).Value =              ent.Balance;
                    cmd.Parameters.Add("@ISpent", SqlDbType.Decimal).Value =                ent.Spent;
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

        public static List<MProjectFinancials> FilProjectFinancial(MProjectFinancials ent, ref int Val)
        {
            //MProjectFinancials lisQuery = new MProjectFinancials();
            List<MProjectFinancials> list = new List<MProjectFinancials>();
            using (SqlConnection con = new SqlConnection(ConnectionDB.GetConnectionString()))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ProjectFinancial_Fil", con);
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@IYear", SqlDbType.VarChar).Value = ent.Year;
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    Val = 0;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            MProjectFinancials lisQuery = new MProjectFinancials();
                            lisQuery.ProjectCode = Convert.ToString(reader["ProjectCode"]);
                            lisQuery.DeparmentCode = Convert.ToString(reader["Budget"]);
                            lisQuery.ImplementAgencyCode = Convert.ToString(reader["ImplementAgencyCode"]);
                            lisQuery.FundCode = Convert.ToString(reader["FundCode"]);
                            lisQuery.Year = Convert.ToString(reader["BudgetPeriod"]);
                            lisQuery.Budget = Convert.ToDecimal(reader["Budget"]);
                            lisQuery.Expenditure = Convert.ToDecimal(reader["Expenditure"]);
                            lisQuery.Balance = Convert.ToDecimal(reader["Balance"]);
                            lisQuery.Spent = Convert.ToDecimal(reader["Spent"]);
                            lisQuery.OperUnit = Convert.ToString(reader["OperUnit"]);

                            lisQuery.ProjectManager = Convert.ToString(reader["ProjectManager"]);
                            lisQuery.ShortDesc = Convert.ToString(reader["ShortDesc"]);
                            lisQuery.DescrFund = Convert.ToString(reader["DescrFund"]);
                            lisQuery.PreEncumbrance = Convert.ToDecimal(reader["PreEncumbrance"]);
                            lisQuery.Encumbrance = Convert.ToDecimal(reader["Encumbrance"]);
                            lisQuery.Disbursement = Convert.ToDecimal(reader["Disbursement"]);
                            lisQuery.DescrProject = Convert.ToString(reader["DescrProject"]);
                            list.Add(lisQuery);
                        }
                    }
                    con.Close();
                }

                catch (Exception ex)
                {
                    Val = 2;
                    list = new List<MProjectFinancials>();
                }
            }
            return list;
        }


    }
}
