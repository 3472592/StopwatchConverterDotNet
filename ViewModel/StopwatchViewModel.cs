using System;
using Stopwatch.Model;
using System.ComponentModel;
using System.Windows.Threading;

namespace Stopwatch.ViewModel
{
    /// <summary>
    /// Represents a ViewModel for a stopwatch, providing properties and methods
    /// for interacting with and displaying stopwatch data in a WPF application.
    /// </summary>
    class StopwatchViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Represents the underlying model responsible for managing stopwatch operations and data.
        /// </summary>
        private readonly StopwatchModel _stopwatchModel = new StopwatchModel();

        /// <summary>
        /// Represents a timer specifically designed for WPF applications, used for periodic updates of UI components.
        /// </summary>
        private readonly DispatcherTimer _timer = new DispatcherTimer();

        /// <summary>
        /// Gets a value indicating whether the stopwatch is currently running.
        /// </summary>
        public bool Running 
        {
            // This property getter returns the value of the _stopwatchModel.Running property.
            get
            {
                return _stopwatchModel.Running; 
            } 
        }

        /// <summary>
        /// Initializes a new instance of the StopwatchViewModel class.
        /// Configures and starts a timer to update stopwatch data.
        /// </summary>
        public StopwatchViewModel()
        {
            // Set the timer interval to 50 milliseconds.
            _timer.Interval = TimeSpan.FromMilliseconds(50);

            // Associate the TimerTick method with the Tick event of the timer.
            _timer.Tick += TimerTick;

            // Start the timer.
            _timer.Start();

            // Call the Start() method, which presumably starts some kind of stopwatch operation.
            Start();

            // Subscribe the LapTimeUpdatedEventHandler method to the LapTimeUpdated event of the stopwatch model.
            _stopwatchModel.LapTimeUpdated += LapTimeUpdatedEventHandler;
        }

        /// <summary>
        /// Starts the stopwatch.
        /// </summary>
        public void Start()
        {
            // Start the stopwatch or timer represented by the _stopwatchModel object.
            _stopwatchModel.Start();

        }

        /// <summary>
        /// Stops the stopwatch.
        /// </summary>
        public void Stop()
        {
            // Stop the stopwatch or timer represented by the _stopwatchModel object.
            _stopwatchModel.Stop();

        }

        /// <summary>
        /// Records a lap time in the stopwatch.
        /// </summary>
        public void Lap()
        {
            // Record a lap or split time using the _stopwatchModel object.
            _stopwatchModel.Lap();
        }

        /// <summary>
        /// Resets the stopwatch and optionally restarts it if it was running.
        /// Notifies property changes for lap time components.
        /// </summary>
        public void Reset()
        {
            // Store the current running state in a boolean variable.
            bool running = Running;

            // Reset the stopwatch or timer represented by the _stopwatchModel object.
            _stopwatchModel.Reset();

            // If the stopwatch or timer was previously running, start it again.
            if (running)
                _stopwatchModel.Start();

            // Notify that the properties LapHours, LapMinutes, and LapSeconds have changed.
            OnPropertyChanged("LapHours");
            OnPropertyChanged("LapMinutes");
            OnPropertyChanged("LapSeconds");

        }

        int _lastHours;
        int _lastMinutes;
        decimal _lastSeconds;
        bool _lastRunning;
        void TimerTick(object sender, object e)
        {
            if (_lastRunning != Running)
            {
                _lastRunning = Running;
                OnPropertyChanged("Running");
            }
            if (_lastHours != Hours)
            {
                _lastHours = Hours;
                OnPropertyChanged("Hours");
            }
            if (_lastMinutes != Minutes)
            {
                _lastMinutes = Minutes;
                OnPropertyChanged("Minutes");
            }
            if (_lastSeconds != Seconds)
            {
                _lastSeconds = Seconds;
                OnPropertyChanged("Seconds");
            }
        }

        /// <summary>
        /// Gets the elapsed hours of the stopwatch.
        /// </summary>
        public int Hours
        {
            // This property getter returns the number of hours from the Elapsed property of _stopwatchModel,
            // but it returns 0 if Elapsed is null.
            get
            {
                return _stopwatchModel.Elapsed.HasValue ?
                    _stopwatchModel.Elapsed.Value.Hours : 0;
            }

        }

