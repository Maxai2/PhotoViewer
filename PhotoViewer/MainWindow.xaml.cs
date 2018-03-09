using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WinForms = System.Windows.Forms;
//---------------------------------------------------------------------------------------
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

        //---------------------------------------------------------------------------------------
        public MainWindow()
        {
            InitializeComponent();

            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 3);

            InfoListBox.Visibility = Visibility.Hidden;
        }
        //---------------------------------------------------------------------------------------
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
        //---------------------------------------------------------------------------------------
        private void FolderOpen_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.ShowNewFolderButton = false;
            //folderDialog.SelectedPath = System.AppDomain.CurrentDomain.BaseDirectory;
            DialogResult result = folderDialog.ShowDialog();

            if (result == WinForms.DialogResult.OK)
            {
                if (list.Count != 0)
                {
                    InfoListBox.Items.Clear(); 

                    PhotoButtonPanel.Children.RemoveAt(list.Count - 1);

                    list.Clear();

                    ImageFrame.Source = null; 

                    current = 0;
                }

                String spath = folderDialog.SelectedPath;
                FolderPath.Text = spath;

                DirectoryInfo folder = new DirectoryInfo(spath);

                int count = 0;

                if (folder.Exists)
                {
                    foreach (FileInfo item in folder.GetFiles())
                    {
                        list.Add(item);

                        System.Windows.Controls.Button button = new System.Windows.Controls.Button();

                        button.Margin = new Thickness(10);

                        button.Width = 60;

                        button.Background = new ImageBrush(new BitmapImage(new Uri(item.FullName)));

                        //string name = item.Name.Trim(new char[] {' ', '(', });

                        //string name = item.Name.Replace('.', ' ').Replace('(',' ').Replace(')',' ').Replace(' ','_');

                        button.Name = "Pic" + count;

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
        //---------------------------------------------------------------------------------------
        private void AddNewPhoto_Click(object sender, RoutedEventArgs e)
        {
            if (list.Count != 0)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();

                //openFileDialog.Multiselect = true;

                openFileDialog.Filter = "Image files (*.jpg)|*.jpg|(*.png)|*.png";

                if (openFileDialog.ShowDialog() == WinForms.DialogResult.OK)
                {
                    string fileName = openFileDialog.FileName;

                    fileName = fileName.Substring(fileName.LastIndexOf('\\') + 1);

                    string newPathFile = FolderPath.Text + '\\' + fileName;

                    FileInfo file = new FileInfo(newPathFile);

                    Directory.Move(openFileDialog.FileName, newPathFile);

                    list.Add(file);

                    System.Windows.Controls.Button button = new System.Windows.Controls.Button();

                    button.Margin = new Thickness(10);

                    button.Width = 60;

                    button.Background = new ImageBrush(new BitmapImage(new Uri(newPathFile)));

                    button.Name = "Pic" + (list.Count - 1);

                    PhotoButtonPanel.Children.Add(button);
                }
            }
        }
        //---------------------------------------------------------------------------------------
        private void OlderPhoto_Click(object sender, RoutedEventArgs e)
        {
            if (list.Count != 0)
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
        }
        //---------------------------------------------------------------------------------------
        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            if (list.Count != 0)
            {
                dispatcherTimer.Start();

                PlayButton.Visibility = Visibility.Hidden;
                StopButton.Visibility = Visibility.Visible;
            }
        }
        //---------------------------------------------------------------------------------------
        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();

            StopButton.Visibility = Visibility.Hidden;
            PlayButton.Visibility = Visibility.Visible;
        }
        //---------------------------------------------------------------------------------------
        private void NextPhoto_Click(object sender, RoutedEventArgs e)
        {
            if (list.Count != 0)
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
        }
        //---------------------------------------------------------------------------------------
        private void PhotoButtonPanel_Click(object sender, RoutedEventArgs e)
        {
            if (list.Count != 0)
            {
                System.Windows.Controls.Button button = e.Source as System.Windows.Controls.Button;

                int num = Convert.ToInt16(button.Name.Remove(0, 3));

                FileInfo file = new FileInfo(list[num].FullName);

                ImageFrame.Source = new BitmapImage(new Uri(file.ToString()));

                current = num;
            }
        }
        //---------------------------------------------------------------------------------------
        private void InfoAboutPhoto_Click(object sender, RoutedEventArgs e)
        {
            if (InfoListBox.Visibility == Visibility.Hidden)
                InfoListBox.Visibility = Visibility.Visible;
            else
                InfoListBox.Visibility = Visibility.Hidden;
        }
        //---------------------------------------------------------------------------------------
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
    }
}
//---------------------------------------------------------------------------------------