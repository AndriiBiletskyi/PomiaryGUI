using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Pomiary.BL;
using System.Windows.Forms;
using LiveCharts.WinForms;
using System.Drawing;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Geared;
using System.Data;


//-----------------------------------------------------------------------------------
//  P1    
//  P2
//  P3
//  P
//
//  Q1
//  Q2
//  Q3
//  Q
//
//  U1
//  U2
//  U3
//  
//  I1
//  I2
//  I3
//
//  cosp
//
//  P_day
//  Q_day
//
//  
//
//-----------------------------------------------------------------------------------

namespace PomiaryGUI
{
    public class Presenter
    {
        private readonly IMainForm _mainForm;
        private readonly IDataManager _dataManager;
        private DataTable table = null;
        private List<string> l = new List<string>();

        //private List<LiveCharts.WinForms.CartesianChart> c = new List<LiveCharts.WinForms.CartesianChart>();
        //private LiveCharts.WinForms.CartesianChart chart1 = new LiveCharts.WinForms.CartesianChart
        //{
        //    AnimationsSpeed = new TimeSpan(0, 0, 0, 0, 0)
        //};
        //private LiveCharts.WinForms.CartesianChart chart2 = new LiveCharts.WinForms.CartesianChart
        //{
        //    AnimationsSpeed = new TimeSpan(0, 0, 0, 0, 0)
        //};
        //private LiveCharts.WinForms.CartesianChart chart3 = new LiveCharts.WinForms.CartesianChart
        //{
        //    AnimationsSpeed = new TimeSpan(0, 0, 0, 0, 0)
        //};
        //private LiveCharts.WinForms.CartesianChart chart4 = new LiveCharts.WinForms.CartesianChart
        //{
        //    AnimationsSpeed = new TimeSpan(0, 0, 0, 0, 0)
        //};
        //private LiveCharts.WinForms.CartesianChart chart5 = new LiveCharts.WinForms.CartesianChart
        //{
        //    AnimationsSpeed = new TimeSpan(0, 0, 0, 0, 0)
        //};
        //private LiveCharts.WinForms.CartesianChart chart6 = new LiveCharts.WinForms.CartesianChart
        //{
        //    AnimationsSpeed = new TimeSpan(0, 0, 0, 0, 0)
        //};
        //private LiveCharts.WinForms.CartesianChart chart7 = new LiveCharts.WinForms.CartesianChart
        //{
        //    AnimationsSpeed = new TimeSpan(0, 0, 0, 0, 0)
        //};
        //private LiveCharts.WinForms.CartesianChart chart8 = new LiveCharts.WinForms.CartesianChart
        //{
        //    AnimationsSpeed = new TimeSpan(0, 0, 0, 0, 0)
        //};
        //private LiveCharts.WinForms.CartesianChart chart9 = new LiveCharts.WinForms.CartesianChart
        //{
        //    AnimationsSpeed = new TimeSpan(0, 0, 0, 0, 0)
        //};
        //private LiveCharts.WinForms.CartesianChart chart10 = new LiveCharts.WinForms.CartesianChart
        //{
        //    AnimationsSpeed = new TimeSpan(0, 0, 0, 0, 0)
        //};
        //private LiveCharts.WinForms.CartesianChart chart11 = new LiveCharts.WinForms.CartesianChart
        //{
        //    AnimationsSpeed = new TimeSpan(0, 0, 0, 0, 0)
        //};
        //private LiveCharts.WinForms.CartesianChart chart12 = new LiveCharts.WinForms.CartesianChart
        //{
        //    AnimationsSpeed = new TimeSpan(0, 0, 0, 0, 0)
        //};

        List<string> dates = new List<string>();
        GearedValues<double> PL = new GearedValues<double>();
        GearedValues<double> QL = new GearedValues<double>();
        //DataRow row = null;

        enum Raports
        {
            day, week, month, year
        }

        public Presenter(IMainForm mainForm, IDataManager dataManager)
        {
            _mainForm = mainForm;
            _dataManager = dataManager;

            _mainForm.ButChartsPowerClick += new EventHandler(_mainForm_ButPowerClick);
            _mainForm.ButChartsCurrentClick += new EventHandler(_mainForm_ButCurrentClick);
            _mainForm.ButChartsVoltageClick += new EventHandler(_mainForm_ButVoltageClick);
            _mainForm.ButChartsCosClick += new EventHandler(_mainForm_ButCosClick);

            _mainForm.ButRaportsDailyClick += new EventHandler(_mainForm_ButRaportsDailyClick);
            _mainForm.ButRaportsWeeklyClick += new EventHandler(_mainForm_ButRaportsWeeklyClick);
            _mainForm.ButRaportsMonthlyClick += new EventHandler(_mainForm_ButRaportsMonthlyClick);
            _mainForm.ButRaportsAnnualClick += new EventHandler(_mainForm_ButRaportsAnnualClick);

            _mainForm.ButCloseClick += new EventHandler(_mainForm_ButCloseClick);

            _mainForm.ButChartsEquipmentsClick += new EventHandler(_mainForm_ButEquClick);

            _mainForm.ChangeConnect += new EventHandler(_mainForm_ChangeConnect);
            _mainForm.ReplaceDDMM += new EventHandler(_mainForm_ReplaceDDMM);
      
        }

