using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Containerschip
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Ship _ship;
        private List<IContainer> _containers = new List<IContainer>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddContainerToList_Click(object sender, RoutedEventArgs e)
        {
            int containerWeight = GetContainerWeight(TbxContainerWeight);
            Button btnPressed = (Button)sender;
            if (containerWeight != 0 && GetContainerName(btnPressed) is string containerName)
            {
                switch (containerName)
                {
                    case nameof(ContainerNormal):
                        {
                            _containers.Add(new ContainerNormal(containerWeight));
                            break;
                        }
                    case nameof(ContainerCoolable):
                        {
                            _containers.Add(new ContainerCoolable(containerWeight));
                            break;
                        }
                    case nameof(ContainerValuable):
                        {
                            _containers.Add(new ContainerValuable(containerWeight));
                            break;
                        }
                    case nameof(ContainerCoolableValuable):
                        {
                            _containers.Add(new ContainerCoolableValuable(containerWeight));
                            break;
                        }
                }
                UpdateListContent();
                UpdateButtonContent(btnPressed, 1);
            }
        }

        private void BtnCalculateLayout_Click(object sender, RoutedEventArgs e)
        {
            if (_containers.Count != 0)
            {

                int shipLength = GetShipAttribute(TbxShipLength);
                int shipWidth = GetShipAttribute(TbxShipWidth);

                if (shipLength != 0 && shipWidth != 0)
                {
                    _ship = new Ship(_containers, shipLength, shipWidth);
                }
            }
            else
            {
                ShowError("Voeg ten minste 1 container toe");
            }
        }

        private void LbxContainers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IContainer selectedContainer = (IContainer)LbxContainers.SelectedItem;
            if (selectedContainer != null)
            {
                if (MessageBox.Show("Wilt u deze container verwijderen?", "Vraag", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    UpdateButtonContent(selectedContainer.GetType().ToString());
                    _containers.Remove(selectedContainer);
                    UpdateListContent();
                }
            }
        }

        private void UpdateListContent()
        {
            LbxContainers.ItemsSource = null;
            LbxContainers.ItemsSource = _containers;
        }

        private void UpdateButtonContent(Button btn, int amount)
        {
            try
            {
                string[] splitContent = btn.Content.ToString().Split('(');
                btn.Content = $"{splitContent[0]}({Convert.ToInt32(splitContent[1].TrimEnd(')')) + amount})";
            }
            catch (Exception)
            {
                btn.Content = $"{btn.Content} (1)";
            }
        }


        private void UpdateButtonContent(string containerName)
        {
            foreach (Button btn in GetButtons<Button>(DpButtons))
            {
                string[] splitContainerName = containerName.Split('.');
                if (btn.Name.Substring(3) == splitContainerName[1])
                {
                    UpdateButtonContent(btn, -1);
                }
            }
        }

        private IEnumerable<T> GetButtons<T>(DependencyObject depObj)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                if (child != null && child is T t)
                    yield return t;

                foreach (T childOfChild in GetButtons<T>(child))
                    yield return childOfChild;
            }
        }

        private int GetContainerWeight(TextBox tbx)
        {
            if (IsInputValid(tbx, 4000, 30000))
            {
                return Convert.ToInt32(tbx.Text);
            }
            return 0;
        }

        private string GetContainerName(Button btn)
        {
            return btn.Name.Substring(3);
        }

        private int GetShipAttribute(TextBox tbx)
        {
            if (IsInputValid(tbx))
            {
                return Convert.ToInt32(tbx.Text);
            }
            return 0;
        }

        private bool IsInputValid(TextBox tbx)
        {
            try
            {
                tbx.BorderBrush = Brushes.Black;
                if (Convert.ToInt32(tbx.Text) > 0)
                {
                    HideError(tbx);
                    return true;
                }
            }
            catch (Exception)
            {
                // do nothing
            }
            ShowError(tbx, "Input niet geldig");
            return false;
        }

        private bool IsInputValid(TextBox tbx, int min, int max)
        {
            try
            {
                if (Convert.ToInt32(tbx.Text) >= min && Convert.ToInt32(tbx.Text) <= max)
                {
                    HideError(tbx);
                    return true;
                }
            }
            catch (Exception)
            {
                // do nothing
            }
            ShowError(tbx, "Input niet geldig");
            return false;
        }

        private void HideError(TextBox tbx)
        {
            tbx.BorderBrush = Brushes.Black;
            tbx.Foreground = Brushes.Black;
        }

        private void ShowError(string msg)
        {
            MessageBox.Show(msg, "Oeps!", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void ShowError(TextBox tbx, string msg)
        {
            tbx.BorderBrush = Brushes.Red;
            tbx.Foreground = Brushes.Red;
            MessageBox.Show(msg, "Oeps!", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
