﻿/*
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

namespace NVG.Data
{
    /// <summary>
    /// Represents a NVG element.
    /// </summary>
    public class NvgElement
    {
        /// <summary>
        /// Creates a new NVG element instance.
        /// </summary>
        public NvgElement()
        {
            HyperlinkElements = new List<NvgHyperlinkElement>();
        }

        /// <summary>
        /// The version of this NVG element.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// The hyperlink elements of this NVG element.
        /// </summary>
        public ICollection<NvgHyperlinkElement> HyperlinkElements { get; private set; }
    }
}