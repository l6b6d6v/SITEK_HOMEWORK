using SITEK_HOMEWORK;

namespace SITEK_HOMEWORK
{
    public static class ListFunctions
    {
        //чтение из файла
        internal static List<Document> ListOfDocumentsFromFile(string path)
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

        internal static List<OtchetRecord> FillTmpOtchet(List<Document> documents)
        {
            List<OtchetRecord> TmpOtchet = new List<OtchetRecord>();
            for (int i = 0; i < documents.Count; i++)
            {
                if (documents[i].GetRuk().Equals("Климов С.А.") && (documents[i].GetOtv() != null || documents[i].GetOtv().Equals("") != true))
                {
                    TmpOtchet.Add(new OtchetRecord());
                    TmpOtchet[^1].SetIspolnitel(documents[i].GetOtv());

                }

                if (!documents[i].GetRuk().Equals("Климов С.А.") && (documents[i].GetRuk() != null || documents[i].GetRuk().Equals("") != true))
                {
                    TmpOtchet.Add(new OtchetRecord());
                    TmpOtchet[^1].SetIspolnitel(documents[i].GetRuk());
                }

            }
            return TmpOtchet;
        }

        internal static void FillOtchet(List<OtchetRecord> Otchet, List<OtchetRecord> tmpOtchet, int t)
        {
            int i = 0;
            //Заполняем первую строку в OtchetForAddedElements
            if (tmpOtchet.Count != 0)
            {
                if (Otchet.Count == 0)
                {
                    Otchet.Add(new OtchetRecord());
                    Otchet[0].SetIspolnitel(tmpOtchet[i].GetIspolnitel());
                    if (t == 0)
                        Otchet[0].SetCountRKK();
                    if (t == 1)
                        Otchet[0].SetCountOBR();
                    Otchet[0].SetCountRKK_OBR();
                    i++;
                }
            }
            else
            {
                Console.WriteLine("!Один из файлов не заполнен!");
                return;
            }

            int index;
            //Заполняем остальные строки в OtchetForAddedElements
            while (i < tmpOtchet.Count)
            {
                index = Otchet.FindIndex(x => x.GetIspolnitel().Equals(tmpOtchet[i].GetIspolnitel()));

                if (index == -1)
                {

                    Otchet.Add(new OtchetRecord());
                    Otchet[^1].SetIspolnitel(tmpOtchet[i].GetIspolnitel());
                    if (t == 0)
                        Otchet[^1].SetCountRKK();
                    if (t == 1)
                        Otchet[^1].SetCountOBR();
                    Otchet[^1].SetCountRKK_OBR();
                }

                if (index != -1)
                {
                    if (t == 0)
                        Otchet[index].SetCountRKK();
                    if (t == 1)
                        Otchet[index].SetCountOBR();
                    Otchet[index].SetCountRKK_OBR();
                }
                i++;
            }
        }
    }
}