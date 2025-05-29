using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Reflection.Metadata.BlobBuilder;

namespace Pilots_GUI;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window, INotifyPropertyChanged
{
    private readonly AppContext context = AppContext.GetContext();
    private ObservableCollection<Pilot> pilots;

    public ObservableCollection<Pilot> Pilots
    {
        get { return pilots; }
        set { pilots = value; OnPropertyChanged("Pilots"); }
    }

    public Pilot SelectedPilot { get; set; }

    //private Pilot newPilot;

    //public Pilot NewPilot
    //{
    //    get { return newPilot; }
    //    set { newPilot = value; OnPropertyChanged("NewPilot"); }
    //}

    public Pilot NewPilot { get; set; } = new Pilot();


    public List<string> Gender { get; set; } = new List<string>() { "M", "F" }; 

    public event PropertyChangedEventHandler PropertyChanged;

    public void OnPropertyChanged(string tulajdonsagNev)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(tulajdonsagNev));
    }

    public MainWindow()
    {
        InitializeComponent();
        this.DataContext = this;
        GetData();

    }

    private void GetData()
    {
        context.Pilots.Load();
        Pilots = context.Pilots.Local.ToObservableCollection();
    }

    private void save_BTN_Click(object sender, RoutedEventArgs e)
    {
        if (String.IsNullOrWhiteSpace(name_TXB.Text))
        {
            MessageBox.Show("A név mező kitöltése kötelező!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        if (gender_CBX.SelectedItem == null)
        {
            MessageBox.Show("A pilóta neme nincs megadva!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        if (date_PCR.SelectedDate == null || date_PCR.SelectedDate > DateTime.Now)
        {
            MessageBox.Show("A születési dátumnak muszáj korábbi dátumnak lennie!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        if (String.IsNullOrWhiteSpace(nation_TXB.Text))
        {
            MessageBox.Show("A nemzetiség mező kitöltése kötelező!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        context.Pilots.Add(NewPilot);
        context.SaveChanges();
        Pilots.Add(NewPilot);
        data_DG.Items.Refresh();
    }

    private void delete_BTN_Click(object sender, RoutedEventArgs e)
    {
        if (SelectedPilot != null)
        {
            MessageBoxResult result = MessageBox.Show($"Biztosan törölni kívánja a {SelectedPilot.Name} nevű pilótát?", "Megerősítés", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                context.Pilots.Remove(SelectedPilot);
                context.SaveChanges();
                Pilots.Remove(SelectedPilot);
                data_DG.Items.Refresh();
            }
            return;
        }
    }
}