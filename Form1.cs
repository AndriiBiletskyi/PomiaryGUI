using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveCharts.WinForms;
using Brushes = System.Windows.Media.Brushes;


namespace PomiaryGUI
{
    public interface IMainForm
    {
        #region events
        event EventHandler ButChartsClick;
        event EventHandler ButChartsPowerClick;
        event EventHandler ButChartsCurrentClick;
        event EventHandler ButChartsVoltageClick;
        event EventHandler ButChartsCosClick;
        event EventHandler ButChartsEquipmentsClick;

        event EventHandler ButRaportsClick;
        event EventHandler ButRaportsDailyClick;
        event EventHandler ButRaportsWeeklyClick;
        event EventHandler ButRaportsMonthlyClick;
        event EventHandler ButRaportsAnnualClick;

        event EventHandler ButSettingsClick;
        event EventHandler ButCloseClick;

        event EventHandler LangComboBoxTextChanged;
        #endregion

        void FormClose();
        List<List<string>> AllEquipmentsList { get; set; }
        /*
            number of equgroup (from 1001), 
            name of equgroup, 
            number of equ (RTU)(from 1), 
            name of equ
        */

        #region Equipments
        //string EquGroup { get; }
        //string EquEquipments { get; }
        //List<string> GroupsList { get; set; }
        List<string> EquList { get; }
        void EquipmentsFill(DataTable dataTable);
        #endregion

        #region Power
        void powerchart(DataTable tb, string _mode);
        DateTime PowerGetDateFrom();
        DateTime PowerGetDateTo();
        int PowerEquNumber{get; set;}
        #endregion

        #region Raports
        void RaportsData(DataTable dataTable);
        DateTime RaportsGetDateFrom();
        DateTime RaportsGetDateTo();
        #endregion


        #region Settings
        List<string> SettingsGetDataConnection();
        event EventHandler ChangeConnect;
        event EventHandler ReplaceDDMM;
        bool GetReplace();
        #endregion
    }

    public partial class MainForm : Form, IMainForm
    {
        EquControl PanelEqu = new EquControl();
        
        Power PanelPower = new Power("power");
        List<string> equlist = new List<string>();
        List<InstantaneousValues> inst = new List<InstantaneousValues>();
        int timeTick = 0;
        bool _init = false;
        Raports PanelRaport = new Raports();

        Settings PanelSettings = new Settings();

        public MainForm()
        {
            InitializeComponent();

            #region Buttons events
            butCharts.Click += new EventHandler(butCharts_Click);
            butChartsPower.Click += new EventHandler(butChartsPower_Click);
            butChartsCurrent.Click += new EventHandler(butChartsCurrent_Click);
            butChartsVoltage.Click += new EventHandler(butChartsVoltage_Click);
            butChartsCos.Click += new EventHandler(butChartsCos_Click);
            butChartsEquipments.Click += new EventHandler(butChartsEquipments_Click);

            butRaports.Click += new EventHandler(butRaports_Click);
            butRaportsDaily.Click += new EventHandler(butRaportsDaily_Click);
            butRaportsWeekly.Click += new EventHandler(butRaportsWeekly_Click);
            butRaportsMonthly.Click += new EventHandler(butRaportsMonthly_Click);
            butRaportsAnnual.Click += new EventHandler(butRaportsAnnual_Click);
            butSettings.Click += new EventHandler(butSettings_Click);
            #endregion

            LangComboBox.SelectedValueChanged += new EventHandler(LangComboBox_TextChanged);
        
            #region Equipment panel events
            //PanelEqu.EquComboBoxTextChanged += new EventHandler(_EquComboBoxTextChanged);
            //PanelEqu.GroupsComboBoxTextChanged += new EventHandler(_GroupsComboBoxTextChanged);
            #endregion

            #region Power panel events
            PanelPower.ButtonShowClick += new EventHandler(_ButtonShowClick);

            #endregion

            #region Raports panel events
            PanelRaport.ButtonShowClick += new EventHandler(_ButtonShowClickRaport);

            #endregion

            #region Settings events
            PanelSettings.ButtonConnectClick += new EventHandler(Change_Connect);
            PanelSettings.ReplaceDDMM += new EventHandler(Replace_DD_MM);
            #endregion

            //equlist.Add("1");
            //equlist.Add("2");
            //equlist.Add("3");
            //equlist.Add("4");
            //equlist.Add("5");
            //equlist.Add("6");
            //equlist.Add("7");
            //equlist.Add("8");
            //equlist.Add("9");
            //equlist.Add("10");
            //equlist.Add("11");
            //equlist.Add("12");
            //foreach (var c in equlist)
            //{
            //    for (int i = 0; i < 4; i++)
            //    {
            //        inst.Add(new InstantaneousValues());
            //        inst.Add(new InstantaneousValues());
            //        inst.Add(new InstantaneousValues());
            //        inst.Add(new InstantaneousValues());
            //    }
            //}
            //foreach (InstantaneousValues c in inst)
            //{
            //    c.Visible = false;
            //    PanelEqu.Controls.Add(c);
            //}
        }

