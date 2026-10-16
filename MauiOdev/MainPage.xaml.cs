namespace MauiOdev
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            // Başlangıçta Gündüz modunu aktif et
            SetTheme(false);
        }

        // --- Gece/Gündüz Modu Mantığı ---
        private void OnThemeSwitchToggled(object sender, ToggledEventArgs e)
        {
            SetTheme(e.Value); // e.Value true ise Gece, false ise Gündüz modu
        }

        private void SetTheme(bool isDark)
        {
            if (isDark)
            {
                ThisPage.BackgroundColor = (Color)Resources["DarkBackground"];
                MainCard.BackgroundColor = (Color)Resources["DarkCardBackground"];
                Resources["TextColor"] = Resources["DarkTextColor"];
                Resources["SubTextColor"] = Resources["DarkSubTextColor"];
            }
            else
            {
                ThisPage.BackgroundColor = (Color)Resources["LightBackground"];
                MainCard.BackgroundColor = (Color)Resources["LightCardBackground"];
                Resources["TextColor"] = Resources["LightTextColor"];
                Resources["SubTextColor"] = Resources["LightSubTextColor"];
            }
        }

        // --- Diğer Bileşenlerin Mantıkları ---
        private void OnAgeStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            AgeLabel.Text = $"Yaş: {e.NewValue}";
        }

        private void OnScoreSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            ScoreLabel.Text = $"Yazılım Tutkusu: %{Math.Round(e.NewValue)}";
        }

        private async void OnSubmitButtonClicked(object sender, EventArgs e)
        {
            // 1. KONTROL AŞAMASI (Validation)
            // İsim kutusu boş mu VEYA (-) Açılır menüden seçim yapılmamış mı?
            if (string.IsNullOrWhiteSpace(NameEntry.Text) || ExpertisePicker.SelectedIndex == -1)
            {
                // Ekrana uyarı penceresi (Popup) çıkar
                await DisplayAlert("Eksik Bilgi", "Lütfen adınızı giriniz ve bir uzmanlık alanı seçiniz.", "Tamam");

                // Kodun aşağıya devam etmesini engelle (böylece eksik bilgiyle kayıt yapmaz)
                return;
            }

            // 2. KAYIT AŞAMASI (Bilgiler tamamsa burası çalışır)
            string isim = NameEntry.Text;
            string alan = ExpertisePicker.SelectedItem.ToString();
            double yas = AgeStepper.Value;
            double puan = Math.Round(ScoreSlider.Value);

            // Sonuç metni
            ResultLabel.Text = $"Hoş geldin, {isim}!\nAlan: {alan}\n{yas} yaşında %{puan} tutkuyla devam!";

            // Animasyon
            ResultLabel.Opacity = 0;
            await ResultLabel.FadeTo(1, 1000);
        }
    }
}