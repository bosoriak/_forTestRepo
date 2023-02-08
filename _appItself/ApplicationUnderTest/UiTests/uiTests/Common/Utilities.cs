using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uiTests.Common
{
    public class Utilities
    {
        /*
        * This method takes an Action delegate (a delegate that does not return a value), a condition delegate (a delegate that returns a bool), 
        * a timeout (the maximum amount of time in milliseconds to wait for the function to succeed), 
        * and a step (the amount of time in milliseconds to wait between each retry). 
        * The method will keep executing the function and checking the condition until 
        * either the condition returns true or the timeout has been reached.
        */
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
