﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaojingPaymentHelper {
    public static class Extension {
        public static bool IsEmpty(this string s) {
            return string.IsNullOrEmpty(s);
        }
    }
}
