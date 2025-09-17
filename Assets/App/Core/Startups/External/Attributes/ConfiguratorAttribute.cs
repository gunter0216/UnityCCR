using System;

namespace App.Common.Autumn.Runtime.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class ConfiguratorAttribute : Attribute
    {
        public int Context { get; set; }

        public ConfiguratorAttribute(int context)
        {
            Context = context;
        }
    }
}