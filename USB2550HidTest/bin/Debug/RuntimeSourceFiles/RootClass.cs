using System;
using System.Text;
using System.Windows.Forms;
using Microsan;
using USB2550HidTest.Forms;
//using OxyPlot;
//using OxyPlot.Series;
//using OxyPlot.WindowsForms;
//using System.Collections.Generic;
//using System.Drawing;
//using System.IO.Ports;

namespace MyNamespace
{ 
    public class RootClass
    {
        
        static Mainform form; // root object
        
        static int loopCounter = 0;
        
        static bool cpuTemp_wasHigh = false;
        static bool gpuTemp_wasHigh = false;
        static bool nbTemp_wasHigh = false;
        static bool cpuPump_error = false;
        static bool cpuFanA_error = false;
        static bool cpuFanB_error = false;
        
        static int cpuPump = 1;
        static int cpuFanA = 3;
        static int cpuFanB = 4;
        static int gpuPump = 2;
        static int gpuFanA = 5;
        static int gpuFanB = 6;
        
        static int cpuMaxTemp = 60;
        static int cpuMaxTempLow = 50;
        static int gpuMaxTemp = 70;
        static int nbMaxTemp = 60;

        static int cpuPumpMinRpm = 2000;
        static int cpuPumpMaxRpm = 7000;

        static int cpuPumpMinPwm = 450;
        static int cpuPumpMaxPwm = 640;

        static int cpuFanMinRpm = 200;
        static int cpuFanMaxRpm = 2500;

        static int cpuFanMinPwm = 100;
        static int cpuFanMidPwm = 320;
        static int cpuFanMaxPwm = 640;

        static int gpuFanMinRpm = 500;
        static int gpuFanMaxRpm = 2500;

        static int gpuFanMinPwm = 100;
        static int gpuFanMaxPwm = 640;
        
        static int gpuR9Fan = 0;
        static int  cpuTemp = 0;
        static int  gpuTemp = 0;
        static int  gpuTempOld = 0;
        static int  gpuTempDelta = 2;
        static int  gpuR9FanCtrlVal = 0;
        
        static int  nbTemp  = 0;
        static int  nbTempOld  = 0;
        static int  nbTempDelta  = 2;
        static int  nbTempStart = 45; // at wish temperature the fans should be start regulating.
        static int  nbTempStartAdd = 150;
        static int  nbTempNext = 50; // at wish temperature the fans should be start regulating.
        static int  nbTempNextAdd = 200;
        static int  nbFanCtrlVal = 0;
        static int  nbFanCtrlValOld = 0;

        static int  cpuPumpRpm  = 0;
        static int  cpuFanA_rpm = 0;
        static int  cpuFanB_rpm = 0;
        
        static bool StartedOnce = true;
        
        public static void initGUI()
        {
            
        }
        
