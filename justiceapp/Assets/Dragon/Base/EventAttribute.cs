using System;
using _scripts;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class EventAttribute : Attribute
{
    public Events Event { get; set; }
}