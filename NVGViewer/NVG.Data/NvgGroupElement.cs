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
    /// Represents a NVG group element.
    /// </summary>
    public class NvgGroupElement : INvgElement
    {
        /// <summary>
        /// Create a new group element instance.
        /// </summary>
        public NvgGroupElement()
        {
            Children = new List<INvgElement>();
        }

        /// <summary>
        /// The link this element refers to.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// The child elements of this NVG element.
        /// </summary>
        public ICollection<INvgElement> Children { get; private set; }

        public void ConstructFromReader(XmlTextReader reader)
        {
            // Read the group attributes
            while (reader.MoveToNextAttribute())
            {
                if (0 == string.CompareOrdinal(@"href", reader.Name.ToLowerInvariant()))
                {
                    Url = reader.Value;
                }
            }
        }
    }
}
