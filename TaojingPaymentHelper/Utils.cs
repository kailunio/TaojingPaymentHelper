using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TaojingPaymentHelper {
    public static class Util {
        public static string[] ReadAllLines(string path, Encoding encoding) {
            string content = File.ReadAllText(path, encoding);
            string[] lines = content.Split(new string[]{"\r\n"}, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < lines.Length; i++) {
                if (lines[i].Contains('\r')) {
                    lines[i] = lines[i].Replace("\r", "");
                }
            }
            return lines;
        }
    }
}
