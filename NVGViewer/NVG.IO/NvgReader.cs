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
            return ReadNvgElement(null, null);
        }

        private INvgElement ReadNvgElement(INvgElement parentElement, NvgElementTag elementTag)
        {
            INvgElement element = null;
            NvgElementTag startTag = null;
            NvgElementTag endTag = null;
            while (_xmlTextReader.Read())
            {
                switch (_xmlTextReader.NodeType)
                {
                    case XmlNodeType.Element:
                        startTag = new NvgElementTag(_xmlTextReader.LocalName);
                        if (startTag.IsNvgTag)
                        {
                            // Read the NVG element
                            element = new NvgElement();
                            element.ConstructFromReader(_xmlTextReader);
                            if (null != parentElement)
                            {
                                parentElement.Children.Add(element);
                            }
                            ReadNvgElement(element, startTag);
                        }
                        else if (startTag.IsGroupTag)
                        {
                            // Read the NVG group element
                            element = new NvgGroupElement();
                            element.ConstructFromReader(_xmlTextReader);
                            if (null != parentElement)
                            {
                                parentElement.Children.Add(element);
                            }
                            ReadNvgElement(element, startTag);
                        }
                        else if (startTag.IsPointTag)
                        {
                            // Read the NVG point element
                            element = new NvgPointElement();
                            element.ConstructFromReader(_xmlTextReader);
                            if (null != parentElement)
                            {
                                parentElement.Children.Add(element);
                            }
                            ReadNvgElement(element, startTag);
                        }
                        break;

                    case XmlNodeType.EndElement:
                        endTag = new NvgElementTag(_xmlTextReader.LocalName);
                        if (endTag.IsNvgTag)
                        {
                            return element;
                        }
                        else if (endTag.IsEqualTo(elementTag))
                        {
                            return element;
                        }
                        break;
                }
            }
            return element;
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
