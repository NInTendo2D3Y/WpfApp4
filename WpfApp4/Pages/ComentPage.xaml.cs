using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WpfApp4.Pages
{
    /// <summary>
    /// Логика взаимодействия для ComentPage.xaml
    /// </summary>
    public partial class ComentPage : Page
    {
        public ObservableCollection<Comment> Comments { get; set; }
        private Post post;
        public ComentPage( Post post)
        {
            InitializeComponent();
            Comments = new ObservableCollection<Comment>(post.Comment.ToList());
            
            this.post = post;

            DataContext= this;
        }

        private void CommentGo(object sender, RoutedEventArgs e)
        {
            string comentText=tbCommentText.Text.Trim();
            if (comentText.Length == 0) 
                return;

            Comment comment = new Comment()
            {
                Sander = MainWindow.User.Id,
                Post=post.Id,
                Message=comentText,
                Date=DateTime.Now,
            };
            MainWindow.Connection.Comments.Add(comment);
            MainWindow.Connection.SaveChanges();
            Comments.Add(comment);
            tbCommentText.Clear();
        }
    }
}
