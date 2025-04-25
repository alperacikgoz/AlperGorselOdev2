using System;
using Microsoft.Maui.Controls;

namespace MyMauiApp
{
    public partial class CreditPage : ContentPage
    {
        public CreditPage()
        {
            InitializeComponent();

            // Başlangıç değerleri
            pickerCreditType.SelectedIndex = 0;
            sliderTerm.ValueChanged += SliderTerm_ValueChanged;
        }

        private void SliderTerm_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            lblTermValue.Text = $"Vade: {(int)e.NewValue} ay";
        }

        private async void OnCalculateClicked(object sender, EventArgs e)
        {
            // Alan kontrolü
            if (pickerCreditType.SelectedIndex < 0 ||
                string.IsNullOrWhiteSpace(entryAmount.Text) ||
                string.IsNullOrWhiteSpace(entryInterestRate.Text))
            {
                await DisplayAlert("Hata", "Lütfen tüm alanları doldurun.", "Tamam");
                return;
            }

            // Girdi değerleri
            double principal = double.Parse(entryAmount.Text);
            double annualRate = double.Parse(entryInterestRate.Text) / 100.0;
            int term = (int)sliderTerm.Value;

            // Vergi oranları
            double kkdf = 0, bsmv = 0;
            switch ((string)pickerCreditType.SelectedItem)
            {
                case "İhtiyaç Kredisi":
                    kkdf = 0.15; bsmv = 0.10;
                    break;
                case "Taşıt Kredisi":
                    kkdf = 0.15; bsmv = 0.05;
                    break;
                case "Konut Kredisi":
                    kkdf = 0.00; bsmv = 0.00;
                    break;
                case "Ticari Kredisi":
                    kkdf = 0.00; bsmv = 0.05;
                    break;
            }

            // Aylık faiz oranı
            double monthlyRate = annualRate / 12.0;

            // Brüt faiz (vergiler dahil)
            double brutRate = monthlyRate * (1 + kkdf + bsmv);

            // Anüite formülüyle aylık taksit
            double installment = (principal * brutRate * Math.Pow(1 + brutRate, term))
                                 / (Math.Pow(1 + brutRate, term) - 1);

            double totalPayment = installment * term;

            // Sonucu göster
            lblResult.Text = $"Aylık Taksit: {installment:F2}₺\n" +
                             $"Toplam Ödeme: {totalPayment:F2}₺";
        }
    }
}

