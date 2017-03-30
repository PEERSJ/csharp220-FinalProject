using PartApp.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace PartApp
{
    /// <summary>
    /// Interaction logic for PartWindow.xaml
    /// </summary>
    public partial class PartWindow : Window
    {
            public PartWindow()
            {
                InitializeComponent();

                // Don't show this window in the task bar
                ShowInTaskbar = false;
            }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Part != null)
            {
                if (Part.PartType == "Expendable")
                {
                    uxExpendable.IsChecked = true;
                }
                else if (Part.PartType == "Repairable")
                {
                    uxRepairable.IsChecked = true;
                }
                else if (Part.PartType == "Rotable")
                {
                    uxRotable.IsChecked = true;
                }

                uxSubmit.Content = "Update";
            }
            else
            {
                Part = new PartModel();
                Part.LastUpdatedOn = DateTime.Now;
            }

            uxGrid.DataContext = Part;
        }


        public PartModel Part { get; set; }

            private void uxSubmit_Click(object sender, RoutedEventArgs e)
            {
            //    Part = new PartModel();

            //    Part.PartNumber = uxPart.Text;
            //    Part.PartDescription= uxDescription.Text;

            if (uxExpendable.IsChecked.Value)
            {
                Part.PartType = "Exp";
            }
            else if(uxRepairable.IsChecked.Value)
            {
                Part.PartType = "Rep";
            }
            else if (uxRotable.IsChecked.Value)
            {
                Part.PartType = "Rot";
            }

            //    decimal cost = Convert.ToDecimal(uxCost.Text);
            //    Part.PartCost = cost;
            //    Part.PartQOH = 0;
            //    //Part.Notes = uxNotes.Text;
            //    //Part.CreatedDate = DateTime.Now;

            DialogResult = true;
                Close();
            }

            private void uxCancel_Click(object sender, RoutedEventArgs e)
            {
                DialogResult = false;
                Close();
            }




        private void SelectAddress(object sender, RoutedEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            if (tb != null)
            {
                tb.SelectAll();
            }
        }

        private void SelectivelyIgnoreMouseButton(object sender, MouseButtonEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            if (tb != null)
            {
                if (!tb.IsKeyboardFocusWithin)
                {
                    e.Handled = true;
                    tb.Focus();
                }
            }
        }

    }
}
