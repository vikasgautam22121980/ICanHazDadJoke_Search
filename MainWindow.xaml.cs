using System.Threading.Tasks;
using System.Windows;

namespace ICanHazDadJoke_Search
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ApiHelper.InitializeClient();
        }
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadRandomJoke();
        }
        private async void searchJokesButton_Click(object sender, RoutedEventArgs e)
        {
            await SearchJokes();
        }
        private async Task SearchJokes()
        {
            var searchTerm = searchTextBox.Text;
            var jokes = await ICanHazDadJokeProcessor.SearchJokes(searchTerm);

            displayRandomJoke.Visibility = Visibility.Hidden;
            displaySearchedJokes.Visibility = Visibility.Visible;

            var contents = Common.Utilities.SetSearchContent(jokes, searchTerm);
            displaySearchedJokes.Content = contents;

        }
        private async Task LoadRandomJoke()
        {
            var joke = await ICanHazDadJokeProcessor.LoadRandomJoke();

            displayRandomJoke.Visibility = Visibility.Visible;
            displaySearchedJokes.Visibility = Visibility.Hidden;

            displayRandomJoke.Content = joke.Joke;
        }

        private void displaySearchedJokes_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }
    }
}
