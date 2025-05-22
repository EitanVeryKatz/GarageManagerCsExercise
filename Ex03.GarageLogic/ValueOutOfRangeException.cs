using System;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        public float MinValue { get; }
        public float MaxValue { get; }

        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue, string i_Message = null)
            : base(i_Message ?? $"Value is out of range. Must be between {i_MinValue} and {i_MaxValue}.")
        {
            MinValue = i_MinValue;
            MaxValue = i_MaxValue;
        }
    }
}
