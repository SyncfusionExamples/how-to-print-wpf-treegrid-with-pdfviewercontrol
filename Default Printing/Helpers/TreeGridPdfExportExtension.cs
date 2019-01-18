using Syncfusion.Pdf;
using Syncfusion.UI.Xaml.TreeGrid;
using Syncfusion.UI.Xaml.TreeGrid.Converter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintingDemo.Helpers
{
    public static class TreeGridCustomPdfExportExtension
    {
        /// <summary>
        /// Export to PDF by excluding the non expanded nodes
        /// </summary>
        /// <param name="treeGrid"></param>
        /// <param name="exportingOptions"></param>
        /// <param name="excludeNonExpandedNodes"></param>
        /// <returns></returns>
        public static PdfDocument ExportToPdf(this SfTreeGrid treeGrid, TreeGridPdfExportingOptions exportingOptions, bool excludeNonExpandedNodes)
        {
            if (excludeNonExpandedNodes)
            {
                TreeGridCustomPdfConverter converter = new TreeGridCustomPdfConverter(excludeNonExpandedNodes);
                var pdfDocument = converter.ExportToPdf(treeGrid, exportingOptions);
                converter = null;
                return pdfDocument;
            }
            else
                return treeGrid.ExportToPdf(exportingOptions);
        }
    }
}
