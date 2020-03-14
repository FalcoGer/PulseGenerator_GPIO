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
        private static GPIOMem rotationSensor1 = null;
        /// <summary>
        /// Sensor 2 simulation pin.
        /// </summary>
        private static GPIOMem rotationSensor2 = null;
        /// <summary>
        /// Power sensor simulation pin.
        /// </summary>
        private static GPIOMem powerSensor = null;

        Thread t = null;

        /// <summary>
        /// The pulse width in ms for Sensor 1.
        /// </summary>
        static int pulseLength1 = 0;
        /// <summary>
        /// Default state of S1 Pin when not triggered.
        /// </summary>
        static bool s1_default = false;
        /// <summary>
        /// The pulse width in ms for Sensor 2.
        /// </summary>
        static int pulseLength2 = 0;
        /// <summary>
        /// The length between pulses of Sensor 1
        /// </summary>
        static int cycleLength = 0;
        /// <summary>
        ///  Default state of S2 Pin when not triggered.
        /// </summary>
        static bool s2_default = false;
        /// <summary>
        /// The delay between S1 and S2 triggers.
        /// </summary>
        static int delay = 0;

        /// <summary>
        /// Wether S1 should generate random pulses
        /// </summary>
        static bool randomPulses1 = false;
        /// <summary>
        /// The pulse width in ms for the random pulses on S1
        /// </summary>
        static int randomPulsesLength1 = 0;
        /// <summary>
        /// Wether S2 should generate random pulses
        /// </summary>
        static bool randomPulses2 = false;
        /// <summary>
        /// The pulse width in ms for the random pulses on S2
        /// </summary>
        static int randomPulsesLength2 = 0;

        static bool run = false;

        /// <summary>
        /// Is called as a thread. Manages turning on and off the GPIOs as set by static variables above.
        /// </summary>
        private static void RunPulseGen()
        {
            var sw = new System.Diagnostics.Stopwatch();
            sw.Reset();
            while (run)
            {
                sw.Reset();
                sw.Start();
                // start pulse 1
                if (!(rotationSensor1 is null))
                {
                    rotationSensor1.Write(!s1_default);
                }
                // determine which comes first: start pulse 2 or stop pulse 1
                if (pulseLength1 < delay)
                {
                    // S1: ****________...
                    // S2: ______****__...
                    //
                    // stop pulse 1
                    Thread.Sleep(pulseLength1);
                    if (!(rotationSensor1 is null))
                    {
                        rotationSensor1.Write(s1_default);
                    }
                    // do pulse 2
                    // pulseLength1 is over, but measure for more accuracy
                    {
                        int sleepTime = delay - (int)sw.ElapsedMilliseconds;
                        if (sleepTime > 0)
                        {
                            Thread.Sleep(sleepTime);
                        }
                    }
                    if (!(rotationSensor2 is null))
                    {
                        rotationSensor2.Write(!s2_default);
                    }
                    // sleep until pulse duration over
                    Thread.Sleep(pulseLength2);
                    // stop pulse 2
                    if (!(rotationSensor2 is null))
                    {
                        rotationSensor2.Write(s2_default);
                    }
                }
                else
                {
                    // pulse 2 starts first, pulse 1 still running
                    Thread.Sleep(delay);
                    if (!(rotationSensor2 is null))
                    {
                        rotationSensor2.Write(!s2_default);
                    }
                    // determine which stops first: pulse 1 or pulse 2
                    if (pulseLength1 > pulseLength2 + delay)
                    {
                        // S1: ********____...
                        // S2: __****______...
                        // pulse 2 stops first
                        Thread.Sleep(pulseLength2);
                        if (!(rotationSensor2 is null))
                        {
                            rotationSensor2.Write(s2_default);
                        }
                        // sleep the remainder of the time till pulse 1 stops
                        {
                            int sleepTime = pulseLength1 - (int)sw.ElapsedMilliseconds;
                            if (sleepTime > 0)
                            {
                                Thread.Sleep(sleepTime);
                            }
                        }
                        // stop pulse 1
                        if (!(rotationSensor1 is null))
                        {
                            rotationSensor1.Write(s1_default);
                        }
                    }
                    else
                    {
                        // S1: ****______...
                        // S2: __****____...
                        //
                        {
                            int sleepTime = pulseLength1 - delay;
                            if (sleepTime > 0)
                            {
                                Thread.Sleep(sleepTime);
                            }
                        }
                        // stop pulse 1
                        if (!(rotationSensor1 is null))
                        {
                            rotationSensor1.Write(s1_default);
                        }
                        // sleep till end of pulse 2
                        {
                            int sleepTime = pulseLength2 - ((int)sw.ElapsedMilliseconds - delay);
                            if (sleepTime > 0)
                            {
                                Thread.Sleep(sleepTime);
                            }
                        }
                        
                        // stop pulse 2
                        if (!(rotationSensor2 is null))
                        {
                            rotationSensor2.Write(s2_default);
                        }
                    }
                }
                // sleep for the rest of the cycle, measure time for more accuracy
                // TODO: random pulses
                {
                    int sleepTime = cycleLength - (int)sw.ElapsedMilliseconds;
                    if (sleepTime > 0)
                    {
                        Thread.Sleep(sleepTime);
                    }
                }
            } // \while(run)
            //
            if (!(rotationSensor1 is null))
            {
                rotationSensor1.Write(false);
            }
            if (!(rotationSensor2 is null))
            {
                rotationSensor2.Write(false);
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
            foreach (ComboBox ctrl in new List<ComboBox> { ddl_GPIO1, ddl_GPIO2, ddl_GPIO3 })
            {
                foreach (string gPIO_PinName in Enum.GetNames(typeof(GPIOPins)))
                {
                    ctrl.Items.Add(gPIO_PinName);
                }
            }

            // Set up static variables for run-thread from default values.
            pulseLength1 = (int)nud_S1_PulseLength.Value;
            cycleLength = (int)nud_CycleLength.Value;
            randomPulsesLength1 = (int)nud_S1_RandomPulseLength.Value;
            pulseLength2 = (int)nud_S2_PulseLength.Value;
            delay = (int)nud_S2_Delay.Value;
            randomPulsesLength2 = (int)nud_S2_RandomPulseLength.Value;
            randomPulses1 = cb_S1_RandomPulses.Checked;
            randomPulses2 = cb_S2_RandomPulses.Checked;
            s1_default = cb_GPIO1_Default.Checked;
            s2_default = cb_GPIO2_Default.Checked;

            // Set up trackControl (Slider) values.
            sl_Delay.Value = delay;
            sl_Delay.Maximum = (int)nud_S2_Delay.Maximum;
            sl_CycleLength.Value = cycleLength;
            sl_CycleLength.Maximum = (int)nud_CycleLength.Maximum;
            sl_PulseLength1.Value = pulseLength1;
            sl_PulseLength1.Maximum = (int)nud_S1_PulseLength.Maximum;
            sl_PulseLenght2.Value = pulseLength2;
            sl_PulseLenght2.Maximum = (int)nud_S2_PulseLength.Maximum;
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
                run = false;
                cb_Run.Checked = false;
                cb_Run_CheckedChanged(sender, e);

                // set gpio pins
                Control ctrl = (Control)sender;
                // Control is rotation sensor 1
                if (ctrl.Name == ddl_GPIO1.Name)
                {
                    // reset old Pin
                    if (!(rotationSensor1 is null))
                    {
                        rotationSensor1.Dispose();
                        rotationSensor1 = null;
                    }
                    // set new pin
                    GPIOPins pin = (GPIOPins)Enum.Parse(typeof(GPIOPins), ddl_GPIO1.SelectedItem.ToString());
                    if (pin != GPIOPins.GPIO_NONE)
                    {
                        rotationSensor1 = new GPIOMem(pin, GPIODirection.Out);
                    }
                }
                // Control is rotation sensor 2
                else if (ctrl.Name == ddl_GPIO2.Name)
                {
                    if (!(rotationSensor2 is null))
                    {
                        rotationSensor2.Dispose();
                        rotationSensor2 = null;
                    }
                    GPIOPins pin = (GPIOPins)Enum.Parse(typeof(GPIOPins), ddl_GPIO2.SelectedItem.ToString());
                    if (pin != GPIOPins.GPIO_NONE)
                    {
                        rotationSensor2 = new GPIOMem(pin, GPIODirection.Out);
                    }
                }
                // Control is power sensor
                else if (ctrl.Name == ddl_GPIO3.Name)
                {
                    if (!(powerSensor is null))
                    {
                        powerSensor.Dispose();
                        powerSensor = null;
                    }
                    GPIOPins pin = (GPIOPins)Enum.Parse(typeof(GPIOPins), ddl_GPIO3.SelectedItem.ToString());
                    if (pin != GPIOPins.GPIO_NONE)
                    {
                        powerSensor = new GPIOMem(pin, GPIODirection.Out);
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
                powerSensor.Write(!(cb_GPIO3_Default.Checked ^ cb_PowerSensorOut.Checked));
            }
        }

        /// <summary>
        /// Called when Run CB changes or when Run status changes programatically.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_Run_CheckedChanged(object sender, EventArgs e)
        {
            run = cb_Run.Checked;
            if (run)
            {
                t = new Thread(new ThreadStart(RunPulseGen));
                t.Start();
            }
            else
            {
                if (!(t is null))
                {
                    t.Join();
                    t = null;
                }
            }
        }

        /// <summary>
        /// Gets the minimum value for the pulse cycle length
        /// </summary>
        /// <returns>Minimum value for cycle length in ms</returns>
        private int getMinCycleLength()
        {
            return Math.Max(pulseLength1, pulseLength2 + delay);
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nud_S1_PulseLength_ValueChanged(object sender, EventArgs e)
        {
            pulseLength1 = (int)nud_S1_PulseLength.Value;
            sl_PulseLength1.Value = pulseLength1;
            int min = getMinCycleLength();
            nud_CycleLength.Minimum = min;
            sl_CycleLength.Minimum = min;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nud_CycleLength_ValueChanged(object sender, EventArgs e)
        {
            cycleLength = (int)nud_CycleLength.Value;
            sl_CycleLength.Value = cycleLength;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nud_S1_RandomPulseLength_ValueChanged(object sender, EventArgs e)
        {
            randomPulsesLength1 = (int)nud_S1_RandomPulseLength.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nud_S2_PulseLength_ValueChanged(object sender, EventArgs e)
        {
            pulseLength2 = (int)nud_S2_PulseLength.Value;
            sl_PulseLenght2.Value = pulseLength2;
            int min = getMinCycleLength();
            nud_CycleLength.Minimum = min;
            sl_CycleLength.Minimum = min;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nud_S2_Delay_ValueChanged(object sender, EventArgs e)
        {
            delay = (int)nud_S2_Delay.Value;
            sl_Delay.Value = delay;
            int min = getMinCycleLength();
            nud_CycleLength.Minimum = min;
            sl_CycleLength.Minimum = min;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nud_S2_RandomPulseLength_ValueChanged(object sender, EventArgs e)
        {
            randomPulsesLength2 = (int)nud_S2_RandomPulseLength.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_S1_RandomPulses_CheckedChanged(object sender, EventArgs e)
        {
            randomPulses1 = cb_S1_RandomPulses.Checked;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_S2_RandomPulses_CheckedChanged(object sender, EventArgs e)
        {
            randomPulses2 = cb_S2_RandomPulses.Checked;
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
            // Stop thread by letting it run out.
            if (!(t is null))
            {
                run = false;
                t.Join();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sl_PulseLength1_Scroll(object sender, EventArgs e)
        {
            nud_S1_PulseLength.Value = sl_PulseLength1.Value;
            nud_S1_PulseLength_ValueChanged(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sl_CycleLength_Scroll(object sender, EventArgs e)
        {
            nud_CycleLength.Value = sl_CycleLength.Value;
            nud_CycleLength_ValueChanged(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sl_PulseLenght2_Scroll(object sender, EventArgs e)
        {
            nud_S2_PulseLength.Value = sl_PulseLenght2.Value;
            nud_S2_PulseLength_ValueChanged(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sl_Delay_Scroll(object sender, EventArgs e)
        {
            nud_S2_Delay.Value = sl_Delay.Value;
            nud_S2_Delay_ValueChanged(sender, e);
        }
    }
}
