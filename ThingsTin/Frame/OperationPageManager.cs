using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using ThingsTin.Exceptions;
using ThingsTin.Interfaces.Container;
using ThingsTin.ViewModels;

namespace ThingsTin.Frame
{
    public class OperationPageManager:IPageManager
    {
        private ThingsTinViewModel _viewModel;
        private List<OpenedPage> _pages;
        private IThingsTin _thingsTin;

        public OperationPageManager(IThingsTin thingsTin)
        {
            _pages = new List<OpenedPage>();
            _thingsTin = thingsTin;
        }

        public void SetViewModel(ThingsTinViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void OpenStartPage(UIElement control)
        {
            _viewModel.BackgroundPage = control;
        }

        public void OpenPage(IOperationPage page, object paramter=null)
        {
            try
            {
                var existPage = _pages.FirstOrDefault(p => p.Page.PageId == page.PageId);
                if (existPage != null)
                {
                    _viewModel.CurrentPage = existPage.Model;
                    existPage.Model.Focus();
                }
                else
                {
                    var model = _viewModel.AddPage(page.PageId, page.PageTitle, page.PageContent, new SimpleCommand(CloseOpendPage));
                    _pages.Add(new OpenedPage { Page = page, Model = model });
                    _thingsTin.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        page.Initialize(paramter);
                        model.Focus();
                    }));
                }
            }
            catch (Exception ex)
            {
                var method=MethodInfo.GetCurrentMethod();
                ThingsTinExceptionProcesser.Instance.ProcessException(method.DeclaringType.Name, method.Name, ex);
            }
        }

        public void ClosePage(Guid pageId)
        {
            try
            {
                CloseOpendPage(pageId);
            }
            catch (Exception ex)
            {
                var method = MethodInfo.GetCurrentMethod();
                ThingsTinExceptionProcesser.Instance.ProcessException(method.DeclaringType.Name, method.Name, ex);
            }
        }

        public void CloseAllPages(bool force)
        {
            int count = PagesCount;
            for (int i = 0; i < count; i++)
            {
                var resullt = CloseOpendPage(_pages[0].Page.PageId, force);
                if(!resullt)
                {
                    break;
                }
            }
        }

        public int PagesCount
        {
            get
            {
                return _pages.Count;
            }
        }

        private void CloseOpendPage(object pageId)
        {
            CloseOpendPage(pageId, false);
        }

        private bool CloseOpendPage(object pageId, bool force)
        {
            var existPage = _pages.FirstOrDefault(p => p.Page.PageId.ToString() == pageId.ToString());
            if (existPage != null)
            {
                var pageEvent = new PageEvent(existPage.Page.PageId) { IsForce = force };
                existPage.Page.OnPageClosing(pageEvent);

                if (!pageEvent.IsCanceled || force)
                {
                    _pages.Remove(existPage);
                    _viewModel.RemovePage(existPage.Model);
                    existPage.Page.OnPageClosed();
                    return true;
                }

                return false;
            }
            else
            {
                return false;
            }
        }
    }
}
