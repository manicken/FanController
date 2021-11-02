

using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Windows.Forms;
using UsbHid;
using UsbHid.USB.Classes.Messaging;
using System.Drawing;
using System.Data;
using Microsoft.Win32;
using Microsan;

namespace USB2550HidTest.Forms
{
    public partial class Mainform : Form
    {
		private const int USB_DEV_VID = 0x04D8;
		private const int USB_DEV_PID = 0x003F;
		
        private const int PWM_DEFAULT_MAX_SETTING = 639;
        private const int PWM_PUMP_MIN_SETTING = 9;
        private const int PWM_FAN_MIN_SETTING = 100;

        public UsbHidDevice usbHidDev;
        public RuntimeProgramming rtp;

        public OpenHardwareMonitor.GUI.OpenHardwareMonitorControl openHardwareMonitorControl;

        private bool appendAtEnd = true;

        private AdvancedForm frmAdvanced;

        private Label[] lblRPMs;
        private CustomSlider[] tbarPWMs;
        //private BoolByRef rpmValuesUpdated = new BoolByRef(false);
        private int[] rpmValues;

        public bool debuglog_rpm = false;
        public bool DebugPwmSet = false;
        

        //private bool pwmDirectSet = true;

        private volatile ConcurrentQueue<CommandMessage> usbHidDeviceSendQueue;
        
        private volatile bool usbHidDevice_IsSending = false;

        public Mainform()
        {
            InitializeComponent();
            tsslblScriptRunCountText.Text = "0";
            chkBoxShowGetRPMdata.Checked = debuglog_rpm;
            chkBoxDebugPwmSet.Checked = DebugPwmSet;

            rpmValues = new int[6];
            for (int ifuck = 0; ifuck < 6; ifuck++)
                rpmValues[ifuck] = -1;

            rtp = new RuntimeProgramming(this);

            openHardwareMonitorControl = new OpenHardwareMonitor.GUI.OpenHardwareMonitorControl();
            openHardwareMonitorControl.SensorValuesUpdated += OpenHardwareMonitorControl_SensorValueRead;
            openHardwareMonitorControl.SensorPathRequested += OpenHardwareMonitorControl_SensorPathRequested;

            usbHidDeviceSendQueue = new ConcurrentQueue<CommandMessage>();

            frmAdvanced = new AdvancedForm();
            frmAdvanced.HidSendCommand = HidSendCommand_Enqueued;
            frmAdvanced.HidSendCommandMessage = HidSendCommandMessage_Enqueued;
            frmAdvanced.AppendTextToLog = AppendToLog;

            rtxtLog.Clear();

            SetConnectedState(false);

            lblRPMs = new Label[6];
            tbarPWMs = new CustomSlider[6];

            
            lblRPMs[0] = lblRPM1; lblRPMs[1] = lblRPM2; lblRPMs[2] = lblRPM3; lblRPMs[3] = lblRPM4; lblRPMs[4] = lblRPM5; lblRPMs[5] = lblRPM6;

            tbarPWMs[0] = tbarPWM1; tbarPWMs[1] = tbarPWM2; tbarPWMs[2] = tbarPWM3; tbarPWMs[3] = tbarPWM3; tbarPWMs[4] = tbarPWM5; tbarPWMs[5] = tbarPWM5;

            tbarPWM1.Tag = 1;
            tbarPWM2.Tag = 2;
            tbarPWM1.ValueMin = PWM_PUMP_MIN_SETTING;
            tbarPWM2.ValueMin = PWM_PUMP_MIN_SETTING;
            tbarPWM1.ValueMax = PWM_DEFAULT_MAX_SETTING;
            tbarPWM2.ValueMax = PWM_DEFAULT_MAX_SETTING;
            int i = 0;
            for (i = 2; i < 6; i++)
            {
                tbarPWMs[i].Tag = i + 1;
                
                tbarPWMs[i].ValueMin = PWM_FAN_MIN_SETTING;
                tbarPWMs[i].ValueMax = PWM_DEFAULT_MAX_SETTING;
            }

            for (i = 0; i < 6; i++)
            {
                lblRPMs[i].Text = "0";
                lblRPMs[i].AutoSize = false;
                lblRPMs[i].TextAlign = ContentAlignment.MiddleCenter;
                lblRPMs[i].Size = new Size(lblRPMs[i].Parent.Width, 16);
                lblRPMs[i].Left = 0;

                tbarPWMs[i].SliderMoved += new CustomSlider.SliderMovedEventHandler(tbarPWM_SliderMoved);
            }
            
            PrintCurrentThread_NameOrHashCode("main");
        }
        
