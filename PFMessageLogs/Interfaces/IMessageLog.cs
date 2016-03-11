using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PFMessageLogs.Interfaces
{
    /// <summary>
    /// Interface for creating MessageLog classes.
    /// </summary>
    public interface IMessageLog
    {

#pragma warning disable 1591
        bool AllowFileErase { get; set; }
        string Caption { get; set; }
        void Clear();
        void CloseWindow();
        void Focus();
        bool FormIsVisible { get; }
        void HideWindow();
        void LoadFile(string filename);
        bool RetainFocus { get; set; }
        void SaveFile(string filename);
        bool ShowDatetime { get; set; }
        void ShowWindow();
        void WriteLine(string message);
#pragma warning restore 1591

    }
}
