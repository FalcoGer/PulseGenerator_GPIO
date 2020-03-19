using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

using RaspberryPiDotNet;

namespace PulseGenerator_GPIO
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Sensor 1 simulation pin.
        /// </summary>
        private static GPIOMem rotationSensor1_1 = null;
        /// <summary>
        /// Sensor 2 simulation pin.
        /// </summary>
        private static GPIOMem rotationSensor1_2 = null;
        /// <summary>
        /// Power sensor simulation pin.
        /// </summary>
        private static GPIOMem powerSensor = null;

        /// <summary>
        /// Sensor 1 simulation pin.
        /// </summary>
        private static GPIOMem rotationSensor2_1 = null;
        /// <summary>
        /// Sensor 2 simulation pin.
        /// </summary>
        private static GPIOMem rotationSensor2_2 = null;

        Thread t1 = null;
        Thread t2 = null;

        /// <summary>
        /// The pulse width in ms for Sensor 1 and 2.
        /// </summary>
        static int pulseLength1 = 0;
        /// <summary>
        /// The pulse width in ms for Sensor 3 and 4.
        /// </summary>
        static int pulseLength2 = 0;
        /// <summary>
        /// The length between pulses of Sensor 1 and 2
        /// </summary>
        static int cycleLength1 = 0;
        /// <summary>
        /// The length between pulses of Sensor 3 and 4
        /// </summary>
        static int cycleLength2 = 0;
        /// <summary>
        /// Default state of S1 Pin when not triggered.
        /// </summary>
        static bool s1_default = false;
        /// <summary>
        ///  Default state of S2 Pin when not triggered.
        /// </summary>
        static bool s2_default = false;
        /// <summary>
        /// Default state of S3 Pin when not triggered.
        /// </summary>
        static bool s3_default = false;
        /// <summary>
        /// Default state of S4 Pin when not triggered.
        /// </summary>
        static bool s4_default = false;
        /// <summary>
        /// The delay between S1 and S2 triggers.
        /// </summary>
        static int delay1 = 0;
        /// <summary>
        /// The delay between S3 and S4 triggers.
        /// </summary>
        static int delay2 = 0;

        /// <summary>
        /// Wether S1 should generate random pulses
        /// </summary>
        static bool randomPulses1_1 = false;
        /// <summary>
        /// The pulse width in ms for the random pulses on S1
        /// </summary>
        static int randomPulsesLength1_1 = 0;
        /// <summary>
        /// Wether S2 should generate random pulses
        /// </summary>
        static bool randomPulses1_2 = false;
        /// <summary>
        /// The pulse width in ms for the random pulses on S2
        /// </summary>
        static int randomPulsesLength1_2 = 0;

        /// <summary>
        /// Wether S3 should generate random pulses
        /// </summary>
        static bool randomPulses2_1 = false;
        /// <summary>
        /// The pulse width in ms for the random pulses on S3
        /// </summary>
        static int randomPulsesLength2_1 = 0;
        /// <summary>
        /// Wether S4 should generate random pulses
        /// </summary>
        static bool randomPulses2_2 = false;
        /// <summary>
        /// The pulse width in ms for the random pulses on S4
        /// </summary>
        static int randomPulsesLength2_2 = 0;

        static bool run1 = false;
        static bool run2 = false;

        /// <summary>
        /// Is called as a thread. Manages turning on and off the GPIOs as set by static variables above.
        /// </summary>
        /// <param name="sensor">If false, use sensor 1 and 2 otherwise 3 and 4</param>
        private static void RunPulseGen(bool sensor)
        {
            var sw = new System.Diagnostics.Stopwatch();
            sw.Reset();
            while (!sensor ? run1 : run2)
            {
                sw.Reset();
                sw.Start();
                // start pulse 1
                bool boolExpr = false;
                int sleepTime = 0;
                if (!sensor)
                {
                    if (!(rotationSensor1_1 is null))
                    {
                        rotationSensor1_1.Write(!s1_default);
                    }
                    // determine which comes first: start pulse 2 or stop pulse 1
                    boolExpr = pulseLength1 < delay1;
                }
                else
                {
                    if (!(rotationSensor2_1 is null))
                    {
                        rotationSensor2_1.Write(!s1_default);
                    }
                    // determine which comes first: start pulse 2 or stop pulse 1
                    boolExpr = pulseLength2 < delay2;
                }
                if (boolExpr)
                {
                    // S1: ****________...
                    // S2: ______****__...
                    //
                    // stop pulse 1
                    sleepTime = !sensor ? pulseLength1 : pulseLength2;
                    Thread.Sleep(sleepTime);
                    if (!sensor)
                    {
                        if (!(rotationSensor1_1 is null))
                        {
                            rotationSensor1_1.Write(s1_default);
                        }
                    }
                    else
                    {
                        if (!(rotationSensor2_1 is null))
                        {
                            rotationSensor2_1.Write(s1_default);
                        }
                    }
                    
                    // do pulse 2
                    // pulseLength1 is over, but measure for more accuracy
                    {
                        sleepTime = (!sensor ? delay1 : delay2) - (int)sw.ElapsedMilliseconds;
                        if (sleepTime > 0)
                        {
                            Thread.Sleep(sleepTime);
                        }
                    }
                    if (!sensor)
                    {
                        if (!(rotationSensor1_2 is null))
                        {
                            rotationSensor1_2.Write(!s2_default);
                        }
                        // sleep until pulse duration over
                        sleepTime = pulseLength1;
                    }
                    else
                    {
                        if (!(rotationSensor2_2 is null))
                        {
                            rotationSensor2_2.Write(!s2_default);
                        }
                        sleepTime = pulseLength2;
                    }
                    Thread.Sleep(sleepTime);
                    // stop pulse 2
                    if (!sensor)
                    {
                        if (!(rotationSensor1_2 is null))
                        {
                            rotationSensor1_2.Write(s2_default);
                        }
                    }
                    else
                    {
                        if (!(rotationSensor2_2 is null))
                        {
                            rotationSensor2_2.Write(s2_default);
                        }
                    }
                }
                else
                {
                    // S1: ****______...
                    // S2: __****____...
                    // pulse 2 starts first, pulse 1 still running
                    sleepTime = (!sensor ? delay1 : delay2);
                    Thread.Sleep(sleepTime);
                    if (!sensor)
                    {
                        if (!(rotationSensor2_2 is null))
                        {
                            rotationSensor2_2.Write(!s2_default);
                        }
                    }
                    else
                    {
                        if (!(rotationSensor2_2 is null))
                        {
                            rotationSensor2_2.Write(!s2_default);
                        }
                    }
                    // sleep till stop of pulse 1
                    sleepTime = !sensor ? pulseLength1 - delay1 : pulseLength2 - delay2;
                    if (sleepTime > 0)
                    {
                        Thread.Sleep(sleepTime);
                    }
                    // stop pulse 1
                    if (!sensor)
                    {
                        if (!(rotationSensor1_1 is null))
                        {
                            rotationSensor1_1.Write(s1_default);
                        }
                    }
                    else
                    {
                        if (!(rotationSensor2_1 is null))
                        {
                            rotationSensor2_1.Write(s1_default);
                        }
                    }
                    
                    // sleep till end of pulse 2
                    sleepTime = (!sensor ? pulseLength1 : pulseLength2) - ((int)sw.ElapsedMilliseconds - (!sensor ? delay1 : delay2));
                    if (sleepTime > 0)
                    {
                        Thread.Sleep(sleepTime);
                    }
                    // stop pulse 2
                    if (!sensor)
                    {
                        if (!(rotationSensor1_2 is null))
                        {
                            rotationSensor1_2.Write(s2_default);
                        }
                    }
                    else
                    {
                        if (!(rotationSensor2_2 is null))
                        {
                            rotationSensor2_2.Write(s2_default);
                        }
                    }
                }
                // sleep for the rest of the cycle, measure time for more accuracy
                // TODO: random pulses
                sleepTime = cycleLength1 - (int)sw.ElapsedMilliseconds;
                if (sleepTime > 0)
                {
                    Thread.Sleep(sleepTime);
                }
            } // \while(!sensor ? run1 : run2)
            //
            if (!sensor)
            {
                if (!(rotationSensor1_1 is null))
                {
                    rotationSensor1_1.Write(false);
                }
                if (!(rotationSensor1_2 is null))
                {
                    rotationSensor1_2.Write(false);
                }
            }
            else
            {
                if (!(rotationSensor2_1 is null))
                {
                    rotationSensor2_1.Write(false);
                }
                if (!(rotationSensor2_2 is null))
                {
                    rotationSensor2_2.Write(false);
                }
            }
            
        }

        /// <summary>
        /// CTor for Main Form
        /// </summary>
        public MainForm()
        {
            // Create form and set up values for controls.
            InitializeComponent();

            // Populate GPIO-Pin Selection
            foreach (ComboBox ctrl in new List<ComboBox> { ddl_GPIO1, ddl_GPIO2, ddl_GPIO_PWR })
            {
                foreach (string gPIO_PinName in Enum.GetNames(typeof(GPIOPins)))
                {
                    ctrl.Items.Add(gPIO_PinName);
                }
            }

            // Set up static variables for run-thread from default values.
            // S1 / S2
            cycleLength1 = (int)nud_RPM.Value;
            pulseLength1 = (int)((nud_PulseLength1.Value / 100) * cycleLength1);
            delay1 = (int)(nud_Delay1.Value * cycleLength1 / 100);
            randomPulses1_1 = cb_S1_RandomPulses.Checked;
            randomPulses1_2 = cb_S2_RandomPulses.Checked;
            randomPulsesLength1_1 = (int)nud_S1_RandomPulseLength.Value;
            randomPulsesLength1_2 = (int)nud_S2_RandomPulseLength.Value;
            s1_default = cb_GPIO1_Default.Checked;
            s2_default = cb_GPIO2_Default.Checked;
            // S3 / S4
            cycleLength2 = (int)(cycleLength1 * (nud_Ratio.Value / 100));
            pulseLength2 = (int)((nud_PulseLength2.Value / 100) * cycleLength2);
            delay2 = (int)(nud_Delay2.Value * cycleLength2 / 100);
            randomPulses2_1 = cb_S3_RandomPulses.Checked;
            randomPulses2_2 = cb_S4_RandomPulses.Checked;
            randomPulsesLength2_1 = (int)nud_S3_RandomPulseLength.Value;
            randomPulsesLength2_2 = (int)nud_S4_RandomPulseLength.Value;
            s3_default = cb_GPIO3_Default.Checked;
            s4_default = cb_GPIO4_Default.Checked;

            // Set up trackControl (Slider) values.
            // S1 / S2
            sl_Delay1.Value = (int)nud_Delay1.Value;
            sl_Delay1.Maximum = (int)nud_Delay1.Maximum;
            sl_RPM.Value = (int)nud_RPM.Value;
            sl_RPM.Maximum = (int)nud_RPM.Maximum;
            sl_PulseLength1.Value = (int)nud_PulseLength1.Value;
            sl_PulseLength1.Maximum = (int)nud_PulseLength1.Maximum;
            // S3 / S4
            sl_Delay2.Value = (int)nud_Delay2.Value;
            sl_Delay2.Maximum = (int)nud_Delay2.Maximum;
            sl_Ratio.Value = (int)nud_Ratio.Value;
            sl_Ratio.Maximum = (int)nud_Ratio.Maximum;
            sl_PulseLength2.Value = (int)nud_PulseLength2.Value;
            sl_PulseLength2.Maximum = (int)nud_PulseLength2.Maximum;
        }

        /// <summary>
        /// Called when GPIO-Pin selection changed. Sets up GPIOMem variables.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GPIO_SettingsChanged(object sender, EventArgs e)
        {
            try
            {
                // stop running
                run1 = false;
                cb_Run1.Checked = false;
                cb_Run1_CheckedChanged(sender, e);
                //
                run2 = false;
                cb_Run2.Checked = false;
                cb_Run2_CheckedChanged(sender, e);

                // set gpio pins
                Control ctrl = (Control)sender;
                // Control is rotation sensor 1
                if (ctrl.Name == ddl_GPIO1.Name)
                {
                    // reset old Pin
                    if (!(rotationSensor1_1 is null))
                    {
                        rotationSensor1_1.Dispose();
                        rotationSensor1_1 = null;
                    }
                    // set new pin
                    GPIOPins pin = (GPIOPins)Enum.Parse(typeof(GPIOPins), ddl_GPIO1.SelectedItem.ToString());
                    if (pin != GPIOPins.GPIO_NONE)
                    {
                        rotationSensor1_1 = new GPIOMem(pin, GPIODirection.Out);
                    }
                }
                // Control is rotation sensor 2
                else if (ctrl.Name == ddl_GPIO2.Name)
                {
                    if (!(rotationSensor1_2 is null))
                    {
                        rotationSensor1_2.Dispose();
                        rotationSensor1_2 = null;
                    }
                    GPIOPins pin = (GPIOPins)Enum.Parse(typeof(GPIOPins), ddl_GPIO2.SelectedItem.ToString());
                    if (pin != GPIOPins.GPIO_NONE)
                    {
                        rotationSensor1_2 = new GPIOMem(pin, GPIODirection.Out);
                    }
                }
                // Control is power sensor
                else if (ctrl.Name == ddl_GPIO_PWR.Name)
                {
                    if (!(powerSensor is null))
                    {
                        powerSensor.Dispose();
                        powerSensor = null;
                    }
                    GPIOPins pin = (GPIOPins)Enum.Parse(typeof(GPIOPins), ddl_GPIO_PWR.SelectedItem.ToString());
                    if (pin != GPIOPins.GPIO_NONE)
                    {
                        powerSensor = new GPIOMem(pin, GPIODirection.Out);
                    }
                }
                else if (ctrl.Name == ddl_GPIO3.Name)
                {
                    if (!(rotationSensor2_1 is null))
                    {
                        rotationSensor2_1.Dispose();
                        rotationSensor2_1 = null;
                    }
                    GPIOPins pin = (GPIOPins)Enum.Parse(typeof(GPIOPins), ddl_GPIO3.SelectedItem.ToString());
                    if (pin != GPIOPins.GPIO_NONE)
                    {
                        rotationSensor2_1 = new GPIOMem(pin, GPIODirection.Out);
                    }
                }
                else if (ctrl.Name == ddl_GPIO4.Name)
                {
                    if (!(powerSensor is null))
                    {
                        rotationSensor2_2.Dispose();
                        rotationSensor2_2 = null;
                    }
                    GPIOPins pin = (GPIOPins)Enum.Parse(typeof(GPIOPins), ddl_GPIO4.SelectedItem.ToString());
                    if (pin != GPIOPins.GPIO_NONE)
                    {
                        rotationSensor2_2 = new GPIOMem(pin, GPIODirection.Out);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Called when Power Out CB changes or when Power Default CB changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_PowerSensorOut_CheckedChanged(object sender, EventArgs e)
        {
            if (!(powerSensor is null))
            {
                powerSensor.Write(!(cb_GPIO_PWR_Default.Checked ^ cb_PowerSensorOut.Checked));
            }
        }

        /// <summary>
        /// Called when Run CB changes or when Run status changes programatically.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_Run1_CheckedChanged(object sender, EventArgs e)
        {
            run1 = cb_Run1.Checked;
            if (run1)
            {
                t1 = new Thread(() => RunPulseGen(false));
                t1.Start();
            }
            else
            {
                if (!(t1 is null))
                {
                    t1.Join();
                    t1 = null;
                }
            }
        }

        private void cb_Run2_CheckedChanged(object sender, EventArgs e)
        {
            run2 = cb_Run2.Checked;
            if (run2)
            {
                t2 = new Thread(() => RunPulseGen(true));
                t2.Start();
            }
            else
            {
                if (!(t2 is null))
                {
                    t2.Join();
                    t2 = null;
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nud_PulseLength1_ValueChanged(object sender, EventArgs e)
        {
            pulseLength1 = (int)((nud_PulseLength1.Value / 100) * cycleLength1);
            sl_PulseLength1.Value = (int)nud_PulseLength1.Value;
            lbl_PulseLength1.Text = "PW: " + pulseLength1.ToString() + "ms";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nud_RPM_ValueChanged(object sender, EventArgs e)
        {
            cycleLength1 = (int)(60000 / nud_RPM.Value);
            sl_RPM.Value = (int)nud_RPM.Value;

            lbl_CycleLength1.Text = "Period: " + cycleLength1.ToString() + "ms";

            nud_Delay1_ValueChanged(sender, e);
            nud_PulseLength1_ValueChanged(sender, e);
            nud_Ratio_ValueChanged(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nud_S1_RandomPulseLength_ValueChanged(object sender, EventArgs e)
        {
            randomPulsesLength1_1 = (int)nud_S1_RandomPulseLength.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nud_PulseLength2_ValueChanged(object sender, EventArgs e)
        {
            pulseLength2 = (int)((nud_PulseLength2.Value / 100) * cycleLength2);
            sl_PulseLength2.Value = (int)nud_PulseLength2.Value;

            lbl_PulseLength2.Text = "PW: " + pulseLength2.ToString() + "ms";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nud_Delay1_ValueChanged(object sender, EventArgs e)
        {
            delay1 = (int)((nud_Delay1.Value / 100) * cycleLength1);
            sl_Delay1.Value = (int)nud_Delay1.Value;

            lbl_Delay1.Text = "PD: " + delay1.ToString() + "ms";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nud_S2_RandomPulseLength_ValueChanged(object sender, EventArgs e)
        {
            randomPulsesLength1_2 = (int)nud_S2_RandomPulseLength.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_S1_RandomPulses_CheckedChanged(object sender, EventArgs e)
        {
            randomPulses1_1 = cb_S1_RandomPulses.Checked;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_S2_RandomPulses_CheckedChanged(object sender, EventArgs e)
        {
            randomPulses1_2 = cb_S2_RandomPulses.Checked;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_GPIO1_Default_CheckedChanged(object sender, EventArgs e)
        {
            s1_default = cb_GPIO1_Default.Checked;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_GPIO2_Default_CheckedChanged(object sender, EventArgs e)
        {
            s2_default = cb_GPIO2_Default.Checked;
        }

        /// <summary>
        /// Called when Form closes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Stop threads by letting them run out.
            if (!(t1 is null))
            {
                run1 = false;
                t1.Join();
            }
            if (!(t2 is null))
            {
                run2 = false;
                t2.Join();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sl_PulseLength1_Scroll(object sender, EventArgs e)
        {
            nud_PulseLength1.Value = sl_PulseLength1.Value;
            nud_PulseLength1_ValueChanged(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sl_RPM_Scroll(object sender, EventArgs e)
        {
            nud_RPM.Value = sl_RPM.Value;
            nud_RPM_ValueChanged(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sl_PulseLenght2_Scroll(object sender, EventArgs e)
        {
            nud_PulseLength2.Value = sl_PulseLength2.Value;
            nud_PulseLength2_ValueChanged(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sl_Delay_Scroll(object sender, EventArgs e)
        {
            nud_Delay1.Value = sl_Delay1.Value;
            nud_Delay1_ValueChanged(sender, e);
        }

        private void sl_Ratio_Scroll(object sender, EventArgs e)
        {
            nud_Ratio.Value = sl_Ratio.Value;
            nud_Ratio_ValueChanged(sender, e);
        }

        private void nud_Ratio_ValueChanged(object sender, EventArgs e)
        {
            cycleLength2 = (int)(cycleLength1 / (nud_Ratio.Value / 100));
            sl_Ratio.Value = (int)nud_Ratio.Value;

            nud_Delay2_ValueChanged(sender, e);
            nud_PulseLength2_ValueChanged(sender, e);

            lbl_RPM2.Text = "RPM: " + (60000.0 / cycleLength2).ToString("N1");
            lbl_CycleLength2.Text = "Period: " + cycleLength2.ToString() + "ms";
        }

        private void nud_Delay2_ValueChanged(object sender, EventArgs e)
        {
            delay2 = (int)((nud_Delay2.Value / 100) * cycleLength2);
            sl_Delay2.Value = (int)nud_Delay2.Value;

            lbl_Delay2.Text = "PD: " + delay2.ToString() + "ms";
        }

        private void sl_Delay2_Scroll(object sender, EventArgs e)
        {
            nud_Delay2.Value = sl_Delay2.Value;
            nud_Delay2_ValueChanged(sender, e);
        }

        private void cb_GPIO3_Default_CheckedChanged(object sender, EventArgs e)
        {
            s3_default = cb_GPIO3_Default.Checked;
        }

        private void cb_GPIO4_Default_CheckedChanged(object sender, EventArgs e)
        {
            s4_default = cb_GPIO4_Default.Checked;
        }
    }
}
