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
using WinForms = System.Windows.Forms;

namespace PhotoViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<FileInfo> list = new List<FileInfo>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = new Button();

            //Margin="10" Width="60"

            Thickness margin = button.Margin;

            margin.Bottom = 10;
            margin.Left = 10;
            margin.Right = 10;
            margin.Top = 10;

            button.Margin = margin;

            button.Width = 60;


            PhotoButtonPanel.Children.Add(button);
        }

        private void FolderOpen_Click(object sender, RoutedEventArgs e) 
        {
            WinForms.FolderBrowserDialog folderDialog = new WinForms.FolderBrowserDialog();
            folderDialog.ShowNewFolderButton = false;
//            folderDialog.SelectedPath = System.AppDomain.CurrentDomain.BaseDirectory;
            WinForms.DialogResult result = folderDialog.ShowDialog();

            if (result == WinForms.DialogResult.OK)
            {
                String spath = folderDialog.SelectedPath;
                FolderPath.Text = spath;

                DirectoryInfo folder = new DirectoryInfo(spath);

                if (folder.Exists)
                {
                    foreach (FileInfo item in folder.GetFiles())
                    {
                        list.Add(item);

                        Button button = new Button();

                        button.Margin = new Thickness(10);

                        button.Width = 60;

                        button.Background = new ImageBrush(new BitmapImage(new Uri(item.FullName)));

                        PhotoButtonPanel.Children.Add(button);
                    }
                }

                ImageFrame.Source = $"{folder.FullName}";
            }

        }

        private void OlderPhoto_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NextPhoto_Click(object sender, RoutedEventArgs e)
        {

        }

        private void InfoAboutPhoto_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
