using System;

namespace Scripts
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class DocumentationClassAttribute : Attribute
    {
        public string Title { get; }
        public string Description { get; }
        
        public DocumentationClassAttribute(string title, string description)
        {
            Title = title;
            Description = description;
        }
    }
    
    [AttributeUsage(AttributeTargets.Method)]
    public class DocumentationMethodAttribute : Attribute
    {
        public string Description { get; }
        
        public DocumentationMethodAttribute(string description)
        {
            Description = description;
        }
    }
}