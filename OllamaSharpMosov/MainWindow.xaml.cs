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
using OllamaSharp;

namespace OllamaSharpMosov
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();        }

        public async Task ollama(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                MessageBox.Show("Enter your question");
                return;
            }
            try
            {
                var uri = new Uri("http://localhost:11434");
                var ollama = new OllamaApiClient(uri);

                ollama.SelectedModel = "llama2";
                var models = await ollama.ListLocalModelsAsync();

                var chat = new Chat(ollama);
                string moderRole = "Ты мастер по определению видов цветов и уходу за ними";

                await foreach (var answerToken in chat.SendAsync($"{moderRole}, {message}"))
                {
                    answerTB.Text = $"{answerToken} 123123";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(""+ex);
            }
        }

        private async void askOllama(object sender, RoutedEventArgs e)
        {
            await ollama(questionTB.Text);
            questionTB.Text = "";
        }
    }
}