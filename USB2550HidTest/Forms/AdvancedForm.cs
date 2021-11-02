using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace USB2550HidTest.Forms
{
    public partial class AdvancedForm : Form
    {
        public Action<byte, byte[]> HidSendCommandMessage;
        public Action<byte> HidSendCommand;
        public Action<string> AppendTextToLog;

        private string[] HID_CMD_GET_ENUM_NAMES = Enum.GetNames(typeof(HID_CMD_GET));
        private int[] HID_CMD_GET_ENUM_VALUES = (int[])Enum.GetValues(typeof(HID_CMD_GET));
        private int[] HID_CMD_SET_ENUM_VALUES = (int[])Enum.GetValues(typeof(HID_CMD_SET));

        private int[] HID_CMD_GET_dataRegCount = new int[] { 9, 9, 9, 9, 5, 5 };
        private int[] HID_CMD_GET_pair_reg_count = new int[] { 2, 1, 1, 2, 1, 1 };

        //public DataTable[] dtData;

        public DataSet dsData;

        private TextBox dgvTextBoxEditingControl;

        private readonly string REG_DEFAULT_VALUE = "0000";

        public AdvancedForm()
        {
            InitializeComponent();

            dsData = new DataSet("Registers");

            for (int dti = 0; dti < HID_CMD_GET_ENUM_NAMES.Length; dti++)
            {
                HID_CMD_GET_ENUM_NAMES[dti] = HID_CMD_GET_ENUM_NAMES[dti].Replace("HID_CMD_GET_", "").Replace("_REGs", "");
                DataTable dt = new DataTable(HID_CMD_GET_ENUM_NAMES[dti]);
                dt.Columns.Add("Reg", typeof(string));
                dt.Columns.Add("Value", typeof(string));

                for (int ri = 0; ri < HID_CMD_GET_dataRegCount[dti]; ri++)
                {
                    string name = HID_CMD_GET_ENUM_NAMES[dti].Replace("x", (ri + 1).ToString());
                    if (HID_CMD_GET_pair_reg_count[dti] == 1)
                    {
                        dt.Rows.Add(new object[] { name, REG_DEFAULT_VALUE });
                    }
                    else
                    {
                        for (int pri = 0; pri < HID_CMD_GET_pair_reg_count[dti]; pri++)
                        {
                            dt.Rows.Add(new object[] { name + (pri + 1), REG_DEFAULT_VALUE });
                        }
                    }
                }
                dsData.Tables.Add(dt);
            }

            cmbRegSel.Items.AddRange(HID_CMD_GET_ENUM_NAMES);
            cmbRegSel.SelectedIndex = 0;
        }

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < HID_CMD_GET_ENUM_NAMES.Length; i++)
            {
                if (System.IO.File.Exists(HID_CMD_GET_ENUM_NAMES[i] + ".xml"))
                    dsData.ReadXml(HID_CMD_GET_ENUM_NAMES[i] + ".xml");
            }
        }

        private void btnSaveFile_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < HID_CMD_GET_ENUM_NAMES.Length; i++)
            {
                dsData.WriteXml(HID_CMD_GET_ENUM_NAMES[i] + ".xml");
            }
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            HidSendCommand((byte)HID_CMD_GET_ENUM_VALUES[cmbRegSel.SelectedIndex]);
        }

        private void cmbRegSel_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgv.DataSource = dsData.Tables[cmbRegSel.SelectedIndex];
            if (chkBoxAutoRefresh.Checked && (HidSendCommand != null))
                HidSendCommand((byte)HID_CMD_GET_ENUM_VALUES[cmbRegSel.SelectedIndex]);
        }

        private void dgv_DataSourceChanged(object sender, EventArgs e)
        {
            dgv.Columns[0].Width = 96;
            ((DataGridViewTextBoxColumn)dgv.Columns[1]).MaxInputLength = 4;

        }

        private void btnSetData_Click(object sender, EventArgs e)
        {
            byte[] byteArray = dsData.Tables[cmbRegSel.SelectedIndex].GetFrom_DataTable_Col(1, 16).ToByteArray();
            if (HidSendCommandMessage != null)
                HidSendCommandMessage((byte)HID_CMD_SET_ENUM_VALUES[cmbRegSel.SelectedIndex], byteArray);
            //rtxtLog.AppendLine_ThreadSafe("FAKE TEST SEND: " + byteArray.ToHexString(-1, -1, " ", 2));
        }

        private void dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            ushort temp = 0;
            try
            {
                temp = Convert.ToUInt16(dsData.Tables[cmbRegSel.SelectedIndex].Rows[e.RowIndex][e.ColumnIndex].ToString(), 16);
            }
            catch
            {
                temp = 0;
            }
            dsData.Tables[cmbRegSel.SelectedIndex].Rows[e.RowIndex][e.ColumnIndex] = temp.ToString("x4").ToUpper();

            dgvTextBoxEditingControl.KeyDown -= new KeyEventHandler(dgvTextBoxEditingControl_KeyDown);

            dgvTextBoxEditingControl = null;
        }

        private void dgv_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {

        }

        private void dgvTextBoxEditingControl_KeyDown(object s, KeyEventArgs kea)
        {
            int kv = kea.KeyValue;
            kea.SuppressKeyPress = false;

            if ((kv >= 48) && (kv <= 57)) return;              // 0-9
            else if ((kv >= 65) && (kv <= 70)) return;              // a-f 
            else if ((kv == 8) || (kv == 37) || (kv == 39)) return; // backspace, left, right
            else if ((kv >= 96) && (kv <= 105)) return;             // numpad 0-9
            else
            {
                if (AppendTextToLog != null)
                    AppendTextToLog("Supressed Key: " + kv + Environment.NewLine);
                kea.SuppressKeyPress = true;
            }
        }
        private void dgvTextBoxEditingControl_KeyPress(object s, KeyPressEventArgs kpea)
        {
            kpea.KeyChar = char.ToUpper(kpea.KeyChar);
        }

        private void dgv_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            dgvTextBoxEditingControl = (TextBox)dgv.EditingControl;
            dgvTextBoxEditingControl.KeyDown += new KeyEventHandler(dgvTextBoxEditingControl_KeyDown);
            dgvTextBoxEditingControl.KeyPress += new KeyPressEventHandler(dgvTextBoxEditingControl_KeyPress);


        }

        private void AdvancedForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;
        }

        private void btnGetLastCaptureValues_Click(object sender, EventArgs e)
        {
            HidSendCommand((byte)HID_CMD.HID_CMD_IC_GET_LAST_CAPTURED_VALUES);
        }
    }
}
