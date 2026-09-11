using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App_RumahSakit
{
    class KF
    {
        public static void UntukFormBilal(Form FormApa, Panel PanelApa)
        {
            PanelApa.Controls.Clear();
            FormApa.FormBorderStyle = FormBorderStyle.None;
            FormApa.Dock = DockStyle.Fill;
            PanelApa.Controls.Add(FormApa);
            FormApa.Visible = true;


        }
    }
}
