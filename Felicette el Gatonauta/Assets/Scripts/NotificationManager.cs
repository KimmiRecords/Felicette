using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;
using System;

public class NotificationManager : MonoBehaviour
{

    private void Start()
    {
        AndroidNotificationCenter.CancelAllDisplayedNotifications();
        AndroidNotificationCenter.CancelAllScheduledNotifications();

        var notifChannel = new AndroidNotificationChannel()
        {
            Id = "reminder_notif",
            Name = "Reminder Notification",
            Description = "Channel for Reminders Notifications",
            Importance = Importance.High
        };

        AndroidNotificationCenter.RegisterNotificationChannel(notifChannel);

        var notification = new AndroidNotification();
        notification.Title = "Volvé a jugar!";
        notification.Text = "Porque mi juego está re bueno.";
        notification.SmallIcon = "icon_reminder";
        notification.LargeIcon = "icon_reminder";
        notification.FireTime = DateTime.Now.AddSeconds(15);

        AndroidNotificationCenter.SendNotification(notification, "reminder_notif");
    }
}
