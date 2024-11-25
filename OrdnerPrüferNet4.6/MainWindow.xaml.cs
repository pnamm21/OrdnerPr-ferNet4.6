using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace FolderChecker
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CheckButton_Click(object sender, RoutedEventArgs e)
        {
            string baseDirectoryPath = PathTextBox.Text.Trim();

            if (string.IsNullOrEmpty(baseDirectoryPath))
            {
                MessageBox.Show("Pfad darf nicht leer sein.");
                return;
            }

            if (!Directory.Exists(baseDirectoryPath))
            {
                MessageBox.Show("Das angegebene Verzeichnis existiert nicht.");
                return;
            }

            int minFolder = GetFolderInput(MinFolderTextBox.Text, "Min");
            int maxFolder = GetFolderInput(MaxFolderTextBox.Text, "Max");

            ResultsListBox.Items.Clear();

            for (int i = minFolder; i <= maxFolder; i++)
            {
                string folderStr = i.ToString("D9");
                string firstPart = folderStr.Substring(0, 3);
                string secondPart = folderStr.Substring(3, 3);
                string thirdPart = folderStr.Substring(6, 3);

                string folderPath = Path.Combine(baseDirectoryPath, firstPart, secondPart, thirdPart);

                if (Directory.Exists(folderPath))
                {
                    ResultsListBox.Items.Add($"Ordner {folderPath} existiert.");
                }
                else
                {
                    ResultsListBox.Items.Add($"Ordner {folderPath} fehlt.");
                }
            }
        }

        private int GetFolderInput(string inputText, string label)
        {
            int input;
            if (int.TryParse(inputText, out input) && input >= 1 && input <= 999999999)
            {
                return input;
            }
            else
            {
                MessageBox.Show($"Ungültige Eingabe für {label}. Bitte geben Sie eine Zahl zwischen 1 und 999999999 ein.");
                return -1;
            }
        }
    }
}