﻿using System;
namespace CourseWork
{
	public class Constants
	{
        public const string DatabaseFilename = "DEVCourseWorkDB.db3";
        public static string DatabasePath => Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);

        public const SQLite.SQLiteOpenFlags Flags =
             // open the database in read/write mode
             SQLite.SQLiteOpenFlags.ReadWrite |
             // create the database if it doesn't exist
             SQLite.SQLiteOpenFlags.Create |
             // enable multi-threaded database access
             SQLite.SQLiteOpenFlags.SharedCache;
    }
}

