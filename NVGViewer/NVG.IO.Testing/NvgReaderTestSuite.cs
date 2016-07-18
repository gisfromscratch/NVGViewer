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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NVG.Data;
using System.IO;

namespace NVG.IO.Testing
{
    [TestClass]
    public class NvgReaderTestSuite
    {
        [TestMethod]
        public void TestNvgReadAis()
        {
            var aisFile = @"Data\AIS.nvg";
            Assert.IsTrue(File.Exists(aisFile), @"The NVG file does not exists!");

            var reader = new NvgReader(aisFile);
            var element = reader.ReadNextElement() as NvgElement;
            Assert.IsNotNull(element, @"The NVG element must not be null!");

            Assert.AreEqual(@"0.3", element.Version, @"The NVG element version is not equal to 0.3!");
            Assert.AreEqual(183, element.Children.Count, @"The NVG child element count does not match!");
        }
    }
}
