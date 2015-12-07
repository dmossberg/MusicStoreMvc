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
/// Summary description for UserInfo
/// </summary>
public class UserInfo
{
    public String FirstName;
    public String LastName;
    public String Address;

	public UserInfo(string FName, string LName, string Address)
	{
        this.FirstName = FName;
        this.LastName = LName;
        this.Address = Address;
    }
}
