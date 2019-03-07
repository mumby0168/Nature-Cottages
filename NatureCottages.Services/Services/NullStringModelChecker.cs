using System;
using System.Collections.Generic;
using System.Text;
using NatureCottages.Services.Interfaces;

namespace NatureCottages.Services.Services
{
    public class NullStringModelChecker : INullStringModelChecker
    {
        public bool CheckStringsForNullOrEmpty<T>(T obj)
        {
            var type = typeof(T);

            var properties = type.GetProperties();

            foreach (var propertyInfo in properties)
            {
                if (propertyInfo.PropertyType == typeof(string))
                {
                    string value = (string)propertyInfo.GetValue(obj);
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        return false;
                    }
                }                
            }

            return true;
        }
    }
}
