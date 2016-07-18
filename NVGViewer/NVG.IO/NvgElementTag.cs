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

namespace NVG.IO
{
    /// <summary>
    /// Represents a NVG element tag.
    /// </summary>
    internal class NvgElementTag
    {
        private readonly string _localName;

        /// <summary>
        /// Creates a new element tag instance using the specified local name.
        /// </summary>
        /// <param name="localName">The local name of this element tag.</param>
        internal NvgElementTag(string localName)
        {
            _localName = localName;
        }

        /// <summary>
        /// <c>True</c> when this element tag is a NVG tag.
        /// </summary>
        internal bool IsNvgTag
        {
            get
            {
                return 0 == string.CompareOrdinal(@"nvg", _localName.ToLowerInvariant());
            }
        }

        /// <summary>
        /// <c>True</c> when this element tag is a group tag.
        /// </summary>
        internal bool IsGroupTag
        {
            get
            {
                return 0 == string.CompareOrdinal(@"a", _localName.ToLowerInvariant())
                    || 0 == string.CompareOrdinal(@"g", _localName.ToLowerInvariant());
            }
        }

        /// <summary>
        /// <c>True</c> when this element tag is a point tag.
        /// </summary>
        internal bool IsPointTag
        {
            get
            {
                return 0 == string.CompareOrdinal(@"point", _localName.ToLowerInvariant());
            }
        }

        /// <summary>
        /// Determines whether or not this tag name is equal to another tag name.
        /// </summary>
        /// <param name="other">The other tag.</param>
        /// <returns><c>True</c> if the tag names are equal.</returns>
        internal bool IsEqualTo(NvgElementTag other)
        {
            if (null == other)
            {
                return false;
            }

            return 0 == string.CompareOrdinal(_localName, other._localName);
        }
    }
}
