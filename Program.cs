using SITEK_HOMEWORK;
using System.Diagnostics;


string pathRKK = @"C:\Users\LEBEDEV\Desktop\СИТЕК\Тестовое задание - РКК.txt";
string pathOBR = @"C:\Users\LEBEDEV\Desktop\СИТЕК\Тестовое задание - Обращения.txt";
string pathSplitter = "\\";
string namePathRKK = pathRKK.Split(pathSplitter)[^1];
string namePathOBR = pathOBR.Split(pathSplitter)[^1];



    //чтение из файла
    static List<Document> ListOfDocumentsFromFile(string path)
{
    List<Document> document = new List<Document>();
    string docSplitter = "\t", otvSplitter = ";", fioSplitter = " ";
    try
    {
        using (StreamReader reader = new StreamReader(path))
        {
            string? line;
            int i = 0;
            while ((line = reader.ReadLine()) != null)
            {
                document.Add(new Document());
                document[i].SetRuk(line.Split(docSplitter)[0].Split(fioSplitter)[0] + ' ' +     //surname
                                   line.Split(docSplitter)[0].Split(fioSplitter)[1][0] + '.' +     //name
                                   line.Split(docSplitter)[0].Split(fioSplitter)[2][0] + '.');      //fathername
                document[i].SetOtv(line.Split(docSplitter)[1].Split(otvSplitter)[0].Replace(" (Отв.)", ""));
                document[i].SetGuid();
                i++;
            }
        }
    }
    catch (Exception ee)
    {
        Console.WriteLine("Exception: " + ee.Message);
    }

    return document;
}


//Считываем файл и замеряем время считывания
Stopwatch WatchRKK = new Stopwatch();
WatchRKK.Start();
List<Document> DocumentsRKK = ListOfDocumentsFromFile(pathRKK);
WatchRKK.Stop();

Stopwatch WatchOBR = new Stopwatch();
WatchOBR.Start();
List<Document> DocumentsOBR = ListOfDocumentsFromFile(pathOBR);
WatchOBR.Stop();

List<OtchetRecord> TmpOtchetForAddElements = new List<OtchetRecord>();

//Заполнение временной таблицы для будущего подсчета исполнителей в отчете
//static void FillTmpOtchet(List<Document> documents, List<OtchetRecord> TmpOtchet)
//{
    for (int i = 0; i < DocumentsRKK.Count; i++)
    {
        if (DocumentsRKK[i].GetRuk().Equals("Климов С.А.") && (DocumentsRKK[i].GetOtv() != null || DocumentsRKK[i].GetOtv().Equals("") != true))
        {
        TmpOtchetForAddElements.Add(new OtchetRecord());
        TmpOtchetForAddElements[^1].SetIspolnitel(DocumentsRKK[i].GetOtv());

        }


        if (!DocumentsRKK[i].GetRuk().Equals("Климов С.А.") && (DocumentsRKK[i].GetRuk() != null || DocumentsRKK[i].GetRuk().Equals("") != true))
        {
        TmpOtchetForAddElements.Add(new OtchetRecord());
        TmpOtchetForAddElements[^1].SetIspolnitel(DocumentsRKK[i].GetRuk());
        }

    }
//}

//FillTmpOtchet(DocumentsRKK, TmpOtchetForAddElements);
//FillTmpOtchet(DocumentsOBR, TmpOtchetForAddElements);

//Сортируем временный массив для ускорения и упрощения вставки элементов в отчет
TmpOtchetForAddElements.Sort((OtchetRecord x, OtchetRecord y) =>
{
    return x.GetIspolnitel() == null && y.GetIspolnitel() == null
            ? 0
            : x.GetIspolnitel() == null
                ? -1
                : y.GetIspolnitel() == null
                    ? 1
                    : x.GetIspolnitel().CompareTo(y.GetIspolnitel());
});

List<OtchetRecord> OtchetForAddedElements = new List<OtchetRecord>();
//Заполняем первую строку в OtchetForAddedElements
    OtchetForAddedElements.Add(new OtchetRecord());
    OtchetForAddedElements[0].SetIspolnitel(TmpOtchetForAddElements[0].GetIspolnitel());
    OtchetForAddedElements[0].SetCountRKK();

