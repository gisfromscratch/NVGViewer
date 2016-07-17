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
using NVG.IO;
using NVGViewer.ViewModel;
using System;
using System.IO;
using System.Windows.Input;

namespace NVGViewer.Commands
{
    internal class LoadNvgFileCommand : ICommand
    {
        private readonly MainViewModel _viewModel;

        /// <summary>
        /// Creates a new load NVG command using the specified view model.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        internal LoadNvgFileCommand(MainViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Determines whether or not this command can be executed.
        /// </summary>
        /// <param name="filePath">The path to the NVG file.</param>
        /// <returns><c>true</c> if the NVG file can be accessed.</returns>
        public bool CanExecute(object filePath)
        {
            var filePathAsString = filePath as string;
            if (null == filePathAsString)
            {
                return false;
            }

            return File.Exists(filePathAsString);
        }

        /// <summary>
        /// Executes reading the specified NVG file.
        /// </summary>
        /// <param name="filePath">The path to the NVG file.</param>
        public void Execute(object filePath)
        {
            var filePathAsString = filePath as string;
            if (null == filePathAsString || !CanExecute(filePath))
            {
                // TODO: Show message dialog to user!
                return;
            }

            using (var nvgReader = new NvgReader(filePathAsString))
            {
                NvgElement nvgElement;
                while (null != (nvgElement = nvgReader.ReadNextElement()))
                {
                    foreach(var nvgHyperLinkElement in nvgElement.HyperlinkElements)
                    {
                        foreach(var nvgPointElement in nvgHyperLinkElement.PointElements)
                        {
                            if (!nvgPointElement.IsEmpty)
                            {
                                // TODO: Create a map graphic
                            }
                        }
                    }
                }
                
                // TODO: Create map graphics using the NVG geometries!
            }
        }
    }
}
