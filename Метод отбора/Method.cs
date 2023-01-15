using System;
using System.Data;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace Методотбора
{
    public partial class Method : Form
    {
        Service service = new Service();
        private string fileName = string.Empty;
        private bool Saved = true;

        private DataSet SelectionDataSet = null;
        private DataTable SelectionDataTable = null;
        private DataTable ResultsDataTable = null;
        private DataTable FrequenciesDataTable = null;
        private static bool Rcheck = false;

        private static int min = 0, max = 1, intervals = 9;
        public static float Xmin
        {
            get { return min; }
        }
        public static float Xmax
        {
            get { return max; }
        }
        public static bool IsRcheck
        {
            get { return Rcheck; }
        }
       
        private double? x2Statistics = null;
        private double? significancelvl = null;
        private void CreateDataSet()
        {
            SelectionDataTable = new DataTable("SelectionDataTable");
            DataColumn xiColumn = new DataColumn("Xi", typeof(double));
            SelectionDataTable.Columns.Add(xiColumn);

            dataGridView.DataSource = SelectionDataTable;

            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.EditMode = DataGridViewEditMode.EditProgrammatically;

            dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.RowHeadersWidth = 70;

            DataGridViewColumn gridColumn = dataGridView.Columns[0];
            gridColumn.HeaderText = "Значение ξ";
            gridColumn.DefaultCellStyle.Format = "F4";
            gridColumn.SortMode = DataGridViewColumnSortMode.NotSortable;

            ResultsDataTable = new DataTable("ResultsDataTable");

            DataColumn column = new DataColumn("SelectionSize", typeof(int));
            ResultsDataTable.Columns.Add(column);
            column = new DataColumn("LeftX", typeof(int));
            ResultsDataTable.Columns.Add(column);
            column = new DataColumn("RightX", typeof(int));
            ResultsDataTable.Columns.Add(column);
            column = new DataColumn("M", typeof(int));
            ResultsDataTable.Columns.Add(column);
            column = new DataColumn("Precision", typeof(int));
            ResultsDataTable.Columns.Add(column);
            column = new DataColumn("Intervals", typeof(int));
            ResultsDataTable.Columns.Add(column);
            column = new DataColumn("x2Statistics", typeof(double));
            ResultsDataTable.Columns.Add(column);
            column = new DataColumn("Significancelvl", typeof(double));
            ResultsDataTable.Columns.Add(column);

            FrequenciesDataTable = new DataTable("FrequenciesTable");
            DataColumn frequencyColumn = new DataColumn("Frequency", typeof(int));
            FrequenciesDataTable.Columns.Add(frequencyColumn);

            SelectionDataSet = new DataSet("SelectionDataSet");
            SelectionDataSet.Tables.Add(SelectionDataTable);
            SelectionDataSet.Tables.Add(ResultsDataTable);
            SelectionDataSet.Tables.Add(FrequenciesDataTable);
        }
        //Инициализация
        public Method()
        {
            InitializeComponent();
        }
        //Загрузка формы
        private void Method_Load(object sender, EventArgs e)
        {
            CreateDataSet();
            buttonGenerate.Select();
        }
        //Генерирование выборки
        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            min = (int)numericUpDownLeft.Value;
            max = (int)numericUpDownRight.Value;
            if (min == max || min > max)
                return;

            if (Service.getPQ == true)
            {
                if (Calculator.Pcheck() > 1 + Service.getPQp || Calculator.Pcheck() < 1 - Service.getPQp)//Проверка плотности P
                {
                    errorProvider.SetError(buttonGenerate, "Плотность P задана неверно");
                    return;
                }


                if (Calculator.Qcheck() > 1 + Service.getPQp || Calculator.Qcheck() < 1 - Service.getPQp)//Проверка плотности Q
                {
                    errorProvider.SetError(buttonGenerate, "Плотность Q задана неверно");
                    return;
                }
            }
            
            int size = (int)numericUpDownSize.Value;
            if (Service.getMcheck == true && Service.getr == true)         
            {
                Rcheck = true;
                Calculator.generate(SelectionDataTable, size, 0.0, Service.getP, true);
            }
            else if (Service.getMcheck == false && Service.getr == true)   
            {
                Rcheck = true;
                Calculator.generate(SelectionDataTable, size, Service.getM, Service.getP, true);
            }
            else if (Service.getMcheck == true && Service.getr == false)   
                Calculator.generate(SelectionDataTable, size, 0.0, Service.getP, false);
            else                                                                
                Calculator.generate(SelectionDataTable, size, Service.getM, Service.getP, false);

            if (Calculator.GM < 1.0)
            {
                errorProvider.SetError(buttonGenerate, "M < 1");
                return;
            }
            errorProvider.Clear();
            setSaved(false);
        }

        private void plotHistogram()
        {
            if (!Calculator.IsFrequenciesNull)
            {   
                int picWidth = pictureBoxGist.ClientRectangle.Width;
                int picHeight = pictureBoxGist.ClientRectangle.Height;

                if (picWidth < 80 || picHeight < 80)
                    return;

                pictureBoxGist.Image = Calculator.plotHistogram(picWidth, picHeight, (float)numericUpDownLeft.Value, (float)numericUpDownRight.Value, (float)Calculator.GM);
            }
                
        }
        //Вычисление + рисование гистограммы
        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            if (!((int)numericUpDownLeft.Value == Xmin && (int)numericUpDownRight.Value == Xmax))
            {
                errorProvider.SetError(buttonCalculate, "Промежуток задан неверно");
                return;
            }
            else
                errorProvider.Clear();
            if (Calculator.GM < 1.0)
                return;
            int intervals = (int)numericUpDownCount.Value;
            if (dataGridView.Rows.Count != 0)
                setSaved(false);
            if (Service.getF == true)
                Calculator.calculateProbabilities(intervals, true);
            else
                Calculator.calculateProbabilities(intervals, false);
            Calculator.calculateFrequencies(SelectionDataTable, FrequenciesDataTable, intervals);
            x2Statistics = Calculator.calculatex2();
            if (x2Statistics.HasValue)
                textBoxStatX.Text = x2Statistics.Value.ToString("F4");
            else
                textBoxStatX.Clear();

            if (x2Statistics.HasValue && intervals % 2 == 1)
            {
                significancelvl = Calculator.significancelvlxi2(x2Statistics.Value, intervals);
                textBoxP.Text = significancelvl.Value.ToString("F6");
            }
            else
            {
                significancelvl = null;
                textBoxP.Clear();
            }
            plotHistogram();
            int min = (int)numericUpDownLeft.Value, max = (int)numericUpDownRight.Value;
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            plotHistogram();
        }

        private void Method_Resize(object sender, EventArgs e)
        {
            plotHistogram();
        }
        private void setTitle()
        {
            string title = "Метод отбора";
            if (!string.IsNullOrEmpty(fileName) || !Saved)
                title += " — ";
            if (!string.IsNullOrEmpty(fileName))
                title += Path.GetFileName(fileName);
            if (!Saved)
                title += "*";
            this.Text = title;
        }
        private void setSaved(bool saved)
        {
             Saved = saved;
             setTitle(); 
        }

        //Объем выборки 
        private void numericUpDownSize_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownSize.Value != decimal.Floor(numericUpDownSize.Value))
                numericUpDownSize.Value = decimal.Floor(numericUpDownSize.Value);
        }
        //Точность
        private void numericUpDownPrecise_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownPrecise.Value != decimal.Floor(numericUpDownPrecise.Value))
                numericUpDownPrecise.Value = decimal.Floor(numericUpDownPrecise.Value);
            dataGridView.Columns[0].DefaultCellStyle.Format = "F" + numericUpDownPrecise.Value;
        }
        //Число интервалов
        private void numericUpDownCount_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownCount.Value != decimal.Floor(numericUpDownCount.Value))
                numericUpDownCount.Value = decimal.Floor(numericUpDownCount.Value);
            intervals = (int)numericUpDownCount.Value;
        }

        private void numericUpDownLeft_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownLeft.ToString() == "")
                numericUpDownLeft.Value = 0;
            if (numericUpDownLeft.Value != decimal.Floor(numericUpDownLeft.Value))
                numericUpDownLeft.Value = decimal.Floor(numericUpDownLeft.Value);
            if (numericUpDownLeft.Value > numericUpDownRight.Value)
                numericUpDownRight.Value = numericUpDownLeft.Value + 1;
            if (numericUpDownLeft.Value == numericUpDownRight.Value)
                numericUpDownLeft.Value -= 1;
            
        }

        private void numericUpDownRight_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDownRight.ToString() == "")
                numericUpDownRight.Value = 1;
            if (numericUpDownRight.Value != decimal.Floor(numericUpDownRight.Value))
                numericUpDownRight.Value = decimal.Floor(numericUpDownRight.Value);
            if (numericUpDownRight.Value < numericUpDownLeft.Value)
                numericUpDownLeft.Value = numericUpDownRight.Value - 1;
        }

        //Новый 
        private void новыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Saved)
            {
                DialogResult dr = MessageBox.Show("Удалить данные?", "Данные не сохранены",
                   MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (dr == DialogResult.No)
                    return;
            }
            dataGridView.Columns[0].DefaultCellStyle.Format = "F4";
            fileName = string.Empty; 
            numericUpDownSize.Value = 100;
            numericUpDownPrecise.Value = 4;
            numericUpDownCount.Value = 9;
            numericUpDownLeft.Value = 0;
            numericUpDownRight.Value = 1;  
            pictureBoxGist.Image = null;
            x2Statistics = null;
            significancelvl = null;
            SelectionDataSet.Clear();
            ResultsDataTable.Rows.Add();
            Calculator.clear();
            textBoxStatX.Clear();
            textBoxP.Clear();
            errorProvider.Clear();
            setSaved(true);
            service = new Service();
        }
        //Открыть файл, считать данные
        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofDialog = new OpenFileDialog();
            ofDialog.Filter = "XML-файлы|*.xml";
            DialogResult dr = ofDialog.ShowDialog();
            if (dr != DialogResult.OK)
                return;
            fileName = ofDialog.FileName;
            Open();
            buttonCalculate.Select();
        }
        private void Open()
        { 
            SelectionDataSet.Clear();
            try
            {
                SelectionDataSet.ReadXml(fileName);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (ResultsDataTable.Rows.Count == 0)
                ResultsDataTable.Rows.Add();

            DataRow row = ResultsDataTable.Rows[0];

            if (!row.IsNull("SelectionSize"))
                numericUpDownSize.Value = (int)row["SelectionSize"];
            else
                numericUpDownSize.Value = 100;

            if (!row.IsNull("LeftX"))
                numericUpDownLeft.Value = (int)row["LeftX"];
            else
                numericUpDownLeft.Value = 0;

            if (!row.IsNull("RightX"))
                numericUpDownRight.Value = (int)row["RightX"];
            else
                numericUpDownRight.Value = 1;

            if (!row.IsNull("Precision"))
                numericUpDownPrecise.Value = (int)row["Precision"];
            else
                numericUpDownPrecise.Value = 4;

            if (!row.IsNull("Intervals"))
                numericUpDownCount.Value = (int)row["Intervals"];
            else
                numericUpDownCount.Value = 9;

            if (!row.IsNull("x2Statistics"))
                x2Statistics = (double)row["x2Statistics"];
            else
                x2Statistics = null;
            if (x2Statistics.HasValue)
                textBoxStatX.Text = x2Statistics.Value.ToString("F4");
            else
                textBoxStatX.Clear();

            if (!row.IsNull("Significancelvl"))
                significancelvl = (double)row["Significancelvl"];
            else
                significancelvl = null;
            if (significancelvl.HasValue)
                textBoxP.Text = significancelvl.Value.ToString("F6");
            else
                textBoxP.Clear();

            Calculator.clear();
            pictureBoxGist.Image = null;

            Calculator.fillFrequencies(FrequenciesDataTable);
            plotHistogram();
            setSaved(true);
        }
        //Сохранение данных
        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(fileName) || sender == сохранитьКакToolStripMenuItem)
            {
                SaveFileDialog sfDialog = new SaveFileDialog();
                sfDialog.Filter = "XML-файлы|*.xml";
                DialogResult dr = sfDialog.ShowDialog();
                if (dr != DialogResult.OK)
                    return;
                fileName = sfDialog.FileName;
                
            }
            Save();
            buttonCalculate.Select();
        }
        private void Save()
        {
            ResultsDataTable.Clear();
            ResultsDataTable.Rows.Add();

            DataRow row = ResultsDataTable.Rows[0];
            row["SelectionSize"] = numericUpDownSize.Value;
            row["Precision"] = numericUpDownPrecise.Value;
            row["Intervals"] = numericUpDownCount.Value;
            row["LeftX"] = numericUpDownLeft.Value;
            row["RightX"] = numericUpDownRight.Value; 
            if (x2Statistics.HasValue)
                row["x2Statistics"] = x2Statistics.Value;
            else
                row["x2Statistics"] = DBNull.Value;
            if (significancelvl.HasValue)
                row["Significancelvl"] = significancelvl.Value;
            else
                row["Significancelvl"] = DBNull.Value;
            try
            {
                SelectionDataSet.WriteXml(fileName);
                setSaved(true);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Ошибка вывода", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Копировать график в буфер
        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBoxGist.Image == null)
                return;
            try
            {
                Clipboard.SetImage(pictureBoxGist.Image);
            }
            catch
            { }
        }
        //Сохранение гистограммы 
        private void сохранитьГистограммуКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBoxGist.Image == null)
                return;
            SaveFileDialog sfDialog = new SaveFileDialog();
            sfDialog.Filter = "Файлы BMP|*.bmp";
            DialogResult dr = sfDialog.ShowDialog();
            if (dr != DialogResult.OK)
                return;
            try
            {
                pictureBoxGist.Image.Save(sfDialog.FileName, ImageFormat.Bmp);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Предрисовка
        private void dataGridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            DataGridViewRow row = dataGridView.Rows[e.RowIndex];
            if (row.IsNewRow)
                return;
            string indexStr = (e.RowIndex + 1).ToString();
            object header = row.HeaderCell.Value;
            if (header == null || !header.Equals(indexStr))
                row.HeaderCell.Value = indexStr;
        }
        //Закрытие приложения
        private void Method_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Saved)
                return;
            DialogResult dr = MessageBox.Show("Вы уверены, что хотите закрыть приложение?",
                "Данные не сохранены", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (dr == DialogResult.No)
                e.Cancel = true;
            else
                e.Cancel = false;
        }

        private void всплывающиеПодсказкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolTip.Active = всплывающиеПодсказкиToolStripMenuItem.Checked;
        }

        private void помощьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help help = new Help();
            help.ShowDialog();
        }

        private void параметрыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            service.ShowDialog();
        }
        
        private void вызовсправкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpCall CH = new HelpCall();
            CH.ShowDialog();
        }
        //About
        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About About = new About();
            About.ShowDialog();
        }
        //Выход из программы
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