        #region FormBL

        enum FormStates
        {
            start,
            power, current, voltage, cos, equipments,
            daily, weekly, monthly, annual,
            settings
        }
        FormStates formStates = FormStates.start;
        
        #region Buttons
        private void MarkBottomBut(Point loc, Size size)
        {
            panelButtonMark.Location = new Point(loc.X, loc.Y + size.Height - panelButtonMark.Height);
            panelButtonMark.Width = size.Width;
            panelButtonMark.BringToFront();
        }

        private void MarkBottomBut(Point loc, int width, int hieght)
        {
            panelButtonMark.Location = loc;
            panelButtonMark.Width = width;
            panelButtonMark.Height = hieght;
        }

        private void MarkBottomBut(Point loc, int width)
        {
            panelButtonMark.Location = loc;
            panelButtonMark.Width = width;
            panelButtonMark.Height = 3;
        }

        private void MarkBottomBut(int x, int y, int width)
        {
            panelButtonMark.Location = new Point(x, y);
            panelButtonMark.Width = width;
            panelButtonMark.Height = 3;
        }

        private void HideButtonsCharts()
        {
            butChartsPower.Visible = false;
            butChartsCurrent.Visible = false;
            butChartsVoltage.Visible = false;
            butChartsCos.Visible = false;
            butChartsEquipments.Visible = false;
        }

        private void HideButtonsRaports()
        {
            butRaportsDaily.Visible = false;
            butRaportsWeekly.Visible = false;
            butRaportsMonthly.Visible = false;
            butRaportsAnnual.Visible = false;
        }

        private void ShowButtonsCharts()
        {
            butChartsPower.Visible = true;
            butChartsCurrent.Visible = true;
            butChartsVoltage.Visible = true;
            butChartsCos.Visible = true;
            butChartsEquipments.Visible = true;
        }

        private void ShowButtonsRaports()
        {
            butRaportsDaily.Visible = true;
            butRaportsWeekly.Visible = true;
            butRaportsMonthly.Visible = true;
            butRaportsAnnual.Visible = true;
        }

        private void ButtonsRightPosition(Button butup,Button but)
        {
            but.Location = new Point(panelButtons.Width - but.Width, butup.Location.Y + butup.Height);
        }
        #endregion

        #region EquPanel
        
        private void _EquComboBoxTextChanged(object sender, EventArgs e)
        {
            if (ButChartsEquipmentsClick != null) ButChartsEquipmentsClick(this, EventArgs.Empty);
        }

        private void _GroupsComboBoxTextChanged(object sender, EventArgs e)
        {
            if (ButChartsEquipmentsClick != null) ButChartsEquipmentsClick(this, EventArgs.Empty);
        }

