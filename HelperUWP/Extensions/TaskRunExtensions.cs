using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperUWP.Extensions
{
    public static class TaskRunExtensions
    {
        /// <summary>
        /// To Update the UI use this, await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static async Task ToTaskDeepAsync(Action action)
        {
            await Task.Run(action);
        }
        
        public async static Task<T> ToTaskAsync<T>(Func<T> function)
        {
            Task<T> task = new Task<T>(function);
            task.Start();
            return await task;
        }
        /// <summary>
        /// To Update the UI use this,  await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="function"></param>
        /// <returns></returns>
        public static async Task<T> ToTaskDeepAsync<T>(Func<T> function)
        {
            return await Task.Run(function);
        }

    }
}
