using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSTestCases.Quality
{
    class ControlFlags
    {
        bool cflag = false;

        public void update()
        {
            if (!cflag)
            {
                if (thatThing())
                    cflag = true;
            }
            else
            {
                thatOtherThing();
            }
        }

        void thatOtherThing()
        {
            bool flag = false;
            if (flag)
            {
                //Do that
            }
            else
{
                //Do something else
                flag = false;
            }
        }

        Boolean thatThing()
        {
            return true;
        }


    }
}
