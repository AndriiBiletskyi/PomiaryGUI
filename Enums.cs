using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomiaryGUI
{
    enum FormStates
    {
        start,
        power, current, voltage, cos, equipments,
        daily, weekly, monthly, annual,
        settings
    }

    enum Raport
    {
        day, week, month, year
    }
}
