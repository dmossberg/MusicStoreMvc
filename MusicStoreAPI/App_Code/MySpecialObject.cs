using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for MySpecialObject
/// </summary>
public class MySpecialObject
{
    private int i;

    public MySpecialObject()
    {
        i = 0;
    }

    public int Counter
    {
        get { return i; }
        set { i = value; }
    }
    

    ~MySpecialObject()
    {
        throw new System.Exception("Evil Exception");
    }

    public DateTime GiveMeTheCurrentDate()
    {
        return System.DateTime.Now;
    }
}
