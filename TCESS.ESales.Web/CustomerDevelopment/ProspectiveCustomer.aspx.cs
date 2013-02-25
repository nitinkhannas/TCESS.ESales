using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CustomerRegistration_ProspectiveCustomer : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ucCustomerRegistration.Event_SetCustomerRegistrationType(false);
    }
 }