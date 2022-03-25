using SITEK_HOMEWORK;
using static SITEK_HOMEWORK.ListFunctions;
using System.Diagnostics;


string pathSplitter = "\\";

//Вставьте свой путь файла с РКК
string pathRKK = @"C:\Users\lebedev\Source\Repos\SITEK_HOMEWORK\Тестовое задание - РКК.txt";
string namePathRKK = pathRKK.Split(pathSplitter)[^1];

//Считываем файл и замеряем время считывания
Stopwatch WatchRKK = new Stopwatch();
WatchRKK.Start();
List<Document> DocumentsRKK = ListOfDocumentsFromFile(pathRKK);
WatchRKK.Stop();

//Вставьте свой путь файла с обращениями
string pathOBR = @"C:\Users\lebedev\Source\Repos\SITEK_HOMEWORK\Тестовое задание - Обращения.txt";
string namePathOBR = pathOBR.Split(pathSplitter)[^1];


//Вставьте свой путь для вывода готового отчета
string pathOtchet = @"C:\Users\lebedev\Source\Repos\SITEK_HOMEWORK\Готовый отчет.txt";

Stopwatch WatchOBR = new Stopwatch();
WatchOBR.Start();
List<Document> DocumentsOBR = ListOfDocumentsFromFile(pathOBR);
WatchOBR.Stop();


List<OtchetRecord> FromRKK = FillTmpOtchet(DocumentsRKK);

List<OtchetRecord> OtchetForAddedElements = new List<OtchetRecord>();
FillOtchet(OtchetForAddedElements, FromRKK, 0); //0 - файл РКК

List<OtchetRecord> FromOBR = FillTmpOtchet(DocumentsOBR);
FillOtchet(OtchetForAddedElements, FromOBR, 1); //1 - файл Обращений

for (int i = 0; i < OtchetForAddedElements.Count; i++)
    Console.WriteLine($"{i,-10}{OtchetForAddedElements[i].GetIspolnitel(),-25}{OtchetForAddedElements[i].GetCountRKK(),-25}" +
        $"{OtchetForAddedElements[i].GetCountOBR(),-25}{OtchetForAddedElements[i].GetCountRKK_OBR(),-25}");






Console.Write(@"Выберите подходящий тип сортировки:
              1) По фамилии ответственного исполнителя
              2) По количеству РКК
              3) По количеству обращений
              4) По общему количеству документов");
Console.WriteLine();

string measurement = Console.ReadLine();
string header = @$"
{"",-10}{"Ответственный",-25}{"Количество",-25}{"Количество",-25}{"Общее количество",-25}
{"№ п.п.",-10}{"исполнитель",-25}{"неисполненных",-25}{"неисполненных",-25}{"документов и",-25}
{"",-10}{"",-25}{"входящих документов",-25}{"письменных",-25}{"обращений",-25}
{"",-10}{"",-25}{"",-25}{"обращений граждан",-25}{"",-25}
";
using (StreamWriter writer = new StreamWriter(pathOtchet, false))
{
            void PrintOtchet(List<OtchetRecord> Otchet)
        {
            for (int i = 0; i < Otchet.Count; i++)
                writer.WriteLine($"{i + 1,-10}{Otchet[i].GetIspolnitel(),-25}{Otchet[i].GetCountRKK(),-25}" +
                    $"{Otchet[i].GetCountOBR(),-25}{Otchet[i].GetCountRKK_OBR(),-25}");
        }
    switch (measurement)
    {
        case "1":
            //Сортировка по фамилии ответственного исполнителя
            OtchetForAddedElements.Sort((OtchetRecord x, OtchetRecord y) => { return x.GetIspolnitel().CompareTo(y.GetIspolnitel()); });
            writer.WriteLine("Сортировка по фамилии ответственного исполнителя");
            writer.WriteLine(header);
            PrintOtchet(OtchetForAddedElements);
            break;
        case "2":
            //Сортировка по количеству РКК (в случае равенства – по количеству обращений);
            OtchetForAddedElements.Sort((OtchetRecord x, OtchetRecord y) => { return y.GetCountRKK().CompareTo(x.GetCountRKK()); });
            writer.WriteLine("Сортировка по количеству РКК");
            writer.WriteLine();
            writer.WriteLine(header);
            PrintOtchet(OtchetForAddedElements);
            break;
        case "3":
            //Сортировка по количеству обращений (в случае равенства – по количеству РКК);
            OtchetForAddedElements.Sort((OtchetRecord x, OtchetRecord y) => { return y.GetCountOBR().CompareTo(x.GetCountOBR()); });
            writer.WriteLine("Сортировка по количеству обращений");
            writer.WriteLine();
            writer.WriteLine(header);
            PrintOtchet(OtchetForAddedElements);
            break;
        case "4":
            //Сортировка по общему количеству документов (в случае равенства – по количеству РКК)
            OtchetForAddedElements.Sort((OtchetRecord x, OtchetRecord y) => { return y.GetCountRKK_OBR().CompareTo(x.GetCountRKK_OBR()); });

            writer.WriteLine("Сортировка по количеству РКК");
            writer.WriteLine();
            writer.WriteLine(header);
            PrintOtchet(OtchetForAddedElements);
            break;
        default:
            Console.WriteLine($"Measured value is {measurement}.");
            break;
    }




    writer.WriteLine();
    writer.WriteLine("Время считывания файла \"" + namePathRKK + "\": " + WatchRKK.Elapsed);
    writer.WriteLine("Время считывания файла \"" + namePathOBR + "\": " + WatchOBR.Elapsed);
    writer.WriteLine();
    writer.WriteLine("Количество неисполненных входящих документов: " + DocumentsRKK.Count + ";");
    writer.WriteLine("Количество неисполненных письменных обращений граждан: " + DocumentsOBR.Count + ";");
    writer.WriteLine();
    writer.WriteLine("Неисполненные входящие документы: " + namePathRKK);
    writer.WriteLine("Неисполненные письменные обращения граждан: " + namePathOBR);
    writer.WriteLine("Дата составления справки: " + DateTime.Now.ToShortDateString());


}
string namePathOtchet = pathOtchet.Split(pathSplitter)[^1];
Console.WriteLine("Данные записаны в файл: " + namePathOtchet);





Console.ReadKey();