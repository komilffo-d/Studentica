﻿namespace Studentica.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class StringValueAttribute : Attribute
    {

        public string StringValue { get; protected set; }


        public StringValueAttribute(string value)
        {
            this.StringValue = value;
        }

    }
}
