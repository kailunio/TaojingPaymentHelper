using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaojingPaymentHelper {
    public class RecordPackage {
        public const int PackageSize = 20; 
        
        public List<Record> Records { get; private set;}

        public RecordPackage(List<Record> record) {
            this.Records = record;
        }
    }
}
