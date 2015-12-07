using System;
using System.Data;

/// <summary>
/// Summary description for DataLayer
/// </summary>
public class DataLayer: IDisposable
{
    DataSet ds = new DataSet();
    private bool disposed = false;

	public DataLayer()
	{
        // Create a new DataTable.
        System.Data.DataTable myDataTable = new DataTable("ParentTable");
        // Declare variables for DataColumn and DataRow objects.
        DataColumn myDataColumn;
        DataRow myDataRow;

        // Create new DataColumn, set DataType, ColumnName and add to DataTable.    
        myDataColumn = new DataColumn();
        myDataColumn.DataType = System.Type.GetType("System.Int32");
        myDataColumn.ColumnName = "id";
        myDataColumn.ReadOnly = true;
        myDataColumn.Unique = true;
        // Add the Column to the DataColumnCollection.
        myDataTable.Columns.Add(myDataColumn);

        // Create second column.
        myDataColumn = new DataColumn();
        myDataColumn.DataType = System.Type.GetType("System.String");
        myDataColumn.ColumnName = "ParentItem";
        myDataColumn.AutoIncrement = false;
        myDataColumn.Caption = "ParentItem";
        myDataColumn.ReadOnly = false;
        myDataColumn.Unique = false;
        // Add the column to the table.
        myDataTable.Columns.Add(myDataColumn);

        // Make the ID column the primary key column.
        DataColumn[] PrimaryKeyColumns = new DataColumn[1];
        PrimaryKeyColumns[0] = myDataTable.Columns["id"];
        myDataTable.PrimaryKey = PrimaryKeyColumns;

        // Add the new DataTable to the DataSet.
        ds.Tables.Add(myDataTable);

        // Create fifty new DataRow objects and add them to the DataTable
        for (int i = 0; i <= 1000; i++)
        {
            myDataRow = myDataTable.NewRow();
            myDataRow["id"] = i;
            myDataRow["ParentItem"] = "ParentItem " + i;
            myDataTable.Rows.Add(myDataRow);
        }
	}

    public void Dispose(){
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public string GetData()
    {
        return ds.GetXml();
    }

    protected virtual void Dispose(bool disposing){
        if(!this.disposed){
            if(disposing){
                //dispose managed resources
                ds = null;
            }
            //simulate releasing unmanaged resources that take a lot of time
            System.Threading.Thread.Sleep(5000);
        }
        disposed = true;
    }

    ~DataLayer()
    {
        Dispose(false);
    }
}
