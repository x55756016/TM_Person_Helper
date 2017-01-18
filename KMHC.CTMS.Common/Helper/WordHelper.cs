namespace Project.Common.Helper
{
    public class WordHelper
    {
        ///// <summary>
        ///// 导出word文件
        ///// </summary>
        ///// <param name="templateFile">模板路径</param>
        ///// <param name="fileNameWord">导出文件名称</param>
        ///// <param name="fileNamePdf">pdf文件名称</param>
        ///// <param name="bookmarks">模板内书签集合</param>
        ///// <param name="invoiceline">发票条目列表</param>
        //public static void GenerateWord(string templateFile, string fileNameWord, string fileNamePdf, Dictionary<string, string> bookmarks, List<InvoiceLineView> invoiceline)
        //{
            //Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();
            //File.Copy(templateFile, fileNameWord, true);
            //Microsoft.Office.Interop.Word.Document doc = new Microsoft.Office.Interop.Word.Document();
            //object Obj_FileName = fileNameWord;
            //object Visible = false;
            //object ReadOnly = false;
            //object missing = System.Reflection.Missing.Value;
            //doc = app.Documents.Open(ref Obj_FileName, ref missing, ref ReadOnly, ref missing, ref missing, ref missing, ref missing,
            //    ref missing, ref missing, ref missing, ref missing, ref Visible, ref missing, ref missing, ref missing, ref missing);
            //doc.Activate();
            //foreach (string bookmarkName in bookmarks.Keys)
            //{
            //    object BookMarkName = bookmarkName;//获得书签名                    
            //    Range range = doc.Bookmarks.get_Item(ref BookMarkName).Range;//表格插入位置
            //    range.Text = bookmarks[bookmarkName];
            //}
            //object IsSave = true;
            //object FileName = fileNamePdf;
            //object FileFormat = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF;
            //object LockComments = false;
            //object AddToRecentFiles = true;
            //object ReadOnlyRecommended = false;
            //object EmbedTrueTypeFonts = false;
            //object SaveNativePictureFormat = true;
            //object SaveFormsData = false;
            //object SaveAsAOCELetter = false;
            //object Encoding = Microsoft.Office.Core.MsoEncoding.msoEncodingSimplifiedChineseGB18030;
            //object InsertLineBreaks = false;
            //object AllowSubstitutions = false;
            //object LineEnding = Microsoft.Office.Interop.Word.WdLineEndingType.wdCRLF;
            //object AddBiDiMarks = false;
            //doc.SaveAs(ref FileName, ref FileFormat, ref LockComments,
            //        ref missing, ref AddToRecentFiles, ref missing,
            //        ref ReadOnlyRecommended, ref EmbedTrueTypeFonts,
            //        ref SaveNativePictureFormat, ref SaveFormsData,
            //        ref SaveAsAOCELetter, ref Encoding, ref InsertLineBreaks,
            //        ref AllowSubstitutions, ref LineEnding, ref AddBiDiMarks);
            //doc.Close(ref IsSave, ref missing, ref missing);
        //}
    }
}
