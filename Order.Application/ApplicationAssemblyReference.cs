﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application
{
    public static class ApplicationAssemblyReference
    {
        public static readonly Assembly Assembly = typeof(ApplicationAssemblyReference).Assembly;
    }
}
