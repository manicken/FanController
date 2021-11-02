using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace System.Windows.Forms
{
    public static class ThreadSafe
    {
        

        public static void Exec(Control ctrl, MethodInvoker method)
        {
            if (ctrl.InvokeRequired)
                ctrl.Invoke(method);
            else
                method();
        }

    }
}
