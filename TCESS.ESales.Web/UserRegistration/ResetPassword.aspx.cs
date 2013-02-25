using System;
using System.Web.Security;
using System.Web.UI;
using TCESS.ESales.CommonLayer.CommonLibrary;
using System.Drawing;
using Resources;

public partial class ResetPassword : Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
	}

	protected void btnSubmit_Click(object sender, EventArgs e)
    {
        bool isValidate = Membership.ValidateUser(txtUserName.Text.Trim(), EncryptDecrypt.EncryptPassword(txtOldPassword.Text.Trim()));
        
        if (isValidate)
        {
            bool isPasswordUpdated = Membership.Provider.ChangePassword(txtUserName.Text.Trim(),
                EncryptDecrypt.EncryptPassword(txtOldPassword.Text.Trim()),
                EncryptDecrypt.EncryptPassword(txtNewPassword.Text.Trim()));

            lblMessage.ForeColor = Color.Green;
            lblMessage.Text = Messages.PasswordChangedSuccessfully;
        }
        else
        {
            lblMessage.ForeColor = Color.Red;
            lblMessage.Text = Messages.IncorrectOldPassword;
        }
	}
}