        public static void StartOnce()
        {
            StartedOnce                         = false;
            form.Text                           = "Microsan84 - PIC24FJ256GB106 pwm controller " + DateTime.Now.ToString();
            form.tsslblScriptRunCountLabel.Text = "Script Run Count:";
            form.Opacity                        = 1.0f;
            loopCounter                         = 0;
            
            SetPwm(cpuPump, cpuPumpMinPwm);
            SetPwm(cpuFanA, cpuFanMinPwm);
            SetPwm(cpuFanB, cpuFanMinPwm);
        }
        /// <summary> The main entry point for the runtime compile code. </summary>
        public static void RootMain(object rootObject)
        {
            form = (Mainform)rootObject;
            
            if (StartedOnce)
                StartOnce();
            form.tsslblScriptRunCountText.Text = (loopCounter++).ToString();
            
            gpuR9Fan = GetSensorValue("/atigpu/0/fan/0");
            cpuTemp  = GetSensorValue("/amdcpu/0/temperature/0");
            gpuTemp  = GetSensorValue("/atigpu/0/temperature/0");
            nbTemp   = GetSensorValue("/lpc/it8728f/temperature/1");

            cpuPumpRpm  = GetRpmValue(cpuPump);
            cpuFanA_rpm = GetRpmValue(cpuFanA);
            cpuFanB_rpm = GetRpmValue(cpuFanB);
            /*
            logLine("R9Fan = " + gpuR9Fan);
            logLine("cpuTemp = " + cpuTemp);
            logLine("gpuTemp = " + gpuTemp);
            logLine("nbTemp = " + nbTemp);
            logLine("cpuPumpRpm = " + cpuPumpRpm);
            logLine("cpuFanA_rpm = " + cpuFanA_rpm);
            logLine("cpuFanB_rpm = " + cpuFanB_rpm);
            */
            
            if (gpuTemp != gpuTempOld)
            {
                if (Math.Abs(gpuTemp - gpuTempOld) >= gpuTempDelta)
                {
                    gpuTempOld      = gpuTemp;
                    gpuR9FanCtrlVal = gpuTempOld - 10;
                    SetSensorControlSoftwareValue("/atigpu/0/control/0", gpuR9FanCtrlVal);
                    logLine("GPU fan changed to " + gpuR9FanCtrlVal + " @ temp " + gpuTemp);
                }
            }
            /*
            if (nbTemp != nbTempOld)
            {
                if (Math.Abs(nbTemp - nbTempOld) >= nbTempDelta)
                {
                    nbTempOld    = nbTemp;
                    
                    if (nbTemp >= nbTempNext)
                        nbFanCtrlVal = nbTempOld + nbTempNextAdd;
                    else if (nbTemp >= nbTempStart)
                        nbFanCtrlVal = nbTempOld + nbTempStartAdd;
                    else
                        nbFanCtrlVal = 100;
                    
                    if (nbFanCtrlVal != nbFanCtrlValOld)
                    {
                        nbFanCtrlValOld = nbFanCtrlVal;
                        SetPwm(cpuFanA, nbFanCtrlVal);
                        SetPwm(cpuFanB, nbFanCtrlVal);
                        logLine("NB & CPU fan changed to " + nbFanCtrlVal + " @ NB temp " + nbTemp);
                    }
                }
            }
            */
            /*
            if (cpuPumpRpm > cpuPumpMinRpm && cpuPumpRpm < cpuPumpMaxRpm)
            {
                if (cpuPump_error)
                {
                    cpuPump_error = false;
                    //SetPwm(cpuPump, cpuPumpMinPwm);
                }
            }
            else
            {
                if (!cpuPump_error)
                {
                    cpuPump_error = true;
                    //SetPwm(cpuPump, cpuPumpMaxPwm);
                    logLine("CPU pump error! RPM@" + cpuPumpRpm);
                }
            }
            */
            if (cpuFanA_rpm > cpuFanMinRpm && cpuFanA_rpm < cpuFanMaxRpm)
            {
                if (cpuFanA_error)
                {
                    cpuFanA_error = false;
                    SetPwm(cpuFanA, cpuFanMidPwm);
                }
            }
            else if (cpuFanA_rpm != -1)
            {
                SetPwm(cpuFanA, cpuFanMaxPwm);
                
                if (!cpuFanA_error)
                {
                    cpuFanA_error = true;
                    logLine("CPU fan A error! RPM@" + cpuFanA_rpm);
                }
            }

            if (cpuFanB_rpm > cpuFanMinRpm && cpuFanB_rpm < cpuFanMaxRpm)
            {
                if (cpuFanB_error)
                {
                    cpuFanB_error = false;
                    SetPwm(cpuFanB, cpuFanMidPwm);
                }
            }
            else if (cpuFanB_rpm != -1)
            {
                SetPwm(cpuFanB, cpuFanMaxPwm);
                
                if (!cpuFanB_error)
                {
                    cpuFanB_error = true;
                    logLine("CPU fan B error! RPM@" + cpuFanB_rpm);
                }
            }

            if (cpuTemp >= cpuMaxTemp)
            {
                if (!cpuTemp_wasHigh)
                {
                    cpuTemp_wasHigh = true;
                    SetPwm(cpuFanA, cpuFanMaxPwm);
                    SetPwm(cpuFanB, cpuFanMaxPwm);
                    SetPwm(cpuPump, cpuPumpMaxPwm);
                    logLine("CPU temp was too high. @ " + cpuTemp);
                }
            }
            else if (cpuTemp <= cpuMaxTempLow)
            {
                if (cpuTemp_wasHigh)
                {
                    cpuTemp_wasHigh = false;
                    SetPwm(cpuFanA, cpuFanMidPwm);
                    SetPwm(cpuFanB, cpuFanMidPwm);
                    SetPwm(cpuPump, cpuPumpMinPwm);
                }
            }
            
        }
        private static void logLine(string text)
        {
            form.AppendLineToLog(text);
        }
        private static int GetSensorValue(string name)
        {
            return Convert.ToInt32(form.openHardwareMonitorControl.GetSensorValue(name));
        }
        private static void SetPwm(int nr, int value)
        {
            form.SetPwm(nr, value);
        }
        private static int GetRpmValue(int nr)
        {
            return form.GetRpmValue(nr);
        }
        private static void SetSensorControlSoftwareValue(string name, int val)
        {
            form.openHardwareMonitorControl.SetSensorControlSoftwareValue(name, (float)val);
        }
        private static void SetSensorControlDefaultValue(string name)
        {
            form.openHardwareMonitorControl.SetSensorControlDefaultValue(name);
        }
        public static void SystemResumeFromSleep() // called from outside
        {
            logLine("SystemResumeFromSleep bajs");
        }
        public static void SystemGoingToSleep() // called from outside
        {
            logLine("SystemGoingToSleep bajs");
        }
    }
}