        /// <summary>
        /// Gets the elapsed minutes of the stopwatch.
        /// </summary>
        public int Minutes
        {
            // This property getter returns the number of minutes from the Elapsed property of _stopwatchModel,
            // but it returns 0 if Elapsed is null.
            get
            {
                return _stopwatchModel.Elapsed.HasValue ?
                    _stopwatchModel.Elapsed.Value.Minutes : 0;
            }

        }

        /// <summary>
        /// Gets the elapsed seconds (including milliseconds) of the stopwatch.
        /// </summary>
        public decimal Seconds
        {
            // This property getter calculates the total seconds (including milliseconds as fractions of a second)
            // from the Elapsed property of _stopwatchModel. It returns 0.0 if Elapsed is null.
            get
            {
                if (_stopwatchModel.Elapsed.HasValue)
                {
                    // Calculate the total seconds and fractions of a second.
                    return (decimal)_stopwatchModel.Elapsed.Value.Seconds
                        + (_stopwatchModel.Elapsed.Value.Milliseconds * 0.001M);
                }
                else
                {
                    // Return 0.0 if Elapsed is null.
                    return 0.0M;
                }
            }

        }

        /// <summary>
        /// Gets the lap hours of the stopwatch.
        /// </summary>
        public int LapHours
        {
            // This property getter returns the number of hours from the LapTime property of _stopwatchModel,
            // but it returns 0 if LapTime is null.
            get
            {
                return _stopwatchModel.LapTime.HasValue ?
                    _stopwatchModel.LapTime.Value.Hours : 0;
            }

        }

        /// <summary>
        /// Gets the lap minutes of the stopwatch.
        /// </summary>
        public int LapMinutes
        {
            // This property getter returns the number of minutes from the LapTime property of _stopwatchModel,
            // but it returns 0 if LapTime is null.
            get
            {
                return _stopwatchModel.LapTime.HasValue ?
                    _stopwatchModel.LapTime.Value.Minutes : 0;
            }

        }

        /// <summary>
        /// Gets the lap seconds (including milliseconds) of the stopwatch.
        /// </summary>
        public decimal LapSeconds
        {
            // This property getter calculates the total seconds (including milliseconds as fractions of a second)
            // from the LapTime property of _stopwatchModel. It returns 0.0 if LapTime is null.
            get
            {
                if (_stopwatchModel.LapTime.HasValue)
                {
                    // Calculate the total seconds and fractions of a second.
                    return (decimal)_stopwatchModel.LapTime.Value.Seconds
                        + (_stopwatchModel.LapTime.Value.Milliseconds * 0.001M);
                }
                else
                {
                    // Return 0.0 if LapTime is null.
                    return 0.0M;
                }
            }

        }

        // Store the last recorded lap hours value.
        int _lastLapHours;

        // Store the last recorded lap minutes value.
        int _lastLapMinutes;

        // Store the last recorded lap seconds value with decimal precision.
        decimal _lastLapSeconds;

        /// <summary>
        /// Event handler for the lap time updated event. Notifies property changes for lap time components.
        /// </summary>
        private void LapTimeUpdatedEventHandler(object sender, LapEventArgs e)
        {
            // Check if the 'LapHours' property has changed.
            if (_lastLapHours != LapHours)
            {
                // Update '_lastLapHours' with the current 'LapHours' value.
                _lastLapHours = LapHours;

                // Notify that the 'LapHours' property has changed.
                OnPropertyChanged("LapHours");
            }

            // Check if the 'LapMinutes' property has changed.
            if (_lastLapMinutes != LapMinutes)
            {
                // Update '_lastLapMinutes' with the current 'LapMinutes' value.
                _lastLapMinutes = LapMinutes;

                // Notify that the 'LapMinutes' property has changed.
                OnPropertyChanged("LapMinutes");
            }

            // Check if the 'LapSeconds' property has changed.
            if (_lastLapSeconds != LapSeconds)
            {
                // Update '_lastLapSeconds' with the current 'LapSeconds' value.
                _lastLapSeconds = LapSeconds;

                // Notify that the 'LapSeconds' property has changed.
                OnPropertyChanged("LapSeconds");
            }

        }

        /// <summary>
        /// Event raised when a property of the ViewModel changes, allowing for UI updates.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notifies subscribers of a property change.
        /// </summary>
        /// <param name="propertyName">The name of the changed property.</param>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
