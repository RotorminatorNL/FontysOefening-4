﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Containerschip
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Ship _ship;
        private readonly List<IContainer> _containers = new List<IContainer>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddContainerToList_Click(object sender, RoutedEventArgs e)
        {
            int containerWeight = GetIntFromInput(TbxContainerWeight, 4000, 30000);
            Button btnPressed = (Button)sender;
            if (containerWeight != 0 && btnPressed.Name.Substring(3) is string containerName)
            {
                _containers.Add(GetContainer(containerName, containerWeight));
                UpdateListContent();
                UpdateButtonContent(btnPressed, 1);
            }
        }

        private IContainer GetContainer(string containerName, int containerWeight)
        {
            switch (containerName)
            {
                case nameof(ContainerNormal):
                    {
                        return new ContainerNormal(containerWeight);
                    }
                case nameof(ContainerCoolable):
                    {
                        return new ContainerCoolable(containerWeight);
                    }
                case nameof(ContainerValuable):
                    {
                        return new ContainerValuable(containerWeight);
                    }
                case nameof(ContainerCoolableValuable):
                    {
                        return new ContainerCoolableValuable(containerWeight);
                    }
            }
            return null;
        }

        private void BtnCalculateLayout_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            int shipLength = GetIntFromInput(TbxShipLength);
            int shipWidth = GetIntFromInput(TbxShipWidth);

            if (_containers.Count != 0 && shipLength != 0 && shipWidth != 0)
            {
                InitializeShip(shipLength, shipWidth);
                ShowShipLayout(GetShipLayout());
                ShowExtraShipInfo();
                ShowUnstorableContainers();
            }
            else
            {
                ShowError("Voeg ten minste 1 container toe");
            }
            Mouse.OverrideCursor = null;
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

        private void BtnStack(object sender, RoutedEventArgs e)
        {
            foreach (Button btn in GetButtons<Button>(SpShipLayout))
            {
                btn.FontFamily = new FontFamily("Segoe UI Light");
            }

            Button clickedButton = (Button)sender;
            clickedButton.FontFamily = new FontFamily("Segoe UI");
            clickedButton.FontWeight = FontWeights.Bold;
            string[] splitSelectedStackContent = clickedButton.Content.ToString().Split(' ')[1].Split('.');

            LblRowNumber.Content = splitSelectedStackContent[0];
            LblStackNumber.Content = splitSelectedStackContent[1];
            LbxSelectedStack.ItemsSource = _ship.GetStack(clickedButton.Tag.ToString());
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

        private void InitializeShip(int shipLength, int shipWidth)
        {
             _ship = new Ship(_containers, shipLength, shipWidth);
        }

        private List<DockPanel> GetShipLayout()
        {
            List<DockPanel> shipRows = GetShipRows(_ship.Width);
            for (int i = 0; i < shipRows.Count; i++)
            {
                shipRows[i] = AddStacksToRow(shipRows[i], _ship.Length);
            }
            return shipRows;
        }

        private List<DockPanel> GetShipRows(int amountRows)
        {
            List<DockPanel> shipRows = new List<DockPanel>();
            for (int i = 0; i < amountRows; i++)
            {
                DockPanel row = new DockPanel
                {
                    Name = $"Row_{i+1}"
                };
                shipRows.Add(row);
            }
            return shipRows;
        }

        private DockPanel AddStacksToRow(DockPanel row, int amountStacks)
        {
            for (int i = 0; i < amountStacks; i++)
            {
                string[] splitRowName = row.Name.Split('_');
                Button button = new Button
                {
                    Content = $"Stack {splitRowName[1]}.{i+1}",
                    FontSize = 20,
                    Tag = $"{Convert.ToInt32(splitRowName[1])-1}_{i}",
                    Width = 160
                };
                button.Click += BtnStack;
                row.Children.Add(button);
            }
            return row;
        }

        private void ShowShipLayout(List<DockPanel> shipRows)
        {
            SpShipLayout.Children.Clear();
            foreach (DockPanel row in shipRows)
            {
                SpShipLayout.Children.Add(row);
            }
        }

        private void ShowExtraShipInfo()
        {
            LblLength.Content = _ship.Length;
            LblWidth.Content = _ship.Width;
            LblWeightLeftWing.Content = _ship.WeightLeftWing;
            LblWeightRightWing.Content = _ship.WeightRightWing;
            LblWeightDifference.Content = _ship.WeightDifferenceOfWings;
            LblTotalWeight.Content = _ship.TotalWeight;
            LblRequiredWeight.Content = _ship.RequiredWeight;
            LblMaxWeight.Content = _ship.MaxWeight;
            LblAbleToGo.Content = _ship.IsAbleToGo == true ? "Ja" : "Nee";
        }

        private void ShowUnstorableContainers()
        {
            LbxUnstorableContainers.ItemsSource = null;
            LbxUnstorableContainers.ItemsSource = _ship.GetUnplacableContainers();
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
            foreach (Button btn in grdButtons.Children)
            {
                string[] splitContainerName = containerName.Split('.');
                if (btn.Name.Substring(3) == splitContainerName[1])
                {
                    UpdateButtonContent(btn, -1);
                }
            }
        }

        private int GetIntFromInput(TextBox tbx)
        {
            if (IsInputValid(tbx))
            {
                return Convert.ToInt32(tbx.Text);
            }
            return 0;
        }

        private int GetIntFromInput(TextBox tbx, int minAmount, int maxAmount)
        {
            if (IsInputValid(tbx, minAmount, maxAmount))
            {
                return Convert.ToInt32(tbx.Text);
            }
            return 0;
        }

        private bool IsInputValid(TextBox tbx)
        {
            try
            {
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

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            ResetAddContainers();
            ResetExtraShipInfo();
            ResetSelectedStack();

            _ship = null;
            SpShipLayout.Children.Clear();
            LbxUnstorableContainers.ItemsSource = null;
        }

        private void ResetAddContainers()
        {
            _containers.Clear();
            LbxContainers.ItemsSource = null;

            foreach (Button btn in grdButtons.Children)
            {
                try
                {
                    string[] splitContent = btn.Content.ToString().Split('(');
                    btn.Content = $"{splitContent[0].Trim(' ')}";
                }
                catch (Exception)
                {
                    btn.Content = $"{btn.Content}";
                }
            }
        }

        private void ResetExtraShipInfo()
        {
            LblLength.Content = "";
            LblWidth.Content = "";
            LblWeightLeftWing.Content = "";
            LblWeightRightWing.Content = "";
            LblWeightDifference.Content = "";
            LblTotalWeight.Content = "";
            LblRequiredWeight.Content = "";
            LblMaxWeight.Content = "";
            LblAbleToGo.Content = "";
        }

        private void ResetSelectedStack()
        {
            LblRowNumber.Content = "";
            LblStackNumber.Content = "";
            LbxSelectedStack.ItemsSource = null;
        }
    }
}
