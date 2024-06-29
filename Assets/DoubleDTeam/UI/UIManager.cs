using System;
using System.Collections.Generic;
using DoubleDTeam.UI.Base;
using UnityEngine;

namespace DoubleDTeam.UI
{
    public class UIManager : IUIManager
    {
        private readonly Dictionary<Type, IPage> _pages = new();

        public event Action<IPage> PageOpened;

        public event Action<IPage> PageClosed;

        public bool ContainsPage<TPage>() where TPage : class, IPage
        {
            var type = typeof(TPage);

            return _pages.ContainsKey(type);
        }

        public bool ContainsPage(IPage page)
        {
            foreach (var (_, registerPage) in _pages)
            {
                if (page == registerPage)
                    return true;
            }

            return false;
        }

        public void RegisterPage<TPage>(TPage page) where TPage : class, IPage
        {
            var type = typeof(TPage);

            if (ContainsPage<TPage>())
                throw new InvalidOperationException($"Attempt to register an existing page {type}");

            _pages.Add(type, page);

            page.Initialize();
        }

        public void RegisterPageByType(IPage page)
        {
            var type = page.GetType();

            if (ContainsPage(page))
                throw new InvalidOperationException($"Attempt to register an existing page {type}");

            _pages.Add(type, page);

            page.Initialize();
        }

        public TPage GetPage<TPage>() where TPage : class, IPage
        {
            var type = typeof(TPage);

            if (ContainsPage<TPage>() == false)
                throw new InvalidOperationException($"Attempt to access an unregistered page {type}");

            return _pages[type] as TPage;
        }

        public void RemovePage<TPage>() where TPage : class, IPage
        {
            _pages.Remove(typeof(TPage));
        }

        public void RemovePage(IPage page)
        {
            var killMarks = new List<Type>();

            foreach (var (type, registerModule) in _pages)
            {
                if (registerModule == page)
                    killMarks.Add(type);
            }

            foreach (var killMark in killMarks)
                _pages.Remove(killMark);
        }

        public void OpenPage<TPage>() where TPage : class, IUIPage
        {
            if (ContainsPage<TPage>() == false)
            {
                Debug.LogError($"Page {typeof(TPage).Name} not show. Unregistered page");
                return;
            }

            var page = GetPage<TPage>();

            page.Open();
        }

        public void OpenPage<TPage, TPayload>(TPayload context) where TPage : class, IPayloadPage<TPayload>
        {
            if (ContainsPage<TPage>() == false)
            {
                Debug.LogError($"Page {typeof(TPage).Name} not show. Unregistered page");
                return;
            }

            var page = GetPage<TPage>();

            page.Open(context);
        }

        public void ClosePage<TPage>() where TPage : class, IPage
        {
            if (ContainsPage<TPage>() == false)
            {
                Debug.LogError($"Page {typeof(TPage).Name} not close. Unregistered page");
                return;
            }

            var page = GetPage<TPage>();

            page.Close();
        }

        public void ResetPages()
        {
            foreach (var pages in _pages.Values)
                pages.Reset();
        }

        public void Clear()
        {
            _pages.Clear();
        }
    }
}