using SITEK_HOMEWORK;
using System.Diagnostics;


string pathRKK = @"C:\Users\LEBEDEV\Desktop\СИТЕК\Тестовое задание - РКК.txt";
string pathOBR = @"C:\Users\LEBEDEV\Desktop\СИТЕК\Тестовое задание - Обращения.txt";
string pathSplitter = "\\";

//чтение из файла
static List<Document> ListOfDocumentsFromFile(string path)
{
    List<Document> document = new List<Document>();
    string docSplitter = "\t", otvSplitter = ";";
    try
    {
        using (StreamReader reader = new StreamReader(path))
        {
                Stopwatch Watch = new Stopwatch();
                Watch.Start();
                string? line;
                int i = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    document.Add(new Document());
                    document[i].SetRuk(line.Split(docSplitter)[0].Split(' ')[0] + ' ' +     //surname
                                       line.Split(docSplitter)[0].Split(' ')[1][0] + '.' +     //name
                                       line.Split(docSplitter)[0].Split(' ')[2][0] + '.');      //fathername
                    document[i].SetOtv(line.Split(docSplitter)[1].Split(otvSplitter)[0].Replace(" (Отв.)", ""));
                    document[i].SetGuid();
                    i++;
                }
                Watch.Stop();
                Console.WriteLine("RunTime File1:" + Watch.Elapsed);
        }
    }
    catch (Exception ee)
    {
        Console.WriteLine("Exception: " + ee.Message);
    }

    return document;
}












List<Document> DocumentsRKK = ListOfDocumentsFromFile(pathRKK);
List<Document> DocumentsOBR = ListOfDocumentsFromFile(pathOBR);


for (int i = 0; i < DocumentsRKK.Count; i++)
    Console.WriteLine(DocumentsRKK[i].GetRuk() + "\t\t" + DocumentsRKK[i].GetOtv());

for (int i = 0; i < DocumentsOBR.Count; i++)
    Console.WriteLine(DocumentsOBR[i].GetRuk() + "\t\t" + DocumentsOBR[i].GetOtv());



List<Otchet> otchet = new List<Otchet>();





Console.WriteLine();
Console.WriteLine("Количество неисполненных входящих документов: " + DocumentsRKK.Count + ";");
Console.WriteLine("Количество неисполненных письменных обращений граждан: " + DocumentsOBR.Count + ";");

string namePath1 = pathRKK.Split(pathSplitter)[^1];
Console.WriteLine("Неисполненные входящие документы: " + namePath1);

string namePath2 = pathOBR.Split(pathSplitter)[^1];
Console.WriteLine("Неисполненные письменные обращения граждан: " + namePath2);

Console.WriteLine(DateTime.Now.ToShortDateString());
