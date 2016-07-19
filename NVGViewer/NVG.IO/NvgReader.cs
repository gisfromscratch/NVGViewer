/*
 * Copyright 2016 Jan Tschada
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using NVG.Data;
using System;
using System.Collections.Generic;
using System.Xml;

namespace NVG.IO
{
    /// <summary>
    /// Represents a NVG reader for XML documents.
    /// </summary>
    public class NvgReader : IDisposable
    {
        private readonly XmlTextReader _xmlTextReader;

        /// <summary>
        /// Creates a new reader instance using the specified file.
        /// </summary>
        /// <param name="filePath">The path to the NVG file.</param>
        public NvgReader(string filePath)
        {
            _xmlTextReader = new XmlTextReader(filePath);
        }

        /// <summary>
        /// Reads the next NVG element from the stream.
        /// </summary>
        /// <returns>The next NVG element or <code>null</code> when there are no more NVG elements.</returns>
        public INvgElement ReadNextElement()
        {
            return ReadNvgElement();
        }

        private INvgElement ReadNvgElement()
        {
            NvgElementPosition elementPosition = null;
            INvgElement element = null;
            while (_xmlTextReader.Read())
            {
                switch (_xmlTextReader.NodeType)
                {
                    case XmlNodeType.Element:
                        var startTag = new NvgElementTag(_xmlTextReader.LocalName);
                        if (startTag.IsNvgTag)
                        {
                            // Read the NVG element
                            element = new NvgElement();
                            element.ConstructFromReader(_xmlTextReader);
                            elementPosition = CreateElementPosition(element, startTag, elementPosition);
                        }
                        else if (startTag.IsGroupTag)
                        {
                            // Read the NVG group element
                            element = new NvgGroupElement();
                            element.ConstructFromReader(_xmlTextReader);
                            elementPosition = CreateElementPosition(element, startTag, elementPosition);
                        }
                        else if (startTag.IsPointTag)
                        {
                            // Read the NVG point element
                            element = new NvgPointElement();
                            element.ConstructFromReader(_xmlTextReader);
                            elementPosition = CreateElementPosition(element, startTag, elementPosition);
                        }
                        break;

                    case XmlNodeType.EndElement:
                        var endTag = new NvgElementTag(_xmlTextReader.LocalName);
                        elementPosition = AddChildren(elementPosition, endTag);
                        if (null != elementPosition)
                        {
                            element = elementPosition.Element;
                        }
                        break;
                }
            }
            return element;
        }

        /// <summary>
        /// Creates a new element position.
        /// </summary>
        /// <param name="element">The current element.</param>
        /// <param name="elementTag">The element tag of the specified element.</param>
        /// <param name="elementPosition">The previous element position.</param>
        /// <returns>A new element position.</returns>
        private static NvgElementPosition CreateElementPosition(INvgElement element, NvgElementTag elementTag, NvgElementPosition elementPosition)
        {
            return new NvgElementPosition
            {
                Element = element,
                ElementTag = elementTag,
                PreviousElementPosition = elementPosition
            };
        }

        /// <summary>
        /// Adds all children by iterating backwards using the specified element position.
        /// The first element matching the specified element tag is treated as the parent element.
        /// </summary>
        /// <param name="elementPosition">The current element position refering to the last created child element.</param>
        /// <param name="elementTag">The element tag of the parent element.</param>
        /// <returns>The element position of the parent element.</returns>
        private static NvgElementPosition AddChildren(NvgElementPosition elementPosition, NvgElementTag elementTag)
        {
            var childElements = new List<INvgElement>();
            for (; null != elementPosition && !elementPosition.ElementTag.IsEqualTo(elementTag); elementPosition = elementPosition.PreviousElementPosition)
            {
                childElements.Add(elementPosition.Element);
            }
            if (null == elementPosition)
            {
                // No start tag found!
                return null;
            }

            // Add all child elements
            foreach (var childElement in childElements)
            {
                elementPosition.Element.Children.Add(childElement);
            }
            return elementPosition;
        }

        /// <summary>
        /// Disposes this reader instance.
        /// </summary>
        public void Dispose()
        {
            _xmlTextReader.Dispose();
        }
    }
}