        private void _mainForm_ButEquClick(object sender, EventArgs e)
        {
            try
            {
                DataTable dataTable = _dataManager.GetEquData(1, new DateTime(1800, 1, 1, 1, 1, 1), new DateTime(1800, 1, 1, 1, 1, 1));

                foreach (string i in _mainForm.EquList)//200ms
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow = _dataManager.GetLastEquData(Convert.ToInt32(i));
                    dataTable.ImportRow(dataRow);
                }
                _mainForm.EquipmentsFill(dataTable);//1635 ms

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void _mainForm_ButCloseClick(object sender, EventArgs e)
        {
            _dataManager.SqlConnectionClose();
            _mainForm.FormClose();
        }

        private void _mainForm_ButCosClick(object sender, EventArgs e)
        {
            _mainForm.powerchart(_dataManager.GetEquData(_mainForm.PowerEquNumber, _mainForm.PowerGetDateFrom(),
                                                            _mainForm.PowerGetDateTo()), "cos");
        }

        private void _mainForm_ButVoltageClick(object sender, EventArgs e)
        {
            _mainForm.powerchart(_dataManager.GetEquData(_mainForm.PowerEquNumber, _mainForm.PowerGetDateFrom(),
                                                            _mainForm.PowerGetDateTo()), "voltage");
        }

        private void _mainForm_ButCurrentClick(object sender, EventArgs e)
        {
            _mainForm.powerchart(_dataManager.GetEquData(_mainForm.PowerEquNumber, _mainForm.PowerGetDateFrom(),
                                                            _mainForm.PowerGetDateTo()), "current");
        }

        private void _mainForm_ButPowerClick(object sender, EventArgs e)
        {
            _mainForm.powerchart(_dataManager.GetEquData(_mainForm.PowerEquNumber, _mainForm.PowerGetDateFrom(),
                                                            _mainForm.PowerGetDateTo()), "power");
        }

        private void _mainForm_ChangeConnect(object sender, EventArgs e)
        {
            _dataManager.SetConnection(_mainForm.SettingsGetDataConnection());
        }

        private void _mainForm_ButRaportsDailyClick(object sender, EventArgs e)
        {
            Raport_data(Raports.day);
        }

        private void _mainForm_ButRaportsWeeklyClick(object sender, EventArgs e)
        {
            Raport_data(Raports.week);
        }

        private void _mainForm_ButRaportsMonthlyClick(object sender, EventArgs e)
        {
            Raport_data(Raports.month);
        }

        private void _mainForm_ButRaportsAnnualClick(object sender, EventArgs e)
        {
            Raport_data(Raports.year);
        }

        private void Raport_data(Raports step)
        {
            try
            {
                DataTable dataTable = _dataManager.GetEquData(1, new DateTime(1800, 1, 1, 1, 1, 1),
                                                                 new DateTime(1800, 1, 1, 1, 1, 1));
                if (!dataTable.Columns.Contains("Nazwa_urzadzenia")) return;
                DataTable dataRaports = new DataTable();
                dataRaports.Columns.Add("Time/Day", typeof(string));
                DateTime dateTimeFrom = _mainForm.RaportsGetDateFrom();
                DateTime dateTimeTo = _mainForm.RaportsGetDateTo();
                List<DateTime> time_day = new List<DateTime>();
                List<List<float>> PQ_List = new List<List<float>>();
                /*  Equ     P,Q(dateTimeFrom)   P,Q(dateTimeFrom + 1.0).... P,Q(dateTimeTo)       
                 *  0       P,Q                 P,Q                         P,Q
                 *  1       ...                 ...                         ...
                 *  ...     ...                 ...                         ...
                 *  n       ...                 ...                         ...
                 */

                DataRow dataRow = dataTable.NewRow();
                for (int i = 1; i < 13; i++)//2000
                {
                    dataRow = _dataManager.GetLastEquData(i);
                    if (dataRow.Table.Columns.Contains("Nazwa_urzadzenia"))
                    {
                        dataRaports.Columns.Add(Convert.ToString(dataRow["Nazwa_urzadzenia"]) + ", P", typeof(float));
                        dataRaports.Columns.Add(Convert.ToString(dataRow["Nazwa_urzadzenia"]) + ", Q", typeof(float));
                    }
                    else
                    {
                        dataRaports.Columns.Add("Unknow " + i.ToString() + ", P", typeof(float));
                        dataRaports.Columns.Add("Unknow " + i.ToString() + ", Q", typeof(float));
                    }
                }
                dataRow = null;

                if (dateTimeTo <= dateTimeFrom) return;
                DateTime dateTime = new DateTime();
                if (step == Raports.day)
                {
                    dateTime = dateTimeFrom.AddHours(1.0);
                    if ((dateTimeTo - dateTimeFrom).TotalDays > 7) dateTimeTo = dateTimeFrom.AddDays(7.0);
                }
                else if (step == Raports.week)
                {
                    dateTime = dateTimeFrom.AddDays(1.0);
                    if ((dateTimeTo - dateTimeFrom).TotalDays > 7 * 4) dateTimeTo = dateTimeFrom.AddDays(7.0 * 4);
                }
                else if (step == Raports.month)
                {
                    dateTime = dateTimeFrom.AddDays(1.0);
                    if ((dateTimeTo - dateTimeFrom).TotalDays > 31 * 4) dateTimeTo = dateTimeFrom.AddDays(31 * 4);
                }
                else if (step == Raports.year)
                {
                    dateTime = dateTimeFrom.AddMonths(1);
                }

                time_day.Add(dateTimeFrom);
                while (dateTime < dateTimeTo)
                {
                    time_day.Add(dateTime);
                    if (step == Raports.day) dateTime = dateTime.AddHours(1.0);
                    else if (step == Raports.week) dateTime = dateTime.AddDays(1.0);
                    else if (step == Raports.month) dateTime = dateTime.AddDays(1.0);
                    else if (step == Raports.year) dateTime = dateTime.AddMonths(1);
                }
                time_day.Add(dateTimeTo);
                //12 000
                
                //DataTable dataTable1 = new DataTable();
                for (int i = 1; i < 13; i++)//10000
                {
                    //L.Clear();
                    List<float> L = new List<float>();
                    for (int r = 0; r < time_day.Count - 1; r++)
                    {
                        //if (r == (time_day.Count - 2)) dataTable1 = _dataManager.GetEquData(i, time_day[r], time_day[r + 1]);
                        //else dataTable1 = _dataManager.GetEquData(i, time_day[r], time_day[r + 1].AddSeconds(-1.0));
                        //if (dataTable1.Columns.Contains("P_day"))
                        //{
                        //    float P_From = 0;
                        //    float P_To = 0;
                        //    float Q_From = 0;
                        //    float Q_To = 0;

                        //    object o = dataTable1.Compute("min([P_day])", string.Empty);
                        //    if (o != DBNull.Value) P_From = Convert.ToSingle(o);
                        //    else P_From = 0;
                        //    o = dataTable1.Compute("max([P_day])", string.Empty);
                        //    if (o != DBNull.Value) P_To = Convert.ToSingle(o);
                        //    else P_To = 0;
                        //    o = dataTable1.Compute("min([Q_day])", string.Empty);
                        //    if (o != DBNull.Value) Q_From = Convert.ToSingle(o);
                        //    else Q_From = 0;
                        //    o = dataTable1.Compute("max([Q_day])", string.Empty);
                        //    if (o != DBNull.Value) Q_To = Convert.ToSingle(o);
                        //    else Q_To = 0;

                        //    L.Add((float)Math.Round((P_To - P_From) / 1000, 2));
                        //    L.Add((float)Math.Round((Q_To - Q_From) / 1000, 2));
                        //}
                        //else
                        //{
                        //    L.Add(0.0f);
                        //    L.Add(0.0f);
                        //}
                        //float P_From = 0;
                        //float P_To = 0;
                        //float Q_From = 0;
                        //float Q_To = 0;
                        float P_day = 0;
                        float Q_day = 0;
                        if (r == (time_day.Count - 2))
                        {
                            _dataManager.GetFirstLastData(i, time_day[r], time_day[r + 1], ref P_day, ref Q_day);
                        }
                        else
                        {
                            _dataManager.GetFirstLastData(i, time_day[r], time_day[r + 1].AddSeconds(-1.0), ref P_day, ref Q_day);
                        }
                        L.Add(P_day);
                        L.Add(Q_day);
                    }
                    PQ_List.Add(L);
                }
                //10 000
                
                for (int i = 0; i < time_day.Count - 1; i++)
                {
                    DataRow row = dataRaports.NewRow();
                    if (i == (time_day.Count - 2)) row[0] = time_day[i].ToString() + System.Environment.NewLine + time_day[i + 1].ToString();
                    else row[0] = time_day[i].ToString() + System.Environment.NewLine + time_day[i + 1].AddSeconds(-1.0).ToString();
                    int q = 1;
                    foreach (var r in PQ_List)
                    {
                        row[q] = r[i * 2];
                        q++;
                        row[q] = r[i * 2 + 1];
                        q++;
                    }
                    dataRaports.Rows.Add(row);
                }

                _mainForm.RaportsData(dataRaports);
                PQ_List = null;
                dataRaports = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    
        private void _mainForm_ReplaceDDMM(object sender, EventArgs e)
        {
            _dataManager.DD_MM(_mainForm.GetReplace());
        }
    }
}
