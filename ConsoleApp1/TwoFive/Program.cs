using System;
using System.IO;
using System.Text;
using DocumentFormat.OpenXml;
using OfficeOpenXml;
using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using df = DocumentFormat.OpenXml.Packaging;
using dfw = DocumentFormat.OpenXml.Wordprocessing;


namespace ConsoleApp5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Типы автомобилей");
            using (FileStream writer= new FileStream(@"C:\Users\gr621_praev\RiderProjects\ConsoleApp1\TwoFive\result.txt", FileMode.OpenOrCreate))
            {
                byte[] input = Encoding.Default.GetBytes(DatabaseRequests.GetCarQuery());
                writer.Write(input, 0, input.Length);
            }
            using (FileStream writer= new FileStream(@"C:\Users\gr621_praev\RiderProjects\ConsoleApp1\TwoFive\result2.txt", FileMode.OpenOrCreate))
            {
                byte[] input = Encoding.Default.GetBytes(DatabaseRequests.GetDriverQuery());
                writer.Write(input, 0, input.Length);
            }

            using (FileStream fs = File.Create(@"C:\Users\gr621_praev\RiderProjects\ConsoleApp1\TwoFive\result.doc"))
            {
                byte[] info = new UTF8Encoding(true).GetBytes(DatabaseRequests.GetCarQuery());
                fs.Write(info, 0, info.Length);
            }
            using (FileStream fs = File.Create(@"C:\Users\gr621_praev\RiderProjects\ConsoleApp1\TwoFive\result2.doc"))
            { 
                byte[] info = new UTF8Encoding(true).GetBytes(DatabaseRequests.GetDriverQuery());
                fs.Write(info, 0, info.Length);
            }
            
            

            string fon = @"C:\Users\gr621_praev\RiderProjects\ConsoleApp2\ConsoleApp2\arial.ttf";

            PdfDocument pdfDoc = new PdfDocument(new PdfWriter(@"C:\Users\gr621_praev\RiderProjects\ConsoleApp1\TwoFive\result.pdf"));
            Document doc = new Document(pdfDoc);
            PdfFont f1 = PdfFontFactory.CreateFont(fon, PdfEncodings.IDENTITY_H);
            Paragraph p1 = new Paragraph(DatabaseRequests.GetCarQuery()).SetFont(f1);
            doc.Add(p1);
            doc.Close();
            PdfDocument pdfDoc1 = new PdfDocument(new PdfWriter(@"C:\Users\gr621_praev\RiderProjects\ConsoleApp1\TwoFive\result2.pdf"));
            Document doc1 = new Document(pdfDoc1);
            PdfFont f2 = PdfFontFactory.CreateFont(fon, PdfEncodings.IDENTITY_H);
            Paragraph p2 = new Paragraph(DatabaseRequests.GetTypeCarQuery()).SetFont(f2);
            doc1.Add(p2);
            doc1.Close();

            Console.WriteLine();
            Console.WriteLine(DatabaseRequests.GetTypeCarQuery());
            Console.WriteLine();
            Console.WriteLine("Хотите добавить новый тип автомобиля?");
            string b = Console.ReadLine();
            if (b == "Yes" || b == "Да")
            {
                Console.WriteLine("Введите тип автомобиля");
                DatabaseRequests.AddTypeCarQuery(Console.ReadLine());
                DatabaseRequests.GetTypeCarQuery();
            }
            Console.WriteLine("Автомобили");
            Console.WriteLine(DatabaseRequests.GetCarQuery());
            Console.WriteLine();
            
            
            Console.WriteLine("Хотите добавить новый автомобиль?");
            string v = Console.ReadLine();
            if (v == "Yes" || v == "Да")
            {
                Console.WriteLine("Введите id типа автомобиля, название автомобиля, штатное название, количество мест");
                DatabaseRequests.AddCarQuery(Convert.ToInt32(Console.ReadLine()), Console.ReadLine(), Console.ReadLine(), Convert.ToInt32(Console.ReadLine()));
                DatabaseRequests.GetCarQuery();
            }
            
            Console.WriteLine("Водители");
            Console.WriteLine(DatabaseRequests.GetDriverQuery());
            Console.WriteLine();
            
            Console.WriteLine("Хотите добавить нового водителя?");
            string a = Console.ReadLine();
            if (a == "Yes" || a == "Да")
            {
                Console.WriteLine("Введите имя, фамилию и дату рождения водителя");
                DatabaseRequests.AddDriverQuery(Console.ReadLine(), Console.ReadLine(), DateTime.Parse(Console.ReadLine()));
            }
            
            Console.WriteLine("Права");
            DatabaseRequests.GetRightsCategoryQuery();
            
            Console.WriteLine();
            Console.WriteLine("Хотите добавить новую категорию прав?");
            string s = Console.ReadLine();
            if (s == "Yes" || s == "Да")
            {
                Console.WriteLine("Введите название категории прав, что бы добавить ее");
                DatabaseRequests.AddRightsCategoryQuery(Console.ReadLine());
                Console.WriteLine();
                DatabaseRequests.GetRightsCategoryQuery();
            }
            
           
            
            Console.WriteLine("Водители и их категории прав");
            DatabaseRequests.GetDriverRightsCategoryQuery();
            
            Console.WriteLine();
            Console.WriteLine("Хотите добавить новую категорию прав водителю?");
            string c = Console.ReadLine();
            if (c == "Yes" || c == "Да")
            {
                Console.WriteLine(
                    "Введите id водителя, затем введите id категории прав, чтобы выдать ему эту категорию прав ");
                DatabaseRequests.AddDriverRightsCategoryQuery(Convert.ToInt32(Console.ReadLine()),
                    Convert.ToInt32(Console.ReadLine()));
            }
            
            Console.WriteLine("Маршруты");
            DatabaseRequests.GetItineraryQuery();
            Console.WriteLine();
            Console.WriteLine("Хотите добавить новый маршрут?");
            string d = Console.ReadLine();
            if (d == "Yes" || d == "Да")
            {
                Console.WriteLine("Введите название маршрута");
                DatabaseRequests.AddItineraryQuery(Console.ReadLine());
                Console.WriteLine();
                DatabaseRequests.GetItineraryQuery();
            }
            
            Console.WriteLine("Рейсы");
            Console.WriteLine(DatabaseRequests.GetRouteQuery());
            Console.WriteLine();
            Console.WriteLine("Хотите добавить новый рейс?");
            string x = Console.ReadLine();
            if (x == "Yes" || x == "Да")
            {
                Console.WriteLine("Введите id водителя, id машины, id маршрута и число пассажиров");
                DatabaseRequests.AddRouteQuery(Convert.ToInt32(Console.ReadLine()), Convert.ToInt32(Console.ReadLine()), 
                    Convert.ToInt32(Console.ReadLine()), Convert.ToInt32(Console.ReadLine()));
                Console.WriteLine();
                Console.WriteLine(DatabaseRequests.GetRouteQuery());
            }
            /*
            FileInfo file = new FileInfo(@"C:\Users\gr621_praev\RiderProjects\ConsoleApp1\TwoFive\result.xlsx");
            using (ExcelPackage package = new ExcelPackage(file))
            {
                // Добавление нового листа
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("3");
                string[] results = DatabaseRequests.GetRouteQuery().Split(new string ( "\n" ));
                // Запись результата в ячейку
                int row = 1;
                foreach (string result in results)
                {
                    worksheet.Cells[row, 1].Value = result;
                    row++;
                }
                // Сохранение файла
                package.Save();
            }
            */
        }// автомобили и водители - в txt, doc, pdf, а рейсы в excel
    }
}