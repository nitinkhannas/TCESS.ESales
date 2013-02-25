using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using BarcodeEntry.ServiceReference;

namespace BarcodeEntry
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message m, System.Windows.Forms.Keys k)
        {
			try
			{
            string data = null;
            // detect the pushing of Enter Key
            if (k == System.Windows.Forms.Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txt_BarCode.Text.Trim()))
                {

                    lbl_BarCode.Text = txt_BarCode.Text.Trim();
                    txt_BarCode.Text = "";

                    if (ConfigurationManager.AppSettings["GATESELECTED"] != null)
                    {
                        data = ConfigurationManager.AppSettings["GATESELECTED"].ToString();


                        SMSServiceClient sc = new SMSServiceClient();
                        ServiceReference.BookingDTO BookingData = sc.UpdateGateInformation(Convert.ToInt32(data), Convert.ToInt32(lbl_BarCode.Text.Trim()));
                        if (BookingData != null)
                        {
                            if (!string.IsNullOrEmpty(BookingData.Booking_Truck_RegNo))
                                lbl_truckno.Text = BookingData.Booking_Truck_RegNo;
                            else
                                lbl_truckno.Text = BookingData.Booking_StandaloneTruck_RegNo;
                           
                            if (data == "1")
                            {
                                if(BookingData.Booking_TruckInTime != null)
                                lbl_Time.Text = (Convert.ToDateTime(BookingData.Booking_TruckInTime.ToString())).ToShortTimeString();
                            }
                            if (data == "2")
                            {
                                if (BookingData.Booking_TruckMatLiftedTime != null)
                                lbl_Time.Text = (Convert.ToDateTime(BookingData.Booking_TruckMatLiftedTime.ToString())).ToShortTimeString();
                            }

                        }
                        lbl_TotalCount.Text = sc.GetTruckCountForDateBarcode(DateTime.Now.Date, Convert.ToInt32(data)).ToString().Trim();


                    }
                    txt_BarCode.Focus();

                    // return true to stop any further interpretation of this key action
                    return true;
                }
            }
            // if not pushing Enter Key, then process the signal as usual
            return base.ProcessCmdKey(ref m, k);
			}
		catch (Exception ex)
        {
            return false;
        }

        }
   
        
		private void Form1_Load(object sender, EventArgs e)
		{
			try
			{

                pictureBox1.Width = SystemInformation.PrimaryMonitorSize.Width;
                Panel_BarCode.Location = new Point((SystemInformation.PrimaryMonitorSize.Width / 2 - this.Panel_BarCode.Size.Width / 2), (SystemInformation.PrimaryMonitorSize.Height / 2 - this.Panel_BarCode.Size.Height / 2));

                Panel_BarCode.Anchor = AnchorStyles.None;
				if (ConfigurationManager.AppSettings["GATESELECTED"] != null)
				{
					string gateSelected = ConfigurationManager.AppSettings["GATESELECTED"].ToString();
					if (gateSelected == "1")
					{
						lbl_TotalCountMsg.Text = ConfigurationManager.AppSettings["TRUCKIN"].ToString();
                        lblpost.Text = ConfigurationManager.AppSettings["STMESSAGEATTRUCKIN"].ToString();
					}
					if (gateSelected == "2")
					{
						lbl_TotalCountMsg.Text = ConfigurationManager.AppSettings["TRUCKOUT"].ToString();
                        lblpost.Text = ConfigurationManager.AppSettings["STMESSAGEATTRUCKOUT"].ToString();
					}

					SMSServiceClient sc = new SMSServiceClient();
					lbl_TotalCount.Text = sc.GetTruckCountForDateBarcode(DateTime.Now.Date, Convert.ToInt32(gateSelected)).ToString().Trim();
				}

				txt_BarCode.Focus();
			}
			catch (Exception ex)
			{
				txt_BarCode.Focus();
			}
		}

    }
}
