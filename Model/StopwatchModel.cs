using System;

namespace Stopwatch.Model
{
    /// <summary>
    /// Represents a model for a simple stopwatch.
    /// </summary>
    class StopwatchModel
    {
        /// <summary>
        /// Represents the time_stamp when the stopwatch was last started.
        /// </summary>
        private DateTime? _started;

        /// <summary>
        /// Represents the total elapsed time recorded before the last stop of the stopwatch.
        /// </summary>
        private TimeSpan? _previousElapsedTime;

        /// <summary>
        /// Gets a value indicating whether the stopwatch is running.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if the stopwatch is running; otherwise, <c>false</c>.
        /// </returns>
        public bool Running
        {
            // This property getter returns true if _started has a value, otherwise it returns false.
            get
            {
                return _started.HasValue; 
            }
        }

        /// <summary>
        /// Gets the total elapsed time on the stopwatch.
        /// </summary>
        /// 
        /// <returns>
        /// The total elapsed time recorded on the stopwatch.
        /// </returns>
        /// 
        /// <remarks>
        /// If the stopwatch is currently running, this property returns the total elapsed time
        /// since the stopwatch was started, including any previously recorded elapsed time.
        /// If the stopwatch is stopped, it returns the previously recorded elapsed time.
        /// If the stopwatch is reset, it returns <c>null</c>.
        /// </remarks>
        public TimeSpan? Elapsed
        {
            get
            {
                if (_started.HasValue)
                {
                    // If _started has a value, calculate the time elapsed since it started.
                    if (_previousElapsedTime.HasValue)
                        // If _previousElapsedTime has a value, calculate the time elapsed since started and add _previousElapsedTime to it.
                        return CalculateTimeElapsedSinceStarted() + _previousElapsedTime;
                    else
                        // If _previousElapsedTime is null, simply calculate the time elapsed since started.
                        return CalculateTimeElapsedSinceStarted();
                }
                else
                    // If _started is null, return the value of _previousElapsedTime.
                    return _previousElapsedTime;
            }
        }

        /// <summary>
        /// Gets the lap time recorded during the stopwatch operation.
        /// </summary>
        public TimeSpan? LapTime 
        { 
            get; private set; 
        }

        /// <summary>
        /// Records the current elapsed time as a lap time and raises the LapTimeUpdated event.
        /// </summary>
        public void Lap()
        {
            // Record the current elapsed time as the lap time.
            LapTime = Elapsed;
            // Raise the LapTimeUpdated event to notify listeners about the updated lap time.
            OnLapTimeUpdated(LapTime);
        }

        /// <summary>
        /// Occurs when the lap time is updated.
        /// </summary>
        public event EventHandler<LapEventArgs> LapTimeUpdated;

        /// <summary>
        /// Raises the LapTimeUpdated event with the specified lap time.
        /// </summary>
        /// 
        /// <param name="lapTime">The lap time to be included in the event.</param>
        private void OnLapTimeUpdated(TimeSpan? lapTime)
        {
            LapTimeUpdated?.Invoke(this, new LapEventArgs(lapTime));
        }

        /// <summary>
        /// Calculates and returns the time elapsed since the stopwatch was last started.
        /// </summary>
        /// 
        /// <returns>
        /// The time elapsed since the stopwatch was started.
        /// </returns>
        private TimeSpan CalculateTimeElapsedSinceStarted()
        {
            return DateTime.Now - _started.Value;
        }

        /// <summary>
        /// Starts the stopwatch, recording the current time as the start time.
        /// </summary>
        /// 
        /// <remarks>
        /// If the stopwatch was previously stopped or reset, the previous elapsed time
        /// will be preserved and added to the total elapsed time.
        /// </remarks>
        public void Start()
        {
            // Set _started to the current date and time.
            _started = DateTime.Now;

            // Check if _previousElapsedTime has a value.
            if (!_previousElapsedTime.HasValue)

                // If not, initialize it with a TimeSpan value of zero.
                _previousElapsedTime = new TimeSpan(0);
        }

        /// <summary>
        /// Stops the stopwatch and records the time elapsed since it was last started.
        /// </summary>
        /// 
        /// <remarks>
        /// The recorded elapsed time will be added to the previously recorded elapsed time.
        /// </remarks>
        public void Stop()
        {
            if (_started.HasValue)
            {
                // Calculate the elapsed time since _started and add it to _previousElapsedTime.
                _previousElapsedTime += DateTime.Now - _started.Value;

                // Set _started to null to indicate that the operation or event has ended.
                _started = null;
            }
        }

        /// <summary>
        /// Resets the stopwatch, clearing all recorded times.
        /// </summary>
        public void Reset()
        {
            // Set _previousElapsedTime to null.
            _previousElapsedTime = null;

            // Set _started to null.
            _started = null;

            // Set LapTime to null.
            LapTime = null;
        }

        /// <summary>
        /// Initializes a new instance of the StopwatchModel class, resetting the stopwatch.
        /// </summary>
        public StopwatchModel()
        {
            Reset();
        }
    }
}
