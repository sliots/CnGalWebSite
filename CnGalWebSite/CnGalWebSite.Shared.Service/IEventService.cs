﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CnGalWebSite.Shared.Service
{
    public interface IEventService
    {
        event Action SavaTheme;
        event Action CleanTempEffectTheme;
        event Action TempEffectTheme;
        event Action ToggleMiniMode;
        event Action<string> ChangeTitle;

        void OnSavaTheme();

        void OnCleanTempEffectTheme();

        Task OpenNewPage(string url);

        void OnTempEffectTheme();

        void OnToggleMiniMode();

        void OnChangeTitle(string title);
    }
}