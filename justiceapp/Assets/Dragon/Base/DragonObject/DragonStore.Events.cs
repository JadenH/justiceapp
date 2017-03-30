using System.Globalization;
using System.Linq;
using System.Reflection;

public partial class DragonStore<T>
{

    public void InitEvents()
    {
        var events = Value.GetType().GetMethods().Where(info => info.GetCustomAttributes(typeof(EventAttribute), true).Any()).ToList();

        foreach (MethodInfo methodInfo in events)
        {
            var info = methodInfo;
            foreach (EventAttribute eventCode in info.GetCustomAttributes(typeof(EventAttribute), true).Cast<EventAttribute>())
            {
                Dispatcher.Register(this, eventCode.Event, o =>
                {
                    info.Invoke(Value, BindingFlags.IgnoreReturn, null, new object[] { o },
                        CultureInfo.CurrentCulture);
                });
            }

        }
    }

}