        public void AppendLineToLog(string line)
        {
            rtxtLog.AppendLine_ThreadSafe(line, true);
        }

        public void SetSensorUpdateInterval(int inMs)
        {
            openHardwareMonitorControl.timerMonitorUpdate.Interval = inMs;
        }

        public void SetPwm(int nr, int value)
        {
            tbarPWMs[nr - 1].Value = value;
            _SetPwm(nr, value);
        }

        public int GetRpmValue(int nr)
        {
            if ((nr < 0) || (nr >= rpmValues.Length)) return -1;
            return rpmValues[nr-1];
        }

        private void OpenHardwareMonitorControl_SensorValueRead()
        {
            if (!this.Visible) return;

            if (usbHidDev.IsDeviceConnected)
                HidSendCommand_Enqueued((byte)HID_CMD.FAN_RPM_GET_VALUES);
            try
            {
                if (rtp.ScriptChanged)
                    rtp.CompileAndGetMainMethodDelegate();

                rtp.TryStart_MainMethod();
            }
            catch (Exception ex)
            {
                ThreadSafe.Exec(this, () => rtxtLog.AppendText(ex.ToString() + Environment.NewLine));
            }
        }
        public void OpenHardwareMonitorControl_SensorPathRequested(string path)
        {
            ThreadSafe.Exec(this, () =>
            {
                if (rtp.srcEditCtrl.Visible)
                    rtp.srcEditCtrl.fctb.SelectedText = "\"" + path + "\"";

                Clipboard.SetText(path);
            });
        }

        public void PrintCurrentThread_NameOrHashCode(string threadAlias)
        {
            string name = System.Threading.Thread.CurrentThread.Name;
            string msg = "";
            if (name == null)
            {
                System.Threading.Thread.CurrentThread.Name = threadAlias;
                msg = threadAlias + " was set for threadname";
            }
            else
            {
                msg = threadAlias + " was executed on thread: " + System.Threading.Thread.CurrentThread.Name;// + System.Threading.Thread.CurrentThread.GetHashCode().ToString("X4");
            }
            ThreadSafe.Exec(this, () => rtxtLog.AppendText(msg + Environment.NewLine));
        }

        private void timerRpmUpdate_Tick(object s, EventArgs ea)
        {
            
        }

        private void AppendToLog(string text)
        {
            rtxtLog.AppendText(text);
        }

        private void FrmMainLoad(object sender, EventArgs e)
        {
            
        }

        private void Register_USB_HID_device_Events()
        {
            usbHidDev.OnConnected += DeviceOnConnected;
            usbHidDev.OnDisConnected += DeviceOnDisConnected;
            usbHidDev.DataReceived += DeviceDataReceived;
            usbHidDev.OnAttached += USB_HID_device_attached;
        }

        private void DeRegister_USB_HID_device_Events()
        {
            usbHidDev.OnConnected -= DeviceOnConnected;
            usbHidDev.OnDisConnected -= DeviceOnDisConnected;
            usbHidDev.DataReceived -= DeviceDataReceived;
            usbHidDev.OnAttached -= USB_HID_device_attached;
        }

        private void Register_SystemEvents_PowerModeChanged()
        {
            SystemEvents.PowerModeChanged += new PowerModeChangedEventHandler(SystemEvents_PowerModeChanged);
        }
        private void DeRegister_SystemEvents_PowerModeChanged()
        {
            SystemEvents.PowerModeChanged -= new PowerModeChangedEventHandler(SystemEvents_PowerModeChanged);
        }

        private void SystemResumeFromSleep()
        {
            rtp.TryStart_RootClass_Method("SystemResumeFromSleep");
            usbHidDev.Connect();
            openHardwareMonitorControl.timerMonitorUpdate.Start();
        }

