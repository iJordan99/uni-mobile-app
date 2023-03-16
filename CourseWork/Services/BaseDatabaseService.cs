using System;
using CourseWork.Interfaces;
using CourseWork.Models;
using SQLite;

namespace CourseWork.Services
{
	public class BaseDatabaseService
	{
        protected readonly SQLiteAsyncConnection _database;

        public BaseDatabaseService(SQLiteAsyncConnection _database)
        {
            this._database = _database;
            _database.CreateTableAsync<User>();
            _database.CreateTableAsync<Metric>();
            _database.CreateTableAsync<Models.Program>();
            _database.CreateTableAsync<ProgramExercise>();
        }
    }
}

