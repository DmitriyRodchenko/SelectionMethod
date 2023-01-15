namespace Методотбора
{
    partial class Service
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.checkBoxM = new System.Windows.Forms.CheckBox();
            this.checkBoxR = new System.Windows.Forms.CheckBox();
            this.checkBoxF = new System.Windows.Forms.CheckBox();
            this.labelPlus = new System.Windows.Forms.Label();
            this.numericUpDownPercent = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownM = new System.Windows.Forms.NumericUpDown();
            this.labelM = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelPQCheck = new System.Windows.Forms.Label();
            this.numericUpDownPQprecise = new System.Windows.Forms.NumericUpDown();
            this.labelP = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownFprecise = new System.Windows.Forms.NumericUpDown();
            this.ButtonReset = new System.Windows.Forms.Button();
            this.checkBoxPQ = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPercent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPQprecise)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFprecise)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBoxM
            // 
            this.checkBoxM.AutoSize = true;
            this.checkBoxM.Location = new System.Drawing.Point(15, 108);
            this.checkBoxM.Name = "checkBoxM";
            this.checkBoxM.Size = new System.Drawing.Size(96, 17);
            this.checkBoxM.TabIndex = 33;
            this.checkBoxM.Text = "Рассчитать M";
            this.checkBoxM.UseVisualStyleBackColor = true;
            this.checkBoxM.CheckedChanged += new System.EventHandler(this.checkBoxM_CheckedChanged);
            // 
            // checkBoxR
            // 
            this.checkBoxR.AutoSize = true;
            this.checkBoxR.Location = new System.Drawing.Point(15, 85);
            this.checkBoxR.Name = "checkBoxR";
            this.checkBoxR.Size = new System.Drawing.Size(90, 17);
            this.checkBoxR.TabIndex = 32;
            this.checkBoxR.Text = "Рассчитать r";
            this.checkBoxR.UseVisualStyleBackColor = true;
            this.checkBoxR.CheckedChanged += new System.EventHandler(this.checkBoxR_CheckedChanged);
            // 
            // checkBoxF
            // 
            this.checkBoxF.AutoSize = true;
            this.checkBoxF.Location = new System.Drawing.Point(15, 41);
            this.checkBoxF.Name = "checkBoxF";
            this.checkBoxF.Size = new System.Drawing.Size(93, 17);
            this.checkBoxF.TabIndex = 31;
            this.checkBoxF.Text = "Рассчитать F";
            this.checkBoxF.UseVisualStyleBackColor = true;
            this.checkBoxF.CheckedChanged += new System.EventHandler(this.checkBoxF_CheckedChanged);
            // 
            // labelPlus
            // 
            this.labelPlus.AutoSize = true;
            this.labelPlus.Location = new System.Drawing.Point(165, 9);
            this.labelPlus.Name = "labelPlus";
            this.labelPlus.Size = new System.Drawing.Size(13, 13);
            this.labelPlus.TabIndex = 30;
            this.labelPlus.Text = "+";
            // 
            // numericUpDownPercent
            // 
            this.numericUpDownPercent.Location = new System.Drawing.Point(179, 7);
            this.numericUpDownPercent.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericUpDownPercent.Name = "numericUpDownPercent";
            this.numericUpDownPercent.Size = new System.Drawing.Size(38, 20);
            this.numericUpDownPercent.TabIndex = 29;
            this.numericUpDownPercent.ValueChanged += new System.EventHandler(this.numericUpDownPercent_ValueChanged);
            // 
            // numericUpDownM
            // 
            this.numericUpDownM.DecimalPlaces = 14;
            this.numericUpDownM.Increment = new decimal(new int[] {
            1,
            0,
            0,
            917504});
            this.numericUpDownM.Location = new System.Drawing.Point(46, 7);
            this.numericUpDownM.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.numericUpDownM.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownM.Name = "numericUpDownM";
            this.numericUpDownM.Size = new System.Drawing.Size(113, 20);
            this.numericUpDownM.TabIndex = 28;
            this.numericUpDownM.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownM.ValueChanged += new System.EventHandler(this.numericUpDownM_ValueChanged);
            // 
            // labelM
            // 
            this.labelM.AutoSize = true;
            this.labelM.Location = new System.Drawing.Point(12, 9);
            this.labelM.Name = "labelM";
            this.labelM.Size = new System.Drawing.Size(28, 13);
            this.labelM.TabIndex = 27;
            this.labelM.Text = "M = ";
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(205, 199);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 30);
            this.buttonOK.TabIndex = 34;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(286, 199);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 30);
            this.buttonCancel.TabIndex = 35;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelPQCheck
            // 
            this.labelPQCheck.AutoSize = true;
            this.labelPQCheck.Location = new System.Drawing.Point(12, 151);
            this.labelPQCheck.Name = "labelPQCheck";
            this.labelPQCheck.Size = new System.Drawing.Size(208, 13);
            this.labelPQCheck.TabIndex = 39;
            this.labelPQCheck.Text = "Погрешность оценки плотностей P и Q:";
            // 
            // numericUpDownPQprecise
            // 
            this.numericUpDownPQprecise.DecimalPlaces = 14;
            this.numericUpDownPQprecise.Increment = new decimal(new int[] {
            1,
            0,
            0,
            917504});
            this.numericUpDownPQprecise.Location = new System.Drawing.Point(226, 149);
            this.numericUpDownPQprecise.Maximum = new decimal(new int[] {
            552894464,
            46566,
            0,
            917504});
            this.numericUpDownPQprecise.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            917504});
            this.numericUpDownPQprecise.Name = "numericUpDownPQprecise";
            this.numericUpDownPQprecise.Size = new System.Drawing.Size(115, 20);
            this.numericUpDownPQprecise.TabIndex = 40;
            this.numericUpDownPQprecise.Value = new decimal(new int[] {
            1215752192,
            23,
            0,
            917504});
            this.numericUpDownPQprecise.ValueChanged += new System.EventHandler(this.numericUpDownPQprecise_ValueChanged);
            // 
            // labelP
            // 
            this.labelP.AutoSize = true;
            this.labelP.Location = new System.Drawing.Point(223, 9);
            this.labelP.Name = "labelP";
            this.labelP.Size = new System.Drawing.Size(15, 13);
            this.labelP.TabIndex = 41;
            this.labelP.Text = "%";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 42;
            this.label1.Text = "Погрешность:";
            // 
            // numericUpDownFprecise
            // 
            this.numericUpDownFprecise.DecimalPlaces = 14;
            this.numericUpDownFprecise.Increment = new decimal(new int[] {
            1,
            0,
            0,
            917504});
            this.numericUpDownFprecise.Location = new System.Drawing.Point(93, 59);
            this.numericUpDownFprecise.Maximum = new decimal(new int[] {
            552894464,
            46566,
            0,
            917504});
            this.numericUpDownFprecise.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            917504});
            this.numericUpDownFprecise.Name = "numericUpDownFprecise";
            this.numericUpDownFprecise.Size = new System.Drawing.Size(115, 20);
            this.numericUpDownFprecise.TabIndex = 43;
            this.numericUpDownFprecise.Value = new decimal(new int[] {
            1215752192,
            23,
            0,
            917504});
            this.numericUpDownFprecise.ValueChanged += new System.EventHandler(this.numericUpDownFprecise_ValueChanged);
            // 
            // ButtonReset
            // 
            this.ButtonReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ButtonReset.Location = new System.Drawing.Point(15, 188);
            this.ButtonReset.Name = "ButtonReset";
            this.ButtonReset.Size = new System.Drawing.Size(93, 42);
            this.ButtonReset.TabIndex = 44;
            this.ButtonReset.Text = "Значения по умолчанию";
            this.ButtonReset.UseVisualStyleBackColor = true;
            this.ButtonReset.Click += new System.EventHandler(this.ButtonReset_Click);
            // 
            // checkBoxPQ
            // 
            this.checkBoxPQ.AutoSize = true;
            this.checkBoxPQ.Location = new System.Drawing.Point(15, 131);
            this.checkBoxPQ.Name = "checkBoxPQ";
            this.checkBoxPQ.Size = new System.Drawing.Size(290, 17);
            this.checkBoxPQ.TabIndex = 45;
            this.checkBoxPQ.Text = "Проверить плотности P и Q на условие нормировки";
            this.checkBoxPQ.UseVisualStyleBackColor = true;
            this.checkBoxPQ.CheckedChanged += new System.EventHandler(this.checkBoxPQ_CheckedChanged);
            // 
            // Service
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(373, 242);
            this.Controls.Add(this.checkBoxPQ);
            this.Controls.Add(this.ButtonReset);
            this.Controls.Add(this.numericUpDownFprecise);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelP);
            this.Controls.Add(this.numericUpDownPQprecise);
            this.Controls.Add(this.labelPQCheck);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.checkBoxM);
            this.Controls.Add(this.checkBoxR);
            this.Controls.Add(this.checkBoxF);
            this.Controls.Add(this.labelPlus);
            this.Controls.Add(this.numericUpDownPercent);
            this.Controls.Add(this.numericUpDownM);
            this.Controls.Add(this.labelM);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Service";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Параметры";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPercent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPQprecise)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFprecise)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox checkBoxR;
        private System.Windows.Forms.CheckBox checkBoxF;
        private System.Windows.Forms.Label labelPlus;
        private System.Windows.Forms.NumericUpDown numericUpDownPercent;
        private System.Windows.Forms.NumericUpDown numericUpDownM;
        private System.Windows.Forms.Label labelM;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        public System.Windows.Forms.CheckBox checkBoxM;
        private System.Windows.Forms.Label labelPQCheck;
        private System.Windows.Forms.NumericUpDown numericUpDownPQprecise;
        private System.Windows.Forms.Label labelP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownFprecise;
        private System.Windows.Forms.Button ButtonReset;
        private System.Windows.Forms.CheckBox checkBoxPQ;
    }
}