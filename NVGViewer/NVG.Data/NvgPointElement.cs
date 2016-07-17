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

namespace NVG.Data
{
    /// <summary>
    /// Represents a NVG point element.
    /// </summary>
    public class NvgPointElement
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
        }

        public string Id { get; set; }

        public string Label { get; set; }

        public double X { get; set; }

        public double Y { get; set; }

        public string SymbolCode { get; set; }

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
    }
}
