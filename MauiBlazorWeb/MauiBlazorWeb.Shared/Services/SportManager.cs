using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiBlazorWeb.Shared.Components.Pages;
using MauiBlazorWeb.Shared.Interfaces;
using MauiBlazorWeb.Shared.Models;
using MauiBlazorWeb.Shared.Models.Sports;

namespace MauiBlazorWeb.Shared.Services
{
	public class SportManager : ISportManager
	{
		public async Task<bool> InsertSport(Sport newSport)
		{
			IDataAccess _data = new DataAccess();
			string sql = "Insert into Sport (name, status, creator_account_id) values (@name, @status, @accountid);";
			int affectedRows = await _data.SaveData(sql, new { name = newSport.Name, status = newSport.Status.ToString(), accountid = newSport.Creator_Account_Id });
			return affectedRows != 0;
		}
		public async Task<bool> UpdateSport(Sport oldSport)
		{
			IDataAccess _data = new DataAccess();
			string sql = "Update Sport set name = @name, status = @status, creator_Account_Id = @userid where id = @colid ;";
			int affectedRows = await _data.SaveData(sql, new { name = oldSport.Name, status = oldSport.Status.ToString(), userid = oldSport.Creator_Account_Id, colid = oldSport.Id });
			return affectedRows != 0;
		}
		public async Task<bool> DeleteSport(Sport oldSport)
		{
			oldSport.Status = SportStatus.Deleted;
			return await UpdateSport(oldSport);
		}


		public async Task<bool> InsertAccountDoesSport(Account_does_Sport newSport)
		{
			IDataAccess _data = new DataAccess();
			string sql = "Insert into Account_does_Sport (account_id, sport_id) values (@accountid, @sportid);";
			int affectedRows = await _data.SaveData(sql, new { accountid = newSport.Account_Id, sportid = newSport.Sport_Id });
			return affectedRows != 0;
		}
		public async Task<bool> DeleteAccountDoesSport(Account_does_Sport oldSport)
		{
			IDataAccess _data = new DataAccess();
			string sql;
			int affectedRows;

			var workouts = await GetWorkouts(oldSport);
			var routines = await GetRoutines(oldSport);

			foreach (var routine in routines)
			{
				sql = "Delete from Routine where id = @postid;";
				affectedRows = await _data.SaveData(sql, new { postid = routine.Id });
				if(affectedRows == 0)
					return false;
			}

			foreach (var workout in workouts)
			{
				bool isCorrect = await DeleteWorkout(workout);
				if (!isCorrect)
					return isCorrect;
			}

			sql = "Delete from Account_does_Sport where id = @postid;";
			affectedRows = await _data.SaveData(sql, new { postid = oldSport.Id });
			return affectedRows != 0;
		}


		public async Task<bool> InsertRoutine(Routine newRoutine)
		{
			IDataAccess _data = new DataAccess();
			string sql = "Insert into Routine (name, notes, status, Account_does_Sport_Id) values (@name, @notes, @status, @aid);";
			int affectedRows = await _data.SaveData(sql, new { name = newRoutine.Name, notes = newRoutine.Notes, status = newRoutine.Status.ToString(), aid = newRoutine.Account_does_Sport_Id });
			return affectedRows != 0;
		}
		public async Task<bool> UpdateRoutine(Routine oldRoutine)
		{
			IDataAccess _data = new DataAccess();
			string sql = "Update Routine set name = @name, notes = @notes, status = @status, Account_does_Sport_Id = @aid where id = @colid ;";
			int affectedRows = await _data.SaveData(sql, new { name = oldRoutine.Name, notes = oldRoutine.Notes, status = oldRoutine.Status.ToString(), aid = oldRoutine.Account_does_Sport_Id, colid = oldRoutine.Id });
			return affectedRows != 0;
		}
		public async Task<bool> DeleteRoutine(Routine oldRoutine)
		{
			oldRoutine.Status = SportStatus.Deleted;
			return await UpdateRoutine(oldRoutine);
		}


		public async Task<bool> InsertExercise(Exercise newExercise)
		{
			IDataAccess _data = new DataAccess();
			string sql = "Insert into Exercise (name, notes, rest, type, status, creator_account_id, sport_id) values (@name, @notes, @rest, @type, @status, @accountid, @sid);";
			int affectedRows = await _data.SaveData(sql, new { name = newExercise.Name, notes = newExercise.Notes, rest = newExercise.Rest.ToString(@"hh\:mm\:ss"), type = newExercise.Type.ToString(), status = newExercise.Status.ToString(), accountid = newExercise.Creator_Account_Id, sid = newExercise.Sport_Id });
			return affectedRows != 0;
		}
		public async Task<bool> UpdateExercise(Exercise oldExercise)
		{
			IDataAccess _data = new DataAccess();
			string sql = "Update Exercise set name = @name, notes = @notes, rest = @rest, type = @type, status = @status, creator_Account_Id = @userid, sport_id = @sid where id = @colid;";
			int affectedRows = await _data.SaveData(sql, new { name = oldExercise.Name, notes = oldExercise.Notes, rest = oldExercise.Rest.ToString(@"hh\:mm\:ss"), type = oldExercise.Type.ToString(), status = oldExercise.Status.ToString(), userid = oldExercise.Creator_Account_Id, sid = oldExercise.Sport_Id, colid = oldExercise.Id });
			return affectedRows != 0;
		}
		public async Task<bool> DeleteExercise(Exercise oldExercise)
		{
			oldExercise.Status = SportStatus.Deleted;
			return await UpdateExercise(oldExercise);
		}


