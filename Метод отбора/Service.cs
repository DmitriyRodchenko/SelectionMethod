using System;
using System.Windows.Forms;

namespace Методотбора
{
    public partial class Service : Form
    {
        public Service()
        {
            InitializeComponent();
        }
        private static int percent = 0;
        private static bool Fcheck = false, rcheck = false, Mcheck = false, PQ = false;
        private static double M = 1.0, PQprecise = 0.00100000000000, Fprecise = 0.00100000000000;
        private int t2 = 0;
        private bool t3 = false, t4 = false, t5 = false, t6 = false;
        private double t7 = 1.0, t8 = 0.00100000000000, t9 = 0.00100000000000;

        public static bool getF
        {
            get { return Fcheck; }
        }
        public static bool getr
        {
            get { return rcheck; }
        }
        public static bool getMcheck
        {
            get { return Mcheck; }
        }
        public static bool getPQ
        {
            get { return PQ; }
        }
        public static double getM
        {
            get { return M; }
        }
        public static int getP
        {
            get { return percent; }
        }
        public static double getFp
        {
            get { return Fprecise; }
        }
        public static double getPQp
        {
            get { return PQprecise; }
        }
        private void numericUpDownM_ValueChanged(object sender, EventArgs e)
        {
            M = (double)numericUpDownM.Value;
        }

        private void numericUpDownPercent_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownPercent.Value != decimal.Floor(numericUpDownPercent.Value))
                numericUpDownPercent.Value = decimal.Floor(numericUpDownPercent.Value);
            percent = (int)numericUpDownPercent.Value;
        }
       
        private void numericUpDownPQprecise_ValueChanged(object sender, EventArgs e)
        {
            PQprecise = (double)numericUpDownPQprecise.Value;
        }

        private void numericUpDownFprecise_ValueChanged(object sender, EventArgs e)
        {
            Fprecise = (double)numericUpDownFprecise.Value;
        }

        private void checkBoxF_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxF.Checked == true)
                Fcheck = true;
            else
                Fcheck = false;
        }

        private void checkBoxR_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxR.Checked == true)
                rcheck = true;
            else
                rcheck = false;
        }

        private void checkBoxM_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxM.Checked == true)
                Mcheck = true;
            else
                Mcheck = false;
        }

        private void checkBoxPQ_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPQ.Checked == true)
                PQ = true;
            else
                PQ = false;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            t2 = percent;
            t3 = Fcheck;
            t4 = rcheck;
            t5 = Mcheck;
            t6 = PQ;
            t7 = M;
            t8 = PQprecise;
            t9 = Fprecise;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            numericUpDownPercent.Value = t2;
            checkBoxF.Checked = t3;
            checkBoxR.Checked = t4;
            checkBoxM.Checked = t5;
            checkBoxPQ.Checked = t6;
            numericUpDownM.Value = (decimal)t7;
            numericUpDownPQprecise.Value = (decimal)t8;
            numericUpDownFprecise.Value = (decimal)t9;
            Close(); 
        }

        private void Clear()
        { 
            numericUpDownPercent.Value = 0;
            checkBoxF.Checked = false; ;
            checkBoxR.Checked = false;
            checkBoxM.Checked = false;
            checkBoxPQ.Checked = false;
            numericUpDownM.Value = 1;
            numericUpDownPQprecise.Value = 0.00100000000000M;
            numericUpDownFprecise.Value = 0.00100000000000M;
        }
        private void ButtonReset_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}
