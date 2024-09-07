using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAudioTest.Logger
{
    

    public class MyLogger
    {
        public Action<string> ActionStr;
        public void Log(string message)
        {
            if(ActionStr != null)
                ActionStr(message);
        }
    }
}
