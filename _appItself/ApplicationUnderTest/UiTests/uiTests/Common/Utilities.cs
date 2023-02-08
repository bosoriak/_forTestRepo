using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uiTests.Common
{
    public class Utilities
    {
        public static void TryExecuteWithTimeout(Action function, Func<bool> condition, int timeout, int step)
        {
            int elapsedTime = 0;
            while (elapsedTime < timeout && !condition())
            {
                try
                {
                    function();
                    if (condition())
                    {
                        break;
                    }
                }
                catch (Exception)
                {
                    elapsedTime += step;
                    Thread.Sleep(step);
                }
            }
        }
    }
}
