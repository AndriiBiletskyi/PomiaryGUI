﻿using System;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace PomiaryGUI
{
    public interface IDataManager
    {
        void SetConnection(List<string> list);//
        void SqlConnectionClose();
        DataRow GetLastEquData(int eq);
        DataTable GetEquData(int eq, DateTime begin, DateTime end);
        void DD_MM(bool state);
        float GetFirstData(int eq, DateTime begin, DateTime end, string Value);
        float GetLastData(int eq, DateTime begin, DateTime end, string Value);
        void GetFirstLastData(int eq, DateTime begin, DateTime end, ref float P_day, ref float Q_day);
    }

    public class DataManager: IDataManager
    {
        private SqlConnection sqlConnection = null;
        private SqlDataAdapter dataAdapter = null;
        private DataSet dataSet = null;
        private SqlCommand cmd;
        //private DataTable table = null;
        //private string connection = @"Data Source=PL02K01-C0AH8FL\SQL25012021;Initial Catalog=pomiary;Userid=uzytkownik;Password=Kayser2021";
        private string con = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS_0804\MSSQL\DATA\pomiary.mdf;Integrated Security = True; Connect Timeout = 30";
        private bool _DD_MM_ = false;

        SqlConnectionStringBuilder connection = new SqlConnectionStringBuilder()
        {
            DataSource = "PL02K01-C0AH8FL,1433\\SQL25012021",
            InitialCatalog = "pomiary",
            UserID = "uzytkownik",
            Password = "Kayser2021"
        };

        private void Sql_Connect()
        {
            if (sqlConnection == null)
            {
                sqlConnection = new SqlConnection(con);
                //sqlConnection = new SqlConnection(connection.ConnectionString);
                sqlConnection.Open();
            }
        }

        public DataRow GetLastEquData(int eq)
        {
            try
            {
                if (eq > 0 && eq < 256)
                {
                    Sql_Connect();
                    DataTable table = null;
                    string str = "SELECT TOP 1 * FROM dbo.\"" + Convert.ToString(eq) + "\" " + "ORDER BY ID DESC";
                    dataAdapter = new SqlDataAdapter(str, sqlConnection);
                    dataSet = new DataSet();
                    dataAdapter.Fill(dataSet, "dbo." + Convert.ToString(eq));
                    table = dataSet.Tables["dbo." + Convert.ToString(eq)];

                    if (table != null) return table.AsEnumerable().Last();
                }

                return new DataTable().NewRow();
            }
            catch(Exception ex)
            {
                return new DataTable().NewRow();
            }
        }

        public DataTable GetEquData(int eq, DateTime begin, DateTime end)
        {                               /*12.03.2021 19:45:53*/
            try
            {
                if (eq > 0 && eq < 256)
                {
                    Sql_Connect();
                                     
                    string str = "SELECT * FROM dbo.\"" + Convert.ToString(eq) + "\" " + "WHERE (Czas BETWEEN '" + replace(begin, _DD_MM_) + "' AND '" + replace(end, _DD_MM_) + "')" + "ORDER BY ID ASC";
                    dataAdapter = new SqlDataAdapter(str, sqlConnection);
                    dataSet = new DataSet();
                    dataAdapter.Fill(dataSet, "dbo." + Convert.ToString(eq));

                    return dataSet.Tables["dbo." + Convert.ToString(eq)];
                }

                return new DataTable();
            }
            catch(Exception ex)
            {
                return new DataTable();
            }
        }

        public float GetFirstData(int eq, DateTime begin, DateTime end, string Value)
        {
            try
            {
                if (eq > 0 && eq < 256)
                {
                    Sql_Connect();

                    string str = "SELECT TOP 1 " + Value + " FROM dbo.\"" + Convert.ToString(eq) + "\" "
                                    + "WHERE (Czas BETWEEN '" + replace(begin, _DD_MM_) + "' AND '"
                                                              + replace(end, _DD_MM_) + "')"
                                                              + " AND "+ Value + " IS NOT NULL "
                                                              + " ORDER BY ID ASC";
                    cmd = new SqlCommand(str, sqlConnection);
                    float res = 0;
                    object o = cmd.ExecuteScalar();
                    if (o != null) res = Convert.ToSingle(o);
                    cmd.Dispose();
                    return res;
                }
                return 0.0f;

            }
            catch (Exception ex)
            {
                string str = ex.ToString();
                return 0.0f;
            }
        }

        public float GetLastData(int eq, DateTime begin, DateTime end, string Value)
        {
            try
            {
                if (eq > 0 && eq < 256)
                {
                    Sql_Connect();

                    string str = "SELECT TOP 1 " + Value + " FROM dbo.\"" + Convert.ToString(eq) + "\" "
                                    + "WHERE (Czas BETWEEN '" + replace(begin, _DD_MM_) + "' AND '"
                                                              + replace(end, _DD_MM_) + "')"
                                                              + " AND " + Value + " IS NOT NULL "
                                                              + " ORDER BY ID DESC";
                    cmd = new SqlCommand(str, sqlConnection);
                    float res = 0;
                    object o = cmd.ExecuteScalar();
                    if (o != null) res = Convert.ToSingle(o);
                    cmd.Dispose();
                    return res;
                }
                return 0.0f;

            }
            catch (Exception ex)
            {
                string str = ex.ToString();
                return 0.0f;
            }
        }

        public void GetFirstLastData(int eq, DateTime begin, DateTime end, ref float P_day, ref float Q_day)
        {
            try
            {
                if (eq > 0 && eq < 256)
                {
                    Sql_Connect();

                    string str = "SELECT * FROM dbo.\"" + Convert.ToString(eq) + "\" "
                                + "WHERE (Czas BETWEEN '" + replace(begin, _DD_MM_) 
                                + "' AND '" + replace(end, _DD_MM_) + "')"
                                + "ORDER BY ID ASC";

                    dataAdapter = new SqlDataAdapter(str, sqlConnection);
                    dataSet = new DataSet();
                    dataAdapter.Fill(dataSet, "dbo." + Convert.ToString(eq));

                    List<object> P_list = new List<object>();
                    List<object> Q_list = new List<object>();
                    using (var dt = dataSet.Tables["dbo." + Convert.ToString(eq)])
                    {
                        if(dt.Columns.Contains("P_day") &&
                           dt.Columns.Contains("Q_day"))
                        {
                            P_list = dt.AsEnumerable().Select(x => x["P_day"]).ToList();
                            Q_list = dt.AsEnumerable().Select(x => x["Q_day"]).ToList();

                            P_day = (float)Math.Round((Convert.ToSingle(P_list.LastOrDefault(x => x!=DBNull.Value)) - Convert.ToSingle(P_list.FirstOrDefault(x => x != DBNull.Value))) / 1000, 2);
                            Q_day = (float)Math.Round((Convert.ToSingle(Q_list.LastOrDefault(x => x != DBNull.Value)) - Convert.ToSingle(Q_list.FirstOrDefault(x => x != DBNull.Value))) / 1000, 2);
                        }
                        else
                        {
                            P_day = 0.0f;
                            Q_day = 0.0f;
                        }
                    }
                    P_list = null;
                    Q_list = null;
                }

            }
            catch (Exception ex)
            {

            }
        }

        public void SetConnection(List<string> list)
        {
            try
            {
                connection = new SqlConnectionStringBuilder()
                {
                    DataSource = list[0],
                    InitialCatalog = list[1],
                    UserID = list[2],
                    Password = list[3]
                };

                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();

                if (sqlConnection == null)
                {
                    sqlConnection = new SqlConnection(connection.ConnectionString);
                    sqlConnection.Open();
                }
            }    
            catch(Exception ex)
            {

            }
        }

        public void SqlConnectionClose()
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }

        public void DD_MM(bool state)
        {
            _DD_MM_ = state;
        }
    
        private string replace(DateTime dt, bool ddmm)
        {
            if (ddmm)
            {
                return  dt.Year.ToString() + "."
                        + dt.Month.ToString() + "."
                        + dt.Day.ToString() + " "
                        + dt.TimeOfDay.ToString();
            }
            else
            {
                return  dt.Year.ToString() + "."
                        + dt.Day.ToString() + " "
                        + dt.Month.ToString() + "."
                        + dt.TimeOfDay.ToString();
            }
        }
    }
}
