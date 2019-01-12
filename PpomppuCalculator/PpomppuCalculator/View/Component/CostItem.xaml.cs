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
using System.Text.RegularExpressions;
using BigLibrary;

namespace PpomppuCalculator.View.Component {
	/// <summary>
	/// CostItem.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class CostItem : UserControl {
		public bool IsDeletable {
			get => isDeletable;
			set {
				isDeletable = value;
				UpdateDeleteButton(false);
			}
		}
		private bool isDeletable = true;
		public float scale;

		public CostItem() {
			Loaded += OnLoaded;
			InitializeComponent();
		}

		private void OnLoaded(object sender, RoutedEventArgs e) {
			TotalContext.MouseEnter += OnMouseEnter;
			TotalContext.MouseLeave += OnMouseLeave;
			NameEditText.Color_Unfocused =
			CostEditText.Color_Unfocused = "FFFFFF".ToColor().ToBrush();
			CostEditText.TextBox.GotFocus += OnCostEditTextGotFocus;
			CostEditText.TextBox.LostFocus += OnCostEditTextLostFocus;
			CostEditText.PreviewTextInput += OnItemCostEditTextPreviewTextInput;
			NameEditText.Text = "이름";
			CostEditText.Text = "1000";
			DeleteButton.SetOnClick(OnDeletaButtonClick);

			DeleteButton.Visibility = Visibility.Hidden;


			UpdateCostFormat();
		}

		private bool IsTextAllowed(string input) {
			Regex regex = new Regex("[^0-9]$");
			return regex.IsMatch(input);
		}
		private void OnItemCostEditTextPreviewTextInput(object sender, TextCompositionEventArgs e) {
			e.Handled = IsTextAllowed(e.Text);
		}
		private void OnCostEditTextGotFocus(object sender, RoutedEventArgs e) {
			CostEditText.Text = CostEditText.Text.Replace(",", "");
		}
		private void OnCostEditTextLostFocus(object sender, RoutedEventArgs e) {
			UpdateCostFormat();
		}
		private void OnMouseEnter(object sender, MouseEventArgs e) {
			UpdateDeleteButton(true);
		}
		private void OnMouseLeave(object sender, MouseEventArgs e) {
			UpdateDeleteButton(false);
		}
		private void OnDeletaButtonClick() {
			((StackPanel)Parent).Children.Remove(this);
		}

		private void UpdateCostFormat() {
			Regex regex = new Regex("^[0-9]+");
			string match = regex.Match(CostEditText.Text).Value;

			int cost;
			int.TryParse(match, out cost);
			CostEditText.Text = string.Format("{0:#,###}", cost);
		}
		private void UpdateDeleteButton(bool showRequest) {
			if(showRequest && isDeletable) {
				DeleteButton.Visibility = Visibility.Visible;
			} else {
				DeleteButton.Visibility = Visibility.Hidden;
			}
		}
	}
}
