using System;

namespace KeyEngine.Game
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class AllowMultipleComponent : Attribute
    {
        public readonly bool Allow = true;

        public AllowMultipleComponent() { }
        public AllowMultipleComponent(bool allow)
        {
            Allow = allow;
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class RequireComponent : Attribute
    {
        public readonly Type ComponentType = null;

        public RequireComponent(Type type)
        {
            ComponentType = type;
        }
    }
}