        private void SystemGoingToSleep()
        {
            openHardwareMonitorControl.timerMonitorUpdate.Stop();
            rtp.TryStart_RootClass_Method("SystemGoingToSleep");
            if (!usbHidDev.IsDeviceConnected) return;
            usbHidDev.Disconnect();
        }

        private void SystemEvents_PowerModeChanged(object s, PowerModeChangedEventArgs pmcea)
        {
            PrintCurrentThread_NameOrHashCode("SystemEvents_PowerModeChanged");

            if (pmcea.Mode == PowerModes.Resume) // system is resumed from suspend
            {
                SystemResumeFromSleep();
            }
            else if (pmcea.Mode == PowerModes.Suspend) // system is going to suspend
            {
                SystemGoingToSleep();
            }
        }

        private void USB_HID_device_attached()
        {
            PrintCurrentThread_NameOrHashCode("USB_HID_device_attached");
            rtxtLog.AppendLine_ThreadSafe("device plugged in!!", appendAtEnd);
        }
        /*
        private int[] getBits_setToOne(byte data)
        {
            List<int> oneBits = new List<int>();
            for (int i = 0; i < 8; i++)
            {
                if ((data & getByteBitMask(i)) == getByteBitMask(i))
                    oneBits.Add(i);
            }
            return oneBits.ToArray();
        }

        private byte getByteBitMask(int bitNr)
        {
            if (bitNr == 0) return 0x01;
            else if (bitNr == 1) return 0x02;
            else if (bitNr == 2) return 0x04;
            else if (bitNr == 3) return 0x08;
            else if (bitNr == 4) return 0x10;
            else if (bitNr == 5) return 0x20;
            else if (bitNr == 6) return 0x40;
            else if (bitNr == 7) return 0x80;

            return 0x00;
        }
        */
        
