using UnityCRUD.Scripts.Data;
using UnityCRUD.Scripts.Views;
using UnityEngine;
using UnityEngine.UI;

namespace UnityCRUD.Scripts.Controllers
{
    public class TableButtonElementController : MainTableElementController
    {
        public Button addButton;
        public TableDataContainer dataContainer;

        protected override void OnButtonClickedCallback(object objectParam)
        {
            base.OnButtonClickedCallback(objectParam);
            MainView.CurrentPage = 0;
            MainView.Instance.InitializeTable(dataContainer.tableName);
            foreach (TableButtonElementController tableButtonElementController in FindObjectsOfType<TableButtonElementController>())
            {
                tableButtonElementController.addButton.gameObject.SetActive(false);
            }
            addButton.gameObject.SetActive(true);
        }
        public override void Initialize()
        {
            addButton.gameObject.SetActive(false);
            addButton.onClick.RemoveAllListeners();
            ElementBodyString = dataContainer.tableName;
            addButton.onClick.AddListener(() =>
            {
                AddEntryView.Instance.InitializeField(dataContainer);
            });
            base.Initialize();
        }
    }
}