		public async Task<bool> InsertSet(Sets newSet)
		{
			IDataAccess _data = new DataAccess();
			string sql = "Insert into Sets (isDone, type, reps, RPE, weight, length, distance, Exercise_id, Workout_id) values (@isDone, @type, @reps, @rpe, @weight, @length, @distance, @exid, @workid);";
			int affectedRows = await _data.SaveData(sql, 
				new { 
					isDone = newSet.IsDone, 
					type = newSet.Type.ToString(), 
					reps = newSet.Reps, 
					rpe = newSet.RPE, 
					weight = newSet.Weight, 
					length = newSet.Length.ToString(@"hh\:mm\:ss"), 
					distance = newSet.Distance, 
					exid = newSet.Exercise_Id, 
					workid = newSet.Workout_Id 
				});
			return affectedRows != 0;
		}
		public async Task<bool> UpdateSet(Sets oldSet)
		{
			IDataAccess _data = new DataAccess();
			string sql = "Update Sets set isDone = @isDone, type = @type, reps = @reps, RPE = @rpe, weight = @weight, length = @length, distance = @distance, Exercise_id = @exid, Workout_id = @workid where id = @colid ;";
			int affectedRows = await _data.SaveData(sql,
				new
				{
					isDone = oldSet.IsDone,
					type = oldSet.Type.ToString(),
					reps = oldSet.Reps,
					rpe = oldSet.RPE,
					weight = oldSet.Weight,
					length = oldSet.Length.ToString(@"hh\:mm\:ss"),
					distance = oldSet.Distance,
					exid = oldSet.Exercise_Id,
					workid = oldSet.Workout_Id,
					colid = oldSet.Id
				});
			return affectedRows != 0;
		}
		public async Task<bool> DeleteSet(Sets oldSet)
		{
			IDataAccess _data = new DataAccess();
			string sql = "Delete from Sets where id = @postid;";
			int affectedRows = await _data.SaveData(sql, new { postid = oldSet.Id });
			return affectedRows != 0;
		}


		public async Task<bool> InsertWorkout(Workout newWorkout)
		{
			IDataAccess _data = new DataAccess();
			string sql = "Insert into Workout (isDone, starttime, finishtime, notes, isroutineexample, routine_id, account_does_sport_id) values (@isDone, @starttime, @finishtime, @notes, @isroutine, @rid, @aid);";
			int affectedRows = await _data.SaveData(sql, new { isdone = newWorkout.IsDone, starttime = newWorkout.Starttime.ToString("yyyy-MM-dd hh:mm:ss"), finishtime = newWorkout.Finishtime.ToString("yyyy-MM-dd hh:mm:ss"), notes = newWorkout.Notes, isroutine = newWorkout.IsRoutineExample, rid = newWorkout.Routine_Id, aid = newWorkout.Account_does_Sport_Id });
			return affectedRows != 0;
		}
		public async Task<bool> UpdateWorkout(Workout oldWorkout)
		{
			IDataAccess _data = new DataAccess();
			string sql = "Update Workout set isDone = @isdone, starttime = @starttime, finishtime= @finishtime, notes = @notes, isroutineexample = @isroutine, routine_id = @rid, account_does_sport_id = @aid where id = @colid ;";
			int affectedRows = await _data.SaveData(sql, new { isdone = oldWorkout.IsDone, starttime = oldWorkout.Starttime.ToString("yyyy-MM-dd hh:mm:ss"), finishtime = oldWorkout.Finishtime.ToString("yyyy-MM-dd hh:mm:ss"), notes = oldWorkout.Notes, isroutine = oldWorkout.IsRoutineExample, rid = oldWorkout.Routine_Id, aid = oldWorkout.Account_does_Sport_Id, colid = oldWorkout.Id });
			return affectedRows != 0;
		}
		public async Task<bool> DeleteWorkout(Workout oldWorkout)
		{
			IDataAccess _data = new DataAccess();

			var sets = await GetSetsByWorkout(oldWorkout.Id);
			foreach (var set in sets)
			{
				bool isCorrect = await DeleteSet(set);
				if (!isCorrect)
					return isCorrect;
			}

			string sql = "Delete from Workout where id = @postid;";
			int affectedRows = await _data.SaveData(sql, new { postid = oldWorkout.Id });
			return affectedRows != 0;
		}


