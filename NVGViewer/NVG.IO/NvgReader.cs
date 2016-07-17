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
using System.Globalization;
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
        public NvgElement ReadNextElement()
        {
            while(_xmlTextReader.Read())
            {
                switch (_xmlTextReader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (0 == string.CompareOrdinal(@"nvg", _xmlTextReader.Name.ToLowerInvariant()))
                        {
                            return ReadNvgElement();
                        }
                        break;
                }
            }
            return null;
        }

        private NvgElement ReadNvgElement()
        {
            NvgElement element = new NvgElement();

            // Read the NVG attributes
            while (_xmlTextReader.MoveToNextAttribute())
            {
                if (0 == string.CompareOrdinal(@"version", _xmlTextReader.Name.ToLowerInvariant()))
                {
                    element.Version = _xmlTextReader.Value;
                }
            }

            while (_xmlTextReader.Read())
            {
                switch (_xmlTextReader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (0 == string.CompareOrdinal(@"a", _xmlTextReader.Name.ToLowerInvariant()))
                        {
                            var hyperlinkElement = ReadHyperlinkElement();
                            element.HyperlinkElements.Add(hyperlinkElement);
                        }
                        break;

                    case XmlNodeType.EndElement:
                        if (0 == string.CompareOrdinal(@"nvg", _xmlTextReader.Name.ToLowerInvariant()))
                        { 
                            return element;
                        }
                        break;

                    case XmlNodeType.Attribute:
                        break;
                }
            }
            return null;
        }

        private NvgHyperlinkElement ReadHyperlinkElement()
        {
            NvgHyperlinkElement hyperlinkElement = new NvgHyperlinkElement();

            // Read the hyperlink attributes
            while (_xmlTextReader.MoveToNextAttribute())
            {
                if (0 == string.CompareOrdinal(@"href", _xmlTextReader.Name.ToLowerInvariant()))
                {
                    hyperlinkElement.Url = _xmlTextReader.Value;
                }
            }

            while (_xmlTextReader.Read())
            {
                switch (_xmlTextReader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (0 == string.CompareOrdinal(@"point", _xmlTextReader.Name.ToLowerInvariant()))
                        {
                            // Read and add the point element
                            var pointElement = ReadPointElement();
                            hyperlinkElement.PointElements.Add(pointElement);
                        }
                        break;

                    case XmlNodeType.EndElement:
                        if (0 == string.CompareOrdinal(@"a", _xmlTextReader.Name.ToLowerInvariant()))
                        {
                            return hyperlinkElement;
                        }
                        break;

                    case XmlNodeType.Attribute:
                        break;
                }
            }
            return null;
        }

        private NvgPointElement ReadPointElement()
        {
            NvgPointElement pointElement = new NvgPointElement();
            double x, y;
            string symbolCode;

            // Read the point attributes
            while (_xmlTextReader.MoveToNextAttribute())
            {
                if (0 == string.CompareOrdinal(@"id", _xmlTextReader.Name.ToLowerInvariant()))
                {
                    pointElement.Id = _xmlTextReader.Value;
                }
                else if (0 == string.CompareOrdinal(@"x", _xmlTextReader.Name.ToLowerInvariant()))
                {
                    if (double.TryParse(_xmlTextReader.Value, NumberStyles.Any, CultureInfo.InvariantCulture, out x))
                    {
                        pointElement.X = x;
                    }
                }
                else if (0 == string.CompareOrdinal(@"y", _xmlTextReader.Name.ToLowerInvariant()))
                {
                    if (double.TryParse(_xmlTextReader.Value, NumberStyles.Any, CultureInfo.InvariantCulture, out y))
                    {
                        pointElement.Y = y;
                    }
                }
                else if (0 == string.CompareOrdinal(@"symbol", _xmlTextReader.Name.ToLowerInvariant()))
                {
                    if (TryParseSymbolCode(_xmlTextReader.Value, out symbolCode))
                    {
                        pointElement.SymbolCode = symbolCode;
                    }
                }
                else if (0 == string.CompareOrdinal(@"label", _xmlTextReader.Name.ToLowerInvariant()))
                {
                    pointElement.Label = _xmlTextReader.Value;
                }
            }

            return pointElement;
        }

        private static bool TryParseSymbolCode(string value, out string symbolCode)
        {
            symbolCode = string.Empty;
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            // Validate if there is any prefix
            // E.G.: app6a:SPSP----------C
            var splittedValue = value.Split(':');
            if (2 == splittedValue.Length)
            {
                symbolCode = splittedValue[1];
                return true;
            }

            // Use the raw value without validation
            symbolCode = value;
            return true;
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
