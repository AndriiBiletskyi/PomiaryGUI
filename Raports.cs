using System;
using System.Windows.Forms;
using System.Data;
using System.Drawing;

namespace PomiaryGUI
{
    public partial class Raports : UserControl
    {
        private DataTable data = new DataTable();
        public event EventHandler ButtonShowClick;

        public Raports()
        {
            InitializeComponent();
            buttonShow.Click += new EventHandler(ButtonShow_Click);
        }

        private void Raports_Load(object sender, EventArgs e)
        {
            HoursFrom.SelectedItem = HoursFrom.Items[0];
            //HoursFrom.Enabled = false;
            HoursTo.SelectedItem = HoursTo.Items[23];
            //HoursTo.Enabled = false;
            MinutesFrom.SelectedItem = MinutesFrom.Items[0];
            MinutesTo.SelectedItem = MinutesTo.Items[59];

            dataGridViewRaports.DefaultCellStyle.Font = new Font("Times New Roman", 16, FontStyle.Bold);
            dataGridViewRaports.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 16, FontStyle.Bold);
            dataGridViewRaports.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewRaports.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewRaports.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridViewRaports.Columns.Clear();
            dataGridViewRaports.Refresh();
        }

        public DateTime GetDateFrom()
        {
            return new DateTime(Convert.ToInt32(DateFrom.Value.Year),
                                Convert.ToInt32(DateFrom.Value.Month),
                                Convert.ToInt32(DateFrom.Value.Day),
                                Convert.ToInt32(HoursFrom.Text),
                                Convert.ToInt32(MinutesFrom.Text),
                                Convert.ToInt32(0));
        }

        public DateTime GetDateTo()
        {
            return new DateTime(Convert.ToInt32(DateTo.Value.Year),
                                Convert.ToInt32(DateTo.Value.Month),
                                Convert.ToInt32(DateTo.Value.Day),
                                Convert.ToInt32(HoursTo.Text),
                                Convert.ToInt32(MinutesTo.Text),
                                Convert.ToInt32(0));
        }

        private void ButtonShow_Click(object sender, EventArgs e)
        {
            if (ButtonShowClick != null) ButtonShowClick(this, EventArgs.Empty);
        }

        public void SetData(DataTable dataTable)
        {
            data.Clear();
            data.Columns.Clear();
            data = dataTable;
            dataGridViewRaports.DataSource = data;
            dataGridViewRaports.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridViewRaports.Refresh();
        }
    }
}
