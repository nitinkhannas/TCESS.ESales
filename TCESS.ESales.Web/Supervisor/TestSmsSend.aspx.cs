using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;

public partial class Bookings_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnvalidate_Click(object sender, EventArgs e)
    {
         string message = "BOOK " + txtCustCode.Text + " T " + txtTruck.Text;
         string url = "http://localhost/SMSGateway/SMSService/DCAGhatoSMSService.aspx?scid=56677&pno=91" + txtMobile.Text + "&msg=" + message;
        WebRequest wr = WebRequest.Create(url);
        wr.Timeout = 1000;
        try
        {
            HttpWebResponse response = (HttpWebResponse)wr.GetResponse();
        }
        catch (Exception ex)
        {
        }
    }
}