        private void EquPanelShow()
        {
            PanelEqu.Visible = true;
            PanelReSize(PanelEqu);
            
            PanelEqu.Refresh();
            
        }

        private void EquPanelHide()
        {
            PanelEqu.Visible = false;
        }

        #endregion

        #region PowerPanel

        private void _ButtonShowClick(object sender, EventArgs e)
        {
            Loading.Show(this);
            if(formStates== FormStates.power)
            {
                if (ButChartsPowerClick != null) ButChartsPowerClick(this, EventArgs.Empty);
            }else if(formStates == FormStates.current)
            {
                if (ButChartsCurrentClick != null) ButChartsCurrentClick(this, EventArgs.Empty);
            }
            else if (formStates == FormStates.voltage)
            {
                if (ButChartsVoltageClick != null) ButChartsVoltageClick(this, EventArgs.Empty);
            }
            else if (formStates == FormStates.cos)
            {
                if (ButChartsCosClick != null) ButChartsCosClick(this, EventArgs.Empty);
            }

        }

        private void PowerPanelShow()
        {
            PanelPower.Visible = true;
            PanelReSize(PanelPower);

            PanelPower.Refresh();

        }

        private void PowerPanelHide()
        {
            PanelPower.Visible = false;
        }

        #endregion

        #region Raports

        private void _ButtonShowClickRaport(object sender, EventArgs e)
        {
            if (formStates == FormStates.daily)
            {
                if (ButRaportsDailyClick != null) ButRaportsDailyClick(this, EventArgs.Empty);
            }
            else if (formStates == FormStates.weekly)
            {
                if (ButRaportsWeeklyClick != null) ButRaportsWeeklyClick(this, EventArgs.Empty);
            }
            else if (formStates == FormStates.monthly)
            {
                if (ButRaportsMonthlyClick != null) ButRaportsMonthlyClick(this, EventArgs.Empty);
            }
            else if (formStates == FormStates.annual)
            {
                if (ButRaportsAnnualClick != null) ButRaportsAnnualClick(this, EventArgs.Empty);
            }

        }

        private void RaportsPanelShow()
        {
            PanelRaport.Visible = true;
            PanelReSize(PanelRaport);

            PanelRaport.Refresh();
        }

        private void RaportsPanelHide()
        {
            PanelRaport.Visible = false;
        }

        #endregion

        #region SettingsPanel
        private void SettingsPanelShow()
        {
            PanelSettings.Visible = true;
            PanelReSize(PanelSettings);

            PanelSettings.Refresh();

        }

        private void SettingsPanelHide()
        {
            PanelSettings.Visible = false;
        }
        #endregion

        private void PanelReSize(Control panel)
        {
            panel.Size = new Size(this.Width - panelButtons.Width, 
                                  this.Height - panelHead.Height - panelBottom.Height);
        }

