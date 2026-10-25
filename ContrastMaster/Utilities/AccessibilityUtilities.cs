using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;

namespace ContrastMaster.Utilities;

public static class AccessibilityUtilities
{
    public static void AnnounceMostRecent(UIElement element, string notificationIdentifier, string text)
    {
        var peer = UIElementAutomationPeer.FromElement(element) ?? UIElementAutomationPeer.CreatePeerForElement(element);

        peer?.RaiseNotificationEvent(AutomationNotificationKind.ActionCompleted, AutomationNotificationProcessing.MostRecent, text, notificationIdentifier);
    }
}
