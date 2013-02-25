using System;

namespace TCESS.ESales.DataTransferObjects
{
    public class SmsExecutiveListDTO:BaseDTO
    {
        #region Primitive Properties

        public int SmsExecutiveList_Id
        {
            get;
            set;
        }

        public string SmsExecutiveList_Name
        {
            get;
            set;
        }

        public string SmsExecutiveList_PhoneNumber
        {
            get;
            set;
        }

        #endregion
    }
}
