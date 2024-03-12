using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class ToggleSpriteBehaviour : MonoBehaviour
{
    [SerializeField] Image m_onImage;
    [SerializeField] Image m_offImage;
    Toggle m_toggle;

    void Start()
    {
        m_toggle = GetComponent<Toggle>();
        m_toggle.onValueChanged.AddListener(value =>
        {
            if (value)
            {
                m_toggle.targetGraphic = m_onImage;
                m_onImage.gameObject.SetActive(true);
                m_offImage.gameObject.SetActive(false);
            }
            else
            {
                m_toggle.targetGraphic = m_offImage;
                m_offImage.gameObject.SetActive(true);
                m_onImage.gameObject.SetActive(false);
            }
        });

        if (m_toggle.isOn)
        {
            m_toggle.targetGraphic = m_onImage;
            m_onImage.gameObject.SetActive(true);
            m_offImage.gameObject.SetActive(false);
        }
        else
        {
            m_toggle.targetGraphic = m_offImage;
            m_offImage.gameObject.SetActive(true);
            m_onImage.gameObject.SetActive(false);
        }
    }
}