		public async Task<List<Sport>> GetAllSports(int accountId)
		{
			IDataAccess _data = new DataAccess();

			string sql = $"Select * from Sport;";
			var allSport = await _data.LoadData<Sport, dynamic>(sql, new { });
			return allSport.Where(s => s.Status == SportStatus.Public || (s.Creator_Account_Id == accountId && s.Status == SportStatus.Private)).ToList();
		}
		public async Task<List<Sport>> GetAccountsSports(int accountId)
		{
			IDataAccess _data = new DataAccess();
			List<Sport> sports = new List<Sport>();

			var accountDoesSports = await GetAccountDoesSports(accountId);
			foreach (var a in accountDoesSports)
			{
				string sql = $"Select * from Sport where id = @id;";
				var list = await _data.LoadData<Sport, dynamic>(sql, new { id = a.Sport_Id });
				var sport = list.FirstOrDefault();
				if( sport != null )
					sports.Add(sport);
			}
			return sports;
		}

		public async Task<List<Account_does_Sport>> GetAccountDoesSport(int accountId, int sportId)
		{
			IDataAccess _data = new DataAccess();

			string sql = $"Select * from Account_does_Sport where (Account_Id = @aid AND  Sport_Id = @sid);";
			return await _data.LoadData<Account_does_Sport, dynamic>(sql, new { aid = accountId, sid = sportId });
		}
		public async Task<List<Account_does_Sport>> GetAccountDoesSport(int accountDoesSportId)
		{
			IDataAccess _data = new DataAccess();

			string sql = $"Select * from Account_does_Sport where Id = @id;";
			return await _data.LoadData<Account_does_Sport, dynamic>(sql, new { id = accountDoesSportId });
		}
		public async Task<List<Account_does_Sport>> GetAccountDoesSports(int accountId)
		{
			IDataAccess _data = new DataAccess();

			string sql = $"Select * from Account_does_Sport where Account_Id = @id;";
			return await _data.LoadData<Account_does_Sport, dynamic>(sql, new { id = accountId });
		}

		public async Task<List<Routine>> GetRoutine(int routineId)
		{
			IDataAccess _data = new DataAccess();

			string sql = $"Select * from Routine where Id = @id;";
			return await _data.LoadData<Routine, dynamic>(sql, new { id = routineId });
		}
		public async Task<List<Routine>> GetRoutines(Account_does_Sport accountDoesSport)
		{
			IDataAccess _data = new DataAccess();

			string sql = $"Select * from Routine where Account_does_Sport_Id = @id;";
			return await _data.LoadData<Routine, dynamic>(sql, new { id = accountDoesSport.Id });
		}
		public async Task<List<Exercise>> GetExercise(int exerciseId)
		{
			IDataAccess _data = new DataAccess();

			string sql = $"Select * from Exercise where Sport_id = @id;";
			return await _data.LoadData<Exercise, dynamic>(sql, new { id = exerciseId });
		}
		public async Task<List<Exercise>> GetExercises(int accountId, int sportId)
		{
			IDataAccess _data = new DataAccess();

			string sql = $"Select * from Exercise where Sport_id = @id;";
			var allExercise = await _data.LoadData<Exercise, dynamic>(sql, new { id = sportId });
			return allExercise.Where(e => e.Status == SportStatus.Public || (e.Creator_Account_Id == accountId && e.Status == SportStatus.Private)).ToList();
		}

		public async Task<List<Workout>> GetWorkout(int workoutId)
		{
			IDataAccess _data = new DataAccess();

			string sql = $"Select * from Workout where Id = @id;";
			return await _data.LoadData<Workout, dynamic>(sql, new { id = workoutId});
		}
		public async Task<List<Workout>> GetWorkouts(Account_does_Sport accountDoesSport)
		{
			IDataAccess _data = new DataAccess();

			string sql = $"Select * from Workout where Account_does_Sport_Id = @id;";
			return await _data.LoadData<Workout, dynamic>(sql, new { id = accountDoesSport.Id });
		}

		public async Task<List<Sets>> GetSetsByExercise(int exerciseId)
		{
			IDataAccess _data = new DataAccess();

			string sql = $"Select * from Sets where Exercise_Id = @eid ;";
			return await _data.LoadData<Sets, dynamic>(sql, new { eid = exerciseId });
		}

		public async Task<List<Sets>> GetSetsByWorkout(int workoutId)
		{
			IDataAccess _data = new DataAccess();

			string sql = $"Select * from Sets where Workout_Id = @wid;";
			return await _data.LoadData<Sets, dynamic>(sql, new { wid = workoutId });
		}
	}
}
