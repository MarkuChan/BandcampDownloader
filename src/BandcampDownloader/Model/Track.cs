﻿using System;

namespace BandcampDownloader {

    internal class Track {

        /// <summary>
        /// The track album.
        /// </summary>
        public Album Album { get; set; }

        /// <summary>
        /// The track length (in seconds).
        /// </summary>
        public Double Duration { get; set; }

        /// <summary>
        /// The track lyrics.
        /// </summary>
        public String Lyrics { get; set; }

        /// <summary>
        /// The URL where the track should be downloaded from.
        /// </summary>
        public String Mp3Url { get; set; }

        /// <summary>
        /// The track number.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// The local path (full path with file name) where the track file should be saved.
        /// </summary>
        public String Path { get; private set; }

        /// <summary>
        /// The track title.
        /// </summary>
        public String Title { get; set; }

        /// <summary>
        /// Initializes a new Track.
        /// </summary>
        /// <param name="album">The track album.</param>
        public Track(Album album) {
            Album = album;
        }

        /// <summary>
        /// Sets the Path property of the current Track.
        /// </summary>
        public void SetPath() {
            String fileName = ParseTrackFileName();

            Path = Album.Path + "\\" + fileName;
            if (Path.Length >= 260) {
                // Windows doesn't do well with path + filename >= 260 characters (and path >= 248 characters)
                // album.Path has been shorten to 247 characters before, so we have 12 characters max left for filename.ext
                int fileNameMaxLength = 12 - System.IO.Path.GetExtension(Path).Length;
                Path = Album.Path + "\\" + fileName.Substring(0, fileNameMaxLength) + System.IO.Path.GetExtension(Path);
            }
        }

        /// <summary>
        /// Returns the file name to be used for the track from the file name format saved in the UserSettings, by
        /// replacing the placeholders strings with their corresponding values. The returned file name DOES contain the extension.
        /// </summary>
        private String ParseTrackFileName() {
            String fileName = App.UserSettings.FileNameFormat
                .Replace("{year}", Album.ReleaseDate.Year.ToString())
                .Replace("{month}", Album.ReleaseDate.Month.ToString("00"))
                .Replace("{day}", Album.ReleaseDate.Day.ToString("00"))
                .Replace("{album}", Album.Title)
                .Replace("{artist}", Album.Artist)
                .Replace("{title}", Title)
                .Replace("{tracknum}", Number.ToString("00"));
            return fileName.ToAllowedFileName();
        }
    }
}