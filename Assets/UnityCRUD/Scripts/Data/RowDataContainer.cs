using System;
using UnityEngine;

namespace UnityCRUD.Scripts.Data
{
    [Serializable]
    public class RowDataContainer
    {
        [SerializeField] public string columnName = default;
        [SerializeField] public string tableName = default;
        [SerializeField] public int rowIndex = default;
        [SerializeField] public string rowData = default;

        public RowDataContainer()
        {
        }

        public RowDataContainer(string colName, string data, string tabName, int index)
        {
            columnName = colName;
            rowData = data;
            tableName = tabName;
            rowIndex = index;
        }
    }
}