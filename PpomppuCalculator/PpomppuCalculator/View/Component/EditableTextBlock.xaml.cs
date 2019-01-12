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
	/// EditableTextBlock.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class EditableTextBlock : UserControl {
		
		public SolidColorBrush BGColor_Focused {
			get => pBGColor_Focused;
			set {
				pBGColor_Focused = value;
				UpdateColor();
			}
		}
		public SolidColorBrush BGColor_Unfocused {
			get => pBGColor_Unfocused;
			set {
				pBGColor_Unfocused = value;
				UpdateColor();
			}
		}
		public SolidColorBrush Color_Focused {
			get => pColor_Focused;
			set {
				pColor_Focused = value;
				UpdateColor();
			}
		}
		public SolidColorBrush Color_Unfocused {
			get => pColor_Unfocused;
			set {
				pColor_Unfocused = value;
				UpdateColor();
			}
		}
		public Brush Color_Selection {
			get => TextBox.SelectionBrush;
			set {
				TextBox.SelectionBrush = value;
			}
		}
		private SolidColorBrush pBGColor_Focused;
		private SolidColorBrush pBGColor_Unfocused;
		private SolidColorBrush pColor_Focused;
		private SolidColorBrush pColor_Unfocused;
		public int MaxLines {
			get => TextBox.MaxLines;
			set {
				TextBox.MaxLines = value;
			}
		}
		public int MaxLength {
			get => TextBox.MaxLength;
			set {
				TextBox.MaxLength = value;
			}
		}
		public double FontSize {
			get => TextBox.FontSize;
			set {
				TextBox.FontSize = value;
			}
		}
		public string Text {
			get => TextBox.Text;
			set {
				TextBox.Text = value;
			}
		}
		public FontFamily FontFamily {
			get => TextBox.FontFamily;
			set {
				TextBox.FontFamily = FontFamily;
			}
		}
		//public new HorizontalAlignment HorizontalContentAlignment {
		//	get => TextBox.HorizontalContentAlignment;
		//	set {
		//		TextBox.HorizontalContentAlignment = value;
		//	}
		//}
		//public new VerticalAlignment VerticalContentAlignment {
		//	get => TextBox.VerticalContentAlignment;
		//	set {
		//		TextBox.VerticalContentAlignment = value;
		//	}
		//}



		public EditableTextBlock() {
			Loaded += OnLoaded;
			InitializeComponent();

			//Default Setting
			pBGColor_Focused = new SolidColorBrush("FFFFFF".ToColor());
			pBGColor_Unfocused = new SolidColorBrush("00000000".ToColor());
			pColor_Focused = pColor_Unfocused = new SolidColorBrush("000000FF".ToColor());
		}
		private void OnLoaded(object sender, RoutedEventArgs e) {
			TextBox.GotFocus += OnTextBoxGotFocus;
			TextBox.LostFocus += OnTextBoxLostFocus;

			UpdateColor();
		}
		private void OnTextBoxGotFocus(object sender, RoutedEventArgs e) {
			UpdateColor();
		}
		private void OnTextBoxLostFocus(object sender, RoutedEventArgs e) {
			UpdateColor();
		}

		private void UpdateColor() {
			if(TextBox.IsFocused) {
				//Background =
				TextBox.Background = pBGColor_Focused;
				TextBox.Foreground = pColor_Focused;
			} else {
				//Background =
				TextBox.Background = pBGColor_Unfocused;
				TextBox.Foreground = pColor_Unfocused;
			}
		}
	}
}
