using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaojingPaymentHelper {
    public class Record {
        public string User { get; private set; }
        public string Prize { get; private set; }
        public Record(string user, string prize) {
            if (user == null && prize == null)
            {
                throw new ArgumentException("用户名和价格不能为null");
            }

            this.User = user;
            this.Prize = prize;
        }
    }
}
