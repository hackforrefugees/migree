﻿using System.ComponentModel;

namespace Migree.Core.Definitions
{
    public enum UserLocation
    {
        [Description("Unspecified")]
        Unspecified = 0,
        [Description("Stockholm")]
        StockholmArea = 1,
        [Description("Göteborg")]
        GothenburgArea = 2,
        [Description("Malmö")]
        MalmoArea = 4
    }
}