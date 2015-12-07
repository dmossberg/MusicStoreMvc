using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

/// <summary>
/// Summary description for PurchaseOrder
/// </summary>
/// 
[XmlRootAttribute("PurchaseOrder", Namespace="http://www.cpandl.com", IsNullable=false)]
public class PurchaseOrder
{
    public Address ShipTo;
    public string OrderDate;
    [XmlArrayAttribute("Items")]
    public OrderedItem[] OrderedItems;
    public decimal SubTotal;
    public decimal ShipCost;
    public decimal TotalCost;
}

public class Address
{
    [XmlAttribute]
    public string Name;
    public string Line1;
    [XmlElementAttribute(IsNullable = false)]
    public string City;
    public string State;
    public string Zip;
}

public class OrderedItem
{
    public string ItemName;
    public string Description;
    public decimal UnitPrice;
    public int Quantity;
    public decimal LineTotal;

    /* Calculate is a custom method that calculates the price per item,
       and stores the value in a field. */
    public void Calculate()
    {
        LineTotal = UnitPrice * Quantity;
    }
}
