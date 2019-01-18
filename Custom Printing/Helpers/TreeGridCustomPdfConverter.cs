using Syncfusion.Pdf.Grid;
using Syncfusion.UI.Xaml.TreeGrid;
using Syncfusion.UI.Xaml.TreeGrid.Converter;

namespace PrintingDemo.Helpers
{
    public class TreeGridCustomPdfConverter : TreeGridToPdfConverter
    {
        internal bool _excludeNonExpandedNodes;
        public TreeGridCustomPdfConverter(bool excludeNonExpandedNodes) :base()
        {
            _excludeNonExpandedNodes = excludeNonExpandedNodes;
        }
        /// <summary>
        /// ExportNodes to PDF
        /// </summary>
        /// <param name="treeGrid"></param>
        /// <param name="nodes"></param>
        /// <param name="pdfGrid"></param>
        /// <param name="pdfExportingOptions"></param>
        protected override void ExportNodesToPdf(SfTreeGrid treeGrid, TreeNodes nodes, PdfGrid pdfGrid, TreeGridPdfExportingOptions pdfExportingOptions)
        {
            if (!_excludeNonExpandedNodes)
            {
                base.ExportNodesToPdf(treeGrid, nodes, pdfGrid, pdfExportingOptions);
            }
            else
            {
                for (int i = 0; i < nodes.Count; i++)
                {
                    TreeNode node = nodes[i];
                    ExportNodeToPdf(treeGrid, node, pdfGrid, pdfExportingOptions);
                    if (node.IsExpanded && node.HasChildNodes)
                    {
                        node.PopulateChildNodes();
                        ExportNodesToPdf(treeGrid, node.ChildNodes, pdfGrid, pdfExportingOptions);
                    }
                }
            }
        }
    }
}