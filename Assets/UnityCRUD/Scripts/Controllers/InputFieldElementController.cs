using TMPro;

namespace UnityCRUD.Scripts.Controllers
{
    public class InputFieldElementController : MainTableElementController
    {
        public string title;
        public TMP_InputField inputField;
        public TextMeshProUGUI titleText;

        public override void Initialize()
        {
            base.Initialize();
            titleText.text = title;
        }
    }
}