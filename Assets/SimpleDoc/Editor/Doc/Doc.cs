using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Doc
{
    public class Doc : EditorWindow
    {
        private RightPanel _rightPanel;

        private DocAttributesFinder _finder = new();
        
        [MenuItem("SimpleDoc/DocWindow")]
        public static void ShowExample()
        {
            Doc wnd = GetWindow<Doc>();
            wnd.titleContent = new GUIContent("Doc");
        }

        private VisualElement LeftPanelInit(string[] items, Action<int> onClick)
        {
            var leftPanel = new VisualElement
            {
                style =
                {
                    paddingBottom = 5,
                    paddingLeft = 5,
                    paddingRight = 5,
                    paddingTop = 5,
                    height = Length.Percent(100),
                    width = Length.Percent(10),
                    // backgroundColor = new StyleColor(new Color32(40, 40, 40, 255)),
                    
                    alignSelf = Align.FlexStart
                }
            };
            
            VisualElement MakeItem() => new Button();

            void BindItem(VisualElement e, int i)
            {
                Button btn = (Button)e;
                btn.text = items[i];
                btn.clicked += () => onClick?.Invoke(i);
            }
            
            var listView = new ListView
            {
                makeItem = MakeItem,
                bindItem = BindItem,
                itemsSource = items,
                selectionType = SelectionType.Multiple
            };
            
            leftPanel.Add(listView);

            return leftPanel;
        }

        private void OnClick(int index)
        {
            _rightPanel.UpdatePanel(_allDocs[index]);
        }


        private IReadOnlyList<DocumentationDto> _allDocs;
        public void CreateGUI()
        {
            _allDocs = _finder.FindAll();
            _rightPanel = new RightPanel();
            var root = rootVisualElement;
            root.style.display = DisplayStyle.Flex;
            root.style.flexDirection = FlexDirection.Row;

            var st = from titles in _allDocs select titles.Title;
            root.Add(LeftPanelInit(st.ToArray(), OnClick));
            root.Add(_rightPanel.RightPanelInit("Select another")); }
    }
}
