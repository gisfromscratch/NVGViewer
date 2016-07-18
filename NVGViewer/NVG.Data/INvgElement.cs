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
using System.Xml;

namespace NVG.Data
{
    /// <summary>
    /// Represents a NVG XML element.
    /// </summary>
    public interface INvgElement
    {
        /// <summary>
        /// The child NVG elements of this instance.
        /// </summary>
        ICollection<INvgElement> Children { get; }

        /// <summary>
        /// Constructs this NVG element by using the specified reader.
        /// </summary>
        /// <param name="reader">The XML reader which reads the underyling NVG file.</param>
        void ConstructFromReader(XmlTextReader reader);
    }
}
