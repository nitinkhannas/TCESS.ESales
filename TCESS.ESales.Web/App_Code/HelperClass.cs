using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for HelperClass
/// </summary>
public class HelperClass
{
    public enum PaymentModes
    {
        CHEQUE = 1,
        DEMANDDRAFT = 2,
        RTGSTRANSFER = 3,
        CASH = 8
    }

    public enum VerificationMode
    {
        CUSTOMERCODE = 1,
        SMSID = 2
        
    }

    /// <summary>
    /// Enum for Action Mode For Manage Payments
    /// </summary>
    public enum ActionModeForManagePayments
    {
        EDIT = 1,
        PRINT = 2
    }

    /// <summary>
    /// Enum for Batch Status
    /// </summary>
    public enum BatchStatus
    {
        PENDING = 1,
        ACTIVATED = 2
    }

    /// <summary>
    /// Enum for Collection Status
    /// </summary>
    public enum UserType
    {
        SUPERADMIN = 1
    }
}