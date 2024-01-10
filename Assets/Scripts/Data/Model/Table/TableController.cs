using NUnit.Framework;
using System.Collections.Generic;

public class TableController
{
	private List<TableLocalization> tableLocalizations;

	public List<TableLocalization> TableLocalizations
    {
		get { return tableLocalizations; }
	}

    public TableController(List<TableLocalization> tableLocalizations)
    {
        this.tableLocalizations = tableLocalizations;
    }
}