// Gist from https://gist.github.com/karljj1/978d76de9c93b20bd9761825ccf08fdf

using UnityEditor;
using UnityEngine.LowLevel;
using UnityEngine.UIElements;

public class ShowPlayerLoopWindow : EditorWindow
{
    [MenuItem("Window/Player Loop")]
    static void ShowWindow()
    {
        var wind = GetWindow<ShowPlayerLoopWindow>(false, "Player Loop");
        wind.Show();
    }

    void OnEnable()
    {
        Refresh();
    }

    void Refresh()
    {
        rootVisualElement.Clear();
        rootVisualElement.Add(new Button(Refresh) { text = "Refresh" });
        var scrollView = new ScrollView();
        rootVisualElement.Add(scrollView);

        var loop = PlayerLoop.GetCurrentPlayerLoop();
        ShowSystems(scrollView.contentContainer, loop.subSystemList, 0);
    }

    static void ShowSystems(VisualElement root, PlayerLoopSystem[] systems, int indent)
    {
        foreach (var playerLoopSystem in systems)
        {
            if (playerLoopSystem.subSystemList != null)
            {
                var foldout = new Foldout{ text = playerLoopSystem.type.Name, style = { left = indent * 15 }};
                root.Add(foldout);
                ShowSystems(foldout, playerLoopSystem.subSystemList, indent + 1);
            }
            else
            {
                root.Add(new Label(playerLoopSystem.type.Name) { style = { left = indent * 15 } });
            }
        }
    }
}