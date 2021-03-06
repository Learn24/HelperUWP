﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace HelperUWP.Component
{
    public class IncrementalLoadingCollection<T> : ObservableCollection<T>, ISupportIncrementalLoading
    {
        private string _continuationToken;
        private readonly Action _errorFunction;
        private readonly Action _loadingFinished;
        private readonly Func<string, Task<PagedResponse<T>>> _loadingFunction;

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="loadDataFunction">The delegate that is used to load data.</param>
        /// <param name="errorFunction">The delegate that is called when an error occurs while loading data.</param>
        /// <param name="loadingFinished">The delegate that is called when loading data has been finished.</param>
        public IncrementalLoadingCollection(Func<string, Task<PagedResponse<T>>> loadDataFunction,
            Action errorFunction, Action loadingFinished = null)
        {
            _loadingFunction = loadDataFunction;
            _errorFunction = errorFunction;
            _loadingFinished = loadingFinished;
        }

        /// <summary>
        /// Determines if more data is available to load.
        /// </summary>
        public bool HasMoreItems { get; private set; } = true;

        /// <summary>
        /// Raised when all data items have been loaded
        /// and added to the list.
        /// </summary>
        public event EventHandler<EventArgs> LoadingFinished; 

        /// <summary>
        /// Initializes incremental loading from the view.
        /// </summary>
        /// <param name="count">The number of items to load.</param>
        /// <returns>
        /// The wrapped results of the load operation.
        /// </returns>
        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            var dispatcher = Window.Current.Dispatcher;

            return
                Task.Run(async () =>
                {
                    try
                    {
                        // Load more data without continuation token
                        var response = await _loadingFunction(_continuationToken);

                        // Add loaded elements to list. This needs to be done
                        // in UI thread.
                        await dispatcher.RunAsync(
                            CoreDispatcherPriority.Normal,
                            () =>
                            {
                                foreach (var item in response.Items)
                                {
                                    Add(item);
                                }
                            });

                        _continuationToken = response.ContinuationToken;

                        // No more loading if continuation token is null.
                        HasMoreItems = _continuationToken != null;

                        await dispatcher.RunAsync(
                            CoreDispatcherPriority.Normal,
                            OnLoadingFinished);

                        return new LoadMoreItemsResult
                        {
                            Count = (uint)response.Items.Count
                        };
                    }
                    catch (Exception)
                    {
                        if (_errorFunction != null)
                        {
                            // Stop re-trying automatically if it fails.
                            // Otherwise we'll get recurring error messages.
                            HasMoreItems = false;

                            _errorFunction();
                        }
                    }

                    return new LoadMoreItemsResult
                    {
                        Count = 0
                    };
                }).AsAsyncOperation();
        }

        private void OnLoadingFinished()
        {
            _loadingFinished?.Invoke();
            LoadingFinished?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Reloads the list with the specified number of elements.
        /// </summary>
        /// <param name="count">The number of elements to load.</param>
        public async Task Refresh(uint count = 10)
        {
            Clear();

            await LoadMoreItemsAsync(count);
        }
    }
}
