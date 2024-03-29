﻿using System;
using UnityEngine;

public class Config
{
    public static readonly string APP_VERSION = Application.version;

    public const string AES_KEY = "23856df2220fea61a0d50e1e8304c555";

    public const string ASSET_BUNDLE_URL = "https://localhost:7212/AssetBundles";

#if DEV
    public const ENVIRONMENT_TYPE E_ENVIRONMENT_TYPE = ENVIRONMENT_TYPE.Dev;
#elif STAGE
    public const ENVIRONMENT_TYPE E_ENVIRONMENT_TYPE = ENVIRONMENT_TYPE.Stage;
#elif LIVE
    public const ENVIRONMENT_TYPE E_ENVIRONMENT_TYPE = ENVIRONMENT_TYPE.Live;
#elif LIVE_TEST
    public const ENVIRONMENT_TYPE E_ENVIRONMENT_TYPE = ENVIRONMENT_TYPE.LiveTest;
#else
    public const ENVIRONMENT_TYPE E_ENVIRONMENT_TYPE = ENVIRONMENT_TYPE.Dev;
#endif

#if DEV
    public const string SERVER_APP_CONFIG_URL = "https://localhost:7212/";
#elif STAGE
    public const string SERVER_APP_CONFIG_URL = "https://localhost:7212/";
#elif LIVE
    public const string SERVER_APP_CONFIG_URL = "https://localhost:7212/";
#else
    public const string SERVER_APP_CONFIG_URL = "https://localhost:7212/";
#endif

#if UNITY_ANDROID
    public const OS_TYPE E_OS_TYPE = OS_TYPE.Android;
#elif UNITY_IOS
    public const OS_TYPE E_OS_TYPE = OS_TYPE.iOS;
#else
    public const OS_TYPE E_OS_TYPE = OS_TYPE.Windows;
#endif
}
