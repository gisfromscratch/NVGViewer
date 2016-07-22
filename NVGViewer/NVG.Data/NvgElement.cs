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
using System.IO;
using System.Xml;

namespace NVG.Data
{
    /// <summary>
    /// Represents a NVG root element.
    /// </summary>
    public class NvgElement : INvgElement, INvgFileMetadata
    {
        /// <summary>
        /// Creates a new NVG element instance.
        /// </summary>
        public NvgElement()
        {
            Children = new List<INvgElement>();
        }

        /// <summary>
        /// The version of this NVG element.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// The child elements of this NVG element.
        /// </summary>
        public ICollection<INvgElement> Children { get; private set; }

        public void ConstructFromReader(XmlTextReader reader)
        {
            // Read the NVG attributes
            while (reader.MoveToNextAttribute())
            {
                if (0 == string.CompareOrdinal(@"version", reader.LocalName.ToLowerInvariant()))
                {
                    Version = reader.Value;
                }
            }
        }

        public FileInfo FileInfo { get; set; }
    }
}
