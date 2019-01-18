#region Copyright Syncfusion Inc. 2001-2018.
// Copyright Syncfusion Inc. 2001-2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Win32;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using Syncfusion.Pdf.Parsing;
using Syncfusion.UI.Xaml.TreeGrid;
using Syncfusion.UI.Xaml.TreeGrid.Converter;
using PrintingDemo.Helpers;
using Syncfusion.Windows.PdfViewer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace PrintingDemo
{
    public class Commands
    {
        static Commands()
        {
            CommandManager.RegisterClassCommandBinding(typeof(SfTreeGrid), new CommandBinding(PrintTreeGrid, OnExecutePrintTreeGrid));
        }

        #region ExportToExcel Command

        public static RoutedCommand PrintTreeGrid = new RoutedCommand("PrintTreeGrid", typeof(SfTreeGrid));
        static PdfViewerControl pdfViewer = new PdfViewerControl();
        private static void OnExecutePrintTreeGrid(object sender, ExecutedRoutedEventArgs args)
        {
            var treeGrid = args.Source as SfTreeGrid;
            if (treeGrid == null) return;
            try
            {
                var options = new TreeGridPdfExportingOptions();               
                options.AllowIndentColumn = true;
                options.FitAllColumnsInOnePage = true;

                var document = treeGrid.ExportToPdf(options);

                
                MemoryStream stream = new MemoryStream();
                document.Save(stream);
                PdfLoadedDocument ldoc = new PdfLoadedDocument(stream);
                pdfViewer.Load(ldoc);
               
                MainWindow pdfPage = new MainWindow();
                pdfPage.Content = pdfViewer;
                pdfPage.Loaded += PdfPage_Loaded;
                pdfPage.Show();
          }
            catch (Exception)
            {

            }
        }

        private static void PdfPage_Loaded(object sender, RoutedEventArgs e)
        {
            var toolbar = pdfViewer.Template.FindName("PART_Toolbar", pdfViewer) as DocumentToolbar;

            //Get the instance of the open and save file button using its template name

            Button openButton = (Button)toolbar.Template.FindName("PART_ButtonOpen", toolbar);
            Button saveButton = (Button)toolbar.Template.FindName("PART_ButtonSave", toolbar);

            //Set the visibility of the button to collapsed 
            openButton.Visibility = System.Windows.Visibility.Collapsed;
            saveButton.Visibility = Visibility.Collapsed;
        }

        #endregion

    }
}
