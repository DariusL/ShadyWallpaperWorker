using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShadyWallpaperService.DataTypes
{
    internal enum R16By9
    {
        All,
        R1280By720,
        R1366By768,
        R1600By900,
        R1920By1080,
        R2560By1440,
        R3840By2160,
        R7680By4320,
        None
    }

    internal enum R4By3
    {
        All,
        R800X600,
        R1024X768,
        R1280By960,
        R1600By1200,
        None
    }

    internal static class TypeUtils
    {
        internal static T ParseEnum<T>(string value) where T : struct
        {
            T ret;
            if(Enum.TryParse<T>(value, true, out ret))
                return ret;
            else 
                throw new FormatException();
        }
    }
}