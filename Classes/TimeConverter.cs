using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace BerlinClock
{
    public class TimeConverter : ITimeConverter
    {
        //constant configuration values
        private const string TimeInputFormat = "HH:mm:ss";
        private const int LampValueFirstRow = 5;
        private const int BlinkIntervalSecond = 2;
        private const int NumLampsFirstRowHour = 4;
        private const int NumLampsFirstRowMinute = 11;
        private const int NumLampsSecondRows = 4;
        private const int RedLampIntervalMinute = 3;
        
        private readonly Dictionary<Lamp, string> _lampString;
        
        //shared variables
        private int _hr;
        private string _errorMessage;

        public TimeConverter()
        {
            _lampString = new Dictionary<Lamp, string> { { Lamp.Yellow, "Y" }, { Lamp.Red, "R" }, { Lamp.Off, "O" } };
        }

        public String convertTime(string aTime)
        {
            try
            {
                DateTime dateTime = ValidateTime(aTime);
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine(ConvertSecond(dateTime.Second));
                stringBuilder.AppendLine(ConvertHour(_hr));
                stringBuilder.Append(ConvertMinute(dateTime.Minute));
                return stringBuilder.ToString();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return string.IsNullOrEmpty(_errorMessage) ? "Convert Time operation failed, please check console output for exception." 
                                                           : _errorMessage;
            }
        }

        private String ConvertHour(int hour)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(GetFirstRowHour(hour));
            stringBuilder.Append(GetSecondRow(hour, Lamp.Red));
            return stringBuilder.ToString();
        }

        private String ConvertMinute(int minute)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(GetFirstRowMinute(minute));
            stringBuilder.Append(GetSecondRow(minute, Lamp.Yellow));
            return stringBuilder.ToString();
        }

        private String ConvertSecond(int second)
        {
            return (second % BlinkIntervalSecond == 0)  ? _lampString[Lamp.Yellow] 
                                                        : _lampString[Lamp.Off];
        }

        private String GetFirstRowHour(int value)
        {
            StringBuilder stringBuilder = new StringBuilder();
            int numOn = value / LampValueFirstRow;
            for (int i = 1; i <= NumLampsFirstRowHour; i++)
            {
                stringBuilder.Append((i <= numOn)   ? _lampString[Lamp.Red] 
                                                    : _lampString[Lamp.Off]);
            }
            return stringBuilder.ToString();
        }

        private String GetFirstRowMinute(int value)
        {
            StringBuilder stringBuilder = new StringBuilder();
            int numOn = value / LampValueFirstRow;
            for (int i = 1; i <= NumLampsFirstRowMinute; i++)
            {
                stringBuilder.Append((i <= numOn)   ? (i % RedLampIntervalMinute == 0 
                                                            ? _lampString[Lamp.Red] 
                                                            : _lampString[Lamp.Yellow]) 
                                                    : _lampString[Lamp.Off]);
            }
            return stringBuilder.ToString();
        }

        private String GetSecondRow(int value, Lamp onType)
        {
            StringBuilder stringBuilder = new StringBuilder();
            int numOn = value % LampValueFirstRow;
            for (int i = 1; i <= NumLampsSecondRows; i++)
            {
                stringBuilder.Append((i <= numOn)   ? _lampString[onType] 
                                                    : _lampString[Lamp.Off]);
            }
            return stringBuilder.ToString();
        }

        private DateTime ValidateTime(string aTime)
        {
            //Special case, 24:00:00 is normally not supported by DateTime
            if (aTime.Equals("24:00:00"))
            {
                _hr = 24;
                aTime = "00:00:00";
            }

            try
            {
                DateTime dateTime = DateTime.ParseExact(aTime, TimeInputFormat, CultureInfo.InvariantCulture);
                _hr = _hr != 24 ? dateTime.Hour : _hr;
                return dateTime;
            }
            catch (FormatException)
            {
                _errorMessage = string.Format("Input Time format is not correct, shoud be HH:mm:ss");
                throw;
            }
            catch (Exception)
            {
                _errorMessage = string.Format("Input Time cannot be validated.");
                throw;
            }
        }

        private enum Lamp
        {
            Yellow,
            Red,
            Off,
        }
    }
}