        private void DeviceDataReceived(byte[] data)
        {
            //PrintCurrentThread_NameOrHashCode("DeviceDataReceived");
            //data[0] is dummy allways zero
            int payLoadLength = data[1];
            int[] int16Data;


            switch (data[2])
            {
                
                case (byte)HID_CMD_GET_ACK.HID_CMD_GET_OCxCON_REGs:
                    int16Data = data.ToIntArray(3, payLoadLength + 2, 2, true);
                    if (frmAdvanced.Visible)
                        int16Data.PutInto_DataTable_Col(frmAdvanced.dsData.Tables[0], 1);
                    rtxtHidCommLog.AppendLine_ThreadSafe("HID_CMD_GET_OCxCON_REGs: " + int16Data.ToHexString(" "), appendAtEnd);
                    break;
                case (byte)HID_CMD_GET_ACK.HID_CMD_GET_OCxR_REGs:
                    int16Data = data.ToIntArray(3, payLoadLength + 2, 2, true);
                    for (int i = 0; i < tbarPWMs.Length; i++)
                        ThreadSafe.Exec(this, () => tbarPWMs[i].Value = int16Data[i]);
                    if (frmAdvanced.Visible)
                        int16Data.PutInto_DataTable_Col(frmAdvanced.dsData.Tables[1], 1);
                    rtxtHidCommLog.AppendLine_ThreadSafe("HID_CMD_GET_OCxR_REGs: " + int16Data.ToHexString(" "), appendAtEnd);
                    break;
                case (byte)HID_CMD_GET_ACK.HID_CMD_GET_OCxRS_REGs:
                    int16Data = data.ToIntArray(3, payLoadLength + 2, 2, true);
                    for (int i = 0; i < tbarPWMs.Length; i++)
                        ThreadSafe.Exec(this, () => tbarPWMs[i].ValueMax = int16Data[i]);
                    if (frmAdvanced.Visible)
                        int16Data.PutInto_DataTable_Col(frmAdvanced.dsData.Tables[2], 1);
                    rtxtHidCommLog.AppendLine_ThreadSafe("HID_CMD_GET_OCxRS_REGs: " + int16Data.ToHexString(" "), appendAtEnd);
                    break;
                case (byte)HID_CMD_GET_ACK.HID_CMD_GET_ICxCON_REGs:
                    int16Data = data.ToIntArray(3, payLoadLength + 2, 2, true);
                    if (frmAdvanced.Visible)
                        int16Data.PutInto_DataTable_Col(frmAdvanced.dsData.Tables[3], 1);
                    rtxtHidCommLog.AppendLine_ThreadSafe("HID_CMD_GET_ICxCON_REGs: " + int16Data.ToHexString(" "), appendAtEnd);
                    break;
                case (byte)HID_CMD_GET_ACK.HID_CMD_GET_TxCON_REGs:
                    int16Data = data.ToIntArray(3, payLoadLength + 2, 2, true);
                    if (frmAdvanced.Visible)
                        int16Data.PutInto_DataTable_Col(frmAdvanced.dsData.Tables[4], 1);
                    rtxtHidCommLog.AppendLine_ThreadSafe("HID_CMD_GET_TxCON_REGs: " + int16Data.ToHexString(" "), appendAtEnd);
                    break;
                case (byte)HID_CMD_GET_ACK.HID_CMD_GET_PRx_REGs:
                    int16Data = data.ToIntArray(3, payLoadLength + 2, 2, true);
                    if (frmAdvanced.Visible) 
                        int16Data.PutInto_DataTable_Col(frmAdvanced.dsData.Tables[5], 1);
                    rtxtHidCommLog.AppendLine_ThreadSafe("HID_CMD_GET_PRx_REGs: " + int16Data.ToHexString(" "), appendAtEnd);
                    break;
                case (byte)HID_CMD_ACK.HID_CMD_IC_GET_LAST_CAPTURED_VALUES:
                    int16Data = data.ToIntArray(3, payLoadLength + 2, 2, false);

                    rtxtHidCommLog.AppendLine_ThreadSafe("HID_CMD_IC_GET_LAST_CAPTURED_VALUES: " + int16Data.ToHexString(" "), appendAtEnd);
                    break;
                case (byte)HID_CMD_ACK.FAN_RPM_GET_VALUES:
                    int16Data = data.ToIntArray(3, payLoadLength + 2, 2, true);

                    for (int i = 0; i < 6; i++)
                    {
                        rpmValues[i] = int16Data[i];
                        ThreadSafe.Exec(lblRPMs[i], () => lblRPMs[i].Text = rpmValues[i].ToString());
                    }
                    //rpmValuesUpdated.Value = true;

                    if (debuglog_rpm) rtxtHidCommLog.AppendLine_ThreadSafe("HID_CMD_ACK.FAN_RPM_GET_VALUES: " + int16Data.ToHexString(" "), appendAtEnd);

                    break;
                case (byte)HID_CMD_SET_ACK.PWM_DUTY:

                    rtxtHidCommLog.AppendLine_ThreadSafe("HID_CMD_SET_ACK.PWM_DUTY: " + data[3].ToString("X2"), appendAtEnd);

                    break;
                case (byte)HID_CMD_ACK.LOAD_SETTINGS_FROM_EEPROM:
                    rtxtHidCommLog.AppendLine_ThreadSafe("LOAD_SETTINGS_FROM_EEPROM (ack): " + data.Trim(3, payLoadLength+3).ToHexString(" ", 2), appendAtEnd);
                    break;
                case (byte)HID_CMD_ACK.SAVE_SETTINGS_TO_EEPROM:
                    rtxtHidCommLog.AppendLine_ThreadSafe("SAVE_SETTINGS_TO_EEPROM (ack): " + data.Trim(3, payLoadLength + 3).ToHexString(" ", 2), appendAtEnd);
                    break;
                    
                default:

                    rtxtHidCommLog.AppendLine_ThreadSafe("RAW data: " + data.ToHexString(" ", 1, false), appendAtEnd);
                    break;
            }

            if (usbHidDeviceSendQueue.Count != 0)
            {
                CommandMessage cmTemp;
                if (usbHidDeviceSendQueue.TryDequeue(out cmTemp))
                    HidSendCommandMessage(cmTemp);
                else
                {
                    rtxtLog.AppendLine_ThreadSafe("problem while usbHidDeviceSendQueue.Dequeue()", appendAtEnd);
                    usbHidDevice_IsSending = false;
                }
            }
            else
                usbHidDevice_IsSending = false;
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            openHardwareMonitorControl.timerMonitorUpdate.Stop();

            DeRegister_SystemEvents_PowerModeChanged();
            DeRegister_USB_HID_device_Events();

            if (usbHidDev.IsDeviceConnected)
            {
                //programClosing = true;
                //e.Cancel = true;
                usbHidDev.Disconnect(); // this method is running syncronized
                
            }
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            rtxtLog.Clear();
        }

