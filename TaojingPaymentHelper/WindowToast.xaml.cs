using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TaojingPaymentHelper {
    /// <summary>
    /// WindowToast.xaml 的交互逻辑
    /// </summary>
    public partial class WindowToast : Window {
        public WindowToast() {
            InitializeComponent();
        }

        Timer _timer = null;
        private void TimerCallBack(object s) {
            this.Dispatcher.Invoke(new Action(() => this.Close()));
            if (_timer != null) {
                _timer.Dispose();
                _timer = null;
            }
        }

        public static void ShowToast(Window owner, string msg, int due = 500) {
            var toast = new WindowToast();
            toast.Owner = owner;
            toast.text.Text = msg;
            toast.Show();
            toast._timer = new Timer(toast.TimerCallBack, null, due, 0);
        }
    }
}
