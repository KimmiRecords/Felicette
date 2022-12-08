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
        print("cancelo todas las notis");
        AndroidNotificationCenter.CancelAllDisplayedNotifications();
        AndroidNotificationCenter.CancelAllScheduledNotifications();
        //PrepareNotification();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Home))
        {
            print("disparo la notificacion");
            PrepareNotification();
        }
    }

    public void UpdateTimeToFullStamina()
    {
        secondsToFullStamina = (LevelManager.instance.maxStamina - LevelManager.instance.Stamina) * LevelManager.instance.myStaminaSystem.timeToRecharge;
    }

    public void PrepareNotification()
    {
        print("preparo la notificacion");

        UpdateTimeToFullStamina();
        CreateChannel();
        CreateNotification();
    }

    public void CreateChannel()
    {

        print("creo el canal");

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
        print("creo y mando la notificacion");

        var notification = new AndroidNotification();
        notification.Title = "ATÚN LLENO";
        notification.Text = "Ya podés volver a jugar!";
        notification.SmallIcon = "icon_0";
        notification.LargeIcon = "icon_1";
        notification.FireTime = DateTime.Now.AddSeconds(secondsToFullStamina);

        AndroidNotificationCenter.SendNotification(notification, "reminder_notif");
        //print("mando la notif");
    }



}
