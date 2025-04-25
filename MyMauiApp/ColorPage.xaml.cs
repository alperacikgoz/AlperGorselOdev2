using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.ApplicationModel;   // Clipboard için

namespace MyMauiApp
{
    public partial class ColorPage : ContentPage
    {
        public ColorPage()
        {
            InitializeComponent();
            sliderRed.Value   = 0;
            sliderGreen.Value = 0;
            sliderBlue.Value  = 0;
            UpdateColor();
        }

        void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
            => UpdateColor();

        void UpdateColor()
        {
            int r = (int)sliderRed.Value;
            int g = (int)sliderGreen.Value;
            int b = (int)sliderBlue.Value;

            lblRed.Text   = $"Red: {r}";
            lblGreen.Text = $"Green: {g}";
            lblBlue.Text  = $"Blue: {b}";

            string hex = $"#{r:X2}{g:X2}{b:X2}";
            lblHex.Text = hex;

            rootLayout.BackgroundColor = Color.FromRgb(r, g, b);
        }

        async void OnCopyClicked(object sender, EventArgs e)
        {
            await Clipboard.Default.SetTextAsync(lblHex.Text);
            await DisplayAlert("Kopyalandı", lblHex.Text, "OK");
        }

        void OnRandomClicked(object sender, EventArgs e)
        {
            var rnd = new Random();
            sliderRed.Value   = rnd.Next(256);
            sliderGreen.Value = rnd.Next(256);
            sliderBlue.Value  = rnd.Next(256);
        }
    }
}
