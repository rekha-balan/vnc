using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EyeOnLife.User_Interface.User_Controls
{
    public partial class wucEyeOnLifeMain : UserControl
    {
        const string TYPE_NAME = "wucEyeOnLifeMain";

        public wucEyeOnLifeMain()
        {
#if TRACE
            //Common.WriteToDebugWindow(string.Format("{0}:{1}()", TYPE_NAME, System.Reflection.MethodInfo.GetCurrentMethod().Name));
#endif
            InitializeComponent();
        }
    }
}
