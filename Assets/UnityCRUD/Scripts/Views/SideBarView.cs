using System.Linq;
using System.Threading.Tasks;
using UnityCRUD.Scripts.Controllers;
using UnityCRUD.Scripts.Data;
using UnityCRUD.Scripts.Data.ScriptableObjects;
using UnityEngine;

namespace UnityCRUD.Scripts.Views
{
    public class SideBarView : BaseView<SideBarView>, IView
    {
        public GameObject cellPrefab;
        public GameObject contentParent;
        public override void Initialize()
        {
            CrudLogger.Log("Initializing Sidebar", ServerSettings.DebuglevelEnum.Log);
            for (int i = 0; i < DatabaseDataContainer.Tables.Count; i++)
            {
                GameObject cell = Instantiate(cellPrefab, contentParent.transform);
                TableButtonElementController tableButtonElementController = cell.GetComponent<TableButtonElementController>();
                tableButtonElementController.dataContainer = DatabaseDataContainer.Tables[i];
                tableButtonElementController.addButton.gameObject.SetActive(true);
            }
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
