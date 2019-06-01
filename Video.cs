﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using Twitch_Downloader_Console;
//
//    var video = Video.FromJson(jsonString);

namespace Twitch_Downloader_Console
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Video
    {
        [JsonProperty("title", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("description")]
        public object Description { get; set; }

        [JsonProperty("description_html")]
        public object DescriptionHtml { get; set; }

        [JsonProperty("broadcast_id", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public long? BroadcastId { get; set; }

        [JsonProperty("broadcast_type", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string BroadcastType { get; set; }

        [JsonProperty("status", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }

        [JsonProperty("language", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Language { get; set; }

        [JsonProperty("tag_list", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string TagList { get; set; }

        [JsonProperty("views", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public long? Views { get; set; }

        [JsonProperty("created_at", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? CreatedAt { get; set; }

        [JsonProperty("published_at", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? PublishedAt { get; set; }

        [JsonProperty("url", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public Uri Url { get; set; }

        [JsonProperty("_id", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("recorded_at", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? RecordedAt { get; set; }

        [JsonProperty("game", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Game { get; set; }

        [JsonProperty("length", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public long? Length { get; set; }

        [JsonProperty("preview", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public Uri Preview { get; set; }

        [JsonProperty("animated_preview_url", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public Uri AnimatedPreviewUrl { get; set; }

        [JsonProperty("thumbnails", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public List<Thumbnail> Thumbnails { get; set; }

        [JsonProperty("fps", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, double> Fps { get; set; }

        [JsonProperty("resolutions", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public Resolutions Resolutions { get; set; }

        [JsonProperty("channel", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public Channel Channel { get; set; }

        [JsonProperty("_links", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public Links Links { get; set; }
    }

    public partial class Channel
    {
        [JsonProperty("name", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("display_name", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string DisplayName { get; set; }
    }

    public partial class Links
    {
        [JsonProperty("self", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public Uri Self { get; set; }

        [JsonProperty("channel", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public Uri Channel { get; set; }
    }

    public partial class Resolutions
    {
        [JsonProperty("160p30", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string The160P30 { get; set; }

        [JsonProperty("360p30", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string The360P30 { get; set; }

        [JsonProperty("480p30", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string The480P30 { get; set; }

        [JsonProperty("720p30", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string The720P30 { get; set; }

        [JsonProperty("720p60", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string The720P60 { get; set; }

        [JsonProperty("chunked", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Chunked { get; set; }
    }

    public partial class Thumbnail
    {
        [JsonProperty("type", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("url", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public Uri Url { get; set; }
    }

    public partial class Video
    {
        public static Video FromJson(string json) => JsonConvert.DeserializeObject<Video>(json, Twitch_Downloader_Console.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Video self) => JsonConvert.SerializeObject(self, Twitch_Downloader_Console.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}