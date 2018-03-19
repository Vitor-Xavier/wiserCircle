using System;
using Core;
using Xamarin.Forms;

namespace Phoneword
{
	public class MainPage : ContentPage
	{
		//Image image = new Image
		//{
		//	image. = true;
		//	Source = ImageSource.FromUri(new Uri("../images/wiseup.jpg")),
		//	HorizontalOptions = LayoutOptions.Center,
		//	VerticalOptions = LayoutOptions.Center
		//};
		Entry numberEntry = new Entry
		{
			Text = "5029"
		};
		//Button translateButton = new Button
		//{
		//	Text = "Translate"
		//};
		Button callButton = new Button
		{
			Text = "Call",
			IsEnabled = true
		};

		public MainPage()
		{
			//Declares padding, with 'Device.OnPlatform' enabling device-specific measurements
			this.Padding = new Thickness(20,
																	 Device.OnPlatform<double>(40, 20, 20), 20, 20);

			
			Title = "WiseUp - Ligação a frio";

			StackLayout panel = new StackLayout
			{
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Orientation = StackOrientation.Vertical,
				Spacing = 15
			};

			panel.Children.Add(new Label
			{
				Text = "Entre com o telefone inicial: "
			});
			panel.Children.Add(numberEntry);
			//panel.Children.Add(translateButton);
			panel.Children.Add(callButton);

			//translateButton.Clicked += OnTranslate;
			callButton.Clicked += OnCall;
			this.Content = panel;
		}

		//private void OnTranslate(object sender, EventArgs e)
		//{
		//	string enteredNumber = numberEntry.Text;
		//	string translatedNumber = PhonewordTranslator.ToNumber(enteredNumber);
		//	if (!string.IsNullOrEmpty(translatedNumber))
		//	{
		//		callButton.Text = "Call " + translatedNumber;
		//		callButton.IsEnabled = true;
		//	}
		//	else {
		//		callButton.Text = "Call";
		//		callButton.IsEnabled = false;
		//	}
		//}

		async void OnCall(object sender, EventArgs e)
		{
			int numeroTelefone = 0;
			string enteredNumber = numberEntry.Text;
			string translatedNumber = PhonewordTranslator.ToNumber(enteredNumber);
			//asynchronous 'await' keyword for this method
			if (await this.DisplayAlert("Dial a Number",
			                            "Você gostaria de ligar para " + translatedNumber + "?", 
			                            "Sim", "Não"))
			{
				IDialer dialer = DependencyService.Get<IDialer>();
				if (dialer != null)
				{
					await dialer.DialAsync(translatedNumber);
					numeroTelefone = Int32.Parse(numberEntry.Text);
					numeroTelefone++;
					numberEntry.Text = numeroTelefone.ToString();

				}
			}
		}
	}
}
