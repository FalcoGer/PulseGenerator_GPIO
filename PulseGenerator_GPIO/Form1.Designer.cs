namespace PulseGenerator_GPIO
{
    partial class MainForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ddl_GPIO1 = new System.Windows.Forms.ComboBox();
            this.ddl_GPIO2 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ddl_GPIO3 = new System.Windows.Forms.ComboBox();
            this.cb_PowerSensorOut = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.nud_S1_PulseLength = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.nud_CycleLength = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cb_GPIO3_Default = new System.Windows.Forms.CheckBox();
            this.cb_GPIO2_Default = new System.Windows.Forms.CheckBox();
            this.cb_GPIO1_Default = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.nud_S1_RandomPulseLength = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cb_S1_RandomPulses = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.nud_S2_RandomPulseLength = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.cb_S2_RandomPulses = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.nud_S2_PulseLength = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.nud_S2_Delay = new System.Windows.Forms.NumericUpDown();
            this.cb_Run = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.nud_S1_PulseLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_CycleLength)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_S1_RandomPulseLength)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_S2_RandomPulseLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_S2_PulseLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_S2_Delay)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sensor_1_GPIO";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Sensor_2_GPIO";
            // 
            // ddl_GPIO1
            // 
            this.ddl_GPIO1.FormattingEnabled = true;
            this.ddl_GPIO1.Location = new System.Drawing.Point(96, 13);
            this.ddl_GPIO1.Name = "ddl_GPIO1";
            this.ddl_GPIO1.Size = new System.Drawing.Size(121, 21);
            this.ddl_GPIO1.TabIndex = 2;
            this.ddl_GPIO1.SelectedIndexChanged += new System.EventHandler(this.GPIO_SettingsChanged);
            // 
            // ddl_GPIO2
            // 
            this.ddl_GPIO2.FormattingEnabled = true;
            this.ddl_GPIO2.Location = new System.Drawing.Point(96, 40);
            this.ddl_GPIO2.Name = "ddl_GPIO2";
            this.ddl_GPIO2.Size = new System.Drawing.Size(121, 21);
            this.ddl_GPIO2.TabIndex = 3;
            this.ddl_GPIO2.SelectedIndexChanged += new System.EventHandler(this.GPIO_SettingsChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Power_GPIO";
            // 
            // ddl_GPIO3
            // 
            this.ddl_GPIO3.FormattingEnabled = true;
            this.ddl_GPIO3.Location = new System.Drawing.Point(96, 67);
            this.ddl_GPIO3.Name = "ddl_GPIO3";
            this.ddl_GPIO3.Size = new System.Drawing.Size(121, 21);
            this.ddl_GPIO3.TabIndex = 5;
            this.ddl_GPIO3.SelectedIndexChanged += new System.EventHandler(this.GPIO_SettingsChanged);
            // 
            // cb_PowerSensorOut
            // 
            this.cb_PowerSensorOut.AutoSize = true;
            this.cb_PowerSensorOut.Location = new System.Drawing.Point(12, 116);
            this.cb_PowerSensorOut.Name = "cb_PowerSensorOut";
            this.cb_PowerSensorOut.Size = new System.Drawing.Size(112, 17);
            this.cb_PowerSensorOut.TabIndex = 7;
            this.cb_PowerSensorOut.Text = "Power Sensor Out";
            this.cb_PowerSensorOut.UseVisualStyleBackColor = true;
            this.cb_PowerSensorOut.CheckedChanged += new System.EventHandler(this.cb_PowerSensorOut_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Pulse Length";
            // 
            // nud_S1_PulseLength
            // 
            this.nud_S1_PulseLength.Location = new System.Drawing.Point(81, 13);
            this.nud_S1_PulseLength.Maximum = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            this.nud_S1_PulseLength.Name = "nud_S1_PulseLength";
            this.nud_S1_PulseLength.Size = new System.Drawing.Size(121, 20);
            this.nud_S1_PulseLength.TabIndex = 9;
            this.nud_S1_PulseLength.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nud_S1_PulseLength.ValueChanged += new System.EventHandler(this.nud_S1_PulseLength_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(208, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(20, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "ms";
            // 
            // nud_CycleLength
            // 
            this.nud_CycleLength.Location = new System.Drawing.Point(81, 40);
            this.nud_CycleLength.Maximum = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            this.nud_CycleLength.Name = "nud_CycleLength";
            this.nud_CycleLength.Size = new System.Drawing.Size(121, 20);
            this.nud_CycleLength.TabIndex = 11;
            this.nud_CycleLength.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nud_CycleLength.ValueChanged += new System.EventHandler(this.nud_CycleLength_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 43);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Cycle Length";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(208, 43);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(20, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "ms";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cb_GPIO3_Default);
            this.groupBox1.Controls.Add(this.cb_GPIO2_Default);
            this.groupBox1.Controls.Add(this.cb_GPIO1_Default);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.ddl_GPIO1);
            this.groupBox1.Controls.Add(this.ddl_GPIO2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.ddl_GPIO3);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(326, 98);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "GPIO Settings";
            // 
            // cb_GPIO3_Default
            // 
            this.cb_GPIO3_Default.AutoSize = true;
            this.cb_GPIO3_Default.Location = new System.Drawing.Point(223, 69);
            this.cb_GPIO3_Default.Name = "cb_GPIO3_Default";
            this.cb_GPIO3_Default.Size = new System.Drawing.Size(85, 17);
            this.cb_GPIO3_Default.TabIndex = 8;
            this.cb_GPIO3_Default.Text = "Default High";
            this.cb_GPIO3_Default.UseVisualStyleBackColor = true;
            this.cb_GPIO3_Default.CheckedChanged += new System.EventHandler(this.cb_PowerSensorOut_CheckedChanged);
            // 
            // cb_GPIO2_Default
            // 
            this.cb_GPIO2_Default.AutoSize = true;
            this.cb_GPIO2_Default.Location = new System.Drawing.Point(223, 42);
            this.cb_GPIO2_Default.Name = "cb_GPIO2_Default";
            this.cb_GPIO2_Default.Size = new System.Drawing.Size(85, 17);
            this.cb_GPIO2_Default.TabIndex = 7;
            this.cb_GPIO2_Default.Text = "Default High";
            this.cb_GPIO2_Default.UseVisualStyleBackColor = true;
            this.cb_GPIO2_Default.CheckedChanged += new System.EventHandler(this.cb_GPIO2_Default_CheckedChanged);
            // 
            // cb_GPIO1_Default
            // 
            this.cb_GPIO1_Default.AutoSize = true;
            this.cb_GPIO1_Default.Location = new System.Drawing.Point(223, 15);
            this.cb_GPIO1_Default.Name = "cb_GPIO1_Default";
            this.cb_GPIO1_Default.Size = new System.Drawing.Size(85, 17);
            this.cb_GPIO1_Default.TabIndex = 6;
            this.cb_GPIO1_Default.Text = "Default High";
            this.cb_GPIO1_Default.UseVisualStyleBackColor = true;
            this.cb_GPIO1_Default.CheckedChanged += new System.EventHandler(this.cb_GPIO1_Default_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.nud_S1_RandomPulseLength);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.cb_S1_RandomPulses);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.nud_S1_PulseLength);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.nud_CycleLength);
            this.groupBox2.Location = new System.Drawing.Point(12, 139);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(326, 116);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sensor 1";
            // 
            // nud_S1_RandomPulseLength
            // 
            this.nud_S1_RandomPulseLength.Location = new System.Drawing.Point(81, 87);
            this.nud_S1_RandomPulseLength.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nud_S1_RandomPulseLength.Name = "nud_S1_RandomPulseLength";
            this.nud_S1_RandomPulseLength.Size = new System.Drawing.Size(121, 20);
            this.nud_S1_RandomPulseLength.TabIndex = 16;
            this.nud_S1_RandomPulseLength.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nud_S1_RandomPulseLength.ValueChanged += new System.EventHandler(this.nud_S1_RandomPulseLength_ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(208, 90);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(20, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "ms";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 90);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Pulse Length";
            // 
            // cb_S1_RandomPulses
            // 
            this.cb_S1_RandomPulses.AutoSize = true;
            this.cb_S1_RandomPulses.Location = new System.Drawing.Point(9, 66);
            this.cb_S1_RandomPulses.Name = "cb_S1_RandomPulses";
            this.cb_S1_RandomPulses.Size = new System.Drawing.Size(100, 17);
            this.cb_S1_RandomPulses.TabIndex = 14;
            this.cb_S1_RandomPulses.Text = "Random Pulses";
            this.cb_S1_RandomPulses.UseVisualStyleBackColor = true;
            this.cb_S1_RandomPulses.CheckedChanged += new System.EventHandler(this.cb_S1_RandomPulses_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.nud_S2_RandomPulseLength);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.cb_S2_RandomPulses);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.nud_S2_PulseLength);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.nud_S2_Delay);
            this.groupBox3.Location = new System.Drawing.Point(12, 261);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(326, 116);
            this.groupBox3.TabIndex = 18;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Sensor 2";
            // 
            // nud_S2_RandomPulseLength
            // 
            this.nud_S2_RandomPulseLength.Location = new System.Drawing.Point(81, 87);
            this.nud_S2_RandomPulseLength.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nud_S2_RandomPulseLength.Name = "nud_S2_RandomPulseLength";
            this.nud_S2_RandomPulseLength.Size = new System.Drawing.Size(121, 20);
            this.nud_S2_RandomPulseLength.TabIndex = 16;
            this.nud_S2_RandomPulseLength.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nud_S2_RandomPulseLength.ValueChanged += new System.EventHandler(this.nud_S2_RandomPulseLength_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(208, 90);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(20, 13);
            this.label10.TabIndex = 17;
            this.label10.Text = "ms";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 90);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(69, 13);
            this.label11.TabIndex = 15;
            this.label11.Text = "Pulse Length";
            // 
            // cb_S2_RandomPulses
            // 
            this.cb_S2_RandomPulses.AutoSize = true;
            this.cb_S2_RandomPulses.Location = new System.Drawing.Point(9, 66);
            this.cb_S2_RandomPulses.Name = "cb_S2_RandomPulses";
            this.cb_S2_RandomPulses.Size = new System.Drawing.Size(100, 17);
            this.cb_S2_RandomPulses.TabIndex = 14;
            this.cb_S2_RandomPulses.Text = "Random Pulses";
            this.cb_S2_RandomPulses.UseVisualStyleBackColor = true;
            this.cb_S2_RandomPulses.CheckedChanged += new System.EventHandler(this.cb_S2_RandomPulses_CheckedChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 16);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(69, 13);
            this.label12.TabIndex = 8;
            this.label12.Text = "Pulse Length";
            // 
            // nud_S2_PulseLength
            // 
            this.nud_S2_PulseLength.Location = new System.Drawing.Point(81, 13);
            this.nud_S2_PulseLength.Maximum = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            this.nud_S2_PulseLength.Name = "nud_S2_PulseLength";
            this.nud_S2_PulseLength.Size = new System.Drawing.Size(121, 20);
            this.nud_S2_PulseLength.TabIndex = 9;
            this.nud_S2_PulseLength.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nud_S2_PulseLength.ValueChanged += new System.EventHandler(this.nud_S2_PulseLength_ValueChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(208, 43);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(20, 13);
            this.label13.TabIndex = 13;
            this.label13.Text = "ms";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(208, 16);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(20, 13);
            this.label14.TabIndex = 10;
            this.label14.Text = "ms";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 43);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(34, 13);
            this.label15.TabIndex = 12;
            this.label15.Text = "Delay";
            // 
            // nud_S2_Delay
            // 
            this.nud_S2_Delay.Location = new System.Drawing.Point(81, 40);
            this.nud_S2_Delay.Maximum = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            this.nud_S2_Delay.Name = "nud_S2_Delay";
            this.nud_S2_Delay.Size = new System.Drawing.Size(121, 20);
            this.nud_S2_Delay.TabIndex = 11;
            this.nud_S2_Delay.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nud_S2_Delay.ValueChanged += new System.EventHandler(this.nud_S2_Delay_ValueChanged);
            // 
            // cb_Run
            // 
            this.cb_Run.AutoSize = true;
            this.cb_Run.Location = new System.Drawing.Point(131, 116);
            this.cb_Run.Name = "cb_Run";
            this.cb_Run.Size = new System.Drawing.Size(46, 17);
            this.cb_Run.TabIndex = 21;
            this.cb_Run.Text = "Run";
            this.cb_Run.UseVisualStyleBackColor = true;
            this.cb_Run.CheckedChanged += new System.EventHandler(this.cb_Run_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 384);
            this.Controls.Add(this.cb_Run);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cb_PowerSensorOut);
            this.Name = "MainForm";
            this.Text = "Pulse Generator";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.nud_S1_PulseLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_CycleLength)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_S1_RandomPulseLength)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_S2_RandomPulseLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_S2_PulseLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_S2_Delay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ddl_GPIO1;
        private System.Windows.Forms.ComboBox ddl_GPIO2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox ddl_GPIO3;
        private System.Windows.Forms.CheckBox cb_PowerSensorOut;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nud_S1_PulseLength;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nud_CycleLength;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cb_GPIO3_Default;
        private System.Windows.Forms.CheckBox cb_GPIO2_Default;
        private System.Windows.Forms.CheckBox cb_GPIO1_Default;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown nud_S1_RandomPulseLength;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox cb_S1_RandomPulses;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown nud_S2_RandomPulseLength;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox cb_S2_RandomPulses;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown nud_S2_PulseLength;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.NumericUpDown nud_S2_Delay;
        private System.Windows.Forms.CheckBox cb_Run;
    }
}

