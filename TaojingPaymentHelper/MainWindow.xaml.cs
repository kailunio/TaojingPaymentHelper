using MahApps.Metro.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow {
        public MainWindow() {
            InitializeComponent();
        }

        public List<Record> ReadRecordsFromFile(string file, Encoding encoding, int accountIndex, int priceIndex){
            var records = new List<Record>();

            string lastQuoteLine = null;
            string[] lines = File.ReadAllLines(file, encoding);
            for (int i = 1; i < lines.Length; i++) {
                string line = lines[i].Trim();
                if (line.IsEmpty()) {
                    continue;
                }

                // 如果以引号开头，那么这一行先不处理，等待下一个引号
                if (line.StartsWith("\"")) {
                    string currentQuoteLine = line.Substring(1);
                    if (lastQuoteLine != null) {
                        line = lastQuoteLine + currentQuoteLine;
                        lastQuoteLine = null;
                    }
                    else {
                        lastQuoteLine = currentQuoteLine;
                        continue;
                    }
                }

                string[] ss = line.Split(',');

                string user = "";
                if (ss.Length > accountIndex - 1){
                    user = ss[accountIndex - 1];
                }
                string prize = "";
                if (ss.Length > priceIndex - 1){
                    prize = ss[priceIndex - 1];
                }
                records.Add(new Record(user, prize));
            }
            return records;
        }

        public List<RecordPackage> PackageRecords(List<Record> records) {
            var packages = new List<RecordPackage>();

            int groupCount = records.Count / RecordPackage.PackageSize + 1;
            for (int i = 0; i < groupCount; i++) {
                int index = i * RecordPackage.PackageSize;
                int count = index + RecordPackage.PackageSize < records.Count ?
                    RecordPackage.PackageSize : records.Count - index;
                var group = records.GetRange(index, count);
                packages.Add(new RecordPackage(group));
            }
            return packages;
        }

        private void ButtonOpenFileClick(object sender, RoutedEventArgs e) {
            var ofd = new OpenFileDialog {
                Filter = "CSV文件(*.csv)|*.csv",
            };
            if (ofd.ShowDialog() ?? false) {
                try
                {
                    WindowToast.ShowToast(this, "数据文件读取中...");
                    ComboBoxItem cb = cbEncoding.SelectedItem as ComboBoxItem;
                    Encoding encoding = Encoding.GetEncoding(cb.Content.ToString());
                    int accountIndex = (int)(tbAccount.Value ?? 1);
                    int priceIndex = (int)(tbPrice.Value ?? 1);

                    panel.Children.Clear();

                    string file = ofd.FileName;
                    var records = ReadRecordsFromFile(file, encoding, accountIndex, priceIndex);
                    var packages = PackageRecords(records);

                    for (int i = 0; i < packages.Count; i++)
                    {
                        var package = packages[i];
                        var container = new RecordPackageContainer();
                        container.Init(i + 1, package);
                        container.PreviewMouseUp += ContainerPreviewMouseUp;

                        panel.Children.Add(container);
                    }
                }
                catch (Exception ex)
                {
                    WindowToast.ShowToast(this, "出错啦：" + ex.ToString(), 10000);
                }
            }
        }

        

        private void ContainerPreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
 	        var container = (RecordPackageContainer)sender;
            container.Checked = !container.Checked;
            if (container.Checked) {
                WindowToast.ShowToast(this, container.Title + "已复制", 2000);
                var script = container.GenerateScript();

                Clipboard.SetText(script);
            } else {
                Clipboard.SetText("");
            }
        }

        private void ButtonClearClick(object sender, RoutedEventArgs e) {
            panel.Children.Clear();
        }

        private void CheckBoxTopMostChecked(object sender, RoutedEventArgs e) {
            var checkBox = (CheckBox)sender;
            this.Topmost = checkBox.IsChecked ?? false;
        }
    }
}