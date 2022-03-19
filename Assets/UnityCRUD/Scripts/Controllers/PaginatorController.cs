using System.Linq;
using TMPro;
using UnityCRUD.Scripts.Data;
using UnityCRUD.Scripts.Plugins;
using UnityCRUD.Scripts.Views;
using UnityEngine;

namespace UnityCRUD.Scripts.Controllers
{
    public class PaginatorController : MonoBehaviour
    {
        public TMP_InputField pageText;
        public void OnPreviousPageClicked()
        {
            if (UiElementsDataContainer.IsInputLocked) return;
            if(MainView.CurrentPage == 0) return;
            pageText.text = ((MainView.CurrentPage - 1) + 1).ToString();
            MainView.Instance.ChangePage(MainView.CurrentPage - 1);
        }

        public void OnNextPageClicked()
        {
            if (UiElementsDataContainer.IsInputLocked) return;
            if (DatabaseDataContainer
                .Rows[DatabaseDataContainer.Tables.Where(x => x.tableName == MainView.CurrentTableName).ToList()[0]]
                .Count < (MainView.CurrentPage * MainView.Instance.rowsPerPage)-1) return;
            pageText.text = ((MainView.CurrentPage + 1) + 1).ToString();
            MainView.Instance.ChangePage(MainView.CurrentPage + 1);
        }

        public void OnPageEnter(string p)
        {
            if (UiElementsDataContainer.IsInputLocked) return;
            pageText.text = p;
            MainView.Instance.ChangePage(p.ToInt()-1);
        }
    }
}
