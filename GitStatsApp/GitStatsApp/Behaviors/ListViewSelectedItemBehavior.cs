﻿using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace GitStatsApp
{
    public class ListViewSelectedItemBehavior : Behavior<ListView>
    {
        const string CommandName = "Command";
        const string ConverterName = "Converter";
        const string ClearSelectedItemAfterCommandPropertyName = "ClearSelectedItemAfterCommand";

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(CommandName, typeof(ICommand), typeof(ListViewSelectedItemBehavior), null);
        public static readonly BindableProperty ClearSelectedItemAfterCommandProperty = BindableProperty.Create(ClearSelectedItemAfterCommandPropertyName, typeof(bool), typeof(ListViewSelectedItemBehavior), false);
        public static readonly BindableProperty InputConverterProperty = BindableProperty.Create(ConverterName, typeof(IValueConverter), typeof(ListViewSelectedItemBehavior), null);

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public IValueConverter Converter
        {
            get { return (IValueConverter)GetValue(InputConverterProperty); }
            set { SetValue(InputConverterProperty, value); }
        }

        public bool ClearSelectedItemAfterCommand
        {
            get { return (bool)GetValue(ClearSelectedItemAfterCommandProperty); }
            set { SetValue(ClearSelectedItemAfterCommandProperty, value); }
        }

        public ListView AssociatedObject { get; private set; }
        
        protected override void OnAttachedTo(ListView bindable)
        {
            base.OnAttachedTo(bindable);
            AssociatedObject = bindable;
            bindable.BindingContextChanged += OnBindingContextChanged;
            bindable.ItemSelected += OnListViewItemSelected;
        }

        protected override void OnDetachingFrom(ListView bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.BindingContextChanged -= OnBindingContextChanged;
            bindable.ItemSelected -= OnListViewItemSelected;
            AssociatedObject = null;
        }

        void OnBindingContextChanged(object sender, EventArgs e)
        {
            OnBindingContextChanged();
        }

        void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (Command == null || e.SelectedItem == null)
            {
                return;
            }

            object parameter = Converter.Convert(e, typeof(object), null, null);
            if (Command.CanExecute(parameter))
            {
                Command.Execute(parameter);

                if (ClearSelectedItemAfterCommand)
                {
                    (sender as ListView).SelectedItem = null;
                }
            }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            BindingContext = AssociatedObject.BindingContext;
        }
    }
}