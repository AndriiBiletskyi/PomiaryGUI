using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
//-----------------------------------------------------------------------------------

namespace PomiaryGUI
{
    public class Presenter
    {
        private readonly IMainForm _mainForm;
        private readonly IDataManager _dataManager;        

        public Presenter(IMainForm mainForm, IDataManager dataManager)
        {
            _mainForm = mainForm;
            _dataManager = dataManager;

            _mainForm.ButChartsPowerClick += new EventHandler(MainForm_ButPowerClick);
            _mainForm.ButChartsCurrentClick += new EventHandler(MainForm_ButCurrentClick);
            _mainForm.ButChartsVoltageClick += new EventHandler(MainForm_ButVoltageClick);
            _mainForm.ButChartsCosClick += new EventHandler(MainForm_ButCosClick);

            _mainForm.ButRaportsDailyClick += new EventHandler(MainForm_ButRaportsDailyClick);
            _mainForm.ButRaportsWeeklyClick += new EventHandler(MainForm_ButRaportsWeeklyClick);
            _mainForm.ButRaportsMonthlyClick += new EventHandler(MainForm_ButRaportsMonthlyClick);
            _mainForm.ButRaportsAnnualClick += new EventHandler(MainForm_ButRaportsAnnualClick);

            _mainForm.ButCloseClick += new EventHandler(MainForm_ButCloseClick);

            _mainForm.ButChartsEquipmentsClick += new EventHandler(MainForm_ButEquClick);

            _mainForm.ChangeConnect += new EventHandler(MainForm_ChangeConnect);
            _mainForm.ReplaceDDMM += new EventHandler(MainForm_ReplaceDDMM);
      
        }

        private void MainForm_ButEquClick(object sender, EventArgs e)
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

        private void MainForm_ButCloseClick(object sender, EventArgs e)
        {
            _dataManager.SqlConnectionClose();
            _mainForm.FormClose();
        }

        private void MainForm_ButCosClick(object sender, EventArgs e)
        {
            _mainForm.powerchart(_dataManager.GetEquData(_mainForm.PowerEquNumber, _mainForm.PowerGetDateFrom(),
                                                            _mainForm.PowerGetDateTo()), "cos");
        }

        private void MainForm_ButVoltageClick(object sender, EventArgs e)
        {
            _mainForm.powerchart(_dataManager.GetEquData(_mainForm.PowerEquNumber, _mainForm.PowerGetDateFrom(),
                                                            _mainForm.PowerGetDateTo()), "voltage");
        }

        private void MainForm_ButCurrentClick(object sender, EventArgs e)
        {
            _mainForm.powerchart(_dataManager.GetEquData(_mainForm.PowerEquNumber, _mainForm.PowerGetDateFrom(),
                                                            _mainForm.PowerGetDateTo()), "current");
        }

        private void MainForm_ButPowerClick(object sender, EventArgs e)
        {
            _mainForm.powerchart(_dataManager.GetEquData(_mainForm.PowerEquNumber, _mainForm.PowerGetDateFrom(),
                                                            _mainForm.PowerGetDateTo()), "power");
        }

        private void MainForm_ChangeConnect(object sender, EventArgs e)
        {
            _dataManager.SetConnection(_mainForm.SettingsGetDataConnection());
        }

        private void MainForm_ButRaportsDailyClick(object sender, EventArgs e)
        {
            Raport_data(Raport.day);
        }

        private void MainForm_ButRaportsWeeklyClick(object sender, EventArgs e)
        {
            Raport_data(Raport.week);
        }

        private void MainForm_ButRaportsMonthlyClick(object sender, EventArgs e)
        {
            Raport_data(Raport.month);
        }

        private void MainForm_ButRaportsAnnualClick(object sender, EventArgs e)
        {
            Raport_data(Raport.year);
        }

        private void Raport_data(Raport step)
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
                if (step == Raport.day)
                {
                    dateTime = dateTimeFrom.AddHours(1.0);
                    if ((dateTimeTo - dateTimeFrom).TotalDays > 7) dateTimeTo = dateTimeFrom.AddDays(7.0);
                }
                else if (step == Raport.week)
                {
                    dateTime = dateTimeFrom.AddDays(1.0);
                    if ((dateTimeTo - dateTimeFrom).TotalDays > 7 * 4) dateTimeTo = dateTimeFrom.AddDays(7.0 * 4);
                }
                else if (step == Raport.month)
                {
                    dateTime = dateTimeFrom.AddDays(1.0);
                    if ((dateTimeTo - dateTimeFrom).TotalDays > 31 * 4) dateTimeTo = dateTimeFrom.AddDays(31 * 4);
                }
                else if (step == Raport.year)
                {
                    dateTime = dateTimeFrom.AddMonths(1);
                }

                time_day.Add(dateTimeFrom);
                while (dateTime < dateTimeTo)
                {
                    time_day.Add(dateTime);
                    if (step == Raport.day) dateTime = dateTime.AddHours(1.0);
                    else if (step == Raport.week) dateTime = dateTime.AddDays(1.0);
                    else if (step == Raport.month) dateTime = dateTime.AddDays(1.0);
                    else if (step == Raport.year) dateTime = dateTime.AddMonths(1);
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
    
        private void MainForm_ReplaceDDMM(object sender, EventArgs e)
        {
            _dataManager.DD_MM(_mainForm.GetReplace());
        }
    }
}
