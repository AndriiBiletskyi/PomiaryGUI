﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PomiaryGUI
{
    public partial class Settings : UserControl
    {
        public event EventHandler ButtonConnectClick;
        public event EventHandler ReplaceDDMM;

        public Settings()
        {
            InitializeComponent();
            butConnect.Click += new EventHandler(ButtonConnect_Click);
            checkBoxTime.CheckStateChanged += new EventHandler(Replace_DD_MM);
        }

        private void ButtonConnect_Click(object sender, EventArgs e)
        {
            if (ButtonConnectClick != null) ButtonConnectClick(this, EventArgs.Empty);
        }

        private void Replace_DD_MM(object sender, EventArgs e)
        {
            if (ReplaceDDMM != null) ReplaceDDMM(this, EventArgs.Empty);
        }

        public List<string> GetDataConnection()
        {
            return new List<string>() { textSerwer.Text, 
                                        textInitialCatalog.Text, 
                                        textUserID.Text,
                                        textPassword.Text
                                      };
        }

        public bool GetReplace()
        {
            return checkBoxTime.Checked;
        }
    }
}
