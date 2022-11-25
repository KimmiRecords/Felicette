using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;
using System;

public class NotificationManager : MonoBehaviour
{
    int secondsToFullStamina = 0;

    private void Start()
    {
        AndroidNotificationCenter.CancelAllDisplayedNotifications();
        AndroidNotificationCenter.CancelAllScheduledNotifications();
        PrepareNotification();

        //UpdateTimeToFullStamina();

        //var notifChannel = new AndroidNotificationChannel()
        //{
        //    Id = "reminder_notif",
        //    Name = "Reminder Notification",
        //    Description = "Channel for Reminders Notifications",
        //    Importance = Importance.High
        //};

        //AndroidNotificationCenter.RegisterNotificationChannel(notifChannel);

        //var notification = new AndroidNotification();
        //notification.Title = "STAMINA LLENA";
        //notification.Text = "Ya podés volver a jugar niveles!";
        //notification.SmallIcon = "icon_0";
        //notification.LargeIcon = "icon_1";
        //notification.FireTime = DateTime.Now.AddSeconds(secondsToFullStamina);

        //AndroidNotificationCenter.SendNotification(notification, "reminder_notif");
        //print("mando la notif");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Home))
        {
            PrepareNotification();
        }
    }

    public void UpdateTimeToFullStamina()
    {
        secondsToFullStamina = (LevelManager.instance.maxStamina - LevelManager.instance.Stamina) * LevelManager.instance.myStaminaSystem.timeToRecharge;
    }

    public void PrepareNotification()
    {
        UpdateTimeToFullStamina();
        CreateChannel();
        CreateNotification();
    }

    public void CreateChannel()
    {
        var notifChannel = new AndroidNotificationChannel()
        {
            Id = "reminder_notif",
            Name = "Reminder Notification",
            Description = "Channel for Reminders Notifications",
            Importance = Importance.High
        };

        AndroidNotificationCenter.RegisterNotificationChannel(notifChannel);
    }

    public void CreateNotification()
    {
        var notification = new AndroidNotification();
        notification.Title = "STAMINA LLENA";
        notification.Text = "Ya podés volver a jugar niveles!";
        notification.SmallIcon = "icon_0";
        notification.LargeIcon = "icon_1";
        notification.FireTime = DateTime.Now.AddSeconds(secondsToFullStamina);

        AndroidNotificationCenter.SendNotification(notification, "reminder_notif");
        //print("mando la notif");
    }



}
