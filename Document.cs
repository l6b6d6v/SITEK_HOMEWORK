using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SITEK_HOMEWORK
{
    internal class Document
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


}