        private void HidSendCommandMessage_Enqueue(byte cmd, params byte[] payload)
        {
            usbHidDeviceSendQueue.Enqueue(new CommandMessage(cmd, payload));
        }
        private void HidSendCommand_Enqueue(byte cmd)
        {
            usbHidDeviceSendQueue.Enqueue(new CommandMessage(cmd));
        }
        private void HidSendCommand_Dequeue_StartSend()
        {
            CommandMessage cm;
            if (usbHidDeviceSendQueue.TryDequeue(out cm))
                HidSendCommandMessage(cm);
            else
            {
                rtxtLog.AppendLine_ThreadSafe("@ HidSendCommand_Dequeue_StartSend\n  problem while usbHidDeviceSendQueue.Dequeue() ", appendAtEnd);
                usbHidDevice_IsSending = false;
            }
        }

        private void HidSendCommandMessage_Enqueued(byte cmd, params byte[] payload)
        {
            if ((usbHidDeviceSendQueue.Count == 0) && !usbHidDevice_IsSending)
                HidSendCommandMessage(new CommandMessage(cmd, payload));
            else
                usbHidDeviceSendQueue.Enqueue(new CommandMessage(cmd, payload));
        }

        private void HidSendCommand_Enqueued(byte cmd)
        {
            if ((usbHidDeviceSendQueue.Count == 0) && !usbHidDevice_IsSending)
                HidSendCommandMessage(new CommandMessage(cmd));
            else
                usbHidDeviceSendQueue.Enqueue(new CommandMessage(cmd));
        }

        private void HidSendCommandMessage(CommandMessage cmdMsg)
        {
            if (usbHidDev == null)
                Initialize_USB_HID_dev();

            if (!usbHidDev.IsDeviceConnected)
                usbHidDev.Connect(); // this is direct (executing and finalizing in caller thread)

            usbHidDevice_IsSending = true;
            usbHidDev.SendMessage(cmdMsg);
        }

        

        private void btnGetRpmValues_Click(object sender, EventArgs e)
        {
            
        }
        /*
        private void tbarPWM_MouseUp(object s, MouseEventArgs e)
        {
            if (pwmDirectSet)
                return;

            TrackBar tb = (TrackBar)s;
            _SetPwm(Convert.ToInt32((string)tb.Tag), tb.Value);
            tb = null;
            s = null;
        }
        */
        private void SetPwms(int[] nr, int[] value)
        {
            for (int i = 0; i < nr.Length; i++)
            {
                byte[] valueBytes = BitConverter.GetBytes((ushort)value[i]);

                if (BitConverter.IsLittleEndian)
                    Array.Reverse(valueBytes);

                HidSendCommandMessage_Enqueue((byte)HID_CMD_SET.PWM_DUTY, Convert.ToByte(nr[i]), valueBytes[0], valueBytes[1]);

                //rtxtLog.AppendText("pwm(" + nr + ") = " + valueBytes.ToHexString(" ", 2, !BitConverter.IsLittleEndian) + "\n");
            }
            HidSendCommand_Dequeue_StartSend();
        }

        private void _SetPwm(int nr, int value)
        {
            
            byte[] valueBytes = BitConverter.GetBytes((ushort)value);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(valueBytes);

            HidSendCommandMessage_Enqueued((byte)HID_CMD_SET.PWM_DUTY, Convert.ToByte(nr), valueBytes[0], valueBytes[1]);

            if (DebugPwmSet)
                rtxtLog.AppendText("_SetPwm(" + nr + ") = " + valueBytes.ToHexString(" ", 2) + "\n");
        }

        private void tsbtnConnect_Click(object sender, EventArgs e)
        {
            if (!usbHidDev.IsDeviceConnected)
                usbHidDev.Connect();
            else
                usbHidDev.Disconnect();
        }

        private void tsbtnDisconnect_Click(object sender, EventArgs e)
        {
            usbHidDev.Disconnect();
        }

        private void DeviceOnDisConnected()
        {
            PrintCurrentThread_NameOrHashCode("DeviceOnDisConnected");
            
            SetConnectedState(false);
        }

