using UnityEngine;
using UnityEngine.UIElements;

namespace Doc
{
    public class RightPanel
    {
        private Label _label;
        private ScrollView _scrollView;

        public void UpdatePanel(DocumentationDto documentations)
        {
            _label.text = documentations.Title;

            _scrollView.Clear();

            foreach (var doc in documentations.Content)
            {
                var container = GetDataContainer(doc.ClassDescription);
                var scrollView = new ScrollView();
                var foldout = GetFoldout(doc.ScriptPath, container, new Color(0.16f, 0.16f, 0.16f));

                foreach (var methodContent in doc.MethodContentDtos)
                {
                    var fd = GetFoldout(methodContent.ScriptPath, GetDataContainer(methodContent.ClassDescription), new Color(0.15f, 0.15f, 0.15f));
                    scrollView.Add(fd);
                }
                container.Add(scrollView);
                _scrollView.Add(foldout);
            }
        }
        
        public VisualElement RightPanelInit(string labelText)
        {
            var rightPanel = new VisualElement
            {
                style =
                {
                    height = Length.Percent(100),
                    width = Length.Percent(90),
                    //backgroundColor = new StyleColor(Color.green),
                    alignSelf = Align.FlexEnd
                }
            };

            _label = new Label
            { 
                style =
                {
                    paddingLeft = 16,
                    paddingTop = 20,
                    fontSize = 56,
                    color = new StyleColor(Color.black)
                },
                
                text = labelText
            };
            
            
            _scrollView = new ScrollView();

            rightPanel.Add(_label);
            rightPanel.Add(_scrollView);
            
            return rightPanel;
        }
        
        private Foldout GetFoldout(string text, VisualElement content, Color backgroundColor)
        {
            var foldout = new Foldout
            {
                style =
                {
                },
                
                text = text
            };
            foldout.contentContainer.Add(content);

            return foldout;
        }
        
        private VisualElement GetDataContainer(string description)
        {
            var descr = new Label
            {
                style =
                {
                    fontSize = 18,
                    paddingLeft = 10,
                    paddingRight = 10,
                    paddingTop = 10,
                    paddingBottom = 10
                },
                text = description
            };
            
            var dataContainer = new VisualElement
            {
                style =
                {
                    marginBottom = 10,
                    marginTop = 10,
                    marginLeft = 10,
                    marginRight = 10,
                    borderBottomColor = new StyleColor(Color.grey),
                    borderLeftColor = new StyleColor(Color.grey),
                    borderRightColor = new StyleColor(Color.grey),
                    borderTopColor = new StyleColor(Color.grey),
                    borderBottomWidth = 1,
                    borderLeftWidth = 1,
                    borderRightWidth = 1,
                    borderTopWidth = 1,
                    borderBottomLeftRadius = 5,
                    borderBottomRightRadius = 5,
                    borderTopLeftRadius = 5,
                    borderTopRightRadius = 40
                }
            };

            dataContainer.Add(descr);

            return dataContainer;
        }
    }
}