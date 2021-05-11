using UnityEngine;
using UnityEngine.UI;

namespace TG.GameJamTemplate
{
    public class SliderController : MonoBehaviour
    {
        [SerializeField] private Slider _slider = default;
        [Header("Optional")]
        [SerializeField] TMPro.TextMeshProUGUI _valueText = default;

        public Slider Slider => _slider;

        public float Value
        {
            get
            {
                float value;
                float.TryParse(_valueText.text, out value);

                return value / 100f;
            }
        }

        private void Start()
        {
        }

        public void Init(float value)
        {
            SetValue(value);
            _slider.value = value;
        }

        public void SetValue(float value)
        {
            var valueInt = Mathf.RoundToInt(value * 100);
            _valueText.text = valueInt.ToString();
        }
    }
}