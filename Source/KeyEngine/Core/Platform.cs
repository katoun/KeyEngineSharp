using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyEngine.Core
{
    public static class Platform
    {
#if KEYENGINE_PLATFORM_WINDOWS
        public static readonly PlatformType Type = PlatformType.WINDOWS;
#elif KEYENGINE_PLATFORM_LINUX
        public static readonly PlatformType Type = PlatformType.LINUX;
#elif KEYENGINE_PLATFORM_ANDORID
        public static readonly PlatformType Type = PlatformType.ANDROID;
#endif
    }
}
