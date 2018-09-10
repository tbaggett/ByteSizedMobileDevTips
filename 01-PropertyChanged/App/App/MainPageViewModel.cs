using System.ComponentModel;

namespace App
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public string GivenNames { get; set; }
        public string FamilyName { get; set; }

        public string FullName => $"{GivenNames} {FamilyName}";

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
