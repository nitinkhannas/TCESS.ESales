using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCESS.ESales.DataTransferObjects
{
    public class LapsedBookingDTO : BaseDTO
    {
        public int SMS_Order_No
        {
            get;
            set;
        }
        public string Truck_No
        {
            get;
            set;
        }
        public string Customer_Code
        {
            get;
            set;
        }
        public string Customer_Name
        {
            get;
            set;
        }
        public string Distt
        {
            get;
            set;
        }
        public string Mobile_No
        {
            get;
            set;
        }
        public string Remarks
        {
            get;
            set;
        }
    }
}
