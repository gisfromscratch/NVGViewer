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

using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace NVG.Data
{
    /// <summary>
    /// Represents a NVG point element.
    /// </summary>
    public class NvgPointElement : INvgElement
    {
        /// <summary>
        /// Creates a new instance and sets the coordinates and the symbol code to the default values.
        /// </summary>
        public NvgPointElement()
        {
            Id = string.Empty;
            X = double.NaN;
            Y = double.NaN;
            SymbolCode = string.Empty;

            Children = new List<INvgElement>();
        }

        public string Id { get; set; }

        public string Label { get; set; }

        public double X { get; set; }

        public double Y { get; set; }

        public string SymbolCode { get; set; }

        /// <summary>
        /// The child elements of this NVG element.
        /// </summary>
        public ICollection<INvgElement> Children { get; private set; }

        /// <summary>
        /// <c>true</c> when this point element has an ID, coordinates and a symbol code.
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return !string.IsNullOrEmpty(Id) && double.IsNaN(X) && double.IsNaN(Y) && string.IsNullOrEmpty(SymbolCode);
            }
        }

        public void ConstructFromReader(XmlTextReader reader)
        {
            // Read the point attributes
            double x, y;
            string symbolCode;
            while (reader.MoveToNextAttribute())
            {
                if (0 == string.CompareOrdinal(@"id", reader.LocalName.ToLowerInvariant())
                    || 0 == string.CompareOrdinal(@"uri", reader.LocalName.ToLowerInvariant()))
                {
                    Id = reader.Value;
                }
                else if (0 == string.CompareOrdinal(@"x", reader.LocalName.ToLowerInvariant()))
                {
                    if (double.TryParse(reader.Value, NumberStyles.Any, CultureInfo.InvariantCulture, out x))
                    {
                        X = x;
                    }
                }
                else if (0 == string.CompareOrdinal(@"y", reader.LocalName.ToLowerInvariant()))
                {
                    if (double.TryParse(reader.Value, NumberStyles.Any, CultureInfo.InvariantCulture, out y))
                    {
                        Y = y;
                    }
                }
                else if (0 == string.CompareOrdinal(@"symbol", reader.LocalName.ToLowerInvariant()))
                {
                    if (TryParseSymbolCode(reader.Value, out symbolCode))
                    {
                        SymbolCode = symbolCode;
                    }
                }
                else if (0 == string.CompareOrdinal(@"label", reader.LocalName.ToLowerInvariant()))
                {
                    Label = reader.Value;
                }
            }
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
    }
}
