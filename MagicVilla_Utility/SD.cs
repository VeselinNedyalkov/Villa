﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicVilla_Utility
{
    public static class SD
    {
        public enum ApiType
        {
            Get,
            Post,
            Put,
            Delete
        }
        public static string SessionToken = "JWTToken";
    }
}
