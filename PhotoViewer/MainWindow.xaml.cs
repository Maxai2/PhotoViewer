using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WinForms = System.Windows.Forms;

namespace PhotoViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<FileInfo> list = new List<FileInfo>();
        int current = 0;

        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();

            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 5);

            InfoListBox.Visibility = Visibility.Hidden;
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            InfoListBox.Items.Clear();

            if (current < list.Count - 1)
                current++;

            FileInfo file = new FileInfo(list[current].FullName);

            ImageFrame.Source = new BitmapImage(new Uri(file.ToString()));

            ListBoxItem box = new ListBoxItem();
            box.Content += "Attributes:\t" + file.Attributes + "\n";
            box.Content += "Size:\t\t" + file.Length + "b\n";
            box.Content += "Name:\t\t" + file.Name + "\n";
            box.Content += "Creation time:\t" + file.CreationTime;

            InfoListBox.Items.Add(box);
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

                int count = 0;

                if (folder.Exists)
                {
                    foreach (FileInfo item in folder.GetFiles())
                    {
                        list.Add(item);

                        Button button = new Button();

                        button.Margin = new Thickness(10);

                        button.Width = 60;

                        button.Background = new ImageBrush(new BitmapImage(new Uri(item.FullName)));

                        button.Name = count.ToString();

                        PhotoButtonPanel.Children.Add(button);

                        count++;
                    }
                }

                FileInfo file = new FileInfo(list[current].FullName);

                ImageFrame.Source = new BitmapImage(new Uri(file.ToString()));

                ListBoxItem box = new ListBoxItem();
                box.Content += "Attributes:\t" + file.Attributes + "\n";
                box.Content += "Size:\t\t" + file.Length + "b\n";
                box.Content += "Name:\t\t" + file.Name + "\n";
                box.Content += "Creation time:\t" + file.CreationTime;

                InfoListBox.Items.Add(box);
            }

        }

        private void OlderPhoto_Click(object sender, RoutedEventArgs e)
        {
            InfoListBox.Items.Clear();

            if (current > 0)
                current--;

            FileInfo file = new FileInfo(list[current].FullName);

            ImageFrame.Source = new BitmapImage(new Uri(file.ToString()));

            ListBoxItem box = new ListBoxItem();
            box.Content += "Attributes:\t" + file.Attributes + "\n";
            box.Content += "Size:\t\t" + file.Length + "b\n";
            box.Content += "Name:\t\t" + file.Name + "\n";
            box.Content += "Creation time:\t" + file.CreationTime;

            InfoListBox.Items.Add(box);
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Start();

            PlayButton.Visibility = Visibility.Hidden;
            StopButton.Visibility = Visibility.Visible;
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();

            StopButton.Visibility = Visibility.Hidden;
            PlayButton.Visibility = Visibility.Visible;
        }

        private void NextPhoto_Click(object sender, RoutedEventArgs e)
        {
            InfoListBox.Items.Clear();

            if (current < list.Count - 1)
                current++;

            FileInfo file = new FileInfo(list[current].FullName);

            ImageFrame.Source = new BitmapImage(new Uri(file.ToString()));

            ListBoxItem box = new ListBoxItem();
            box.Content += "Attributes:\t" + file.Attributes + "\n";
            box.Content += "Size:\t\t" + file.Length + "b\n";
            box.Content += "Name:\t\t" + file.Name + "\n";
            box.Content += "Creation time:\t" + file.CreationTime;

            InfoListBox.Items.Add(box);
        }

        private void InfoAboutPhoto_Click(object sender, RoutedEventArgs e)
        {
            if (InfoListBox.Visibility == Visibility.Hidden)
                InfoListBox.Visibility = Visibility.Visible;
            else
                InfoListBox.Visibility = Visibility.Hidden;
        }

        private void AddNewPhoto_Click(object sender, RoutedEventArgs e)
        {
            Button button = new Button();

            button.Margin = new Thickness(10);

            button.Width = 60;

            PhotoButtonPanel.Children.Add(button);
        }

        bool MouseDownClick = false;

        private void ImageFrame_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MouseDownClick = !MouseDownClick;

            if (MouseDownClick)
            {
                InfoListBox.Width = 0;
                TreeView.Width = 0;
            }
            else
            {
                InfoListBox.Width = 130;
                TreeView.Width = 130;
            }
        }

        private void PhotoButtonPanel_Click(object sender, RoutedEventArgs e)
        {
            Button button = e.OriginalSource as Button;

            MessageBox.Show(button.Name.ToString());

            //ImageFrame.Source = new BitmapImage(new Uri(button.ToString()));
        }
    }
}
