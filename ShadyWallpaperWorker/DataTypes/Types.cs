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
        R1280By1024,
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

        internal static R16By9 FromSizeR16By9(int width, int height)
        {
            if (Math.Abs((float)width / height - 1.777777777f) < 0.01)
            {
                if (width >= 7680 && height >= 4320)
                    return R16By9.R7680By4320;
                if (width >= 3840 && height >= 2160)
                    return R16By9.R3840By2160;
                if (width >= 2560 && height >= 1440)
                    return R16By9.R2560By1440;
                if (width >= 1920 && height >= 1080)
                    return R16By9.R1920By1080;
                if (width >= 1600 && height >= 900)
                    return R16By9.R1600By900;
                if (width >= 1366 && height >= 768)
                    return R16By9.R1366By768;
                if (width >= 1280 && height >= 720)
                    return R16By9.R1280By720;
            }
            return R16By9.None;
        }

        internal static R4By3 FromSizeR4By3(int width, int height)
        {
            if (Math.Abs((float)width / height - 1.333333f) < 0.01)
            {
                if (width >= 1600 && height >= 1200)
                    return R4By3.R1600By1200;
                if (width >= 1280 && height >= 1024)
                    return R4By3.R1280By1024;
                if (width >= 1024 && height >= 768)
                    return R4By3.R1024X768;
                if (width >= 800 && height >= 600)
                    return R4By3.R800X600;
            }

            return R4By3.None;
        }
    }
}