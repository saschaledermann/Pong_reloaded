using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    readonly List<Panel> m_panels = new();

    void Start()
    {
        InitializePanels();
    }

    void InitializePanels()
    {
        foreach (var panel in GetComponentsInChildren<Panel>())
        {
            if (m_panels.Contains(panel)) continue;
            m_panels.Add(panel);
            panel.Init(this);
            Debug.Log($"Panel of type {panel.GetType()} added.");
        }

        var mainPanel = m_panels.OfType<MainPanel>().FirstOrDefault();
        if (mainPanel != null)
        {
            foreach (var panel in m_panels)
            {
                var rectTransform = panel.GetComponent<RectTransform>();
                if (panel.GetType() == typeof(MainPanel))
                    rectTransform.localPosition = new Vector2(0, -325);
                else
                    rectTransform.localPosition = new Vector2(1350, -325);
            }
        }
    }

    public async void OpenPanel<T>(Panel caller, Vector2 destination) where T : Panel
    {
        var panel = m_panels.OfType<T>().FirstOrDefault();
        if (panel != null)
        {
            // Disable caller interactables
            caller.SetInteractables(false);

            // Move caller out
            var moveOut = MovePanel(caller.GetComponent<RectTransform>(), destination);
            // Move panel in
            var moveIn = MovePanel(panel.GetComponent<RectTransform>(), new Vector2(0, -325));
            await Task.WhenAll(moveOut, moveIn);
            // Enable panel interactables
            panel.SetInteractables(true);
        }
    }

    async Task MovePanel(RectTransform panelTransform, Vector2 destination, float duration = 0.25f)
    {
        Vector2 startPos = panelTransform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            var t = elapsed / duration;

            panelTransform.localPosition = Vector2.Lerp(startPos, destination, t);

            await Task.Yield();
        }

        panelTransform.localPosition = destination;
    }
}
