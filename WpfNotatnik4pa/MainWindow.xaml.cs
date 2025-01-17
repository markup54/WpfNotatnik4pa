using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace WpfNotatnik4pa
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string NazwaPliku { get; set; } = "";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click_Info_Autor(object sender, RoutedEventArgs e)
        {
            WindowAutor windowAutor = new WindowAutor();
            windowAutor.Show();//można w tle wykonać inne rzeczy niemodalne
        }

        private void MenuItem_Click_Info_Apka(object sender, RoutedEventArgs e)
        {
            WindowApka windowApka = new WindowApka();
            windowApka.ShowDialog(); // nie można nic zrobić w tle jeżeli to okno jest otwarte, modalne
        }

        private void MenuItem_Click_zapisz(object sender, RoutedEventArgs e)
        {
            if(NazwaPliku.Equals(""))
            {
                MenuItem_Click_Zapisz_Jako(sender, e);
            }
            else
            {
                File.WriteAllText(NazwaPliku, textbox_wpisanytekst.Text);
            }
        }

        private void MenuItem_Click_Zapisz_Jako(object sender, RoutedEventArgs e)
        {
            string tekstDoZapisu = textbox_wpisanytekst.Text;
           SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Plaintext | *.txt";
            if (saveFileDialog.ShowDialog()==true)
            {
                NazwaPliku = saveFileDialog.FileName;
                File.WriteAllText(saveFileDialog.FileName, tekstDoZapisu);
                Title = saveFileDialog.FileName;
            }
        }

        private void MenuItem_Click_Nowy(object sender, RoutedEventArgs e)
        {
            if(NazwaPliku == "")
            {
                if(textbox_wpisanytekst.Text == "")
                {
                    //jest to co miało być
                }
                else
                {
                    MessageBoxResult messageBoxResult= MessageBox.Show("Czy chcesz zapisać tekst do pliku?",
                        "Pytanko", MessageBoxButton.YesNoCancel);
                    if(messageBoxResult== MessageBoxResult.Yes)
                    {
                        MenuItem_Click_Zapisz_Jako(sender, e);
                        textbox_wpisanytekst.Text = "";
                        NazwaPliku = "";
                        Title = "Notatnik";
                    }
                    else if(messageBoxResult == MessageBoxResult.No)
                    {
                        textbox_wpisanytekst.Text = "";
                        NazwaPliku = "";
                        Title = "Notatnik";
                    }
                }
            }
            else
            {
                if(textbox_wpisanytekst.Text == File.ReadAllText(NazwaPliku))
                {
                    textbox_wpisanytekst.Text = "";
                    NazwaPliku = "";
                    Title = "Notatnik";
                }
                else
                {
                    MessageBoxResult messageBoxResult = MessageBox.Show("Czy zapisać zmiany w pliku?","Pytanko", MessageBoxButton.YesNoCancel);
                    if(messageBoxResult== MessageBoxResult.Yes)
                    {
                        File.WriteAllText(NazwaPliku, textbox_wpisanytekst.Text);
                        wyczysc();
                    }
                    else if(messageBoxResult == MessageBoxResult.No)
                    {
                        wyczysc();
                    }
                }
            }
        }
        private void wyczysc()
        {
            textbox_wpisanytekst.Text = "";
            NazwaPliku = "";
            Title = "Notatnik";
        }

        private void MenuItem_Click_Otworz(object sender, RoutedEventArgs e)
        {
            //czy zapisać najpierw stary plik
            OpenFileDialog openFileDialog = new OpenFileDialog();
            //filtr żeby pliki tekstowe
            openFileDialog.Filter = "plik tekstowy | *.txt";
            //jeżeli zostało otwarte okno dialogowe do otwierania pliku to zapisać jaki plik otwarto
            //i wyświetlić zawartość w textbox

           if( openFileDialog.ShowDialog()==true )
            {
                NazwaPliku=openFileDialog.FileName;
                Title = openFileDialog.FileName;
                textbox_wpisanytekst.Text=File.ReadAllText(NazwaPliku);
            }

        }

        private void MenuItem_Click_Kolor_czcionki(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            //(MenuItem) sender
            string kolor = menuItem.Header.ToString();
            switch (kolor)
            {
                case "czerwony":
                    textbox_wpisanytekst.Foreground = Brushes.Red;
                    break;
                case "zielony":
                    textbox_wpisanytekst.Foreground= Brushes.Green;
                    break;
                default:
                    textbox_wpisanytekst.Foreground = Brushes.Black; 
                    break;
            }
        }

        private void MenuItem_ClickDowolnyKolor(object sender, RoutedEventArgs e)
        {
            //otwórz nowe okno 
            //wybierz jakiś kolor
            //zatwierdz i ma się zmienic kolor czcionki
            Window1 nowe = new Window1();
           // textbox_wpisanytekst.Text = nowe.Wartosc;
            nowe.ShowDialog();
            
               textbox_wpisanytekst.Text = nowe.Wartosc;
            
        }
    }
}
