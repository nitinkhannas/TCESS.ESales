using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCESS.ESales.DataTransferObjects
{
    public class BarcodeDTO
    {
        public byte[] BarcodeImage
        {
            get;
            set;
        }

        public string BarcodeValue
        {
            get;
            set;
        }
    }
}