//Заполняем остальные строки в OtchetForAddedElements
for (int i = 1; i < TmpOtchetForAddElements.Count; i++)
{
    for (int j = 0; j < OtchetForAddedElements.Count; j++)
    {

        if (!TmpOtchetForAddElements[i].GetIspolnitel().Equals(OtchetForAddedElements[^1].GetIspolnitel()))
        { 
            OtchetForAddedElements.Add(new OtchetRecord());
            OtchetForAddedElements[^1].SetIspolnitel(TmpOtchetForAddElements[i].GetIspolnitel());
        }
        OtchetForAddedElements[^1].SetCountRKK();
        break;
    }
}



Console.Write(@"Выберите подходящий тип сортировки:
              1) По фамилии ответственного исполнителя
              2) По количеству РКК
              3) По количеству обращений
              4) По общему количеству документов");
Console.WriteLine();

string measurement = Console.ReadLine();
string pathOtchet = @"C:\Users\LEBEDEV\Desktop\СИТЕК\Готовый отчет.txt";
using (StreamWriter writer = new StreamWriter(pathOtchet, false))
{
    switch (measurement)
    {
        case "1":
            //Сортировка по фамилии ответственного исполнителя
            OtchetForAddedElements.Sort((OtchetRecord x, OtchetRecord y) =>
            {
                return x.GetIspolnitel() == null && y.GetIspolnitel() == null
                        ? 0
                        : x.GetIspolnitel() == null
                            ? -1
                            : y.GetIspolnitel() == null
                                ? 1
                                : x.GetIspolnitel().CompareTo(y.GetIspolnitel());
            });

            writer.WriteLine("Сортировка по фамилии ответственного исполнителя");
            for (int i = 0; i < OtchetForAddedElements.Count; i++)
            {
                writer.WriteLine($"{OtchetForAddedElements[i].GetIspolnitel(),-20} {OtchetForAddedElements[i].GetCountRKK()}");
            }
            break;
        case "2":
            //Сортировка по GetCountRKK
            OtchetForAddedElements.Sort((OtchetRecord x, OtchetRecord y) => { return y.GetCountRKK().CompareTo(x.GetCountRKK()); });

            writer.WriteLine("Сортировка по количеству РКК");
            writer.WriteLine();
            string num = "№ п.п.", isp = "Ответственный исполнитель", rkk = "Количество неисполненных входящих документов",
                obr = "Количество неисполненных письменных обращений граждан", rkkobr = "Общее количество документов и обращений";
            writer.WriteLine($"{num,-20} {isp,-20} {rkk,-20} {obr,-20} {rkkobr,-20}");
            for (int i = 0; i < OtchetForAddedElements.Count; i++)
            {
                writer.WriteLine($"{OtchetForAddedElements[i].GetIspolnitel(),-20} {OtchetForAddedElements[i].GetCountRKK()}");
            }
            break;
        case "3":
            //Сортировка по GetCountRKK
            OtchetForAddedElements.Sort((OtchetRecord x, OtchetRecord y) => { return y.GetCountOBR().CompareTo(x.GetCountOBR()); });

            writer.WriteLine("Сортировка по количеству обращений");
            writer.WriteLine();
            num = "№ п.п."; isp = "Ответственный исполнитель"; rkk = "Количество неисполненных входящих документов";
            obr = "Количество неисполненных письменных обращений граждан"; rkkobr = "Общее количество документов и обращений";
            writer.WriteLine($"{num,-20} {isp,-20} {rkk,-20} {obr,-20} {rkkobr,-20}");
            for (int i = 0; i < OtchetForAddedElements.Count; i++)
            {
                writer.WriteLine($"{OtchetForAddedElements[i].GetIspolnitel(),-20} {OtchetForAddedElements[i].GetCountRKK()}");
            }
            break;
        case "4":
            //Сортировка по GetCountRKK
            OtchetForAddedElements.Sort((OtchetRecord x, OtchetRecord y) => { return y.GetCountRKK_OBR().CompareTo(x.GetCountRKK_OBR()); });

            writer.WriteLine("Сортировка по количеству РКК");
            writer.WriteLine();
            num = "№ п.п."; isp = "Ответственный исполнитель"; rkk = "Количество неисполненных входящих документов";
            obr = "Количество неисполненных письменных обращений граждан"; rkkobr = "Общее количество документов и обращений";
            writer.WriteLine($"{num,-20} {isp,-20} {rkk,-20} {obr,-20} {rkkobr,-20}");
            for (int i = 0; i < OtchetForAddedElements.Count; i++)
            {
                writer.WriteLine($"{OtchetForAddedElements[i].GetIspolnitel(),-20} {OtchetForAddedElements[i].GetCountRKK()}");
            }
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
Console.WriteLine("Данные записаны в файл: " + pathOtchet);





Console.ReadKey();