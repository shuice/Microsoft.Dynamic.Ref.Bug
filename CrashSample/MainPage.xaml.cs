using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;

using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using SRC = System.Runtime.CompilerServices;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PythonRange_Crash
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Button_Call_Site_KeyValuePair(object sender, RoutedEventArgs e)
        {            
            
            SRC.CallSite<Func<SRC.CallSite, Object, KeyValuePair<IEnumerator, IDisposable>>> cs
                        = SRC.CallSite<Func<SRC.CallSite, Object, KeyValuePair<IEnumerator, IDisposable>>>.Create(new KeyValuePairCallSiteBinder());
            var r = cs.Target(cs, new object());
            await ShowMessage(r.GetType().FullName);
        }

        
        private async void Button_Call_Site_Dictionary(object sender, RoutedEventArgs e)
        {
            SRC.CallSite<Func<SRC.CallSite, Object, Dictionary<IEnumerator, IDisposable>>> cs
                        = SRC.CallSite<Func<SRC.CallSite, Object, Dictionary<IEnumerator, IDisposable>>>.Create(new DictionaryCallSiteBinder());
            var r = cs.Target(cs, new object());
            await ShowMessage(r.GetType().FullName);
        }

        private async Task ShowMessage(string message)
        {
            MessageDialog dialog = new MessageDialog(message);
            await dialog.ShowAsync();
        }

    }




    #region KeyValuePairCallSiteBinder 
    public class KeyValuePairCallSiteBinder : DynamicMetaObjectBinder
    {
        public override DynamicMetaObject Bind(DynamicMetaObject target, DynamicMetaObject[] args)
        {
            throw new NotImplementedException();
        }

        public override T BindDelegate<T>(SRC.CallSite<T> site, object[] args)
        {
            T t = (T)(object)new Func<SRC.CallSite, object, KeyValuePair<IEnumerator, IDisposable>>(GetListEnumerator);
            return t;
        }

        private KeyValuePair<IEnumerator, IDisposable> GetListEnumerator(SRC.CallSite site, Object value)
        {
            return new KeyValuePair<IEnumerator, IDisposable>(new List<Object> { value }.GetEnumerator(), null);
        }
    }
    #endregion



    #region DictionaryCallSiteBinder 
    public class DictionaryCallSiteBinder : DynamicMetaObjectBinder
    {
        public override DynamicMetaObject Bind(DynamicMetaObject target, DynamicMetaObject[] args)
        {
            throw new NotImplementedException();
        }

        public override T BindDelegate<T>(SRC.CallSite<T> site, object[] args)
        {
            T t = (T)(object)new Func<SRC.CallSite, object, Dictionary<IEnumerator, IDisposable>>(GetListEnumerator);
            return t;
        }

        private Dictionary<IEnumerator, IDisposable> GetListEnumerator(SRC.CallSite site, Object value)
        {
            return new Dictionary<IEnumerator, IDisposable>();
        }
    }
    #endregion
}
