using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UsbHid.USB.Classes.Messaging;

namespace KB.Test
{
    public enum ID_CMD
    {
        SCAN = 0x20,
        ACK = 0x80
    }
    public enum CID_CMD
    {
        KEY_CHANGED = ID_CMD.SCAN | 0x00,

        ENABLED_SET_STATE = ID_CMD.SCAN | 0x01,
        OUT_NR_SET_STATE = ID_CMD.SCAN | 0x02,
        GET_INPUT = ID_CMD.SCAN | 0x03,
        BIT5_IN_ENABLED_SET_STATE = ID_CMD.SCAN | 0x04
    }
    public enum CID_CMD_ACK
    {
        KEY_CHANGED = CID_CMD.KEY_CHANGED | ID_CMD.ACK,
        ENABLED_SET_STATE = CID_CMD.ENABLED_SET_STATE | ID_CMD.ACK,
        OUT_NR_SET_STATE = CID_CMD.OUT_NR_SET_STATE | ID_CMD.ACK,
        GET_INPUT = CID_CMD.GET_INPUT | ID_CMD.ACK,
        BIT5_IN_ENABLED_SET_STATE = CID_CMD.BIT5_IN_ENABLED_SET_STATE | ID_CMD.ACK

    }
    public static class KB_SCAN
    {
        public static CommandMessage Get_EnabledSet_CommandMessage(bool enabled)
        {
            if (enabled)
                return new CommandMessage((byte)CID_CMD.ENABLED_SET_STATE, 0x01);
            else
                return new CommandMessage((byte)CID_CMD.ENABLED_SET_STATE, 0x00);
        }
        public static CommandMessage Get_Bit5inputEnabledSet_CommandMessage(bool enabled)
        {
            if (enabled)
                return new CommandMessage((byte)CID_CMD.BIT5_IN_ENABLED_SET_STATE, 0x01);
            else
                return new CommandMessage((byte)CID_CMD.BIT5_IN_ENABLED_SET_STATE, 0x00);
        }
    }
}
