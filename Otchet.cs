using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITEK_HOMEWORK
{
    internal class Otchet : IComparable<Otchet>
    {

        public int CompareTo(Otchet other)
        {
            int compare;
            compare = String.Compare(this.ispolnitel, other.ispolnitel, true);

            if (compare == 0)
            {
                compare = this.countRKK_OBR.CompareTo(other.countRKK_OBR);

                // Use descending order for speed.
                compare = -compare;
            }
            return compare;
        }

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

}
