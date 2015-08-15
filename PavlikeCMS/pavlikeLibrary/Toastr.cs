﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace pavlikeLibrary
{
    [Serializable]
    public class ToastMessage
    {
    
        public string Title { get; set; }
        public string Message { get; set; }
        public Enum.ToastrType ToastType { get; set; }
        public bool IsSticky { get; set; }
    }
    [Serializable]
    public class Toastr
    {
        public bool ShowNewestOnTop { get; set; }
        public bool ShowCloseButton { get; set; }
        public List<ToastMessage> ToastMessages { get; set; }

        public ToastMessage AddToastMessage(string title, string message, Enum.ToastrType toastType)
        {
            var toast = new ToastMessage()
            {
                Title = title,
                Message = message,
                ToastType = toastType
            };
            ToastMessages.Add(toast);
            return toast;
        }

        public Toastr()
        {
            ToastMessages = new List<ToastMessage>();
            ShowNewestOnTop = false;
            ShowCloseButton = false;
        }

    }

    public static class ControllerExtensions
    {
        public static ToastMessage AddToastMessage(this Controller controller, string title, string message, Enum.ToastrType toastType = Enum.ToastrType.Info)
        {
            var toastr = controller.TempData["Toastr"] as Toastr;
            toastr = toastr ?? new Toastr();

            var toastMessage = toastr.AddToastMessage(title, message, toastType);
            controller.TempData["Toastr"] = toastr;
            return toastMessage;
        }
    }
}
