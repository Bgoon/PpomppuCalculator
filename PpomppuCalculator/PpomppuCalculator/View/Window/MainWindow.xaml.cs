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
using PpomppuCalculator.View.Component;

namespace PpomppuCalculator.View.Window {
	/// <summary>
	/// MainWindow.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class MainWindow : System.Windows.Window {

		private LoopCore core;
		private int resultState;
		private int resultStateCompare;
		private float resultFXFrame;

		public MainWindow() {
			InitializeComponent();
			Loaded += OnLoaded;
			RootStackContext.SizeChanged += OnRootStackContextSizeChanged;
		}
		private void OnLoaded(object sender, RoutedEventArgs e) {
			AssetItem.NameEditText.Visibility = Visibility.Hidden;
			AssetItem.IsDeletable = false;

			HopeAddButton.SetOnClick(AddHopeItem);

			HopeItemContext.Children.Clear();

			//Core
			core = new LoopCore();
			core.AddTask(OnTick);
			core.AddTask(OnTickSlow, TaskPriolity.High);
			core.StartLoop();
		}
		private void OnRootStackContextSizeChanged(object sender, SizeChangedEventArgs e) {
			Height = RootStackContext.ActualHeight + 30f;
		}
		private void OnTick() {
			UpdateCalculateSeparator();
			UpdateFX();
			UpdateItemScale();
		}
		private void OnTickSlow() {
			UpdateCalculate();
		}

		private void AddHopeItem() {
			CostItem item = new CostItem();
			HopeItemContext.Children.Add(item);

			item.LayoutTransform = new ScaleTransform(1f, 0f);
		}
		private void DeleteHopeItem(CostItem item) {
			HopeItemContext.Children.Remove(item);
		}

		private void UpdateCalculateSeparator() {
			ImageBrush brush = (ImageBrush)CalculateSeparator.Background;
			Rect viewport = brush.Viewport;
			viewport.X += 0.002f;
			brush.Viewport = viewport;
		}
		private void UpdateCalculate() {
			int totalAsset = 0;
			int totalHope = 0;
			for(int i=0; i<AssetItemContext.Children.Count; ++i) {
				string text = ((CostItem)AssetItemContext.Children[i]).CostEditText.Text.Replace(",", "");
				int cost;
				bool success = int.TryParse(text, out cost);
				if (success) {
					totalAsset += cost;
				}
			}
			for (int i = 0; i < HopeItemContext.Children.Count; ++i) {
				string text = ((CostItem)HopeItemContext.Children[i]).CostEditText.Text.Replace(",", "");
				int cost;
				bool success = int.TryParse(text, out cost);
				if (success) {
					totalHope += cost;
				}
			}

			TotalAssetText.Text = string.Format("{0:#,###}", totalAsset);
			TotalHopeText.Text = string.Format("{0:#,###}", totalHope);

			int balance = totalAsset - totalHope;
			float usePercent = (float)Math.Round(totalHope * 100f / totalAsset, 1);
			float balancePercent = (float)Math.Round(balance * 100f / totalAsset, 1);

			BalanceText.Text = string.Format("{0:#,###}", balance);
			ResultDescriptText.Text = string.Format("가진 돈의 {0}%를 소비하고,\n{1}%가 남습니다.", usePercent, balancePercent);
			string result;
			Color BGColor;
			if (balancePercent > 80f) {
				result = "사";
				BGColor = "679979".ToColor();
				resultState = 0;
			} else if (balancePercent > 60f) {
				result = "아껴써라";
				BGColor = "8f9967".ToColor();
				resultState = 1;
			} else if (balancePercent > 20f) {
				result = "사면거지됨";
				BGColor = "997467".ToColor();
				resultState = 2;
			} else {
				result = "사지마";
				BGColor = "996767".ToColor();
				resultState = 3;
			}
			ResultText.Text = result;
			ResultContext.Background = BGColor.ToBrush();

			if(resultState != resultStateCompare) {
				resultStateCompare = resultState;

				resultFXFrame = 0f;
			}
		}
		private void UpdateItemScale() {
			for (int i = 0; i < HopeItemContext.Children.Count; ++i) {
				CostItem item = ((CostItem)HopeItemContext.Children[i]);

				item.scale += (1f - item.scale) * 0.2f;
				item.LayoutTransform = new ScaleTransform(1f, item.scale);
			}
		}
		private void UpdateFX() {
			ResultFX.Visibility = Visibility.Visible;
			resultFXFrame = Mathf.Clamp01(resultFXFrame + 0.04f);

			float scale = Mathf.Pow(0.2f + resultFXFrame * 20f, 0.7f);
			Color color = new Color();
			color.R = color.G = color.B = 255;
			color.A = (byte)((1f - resultFXFrame) * 0.2f * 255f);
			ResultFX.RenderTransform = new ScaleTransform(scale, scale);
			ResultFX.Fill = color.ToBrush();
		}
	}
}
