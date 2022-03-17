

List<Document> st_rkk = new List<Document>();
List<Document> st_obr = new List<Document>();


string path1 = @"C:\Users\LEBEDEV\Desktop\СИТЕК\Тестовое задание - РКК.txt";
string pathSplitter = "\\", docSplitter = "\t", otvSplitter = ";";

string namePath1 = path1.Split(pathSplitter)[^1];
// асинхронное чтение
using (StreamReader reader = new StreamReader(path1))
{
    string? line1;
    int i = 0;
    while ((line1 = await reader.ReadLineAsync()) != null)
    {
        st_rkk.Add(new Document());
        st_rkk[i].SetRuk(line1.Split(docSplitter)[0]);
        st_rkk[i].SetOtv(line1.Split(docSplitter)[1].Split(otvSplitter)[0].Replace(" (Отв.)", ""));
        st_rkk[i].SetGuid();
        i++;
    }
}

string path2 = @"C:\Users\LEBEDEV\Desktop\СИТЕК\Тестовое задание - Обращения.txt";
string namePath2 = path2.Split(pathSplitter)[^1];
// асинхронное чтение
using (StreamReader reader = new StreamReader(path2))
{
    string? line2;
    int i = 0;
    while ((line2 = await reader.ReadLineAsync()) != null)
    {
        st_obr.Add(new Document());
        st_obr[i].SetRuk(line2.Split(docSplitter)[0]);
        st_obr[i].SetOtv(line2.Split(docSplitter)[1].Split(otvSplitter)[0].Replace(" (Отв.)", ""));
        st_obr[i].SetGuid();
        i++;
    }
}

for (int i = 0; i < st_rkk.Count; i++)
    Console.WriteLine(st_rkk[i].GetRuk() + "\t\t" + st_rkk[i].GetOtv());

for (int i = 0; i < st_obr.Count; i++)
    Console.WriteLine(st_obr[i].GetRuk() + "\t\t" + st_obr[i].GetOtv());





Console.WriteLine(st_obr);






Console.WriteLine();
Console.WriteLine("Количество неисполненных входящих документов: " + st_rkk.Count + ";");
Console.WriteLine("Количество неисполненных письменных обращений граждан: " + st_obr.Count + ";");

Console.WriteLine("Неисполненные входящие документы: " + namePath1);
Console.WriteLine("Неисполненные письменные обращения граждан: " + namePath2);

Console.WriteLine(DateTime.Now.ToShortDateString());

public class Otchet
{
    private Guid id;
    public Guid GetGuid()
    { return id; }
    public void SetGuid()
    { id = Guid.NewGuid(); }


    private string? ispolnitel;
    public string GetIspolnitel()
    {
        if (ispolnitel == null)
            ispolnitel = "Данные по исп не заполнены";
        return ispolnitel;
    }
    public void SetIspolnitel(string value)
    { ispolnitel = value; }

    private int countRKK;
    public int GetCountRKK()
    { return countRKK; }
    public void SetCountRKK()
    { countRKK = countRKK + 1; }

    private int countOBR;
    public int GetCountOBR()
    { return countOBR; }
    public void SetCountOBR()
    { countOBR = countOBR + 1; }

    private int countRKK_OBR;
    public int GetCountRKK_OBR()
    { return countRKK_OBR; }
    public void SetCountRKK_OBR()
    { countRKK_OBR = countRKK_OBR + 1; }
}

public class Document 
{
    private Guid id;
    public Guid GetGuid()
    { return id; }
    public void SetGuid()
    { id = Guid.NewGuid(); }

    private string? ruk;
    public string GetRuk()
    {
        if (ruk == null)
            ruk = "";

        return ruk;
    }
    public void SetRuk(string value)
    { ruk = value; }

    private string? otv;
    public string GetOtv()
    {
        if (otv == null)
            otv = "";

        return otv;
    }
    public void SetOtv(string value)
    { otv = value; }
}

