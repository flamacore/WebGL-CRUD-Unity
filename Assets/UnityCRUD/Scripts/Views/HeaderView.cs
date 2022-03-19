using System.Linq;
using System.Threading.Tasks;
using UnityCRUD.Scripts.Controllers;
using UnityCRUD.Scripts.Data;
using UnityCRUD.Scripts.Data.ScriptableObjects;
using UnityEngine;

namespace UnityCRUD.Scripts.Views
{
    public class HeaderView : BaseView<HeaderView>, IView
    {
        public GameObject cellPrefab;
        public GameObject contentParent;
        
        public async void InitializeTable(string tableName)
        {
            await ClearTable();
            TableDataContainer tableDataContainer = DatabaseDataContainer.Columns
                .Where(x => x.Key.tableName == tableName)
                .ToDictionary(x => x.Key, x => x.Value)
                .Keys.ElementAt(0);
                
            CrudLogger.Log("Initializing Header", ServerSettings.DebuglevelEnum.Log);
            for (int i = -1; i < DatabaseDataContainer.Columns[tableDataContainer].Count; i++)
            {
                GameObject cell = Instantiate(cellPrefab, contentParent.transform);
                cell.GetComponent<CellTextElementController>().dataContainer = i == -1 ? new ColumnDataContainer(DatabaseDataContainer.Columns[tableDataContainer][0].columnIndex, "-") : DatabaseDataContainer.Columns[tableDataContainer][i];
            }
            RebuildThisView();
            base.Initialize();
        }
        public async Task ClearTable()
        {
            Transform[] children = contentParent.GetComponentsInChildren<Transform>();
            await Task.Yield();
            for (var index = 0; index < children.Length; index++)
            {
                Transform child = children[index];
                if(child.gameObject.name != contentParent.name)
                {
                    child.gameObject.SetActive(false);
                    Destroy(child.gameObject, 7);
                }
            }
        }
    }
}
