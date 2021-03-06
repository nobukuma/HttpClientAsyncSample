﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AsyncAccess
{

    public class Metadata
    {

        [JsonProperty("result_type")]
        public string ResultType;

        [JsonProperty("iso_language_code")]
        public string IsoLanguageCode;
    }

    public class Url2
    {

        [JsonProperty("url")]
        public string Url;

        [JsonProperty("expanded_url")]
        public string ExpandedUrl;

        [JsonProperty("display_url")]
        public string DisplayUrl;

        [JsonProperty("indices")]
        public int[] Indices;
    }

    public class Url
    {

        [JsonProperty("urls")]
        public Url2[] Urls;
    }

    public class Description
    {

        [JsonProperty("urls")]
        public object[] Urls;
    }

    public class Entities
    {

        [JsonProperty("url")]
        public Url Url;

        [JsonProperty("description")]
        public Description Description;
    }

    public class User
    {

        [JsonProperty("id")]
        public int Id;

        [JsonProperty("id_str")]
        public string IdStr;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("screen_name")]
        public string ScreenName;

        [JsonProperty("location")]
        public string Location;

        [JsonProperty("description")]
        public string Description;

        [JsonProperty("url")]
        public string Url;

        [JsonProperty("entities")]
        public Entities Entities;

        [JsonProperty("protected")]
        public bool Protected;

        [JsonProperty("followers_count")]
        public int FollowersCount;

        [JsonProperty("friends_count")]
        public int FriendsCount;

        [JsonProperty("listed_count")]
        public int ListedCount;

        [JsonProperty("created_at")]
        public string CreatedAt;

        [JsonProperty("favourites_count")]
        public int FavouritesCount;

        [JsonProperty("utc_offset")]
        public int UtcOffset;

        [JsonProperty("time_zone")]
        public string TimeZone;

        [JsonProperty("geo_enabled")]
        public bool GeoEnabled;

        [JsonProperty("verified")]
        public bool Verified;

        [JsonProperty("statuses_count")]
        public int StatusesCount;

        [JsonProperty("lang")]
        public string Lang;

        [JsonProperty("contributors_enabled")]
        public bool ContributorsEnabled;

        [JsonProperty("is_translator")]
        public bool IsTranslator;

        [JsonProperty("is_translation_enabled")]
        public bool IsTranslationEnabled;

        [JsonProperty("profile_background_color")]
        public string ProfileBackgroundColor;

        [JsonProperty("profile_background_image_url")]
        public string ProfileBackgroundImageUrl;

        [JsonProperty("profile_background_image_url_https")]
        public string ProfileBackgroundImageUrlHttps;

        [JsonProperty("profile_background_tile")]
        public bool ProfileBackgroundTile;

        [JsonProperty("profile_image_url")]
        public string ProfileImageUrl;

        [JsonProperty("profile_image_url_https")]
        public string ProfileImageUrlHttps;

        [JsonProperty("profile_banner_url")]
        public string ProfileBannerUrl;

        [JsonProperty("profile_link_color")]
        public string ProfileLinkColor;

        [JsonProperty("profile_sidebar_border_color")]
        public string ProfileSidebarBorderColor;

        [JsonProperty("profile_sidebar_fill_color")]
        public string ProfileSidebarFillColor;

        [JsonProperty("profile_text_color")]
        public string ProfileTextColor;

        [JsonProperty("profile_use_background_image")]
        public bool ProfileUseBackgroundImage;

        [JsonProperty("default_profile")]
        public bool DefaultProfile;

        [JsonProperty("default_profile_image")]
        public bool DefaultProfileImage;

        [JsonProperty("following")]
        public object Following;

        [JsonProperty("follow_request_sent")]
        public object FollowRequestSent;

        [JsonProperty("notifications")]
        public object Notifications;
    }

    public class Hashtag
    {

        [JsonProperty("text")]
        public string Text;

        [JsonProperty("indices")]
        public int[] Indices;
    }

    public class Url3
    {

        [JsonProperty("url")]
        public string Url;

        [JsonProperty("expanded_url")]
        public string ExpandedUrl;

        [JsonProperty("display_url")]
        public string DisplayUrl;

        [JsonProperty("indices")]
        public int[] Indices;
    }

    public class Entities2
    {

        [JsonProperty("hashtags")]
        public Hashtag[] Hashtags;

        [JsonProperty("symbols")]
        public object[] Symbols;

        [JsonProperty("urls")]
        public Url3[] Urls;

        [JsonProperty("user_mentions")]
        public object[] UserMentions;
    }

    public class Status
    {

        [JsonProperty("metadata")]
        public Metadata Metadata;

        [JsonProperty("created_at")]
        public string CreatedAt;

        [JsonProperty("id")]
        public long Id;

        [JsonProperty("id_str")]
        public string IdStr;

        [JsonProperty("text")]
        public string Text;

        [JsonProperty("source")]
        public string Source;

        [JsonProperty("truncated")]
        public bool Truncated;

        [JsonProperty("in_reply_to_status_id")]
        public object InReplyToStatusId;

        [JsonProperty("in_reply_to_status_id_str")]
        public object InReplyToStatusIdStr;

        [JsonProperty("in_reply_to_user_id")]
        public object InReplyToUserId;

        [JsonProperty("in_reply_to_user_id_str")]
        public object InReplyToUserIdStr;

        [JsonProperty("in_reply_to_screen_name")]
        public object InReplyToScreenName;

        [JsonProperty("user")]
        public User User;

        [JsonProperty("geo")]
        public object Geo;

        [JsonProperty("coordinates")]
        public object Coordinates;

        [JsonProperty("place")]
        public object Place;

        [JsonProperty("contributors")]
        public object Contributors;

        [JsonProperty("retweet_count")]
        public int RetweetCount;

        [JsonProperty("favorite_count")]
        public int FavoriteCount;

        [JsonProperty("entities")]
        public Entities2 Entities;

        [JsonProperty("favorited")]
        public bool Favorited;

        [JsonProperty("retweeted")]
        public bool Retweeted;

        [JsonProperty("possibly_sensitive")]
        public bool PossiblySensitive;

        [JsonProperty("lang")]
        public string Lang;
    }

    public class SearchMetadata
    {

        [JsonProperty("completed_in")]
        public double CompletedIn;

        [JsonProperty("max_id")]
        public long MaxId;

        [JsonProperty("max_id_str")]
        public string MaxIdStr;

        [JsonProperty("query")]
        public string Query;

        [JsonProperty("refresh_url")]
        public string RefreshUrl;

        [JsonProperty("count")]
        public int Count;

        [JsonProperty("since_id")]
        public int SinceId;

        [JsonProperty("since_id_str")]
        public string SinceIdStr;
    }

    public class SearchTweetsResponse
    {

        [JsonProperty("statuses")]
        public Status[] Statuses;

        [JsonProperty("search_metadata")]
        public SearchMetadata SearchMetadata;
    }

}
