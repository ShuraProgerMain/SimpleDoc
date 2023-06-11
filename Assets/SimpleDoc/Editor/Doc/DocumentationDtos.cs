using System.Collections.Generic;

namespace Doc
{
    public struct DocumentationDto
    {
        public string Title { get; }
        public IList<DocumentationContentDto> Content { get; }
        
        public DocumentationDto(string title, IList<DocumentationContentDto> content)
        {
            Title = title;
            Content = content;
        }
    }
    
    public struct DocumentationContentDto
    {
        public string ScriptPath { get; }
        public string ClassDescription { get; }

        public IList<DocumentationMethodContentDto> MethodContentDtos { get; }
        public DocumentationContentDto(string scriptPath, string classDescription, IList<DocumentationMethodContentDto> methodContentDtos)
        {
            ScriptPath = scriptPath;
            ClassDescription = classDescription;
            MethodContentDtos = methodContentDtos;
        }
    }
    
    public struct DocumentationMethodContentDto
    {
        public string ScriptPath { get; }
        public string ClassDescription { get; }

        public DocumentationMethodContentDto(string scriptPath, string classDescription)
        {
            ScriptPath = scriptPath;
            ClassDescription = classDescription;
        }
    }
}