        private void AllPanelsHide()
        {
            PanelEqu.Visible = false;
            PanelPower.Visible = false;
            PanelSettings.Visible = false;
            PanelRaport.Visible = false;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        #endregion

        #region EventForwarding
        private void butCharts_Click(object sender, EventArgs e)
        {
            if (ButChartsClick != null) ButChartsClick(this, EventArgs.Empty);

            if (butChartsPower.Visible)
            {
                HideButtonsCharts();
                HideButtonsRaports();
                ButtonsRightPosition(butCharts, butRaports);
                ButtonsRightPosition(butRaports, butSettings);
                MarkBottomBut(((Button)sender).Location, ((Button)sender).Size);
            }
            else
            {
                HideButtonsRaports();
                ShowButtonsCharts();
                ButtonsRightPosition(butCharts, butChartsPower);
                ButtonsRightPosition(butChartsPower, butChartsCurrent);
                ButtonsRightPosition(butChartsCurrent, butChartsVoltage);
                ButtonsRightPosition(butChartsVoltage, butChartsCos);
                ButtonsRightPosition(butChartsCos, butChartsEquipments);
                ButtonsRightPosition(butChartsEquipments, butRaports);
                ButtonsRightPosition(butRaports, butSettings);
                if(formStates==FormStates.power) MarkBottomBut(butChartsPower.Location, butChartsPower.Size);
                else if (formStates == FormStates.current) MarkBottomBut(butChartsCurrent.Location, butChartsCurrent.Size);
                else if (formStates == FormStates.voltage) MarkBottomBut(butChartsVoltage.Location, butChartsVoltage.Size);
                else if (formStates == FormStates.cos) MarkBottomBut(butChartsCos.Location, butChartsCos.Size);
                else if (formStates == FormStates.equipments) MarkBottomBut(butChartsEquipments.Location, butChartsEquipments.Size);
                else MarkBottomBut(((Button)sender).Location, ((Button)sender).Size);
            }
        }

        private void butChartsPower_Click(object sender, EventArgs e)
        {
            Loading.Show(this);
            if (ButChartsPowerClick != null) ButChartsPowerClick(this, EventArgs.Empty);
            
            if (formStates != FormStates.power)
            {
                AllPanelsHide();
                PowerPanelShow();
                formStates = FormStates.power;
            }
            MarkBottomBut(((Button)sender).Location, ((Button)sender).Size);
            
        }

        private void butChartsCurrent_Click(object sender, EventArgs e)
        {
            Loading.Show(this);
            if (ButChartsCurrentClick != null) ButChartsCurrentClick(this, EventArgs.Empty);
           
            if (formStates != FormStates.current)
            {
                AllPanelsHide();
                //CurrentPanelShow();
                PowerPanelShow();
                formStates = FormStates.current;
            }
            MarkBottomBut(((Button)sender).Location, ((Button)sender).Size);
        }

        private void butChartsVoltage_Click(object sender, EventArgs e)
        {
            Loading.Show(this);
            if (ButChartsVoltageClick != null) ButChartsVoltageClick(this, EventArgs.Empty);
            
            if (formStates != FormStates.voltage)
            {
                AllPanelsHide();
                //VoltagePanelShow();
                PowerPanelShow();
                formStates = FormStates.voltage;
            }
            MarkBottomBut(((Button)sender).Location, ((Button)sender).Size);
        }

        private void butChartsCos_Click(object sender, EventArgs e)
        {
            Loading.Show(this);
            if (ButChartsCosClick != null) ButChartsCosClick(this, EventArgs.Empty);
           
            if (formStates != FormStates.cos)
            {
                AllPanelsHide();
                //CosPanelShow();
                PowerPanelShow();
                formStates = FormStates.cos;
            }
            MarkBottomBut(((Button)sender).Location, ((Button)sender).Size);
        }

        private void butChartsEquipments_Click(object sender, EventArgs e)
        {
            if (ButChartsEquipmentsClick != null) ButChartsEquipmentsClick(this, EventArgs.Empty);
            
            if (formStates != FormStates.equipments)
            {
                AllPanelsHide();
                EquPanelShow();
                formStates = FormStates.equipments;
            }
            MarkBottomBut(((Button)sender).Location, ((Button)sender).Size);
        }

        private void butRaports_Click(object sender, EventArgs e)
        {
            if (ButRaportsClick != null) ButRaportsClick(this, EventArgs.Empty);
            
            if (butRaportsDaily.Visible)
            {
                HideButtonsCharts();
                HideButtonsRaports();
                ButtonsRightPosition(butCharts, butRaports);
                ButtonsRightPosition(butRaports, butSettings);
                MarkBottomBut(((Button)sender).Location, ((Button)sender).Size);
            }
            else
            {
                HideButtonsCharts();
                ShowButtonsRaports();
                ButtonsRightPosition(butCharts, butRaports);
                ButtonsRightPosition(butRaports, butRaportsDaily);
                ButtonsRightPosition(butRaportsDaily, butRaportsWeekly);
                ButtonsRightPosition(butRaportsWeekly, butRaportsMonthly);
                ButtonsRightPosition(butRaportsMonthly, butRaportsAnnual);
                ButtonsRightPosition(butRaportsAnnual, butSettings);
                if(formStates==FormStates.daily) MarkBottomBut(butRaportsDaily.Location, butRaportsDaily.Size);
                else if (formStates == FormStates.weekly) MarkBottomBut(butRaportsWeekly.Location, butRaportsWeekly.Size);
                else if (formStates == FormStates.monthly) MarkBottomBut(butRaportsMonthly.Location, butRaportsMonthly.Size);
                else if (formStates == FormStates.annual) MarkBottomBut(butRaportsAnnual.Location, butRaportsAnnual.Size);
                else MarkBottomBut(((Button)sender).Location, ((Button)sender).Size);
            }
        }

        private void butRaportsDaily_Click(object sender, EventArgs e)
        {
            if (ButRaportsDailyClick != null) ButRaportsDailyClick(this, EventArgs.Empty);
            
            if (formStates != FormStates.daily)
            {
                AllPanelsHide();
                RaportsPanelShow();
                formStates = FormStates.daily;
            }
            MarkBottomBut(((Button)sender).Location, ((Button)sender).Size);
        }

        private void butRaportsWeekly_Click(object sender, EventArgs e)
        {
            if (ButRaportsWeeklyClick != null) ButRaportsWeeklyClick(this, EventArgs.Empty);
            
            if (formStates != FormStates.weekly)
            {
                AllPanelsHide();
                RaportsPanelShow();
                formStates = FormStates.weekly;
            }
            MarkBottomBut(((Button)sender).Location, ((Button)sender).Size);
        }

        private void butRaportsMonthly_Click(object sender, EventArgs e)
        {
            if (ButRaportsMonthlyClick != null) ButRaportsMonthlyClick(this, EventArgs.Empty);
            
            if (formStates != FormStates.monthly)
            {
                AllPanelsHide();
                RaportsPanelShow();
                formStates = FormStates.monthly;
            }
            MarkBottomBut(((Button)sender).Location, ((Button)sender).Size);
        }

        private void butRaportsAnnual_Click(object sender, EventArgs e)
        {
            if (ButRaportsAnnualClick != null) ButRaportsAnnualClick(this, EventArgs.Empty);
            
            if (formStates != FormStates.annual)
            {
                AllPanelsHide();
                RaportsPanelShow();
                formStates = FormStates.annual;
            }
            MarkBottomBut(((Button)sender).Location, ((Button)sender).Size);
        }

        private void butSettings_Click(object sender, EventArgs e)
        {
            if (ButSettingsClick != null) ButSettingsClick(this, EventArgs.Empty);
            HideButtonsCharts();
            HideButtonsRaports();
            ButtonsRightPosition(butCharts, butRaports);
            ButtonsRightPosition(butRaports, butSettings);

            if (formStates != FormStates.settings)
            {
                AllPanelsHide();
                SettingsPanelShow();
                formStates = FormStates.settings;
            }

            MarkBottomBut(((Button)sender).Location, ((Button)sender).Size);
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            if (ButCloseClick != null) ButCloseClick(this, EventArgs.Empty);
        }

        private void LangComboBox_TextChanged(object sender, EventArgs e)
        {
            if (LangComboBoxTextChanged != null) LangComboBoxTextChanged(this, EventArgs.Empty);
            if (LangComboBox.Text == "EN") panelLang.BackgroundImage = Properties.Resources.GB;
            if (LangComboBox.Text == "PL") panelLang.BackgroundImage = Properties.Resources.PL;
            if (LangComboBox.Text == "DE") panelLang.BackgroundImage = Properties.Resources.DE;
            if (LangComboBox.Text == "UA") panelLang.BackgroundImage = Properties.Resources.UA;
            if (LangComboBox.Text == "ES") panelLang.BackgroundImage = Properties.Resources.ES;
            if (LangComboBox.Text == "HU") panelLang.BackgroundImage = Properties.Resources.HU;
        }

        private void Change_Connect(object sender, EventArgs e)
        {
            if (ChangeConnect != null) ChangeConnect(this, EventArgs.Empty);
        }

        private void Replace_DD_MM(object sender, EventArgs e)
        {
            if (ReplaceDDMM != null) ReplaceDDMM(this, EventArgs.Empty);
        }
        #endregion

        #region IMainForm

        public List<List<string>> AllEquipmentsList
        {
            get
            {
               return AllEquipmentsList;
            }

            set
            {
                AllEquipmentsList = value;
            }
        }

        public void EquipmentsFill(DataTable dataTable)
        {
            try
            {                
                if (dataTable != null &&
                    dataTable.Columns.Contains("Nazwa_urzadzenia") &&
                    dataTable.Columns.Contains("Status") &&
                    dataTable.Columns.Contains("P") &&
                    dataTable.Columns.Contains("P_L1") &&
                    dataTable.Columns.Contains("P_L2") &&
                    dataTable.Columns.Contains("P_L3"))
                {
                    Point p = new Point(((PanelEqu.Width/4) - 200)/2, 40);
                    int step = PanelEqu.Width / 4;
                    int q = 0;
                    for(int i = 0; i < dataTable.Rows.Count; i++)
                    { 
                        if (!dataTable.Rows[i].IsNull("Nazwa_urzadzenia") &&
                            !dataTable.Rows[i].IsNull("Status") &&
                            !dataTable.Rows[i].IsNull("P") &&
                            !dataTable.Rows[i].IsNull("P_L1") &&
                            !dataTable.Rows[i].IsNull("P_L2") &&
                            !dataTable.Rows[i].IsNull("P_L3")) 
                        {
                            for(int r = 0; r < 4; r++)
                            {
                                string quer = "";
                                if (r == 0) quer = "P";
                                else if(r==1) quer = "P_L1";
                                else if (r == 2) quer = "P_L2";
                                else if (r == 3) quer = "P_L3";

                                if (inst.Count >= (q + 1))
                                {
                                    inst.ElementAt(q).Status = Convert.ToBoolean(dataTable.Rows[i]["Status"]);
                                    inst.ElementAt(q).Name = Convert.ToString(dataTable.Rows[i]["Nazwa_urzadzenia"]) + " - " + quer + ", kW";
                                    if (inst.ElementAt(q).Status == false)
                                    {
                                        inst.ElementAt(q).Value = 0;
                                    }
                                    else if (!dataTable.Rows[i].IsNull(quer))
                                    {
                                        inst.ElementAt(q).Value = Convert.ToSingle(dataTable.Rows[i][quer]) / 1000;
                                    }
                                    else inst.ElementAt(q).Value = 0;

                                    inst.ElementAt(q).Max = 100;
                                    inst.ElementAt(q).Min = 0;
                                    if (!inst.ElementAt(q).Visible)
                                    {
                                        inst.ElementAt(q).Size = new Size(200, 200);
                                        inst.ElementAt(q).Location = new Point(p.X + step * (q % 4), p.Y + 220 * (q / 4));
                                        inst.ElementAt(q).Visible = true;
                                    }
                                }
                                q++;
                            }
                        }
                        else
                        {
                            for (int r = 0; r < 4; r++)
                            {
                                string quer = "";
                                if (r == 0) quer = "P";
                                else if (r == 1) quer = "P_L1";
                                else if (r == 2) quer = "P_L2";
                                else if (r == 3) quer = "P_L3";

                                if (inst.Count >= (q + 1))
                                {
                                    inst.ElementAt(q).Status = false;
                                    inst.ElementAt(q).Name = "Unknow" + " - " + quer + ", kW";
                                    inst.ElementAt(q).Value = 0;
                                    inst.ElementAt(q).Max = 100;
                                    inst.ElementAt(q).Min = 0;
                                    if (!inst.ElementAt(q).Visible)
                                    {
                                        inst.ElementAt(q).Size = new Size(200, 200);
                                        inst.ElementAt(q).Location = new Point(p.X + step * (q % 4), p.Y + 220 * (q / 4));
                                        inst.ElementAt(q).Visible = true;
                                    }
                                }
                                q++;
                            }
                        }
                        
                    }
                }                
            }
            catch(Exception ex)
            {
                MessageBox.Show("Form_EquFill \n" + ex.ToString());
            }
        }

        public List<string> EquList
        {
            get
            {
                return equlist;
            }
        }

        //public string EquGroup
        //{
        //    get
        //    {
        //        return PanelEqu.GroupsComboBoxText();
        //    }
        //}

        //public string EquEquipments
        //{
        //    get
        //    {
        //        return PanelEqu.EquComboBoxText();
        //    }
        //}

        //public List<string> EquipmentsList
        //{
        //    get
        //    {
        //        var list = new List<string>();
        //        foreach (var it in equlist)
        //        {
        //            list.Add(it.ToString());
        //        }

        //        return list;

        //        //return EquipmentsList;
        //    }

        //    set
        //    {
        //        PanelEqu.EquComboBoxClear();
        //        foreach (var it in value)
        //        {
        //            PanelEqu.EquComboBoxAdd(it);
        //        }

        //    }
        //}

        //public List<string> GroupsList
        //{
        //    get
        //    {
        //        var list = new List<string>();
        //        foreach (var it in PanelEqu.GroupscomboBoxItems())
        //        {
        //            list.Add(it.ToString());
        //        }

        //        return list;
        //    }

        //    set
        //    {
        //        PanelEqu.GroupsComboBoxClear();
        //        foreach (var it in value)
        //        {
        //            PanelEqu.GroupsComboBoxAdd(it);
        //        }

        //    }
        //}

        public void FormClose()
        {
            MainForm.ActiveForm.Close();
        }

        #region Power
        public void powerchart(DataTable tb, string _mode)
        {
            PanelPower.powerchart(tb,_mode);
        }

        public DateTime PowerGetDateFrom()
        {
            return PanelPower.GetDateFrom();
        }

        public DateTime PowerGetDateTo()
        {
            return PanelPower.GetDateTo();
        }

        public int PowerEquNumber
        {
            get
            {
                return PanelPower.GetEquNumber();
            }
            set
            {
                PanelPower.SetEquNumber(value);
            }
        }
        #endregion
        #region Raports
        public void RaportsData(DataTable dataTable)
        {
            PanelRaport.SetData(dataTable);
        }
        
        public DateTime RaportsGetDateFrom()
        {
            return PanelRaport.GetDateFrom();
        }

        public DateTime RaportsGetDateTo()
        {
            return PanelRaport.GetDateTo();
        }
        #endregion
        #region Settings
        public List<string> SettingsGetDataConnection()
        {
            return PanelSettings.GetDataConnection();
        }
        public bool GetReplace()
        {
            return PanelSettings.GetReplace();
        }
        #endregion
        #region events
        public event EventHandler ButChartsClick;
        public event EventHandler ButChartsPowerClick;
        public event EventHandler ButChartsCurrentClick;
        public event EventHandler ButChartsVoltageClick;
        public event EventHandler ButChartsCosClick;
        public event EventHandler ButChartsEquipmentsClick;
                
        public event EventHandler ButRaportsClick;
        public event EventHandler ButRaportsDailyClick;
        public event EventHandler ButRaportsWeeklyClick;
        public event EventHandler ButRaportsMonthlyClick;
        public event EventHandler ButRaportsAnnualClick;

        public event EventHandler ButSettingsClick;
        public event EventHandler ButCloseClick;

        public event EventHandler LangComboBoxTextChanged;

        public event EventHandler ChangeConnect;
        public event EventHandler ReplaceDDMM;
        #endregion
        #endregion

        #region FormAction

        private void btMin_Click(object sender, EventArgs e)
        {
            MainForm.ActiveForm.WindowState = FormWindowState.Minimized;
        }
        
        private void panelHead_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            System.Drawing.Rectangle rect = Screen.GetWorkingArea(this);
            if (this.MaximizedBounds != Screen.GetWorkingArea(this))
            {
                this.MaximizedBounds = Screen.GetWorkingArea(this);
                this.WindowState = FormWindowState.Minimized;
                this.WindowState = FormWindowState.Maximized;
                MainForm.ActiveForm.Refresh();
            }            
        }

