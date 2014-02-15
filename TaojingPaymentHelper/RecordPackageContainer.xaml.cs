using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TaojingPaymentHelper {
    /// <summary>
    /// RecordPackageContainer.xaml 的交互逻辑
    /// </summary>
    public partial class RecordPackageContainer : UserControl {
        public RecordPackageContainer() {
            InitializeComponent();
        }

        public int Index { get; private set; }

        private string _title = "";
        public string Title {
            get {
                return _title;
            }
            set {
                _title = value;
                title.Text = "第" + this.Index + "组记录";
            }
        }

        private bool _checked = true;
        public bool Checked {
            get { return _checked; }
            set {
                if (_checked != value) {
                    _checked = value;
                    this.BorderBrush = value ? 
                        SystemColors.HighlightBrush: SystemColors.ControlLightBrush; 
                }
            }
        }

        public RecordPackage Package { get; private set; }

        public void Init(int index, RecordPackage package) {
            this.Index = index;
            this.Package = package;

            this.Title = "第" + index + "组记录";

            var sbUsers = new StringBuilder();
            var sbPrizes = new StringBuilder();
            for(int i=0; i<package.Records.Count; i++){
                var record = package.Records[i];
                sbUsers.Append(record.User);
                sbPrizes.Append(record.Prize);
                if(i < package.Records.Count - 1){
                    sbUsers.AppendLine();
                    sbPrizes.AppendLine();
                }
            }

            users.Text = sbUsers.ToString();
            prizes.Text = sbPrizes.ToString();

            this.Checked = false;
        }

        public string GenerateScript() {
            var template = new StringBuilder();

            using (var stream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("TaojingPaymentHelper.Template.js")) {
                using (var reader = new StreamReader(stream, Encoding.UTF8)) {
                    template.Append(reader.ReadToEnd());
                }
            }

            foreach (var record in this.Package.Records) {
                template.AppendLine(string.Format("helper.fill('{0}', '{1}');", record.User, record.Prize));
            }

            return template.ToString();
        }

    }
}
