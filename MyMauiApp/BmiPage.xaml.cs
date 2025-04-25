using System;
using Microsoft.Maui.Controls;

namespace MyMauiApp
{
    public partial class BmiPage : ContentPage
    {
        public BmiPage()
        {
            InitializeComponent();
            // İlk değerleri göster
            UpdateBmiDisplay();
        }

        private void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            UpdateBmiDisplay();
        }

        private void UpdateBmiDisplay()
        {
            // Değerleri oku
            int weight = (int)sliderWeight.Value;
            int heightCm = (int)sliderHeight.Value;

            // Ekrandaki etiketleri güncelle
            lblWeightValue.Text = $"Kilo: {weight} kg";
            lblHeightValue.Text = $"Boy: {heightCm} cm";

            // VKİ hesapla
            double heightM = heightCm / 100.0;
            double bmi = weight / (heightM * heightM);
            lblBmiResult.Text = $"VKİ: {bmi:F2}";

            // Kategoriyi belirle
            lblBmiCategory.Text = GetBmiCategory(bmi);
        }

        private string GetBmiCategory(double bmi)
        {
            if (bmi < 16)
                return "İleri Düzeyde Zayıf";
            else if (bmi < 17)
                return "Orta Düzeyde Zayıf";
            else if (bmi < 18.5)
                return "Hafif Düzeyde Zayıf";
            else if (bmi < 25)
                return "Normal Kilo";
            else if (bmi < 30)
                return "Hafif Şişman / Fazla Kilolu";
            else if (bmi < 35)
                return "1. Derecede Obez";
            else if (bmi < 40)
                return "2. Derecede Obez";
            else
                return "3. Derecede Obez / Morbid Obez";
        }
    }
}
