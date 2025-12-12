using L008_DataGrid_and_TreeView.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
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

namespace L008_DataGrid_and_TreeView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadTracks();
            LoadArtists();
        }

        private void LoadArtists()
        {
            using var db = new MusicContext();

            var artists = db.Artists
                .Where(artist => artist.Albums.Count > 2)
                .Include(artist => artist.Albums)
                .ThenInclude(album => album.Tracks)
                .ToList();

            myTreeView.ItemsSource = new ObservableCollection<Artist>(artists);
        }

        private void LoadTracks()
        {
            using var db = new MusicContext();

            var tracks = db.Tracks
                .Take(10)
                .Include(t => t.Album)
                .Select(t => new { Name = t.Name, AlbumName = t.Album.Title, Length = TimeSpan.FromMilliseconds(t.Milliseconds).ToString(@"mm\:ss") })
                .ToList();

            var collection = new ObservableCollection<object>(tracks);

            myDataGrid.ItemsSource = collection;

            //collection.Add(new Track() { Name = "Fredriks track" });
        }
    }
}