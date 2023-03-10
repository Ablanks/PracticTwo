using System.IO;
using System.Text;
using System.Windows.Documents;
using System.Windows.Xps.Packaging;
using System.Windows.Xps.Serialization;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Создаем новый документ FlowDocument
            FlowDocument document = new FlowDocument();

            // Добавляем параграф и текст на кириллице
            Paragraph paragraph = new Paragraph();
            paragraph.Inlines.Add(new Run("Привет, мир!"));
            document.Blocks.Add(paragraph);

            // Создаем объект MemoryStream для сохранения XPS-документа
            MemoryStream xpsStream = new MemoryStream();

            // Сериализуем документ FlowDocument в XPS-документ
            Package package = Package.Open(xpsStream, FileMode.Create);
            XpsDocument xpsDoc = new XpsDocument(package);
            XpsSerializationManager xpsSerializer = new XpsSerializationManager(new XpsPackagingPolicy(xpsDoc), false);
            DocumentPaginator paginator = ((IDocumentPaginatorSource)document).DocumentPaginator;
            xpsSerializer.SaveAsXaml(paginator);
            xpsDoc.Close();
            package.Close();

            // Создаем объект MemoryStream для сохранения PDF-документа
            MemoryStream pdfStream = new MemoryStream();

            // Конвертируем XPS-документ в PDF-документ
            PdfSharp.Xps.XpsConverter.Convert(xpsStream, pdfStream, 0);

            // Сохраняем PDF-файл
            File.WriteAllBytes("example.pdf", pdfStream.ToArray());

            // Открываем созданный PDF-файл
            System.Diagnostics.Process.Start("example.pdf");
        }
        }
    }
}