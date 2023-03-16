using System;
using CourseWork.Interfaces;
using CourseWork.Models;
using SQLite;

namespace CourseWork.Services
{
	public class BaseDatabaseService
	{
        protected readonly SQLiteAsyncConnection Database;

        protected BaseDatabaseService(SQLiteAsyncConnection database)
        {
            this.Database = database;
            database.CreateTableAsync<User>();
            database.CreateTableAsync<Metric>();
            database.CreateTableAsync<Models.Program>();
            database.CreateTableAsync<ProgramExercise>();
            database.CreateTableAsync<WorkoutSession>();
            database.CreateTableAsync<WorkoutSessionExercise>();
        }
    }
}

