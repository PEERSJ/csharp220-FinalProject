using System;
using System.Collections.Generic;
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
using PartApp.Models;

using System.ComponentModel;

/// <summary>
/// WinForms Application   - Solution Inventory Solution  containing "Parts" project.
/// //////////////////////////////////////////////////////////////////////////////////
/// </summary>
/// Jeff Peerson   CSHARP-220  WPF   Final Project  March 2017
//////////////////////////////////////////////////////////////////////////////////////


namespace PartApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GridViewColumnHeader listViewSortCol = null;
        private SortAdorner listViewSortAdorner = null;
        private PartModel selectedPart;

        public MainWindow()
        {
            InitializeComponent();

            LoadParts();
        }


        private void uxPartList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedPart = (PartModel)uxPartList.SelectedValue;
        }

        private void uxFileChange_Click(object sender, RoutedEventArgs e)
        {
            var window = new PartWindow();
            // window.Part = selectedPart;
            window.Part = selectedPart.Clone();

            if (window.ShowDialog() == true)
            {
                //prepare window for update and load contacts
                App.PartRepository.Update(window.Part.ToRepositoryModel());
                LoadParts();
            }
        }



        private void uxFileChange_Loaded(object sender, RoutedEventArgs e)
        {
            uxFileChange.IsEnabled = (selectedPart != null);
            uxContextFileChange.IsEnabled = uxFileChange.IsEnabled;
        }

        private void uxFileDelete_Click(object sender, RoutedEventArgs e)
        {
            App.PartRepository.Remove(selectedPart.PartId);
            selectedPart = null;
            LoadParts();
        }

        private void uxFileDelete_Loaded(object sender, RoutedEventArgs e)
        {
            uxFileDelete.IsEnabled = (selectedPart != null);
            uxContextFileDelete.IsEnabled = uxFileDelete.IsEnabled;
        }


        private void LoadParts()
        {
            var parts = App.PartRepository.GetAll();

            uxPartList.ItemsSource = parts
                .Select(t => PartModel.ToModel(t))
                .ToList();

            // OR
            //var uiPartModelList = new List<PartModel>();
            //foreach (var repositoryPartModel in contacts)
            //{
            ////    This is the .Select(t => ... )
            //    var uiPartModel = PartModel.ToModel(repositoryPartModel);
            //
            //    uiPartModelList.Add(uiPartModel);
            //}

            //uxPartList.ItemsSource = uiPartModelList;
        }

        private void uxFileNew_Click(object sender, RoutedEventArgs e)
        {
            var window = new PartWindow();

            if (window.ShowDialog() == true)
            {
                var uiPartModel = window.Part;

                var repositoryPartModel = uiPartModel.ToRepositoryModel();

                App.PartRepository.Add(repositoryPartModel);

                // OR
                //App.PartRepository.Add(window.Part.ToRepositoryModel());

                LoadParts();   //when adding call on load contacts to show up
            }
        }


        private void lvPartsColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = (sender as GridViewColumnHeader);
            string sortBy = column.Tag.ToString();
            if (listViewSortCol != null)
            {
                AdornerLayer.GetAdornerLayer(listViewSortCol).Remove(listViewSortAdorner);
                uxPartList.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir = ListSortDirection.Ascending;
            if (listViewSortCol == column && listViewSortAdorner.Direction == newDir)
                newDir = ListSortDirection.Descending;

            listViewSortCol = column;
            listViewSortAdorner = new SortAdorner(listViewSortCol, newDir);
            AdornerLayer.GetAdornerLayer(listViewSortCol).Add(listViewSortAdorner);
            uxPartList.Items.SortDescriptions.Add(new SortDescription(sortBy, newDir));
        }



        public class SortAdorner : Adorner
        {
            private static Geometry ascGeometry =
                    Geometry.Parse("M 0 4 L 3.5 0 L 7 4 Z");

            private static Geometry descGeometry =
                    Geometry.Parse("M 0 0 L 3.5 4 L 7 0 Z");

            public ListSortDirection Direction { get; private set; }

            public SortAdorner(UIElement element, ListSortDirection dir)
                    : base(element)
            {
                this.Direction = dir;
            }

            protected override void OnRender(DrawingContext drawingContext)
            {
                base.OnRender(drawingContext);

                if (AdornedElement.RenderSize.Width < 20)
                    return;

                TranslateTransform transform = new TranslateTransform
                        (
                                AdornedElement.RenderSize.Width - 15,
                                (AdornedElement.RenderSize.Height - 5) / 2
                        );
                drawingContext.PushTransform(transform);

                Geometry geometry = ascGeometry;
                if (this.Direction == ListSortDirection.Descending)
                    geometry = descGeometry;
                drawingContext.DrawGeometry(Brushes.Black, null, geometry);

                drawingContext.Pop();
            }
        }

        private void uxClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
