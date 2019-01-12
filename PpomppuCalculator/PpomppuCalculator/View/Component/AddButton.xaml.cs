using System;
using System.Collections.Generic;
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
using BigLibrary;

namespace PpomppuCalculator.View.Component {
	/// <summary>
	/// AddButton.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class AddButton : UserControl {

		public AddButton() {
			InitializeComponent();
			Loaded += OnLoaded;
		}

		private void OnLoaded(object sender, RoutedEventArgs e) {
			ButtonContext.SetBtnColor("00000033".ToColor(), "00000022".ToColor(), "00000044".ToColor());
		}
	}
}
