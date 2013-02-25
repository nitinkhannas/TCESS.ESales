using System;

/// <summary>
/// Event argument class for customized dialog boxes
/// </summary>
public class DialogEventArgs : EventArgs
{
    private string buttonClicked = String.Empty;

    public string ButtonClicked
    {
        get;
        set;
    }
}