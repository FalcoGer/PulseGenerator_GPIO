using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

using RaspberryPiDotNet;

namespace PulseGenerator_GPIO
{
    public partial class MainForm : Form
    {
        // init gpio
        private static GPIOMem rotationSensor1 = null;
        private static GPIOMem rotationSensor2 = null;
        private static GPIOMem powerSensor = null;

        Thread t = null;

        static int pulseLength1 = 0;
        static bool s1_default = false;
        static int pulseLength2 = 0;
        static int cycleLength = 0;
        static bool s2_default = false;
        static int delay = 0;

        static bool randomPulses1 = false;
        static int randomPulsesLength1 = 0;
        static bool randomPulses2 = false;
        static int randomPulsesLength2 = 0;

        static bool run = false;

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

        public MainForm()
        {
            InitializeComponent();

            foreach (ComboBox ctrl in new List<ComboBox> { ddl_GPIO1, ddl_GPIO2, ddl_GPIO3 })
            {
                foreach (string gPIO_PinName in Enum.GetNames(typeof(GPIOPins)))
                {
                    ctrl.Items.Add(gPIO_PinName);
                }
            }

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
        }

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
                if (ctrl.Name == ddl_GPIO1.Name)
                {
                    if (!(rotationSensor1 is null))
                    {
                        rotationSensor1.Dispose();
                        rotationSensor1 = null;
                    }
                    GPIOPins pin = (GPIOPins)Enum.Parse(typeof(GPIOPins), ddl_GPIO1.SelectedItem.ToString());
                    rotationSensor1 = new GPIOMem(pin, GPIODirection.Out);
                }
                else if (ctrl.Name == ddl_GPIO2.Name)
                {
                    if (!(rotationSensor2 is null))
                    {
                        rotationSensor2.Dispose();
                        rotationSensor2 = null;
                    }
                    GPIOPins pin = (GPIOPins)Enum.Parse(typeof(GPIOPins), ddl_GPIO2.SelectedItem.ToString());
                    rotationSensor2 = new GPIOMem(pin, GPIODirection.Out);
                }
                else if (ctrl.Name == ddl_GPIO3.Name)
                {
                    if (!(powerSensor is null))
                    {
                        powerSensor.Dispose();
                        powerSensor = null;
                    }
                    GPIOPins pin = (GPIOPins)Enum.Parse(typeof(GPIOPins), ddl_GPIO3.SelectedItem.ToString());
                    powerSensor = new GPIOMem(pin, GPIODirection.Out);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cb_PowerSensorOut_CheckedChanged(object sender, EventArgs e)
        {
            if (!(powerSensor is null))
            {
                powerSensor.Write(!(cb_GPIO3_Default.Checked ^ cb_PowerSensorOut.Checked));
            }
        }

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

        private int getMinCycleLength()
        {
            if (pulseLength1 < pulseLength2 + delay)
            {
                //S1: ****____
                //S2: _**_____
                // Only S1 Determines cycle length
                return pulseLength1 + 1;
            }
            else
            {
                //S1: ****____
                //S2: _****___
                // Only S1 determines cycle length
                return delay + pulseLength2 + 1;
            }
        }

        private void nud_S1_PulseLength_ValueChanged(object sender, EventArgs e)
        {
            pulseLength1 = (int)nud_S1_PulseLength.Value;
            nud_CycleLength.Maximum = getMinCycleLength();
        }

        private void nud_CycleLength_ValueChanged(object sender, EventArgs e)
        {
            cycleLength = (int)nud_CycleLength.Value;
        }

        private void nud_S1_RandomPulseLength_ValueChanged(object sender, EventArgs e)
        {
            randomPulsesLength1 = (int)nud_S1_RandomPulseLength.Value;
        }

        private void nud_S2_PulseLength_ValueChanged(object sender, EventArgs e)
        {
            pulseLength2 = (int)nud_S2_PulseLength.Value;
            nud_CycleLength.Maximum = getMinCycleLength();
        }

        private void nud_S2_Delay_ValueChanged(object sender, EventArgs e)
        {
            delay = (int)nud_S2_Delay.Value;
            nud_CycleLength.Maximum = getMinCycleLength();
        }

        private void nud_S2_RandomPulseLength_ValueChanged(object sender, EventArgs e)
        {
            randomPulsesLength2 = (int)nud_S2_RandomPulseLength.Value;
        }

        private void cb_S1_RandomPulses_CheckedChanged(object sender, EventArgs e)
        {
            randomPulses1 = cb_S1_RandomPulses.Checked;
        }

        private void cb_S2_RandomPulses_CheckedChanged(object sender, EventArgs e)
        {
            randomPulses2 = cb_S2_RandomPulses.Checked;
        }

        private void cb_GPIO1_Default_CheckedChanged(object sender, EventArgs e)
        {
            s1_default = cb_GPIO1_Default.Checked;
        }

        private void cb_GPIO2_Default_CheckedChanged(object sender, EventArgs e)
        {
            s2_default = cb_GPIO2_Default.Checked;
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!(t is null))
            {
                run = false;
                t.Join();
            }
        }
    }
}
