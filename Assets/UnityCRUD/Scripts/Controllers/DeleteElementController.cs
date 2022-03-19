using UnityCRUD.Scripts.Data;
using UnityCRUD.Scripts.Views;

namespace UnityCRUD.Scripts.Controllers
{
    public class DeleteElementController : MainTableElementController
    {
        public ColumnDataContainer dataContainer;
        public int rowId;
        public TableDataContainer connectedData;
        public override void Initialize()
        {
            if (IsInitialized) return;
            ElementBodyString = dataContainer.columnName;
            backgroundImage.color = MainView.CurrentColor;
            base.Initialize();
        }
        protected override void OnButtonClickedCallback(object objectParam)
        {
            base.OnButtonClickedCallback(objectParam);
            ConfirmationView.Instance.Initialize();
            ConfirmationView.AcceptedAction = CallDelete;
        }

        public async void CallDelete()
        {
            CrudLogger.Log("Delete called");
            await DatabaseDataContainer.DeleteDataFromTableTask(connectedData, rowId);
        }
    }
}