        private void DeviceOnConnected()
        {
            PrintCurrentThread_NameOrHashCode("DeviceOnConnected");
            SetConnectedState(true);
        }

        private void SetConnectedState(bool connected)
        {
            if (connected)
            {
                HidSendCommand_Enqueued((byte)HID_CMD_GET.HID_CMD_GET_OCxRS_REGs);
                HidSendCommand_Enqueued((byte)HID_CMD_GET.HID_CMD_GET_OCxR_REGs);
                
                tsbtnConnect.Image = imgArray_dis_connect.Images[0];
                tsbtnConnect.Text = "Disconnect";
            }
            else
            {
                
                tsbtnConnect.Image = imgArray_dis_connect.Images[1];
                tsbtnConnect.Text = "Connect";
            }
        }

        private void tsbtnAdvanced_Click(object sender, EventArgs e)
        {
            frmAdvanced.Show();
        }

        private void readSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HidSendCommand_Enqueued((byte)HID_CMD.LOAD_SETTINGS_FROM_EEPROM);
        }

        private void tsmiEEPROM_saveSettings_Click(object sender, EventArgs e)
        {
            HidSendCommand_Enqueued((byte)HID_CMD.SAVE_SETTINGS_TO_EEPROM);
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void numUpDownRpmReadInterval_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void tbarPWM_SliderMoved(object tag, int value)
        {
            int nr = (int)tag;
            if ((nr == 3) || (nr == 4))
            {
                _SetPwm(3, value);
                _SetPwm(4, value);
            }
            else if ((nr == 5) || (nr == 6))
            {
                _SetPwm(5, value);
                _SetPwm(6, value);
            }
            else
                _SetPwm(nr, value);
        }

        private void Initialize_USB_HID_dev()
        {
            usbHidDev = new UsbHidDevice(USB_DEV_VID, USB_DEV_PID);

            Register_USB_HID_device_Events();
        }

        private void Mainform_Shown(object sender, EventArgs e)
        {
            Initialize_USB_HID_dev();
            
            Register_SystemEvents_PowerModeChanged();

            openHardwareMonitorControl.Dock = DockStyle.Fill;

            tsMain.Items.Add(openHardwareMonitorControl.GetMenuItems());
            tsMain.Items.Add(new ToolStripSeparator());

            openHardwareMonitorControl.SetupParentRootForm(this);



            panelOpenHardwareMonitor.Controls.Add(openHardwareMonitorControl);

            openHardwareMonitorControl.timerMonitorUpdate.Start();
        }

        

        private void tsbtnOpenScriptEditor_Click(object sender, EventArgs e)
        {
            //jsEditForm.Show(openHardwareMonitorControl.settings.GetValue("script.main", jurassicScriptEngine_defaultScript).FromBase64());
        }

        private void testFailsafeWithDivitionByZeroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            throw new Exception("Failsafe-test Exception");

        }

        private void testSystemGoingToSleepToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SystemGoingToSleep();
        }

        private void testSystemResumeFromSleepToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SystemResumeFromSleep();
        }

        private void tsbtnOpenNewScriptEditor_Click(object sender, EventArgs e)
        {
            rtp.InitScriptEditor_IfNeeded();
            rtp.ShowScriptEditor();
        }

        private void splitContainer2_SizeChanged(object sender, EventArgs e)
        {
            splitOHM.SplitterDistance = (splitOHM.Width - splitOHM.SplitterWidth) / 2;
        }

        private void rtxtLog_TextChanged(object sender, EventArgs e)
        {
            rtxtLog.ScrollToCaret();
        }

        private void rtxtCommLog_TextChanged(object sender, EventArgs e)
        {
            rtxtHidCommLog.ScrollToCaret();
        }

        private void btnClearCommLog_Click(object sender, EventArgs e)
        {
            rtxtHidCommLog.Clear();
        }

        private void chkBoxShowGetRPMdata_CheckedChanged(object sender, EventArgs e)
        {
            debuglog_rpm = chkBoxShowGetRPMdata.Checked;
        }
    }
    public class BoolByRef
    {
        public bool Value = false;

        public BoolByRef(bool value)
        {
            this.Value = value;
        }


    }
}
