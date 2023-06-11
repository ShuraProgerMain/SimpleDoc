﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Scripts;
using UnityEngine;

namespace Doc
{
    public struct AttributeDto
    {
        public Attribute Attribute { get; set; }
        public Type Parent { get; set; }
    }
    
    public class DocAttributesFinder
    {
        public IReadOnlyList<DocumentationDto> FindAll()
        {
            var unityAssembly = AppDomain.CurrentDomain.GetAssemblies().First(x => x.FullName.Contains("Assembly-CSharp"));

            Debug.Log(unityAssembly.FullName);

            var allTypes = unityAssembly.GetTypes();

            var attributes = new List<AttributeDto>();
            var documentationDtos = new List<DocumentationDto>();
            var dict = new Dictionary<string, IList<DocumentationContentDto>>();

            foreach (var t in allTypes)
            {
                var classAttributes = t.GetCustomAttributes(typeof(DocumentationClassAttribute));

                foreach (var data in classAttributes)
                {
                    attributes.Add(new AttributeDto() { Attribute = data, Parent = t});
                }
            }

            foreach (var currentAttribute in attributes)
            {
                if (currentAttribute.Attribute is DocumentationClassAttribute data)
                {
                    var methods = currentAttribute.Parent.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    var methodContents = new List<DocumentationMethodContentDto>();
                    
                    foreach (var methodInfo in methods)
                    {
                        var methodAttributes = methodInfo.GetCustomAttributes(typeof(DocumentationMethodAttribute));

                        foreach (var attribute in methodAttributes)
                        {
                            if (attribute is DocumentationMethodAttribute some)
                            {
                                methodContents.Add(new DocumentationMethodContentDto(methodInfo.MethodSignature(), some.Description));
                            }
                        }
                    }
                    
                    if (dict.TryGetValue(data.Title, out var value))
                    {
                        value.Add(new DocumentationContentDto(currentAttribute.Parent.FullName, data.Description, methodContents));
                    }
                    else
                    {
                        dict.Add(data.Title, new List<DocumentationContentDto>()
                        {
                            new(currentAttribute.Parent.GetTypeConstructors(), data.Description, methodContents)
                        });
                    }
                }
            }

            foreach (var pair in dict)
            {
                documentationDtos.Add(new DocumentationDto(pair.Key, pair.Value));
            }

            Debug.Log(attributes.Count);
            return documentationDtos;
        }
    }
}