﻿using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMSServiceReference;
using System.Text.RegularExpressions;


public partial class SMSService_DCAGhatoSMSService : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		
	
		//Note: remove all html data from the .aspx page. dont use any html tag in response.
		//To Print Response use "Response.Write()" method.
		string strSource = "", strPhoneNumber = "", strMessage = "";
		string strKeyword = "", strSubKeyword = "", strTruckMessage = "", strcustCode = "";
		//ShowQuerystringValues();
		//Make WCF service call to validate the request

		//Write all your code in this section
		//1. Trap all query string variables
		if ((Request.QueryString["scid"] == null) && (Request.QueryString["scid"] == null) && (Request.QueryString["msg"] == null))
		{
			return;
		}

		strSource = Request.QueryString["scid"].ToString();
		strPhoneNumber = Request.QueryString["pno"].ToString();
		strMessage = Request.QueryString["msg"].ToString();

		//Now Split the Message by space to get keyword/subkeywords.
		//for example you have a keyword "BOOK" and 2 subkeywords under it SUB1 And SUB2.
		char[] arrSeperator = new char[] { ' ' };
		string[] arrKeywords = strMessage.Split(arrSeperator, 4);

		if (arrKeywords.Length == 1)
			strKeyword = Convert.ToString(arrKeywords[0]);
		if (arrKeywords.Length == 2)
		{
			strKeyword = Convert.ToString(arrKeywords[0]);
			strSubKeyword = Convert.ToString(arrKeywords[1]);
		}
		if (arrKeywords.Length == 4)
		{
			strKeyword = Convert.ToString(arrKeywords[0]);
			strSubKeyword = Convert.ToString(arrKeywords[2]);
			strcustCode = Convert.ToString(arrKeywords[1]);
			strTruckMessage = Convert.ToString(arrKeywords[3]);
		}

		//print simple text using "Response.Write" what ever u want in response.
		if (strKeyword.ToUpper() == "BOOK" && strSubKeyword.ToUpper() == "T")
		{
			DateTime midnight = DateTime.Today;
			DateTime eightam = midnight.AddHours(8);
			DateTime sixpm = midnight.AddHours(18);
			if (System.DateTime.Now >= eightam && System.DateTime.Now <= sixpm)
			{
				strTruckMessage = Regex.Replace(strTruckMessage, "[^0-9a-zA-Z]+", "");
				SMSServiceReference.SMSServiceClient sc = new SMSServiceClient();
				string msg = sc.RespondSms(strPhoneNumber, strcustCode.ToUpper(), strTruckMessage.ToUpper());
				Response.Write(msg);
			}
			else
			{
				Response.Write("SMS Booking Sewa subh 8 baje se sham 6 baje tak pratidin uplabdh hai. Apni suvidha anusaar iska labh uthayen aur agle din ke liye booking karen.");
			}
		}
		else
		{
			Response.Write("Message in incorrect format, try again"); //Invalid Keyword
		}
	}
}