        #endregion

        #region MainFormDragging
        private bool isDragging = false;
        private int x, y;
        private void panelHead_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            x = e.X;
            y = e.Y;
        }

        private void panelHead_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                MainForm.ActiveForm.Location = new Point(MainForm.ActiveForm.Location.X + e.X - x, 
                                                         MainForm.ActiveForm.Location.Y + e.Y - y);
            }
        }

        private void timerDataTime_Tick(object sender, EventArgs e)
        {
            labelDataTime.Text = DateTime.Now.ToString();

            if (timeTick % 10 == 0)
            {
                if (formStates == FormStates.equipments) if (ButChartsEquipmentsClick != null) ButChartsEquipmentsClick(this, EventArgs.Empty);
                if (formStates == FormStates.power)
                {
                   // int i = PanelPower.GetEquNumber() - 1;
                    //i++;
                    //if (i > 6) i = 0;
                    //PanelPower.SetEquNumber(0);
                }
                
            }

            timeTick++;
            if(timeTick == 2 && !_init)
            {
                
            }
        }

        private void panelHead_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            _init = true;

            #region PanelEqu
            MainForm.ActiveForm.Controls.Add(PanelEqu);
            this.PanelEqu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right) | System.Windows.Forms.AnchorStyles.Bottom));
            PanelEqu.Visible = false;
            PanelEqu.Location = new Point(panelButtons.Width, panelHead.Height);
            PanelReSize(PanelEqu);
            #endregion

            #region PanelPower
            MainForm.ActiveForm.Controls.Add(PanelPower);
            this.PanelPower.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right) | System.Windows.Forms.AnchorStyles.Bottom));
            PanelPower.Visible = false;
            PanelPower.Location = new Point(panelButtons.Width, panelHead.Height);
            PanelReSize(PanelPower);
            #endregion

            #region PanelRaports
            MainForm.ActiveForm.Controls.Add(PanelRaport);
            this.PanelRaport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right) | System.Windows.Forms.AnchorStyles.Bottom));
            PanelRaport.Visible = false;
            PanelRaport.Location = new Point(panelButtons.Width, panelHead.Height);
            PanelReSize(PanelRaport);
            #endregion

            #region PanelSettings
            MainForm.ActiveForm.Controls.Add(PanelSettings);
            this.PanelEqu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right) | System.Windows.Forms.AnchorStyles.Bottom));
            PanelSettings.Visible = false;
            PanelSettings.Location = new Point(panelButtons.Width, panelHead.Height);
            PanelReSize(PanelSettings);

            LangComboBox.SelectedItem = LangComboBox.Items[0];
            #endregion

            MainFormInit();

            HideButtonsCharts();
            HideButtonsRaports();
            ButtonsRightPosition(butCharts, butRaports);
            ButtonsRightPosition(butRaports, butSettings);
        }

        private void MainFormInit()
        {
            panelButtons.Visible = true;
        }

        #endregion

    }
}
