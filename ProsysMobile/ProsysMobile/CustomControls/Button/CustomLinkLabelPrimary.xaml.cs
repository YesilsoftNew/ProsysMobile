﻿using System;
using ProsysMobile.Helper;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProsysMobile.CustomControls.Button
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomLinkLabelPrimary : Grid
    {
        /// <summary>
        /// Text Property
        /// </summary>
        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(CustomLinkLabelPrimary),
            default(string),
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// Text Property
        /// </summary>
        public static readonly BindableProperty SourceProperty = BindableProperty.Create(
            nameof(Source),
            typeof(string),
            typeof(CustomLinkLabelPrimary),
            "ArrowUpRightBlack",
            Xamarin.Forms.BindingMode.OneWay);

        /// <summary>
        /// Text
        /// </summary>
        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }

            set
            {
                SetValue(TextProperty, value);
            }
        }

        /// <summary>
        /// Source
        /// </summary>
        public string Source
        {
            get
            {
                return (string)GetValue(SourceProperty);
            }

            set
            {
                SetValue(SourceProperty, value);
            }
        }

        public CustomLinkLabelPrimary()
        {
            InitializeComponent();

            ItemLabel.Text = Text;
            ItemImage.Source = Source;
            ItemGrid.GestureRecognizers.Add(new TapGestureRecognizer(itemGrid_Click));
        }

        /// <summary>
        /// Click Event
        /// </summary>
        public event EventHandler<EventArgs> Clicked;

        /// <summary>
        /// On change property
        /// </summary>
        /// <param name="propertyName"></param>
        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == TextProperty.PropertyName)
            {
                ItemLabel.Text = Text;
            }
            else if (propertyName == SourceProperty.PropertyName)
            {
                ItemImage.Source = Source;
            }
        }

        /// <summary>
        /// Main Grid Clicked
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        private async void itemGrid_Click(View arg1, object arg2)
        {
            try
            {
                await ItemGrid.ScaleTo(.8, 100, Easing.CubicIn);

                if (Clicked != null)
                {
                    Clicked(arg2, null);
                }

                await ItemGrid.ScaleTo(1, 100, Easing.CubicOut);

            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);

                return;
            }
        }
    }
}