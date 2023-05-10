using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting;
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
using System.ComponentModel;

namespace WpfApp4.Pages
{


    public partial class PostPage : Page
    {
        public ObservableCollection<Post> Posts { get; set; }
        public ObservableCollection<SortItem> SortItems { get; set; } = new ObservableCollection<SortItem>()
        {
          new SortItem(){Name ="Сначала самые последние", Description= new SortDescription("Date", ListSortDirection.Descending)},
          new SortItem(){Name ="Сначала самые популярные", Description= new SortDescription("Reaction.Count", ListSortDirection.Descending)}
        };

        public SortItem SelectedSortItem { get; set; }

        public PostPage()
        {
            InitializeComponent();
            Posts = new ObservableCollection<Post>(MainWindow.Connection.Posts.OrderByDescending(p=>p.Date).ToList());
            DataContext = this;
        }

        private void SendMessage(object sender, RoutedEventArgs e)
        {
            string message = tbMessage.Text.Trim();
            if (message.Length == 0)
                return;

            Post post = new Post()
            {
                Sander = MainWindow.User.Id,
                Message = message,
                Date = DateTime.Now,
            };
            MainWindow.Connection.Posts.Add(post);
            MainWindow.Connection.SaveChanges();

            Posts.Add(post);
            tbMessage.Clear();
        }

        private void LikeThis(object sender, RoutedEventArgs e)
        {
            if (lvPosts.SelectedItem == null) return;

            var selectedPost = lvPosts.SelectedItem as Post;
            if (selectedPost != null)
            {
                Reaction reaction = new Reaction()
                {
                    Sender = MainWindow.User.Id,
                    Post = selectedPost.Id,
                };
                MainWindow.Connection.Reactions.Add(reaction);
                MainWindow.Connection.SaveChanges();
            }
        }

        private void GoComent(object sender, RoutedEventArgs e)
        {
            if (lvPosts.SelectedItem == null) return;
            NavigationService.Navigate(new ComentPage(lvPosts.SelectedItem as Post));
        }

        private void Sort()
        {
            var view = CollectionViewSource.GetDefaultView(lvPosts.ItemsSource);
            if (view == null)
                return;

            view.SortDescriptions.Clear();
            view.SortDescriptions.Add(SelectedSortItem.Description);


        }


        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Sort();
        }
    }
}
