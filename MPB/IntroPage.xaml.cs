﻿using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MPB
{
    public partial class IntroPage : ContentPage
    {
        public IntroPage()
        {
            InitializeComponent();
        }

        private void GoToLogin(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginPage());
        }

        private void GoToSignup(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignUpPage());
        }
    }
}