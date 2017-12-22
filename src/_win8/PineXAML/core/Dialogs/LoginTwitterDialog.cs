﻿using PineShap.FacebookUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PineShap.Tools;
using PineShap.TwitterUtils;
////using Hammock.Authentication.OAuth;
////using Hammock.Web;
using System.IO;
////using Hammock;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;

namespace PineShap.Dialogs {
    public class LoginTwitterDialogCompleteArgs : EventArgs {
        public bool IsSuccess = false;
        public string Token = "";
        public string TokenSecret = "";
        public LoginTwitterDialogCompleteArgs(bool success, string token, string tokensecret) {
            IsSuccess = success;
            Token = token;
            TokenSecret = tokensecret;
        }
    }
    public delegate void DelegateLoginTwitterDialogComplete(object sender, LoginTwitterDialogCompleteArgs e);
    class LoginTwitterDialog : DialogBase
    {
        public event DelegateLoginTwitterDialogComplete OnComplete;
        Popup _popup = null;

        override public Popup Dialog { get { return _popup; } }
        ////WebBrowser _browser;
        public LoginTwitterDialog() {
            _popup = new Popup();
            Border border_root = new Border();
            border_root.Margin = new Thickness(0, 0, 0, 0);
            border_root.Width = 800;//// Application.Current.Host.Content.ActualHeight;
            border_root.Height = 480;//// Application.Current.Host.Content.ActualWidth;

            Grid grid_root = new Grid();
            //grid_root.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });
            grid_root.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });

            Border border_content = new Border();
            border_content.VerticalAlignment = VerticalAlignment.Stretch;
            border_content.HorizontalAlignment = HorizontalAlignment.Stretch;
            border_content.Margin = new Thickness(15, 15, 15, 15);

            StackPanel panel_content = new StackPanel();
            panel_content.VerticalAlignment = VerticalAlignment.Stretch;
            panel_content.HorizontalAlignment = HorizontalAlignment.Stretch;
            TextBlock title = new TextBlock();
            title.Height = 25;
            title.Text = "Login to Facebook";
            //panel_content.Children.Add(title);
            ////_browser = new WebBrowser();
            ////_browser.Navigated += _browser_Navigated;
            ////_browser.NavigationFailed += _browser_NavigationFailed;
            ////_browser.Navigating += _browser_Navigating;
            ////_browser.VerticalAlignment = VerticalAlignment.Stretch;
            ////_browser.HorizontalAlignment = HorizontalAlignment.Stretch;
            ////_browser.Height = 470;
            //_browser.Width = 800;

            ////panel_content.Children.Add(_browser);

            border_content.Child = panel_content;
            grid_root.Children.Add(border_content);
            border_root.Child = grid_root;
            _popup.Child = border_root;
            //
        }

        /*void _browser_Navigating(object sender, NavigatingEventArgs e) {
            if (e.Uri.ToString().StartsWith(AppSettings.CallbackUri)) {
                //MessageBox.Show("uri:" + e.Uri.ToString());
                var AuthorizeResult = KeyValuePairUtils.GetQueryParameters(e.Uri.ToString());
                try {
                    var VerifyPin = AuthorizeResult["oauth_verifier"];
                    //MessageBox.Show(VerifyPin);
                    if (AuthorizeResult != null && VerifyPin != null) {
                        var AccessTokenQuery = OAuthUtil.GetAccessTokenQuery(TwitterClient.OAuthTokenKey, TwitterClient.TokenSecret, VerifyPin);
                        AccessTokenQuery.QueryResponse += new EventHandler<WebQueryResponseEventArgs>(AccessTokenQuery_QueryResponse);
                        AccessTokenQuery.RequestAsync(AppSettings.AccessTokenUri, null);
                    }
                } catch (Exception ex) {

                }
            }
        }
        void AccessTokenQuery_QueryResponse(object sender, WebQueryResponseEventArgs e) {
            try {
                Deployment.Current.Dispatcher.BeginInvoke(() => {
                    StreamReader reader = new StreamReader(e.Response);
                    string strResponse = reader.ReadToEnd();
                    var parameters = KeyValuePairUtils.GetQueryParameters(strResponse);
                    TwitterClient.AccessToken = parameters["oauth_token"];
                    TwitterClient.AccessTokenSecret = parameters["oauth_token_secret"];
                    TwitterClient.UserID = parameters["user_id"];
                    TwitterClient.UserScreenName = parameters["screen_name"];

                    KeyValuePairUtils.SetKeyValue<string>(TwitterClient.KeyAcessToken, TwitterClient.AccessToken);
                    KeyValuePairUtils.SetKeyValue<string>(TwitterClient.KeyAcessTokenSecret, TwitterClient.AccessTokenSecret);
                    KeyValuePairUtils.SetKeyValue<string>(TwitterClient.KeyUserName, TwitterClient.UserScreenName);
                    KeyValuePairUtils.SetKeyValue<string>(TwitterClient.KeyUserID, TwitterClient.UserID);
                    success(parameters["oauth_token"], parameters["oauth_token_secret"]);
                });
            } catch (Exception ex) {
                fail();
            }
        }
        void _browser_NavigationFailed(object sender, System.Windows.Navigation.NavigationFailedEventArgs e) {
            fail();
        }

        void _browser_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e) {
            try {
                _browser.Navigated -= _browser_Navigated;
            } catch (Exception ex) {
            }
        }
        */
        public void show(float x, float y){
            /*_popup.IsOpen = true;
            try {
                _browser.ClearCookiesAsync();
                var requestTokenQuery = OAuthUtil.GetRequestTokenQuery();
                requestTokenQuery.RequestAsync(AppSettings.RequestTokenUri, null);
                requestTokenQuery.QueryResponse += new EventHandler<WebQueryResponseEventArgs>(requestTokenQuery_QueryResponse);
            } catch (Exception ex) {
                fail();
            }*/
        }
        /*void requestTokenQuery_QueryResponse(object sender, WebQueryResponseEventArgs e) {
            Deployment.Current.Dispatcher.BeginInvoke(() => {
                try {
                
                
                    StreamReader reader = new StreamReader(e.Response);
                    string strResponse = reader.ReadToEnd();
                    var parameters = KeyValuePairUtils.GetQueryParameters(strResponse);
                    TwitterClient.OAuthTokenKey = parameters["oauth_token"];
                    TwitterClient.TokenSecret = parameters["oauth_token_secret"];
                    var authorizeUrl = AppSettings.AuthorizeUri + "?oauth_token=" + TwitterClient.OAuthTokenKey;
                    //Deployment.Current.Dispatcher.BeginInvoke(() => {
                    _browser.Source = new Uri(authorizeUrl, UriKind.RelativeOrAbsolute);
                    // });
            

                
                } catch (Exception ex) {
                    fail();
                }
            });
        }*/
        public void cancel() {
            fail();
        }
        private void success(string token, string tokenex) {
            _popup.IsOpen = false;
            if (OnComplete != null) OnComplete(this, new LoginTwitterDialogCompleteArgs(true, token, tokenex));
        }
        private void fail() {
           // Deployment.Current.Dispatcher.BeginInvoke(()=>
            _popup.IsOpen = false;
           // );
            if (OnComplete != null) {
                OnComplete(this, new LoginTwitterDialogCompleteArgs(false, "", ""));
            }
        }
    }
}