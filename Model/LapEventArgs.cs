using System;

namespace Stopwatch.Model
{
    /// <summary>
    /// 
    /// Provides event data for lap time updates in a stopwatch.
    /// 
    /// </summary>
    public class LapEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// Gets the lap time recorded when the lap event occurred.
        /// 
        /// </summary>
        public TimeSpan? LapTime 
        { 
            get; 
            private set; 
        }

        /// <summary>
        /// 
        /// Initializes a new instance of the LapEventArgs class with the specified lap time.
        /// 
        /// </summary>
        /// 
        /// <param name="lapTime">The lap time to include in the event data.</param>
        public LapEventArgs(TimeSpan? lapTime)
        {
            // Assign the provided lapTime value to the LapTime property.
            LapTime = lapTime;
        }
    }
}
