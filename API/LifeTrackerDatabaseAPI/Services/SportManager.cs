using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeTrackerDatabaseAPI.Intefaces;
using LifeTrackerDatabaseAPI.Models;
using Microsoft.AspNetCore.Components;
using Mysqlx.Session;
using Org.BouncyCastle.Asn1;

namespace LifeTrackerDatabaseAPI.Services
{
	public class SportManager(IDataAccess data) : ISportManager
	{
		[Inject] protected IDataAccess Data { get; set; } = data;

		public async Task<bool> InsertSport(Sport newSport)
		{
			string sql = "Insert into Sport (name, status, creator_account_id) values (@name, @status, @accountid);";
			int affectedRows = await Data.SaveData(sql, new { name = newSport.Name, status = newSport.Status.ToString(), accountid = newSport.Creator_Account_Id });
			return affectedRows != 0;
		}
		public async Task<bool> UpdateSport(Sport oldSport)
		{
			string sql = "Update Sport set name = @name, status = @status, creator_Account_Id = @userid where id = @colid ;";
			int affectedRows = await Data.SaveData(sql, new { name = oldSport.Name, status = oldSport.Status.ToString(), userid = oldSport.Creator_Account_Id, colid = oldSport.Id });
			return affectedRows != 0;
		}
		public async Task<bool> DeleteSport(int accountId, Sport oldSport)
		{
			var exercises = await GetExercises(accountId, oldSport.Id);
			foreach (var ex in exercises)
			{
				bool isCorrect = await DeleteExercise(ex);
				if (!isCorrect)
					return isCorrect;
			}

			oldSport.Status = SportStatus.Deleted;
			return await UpdateSport(oldSport);
		}


		public async Task<bool> InsertAccountDoesSport(Account_does_Sport newSport)
		{
			string sql = "Insert into Account_does_Sport (account_id, sport_id) values (@accountid, @sportid);";
			int affectedRows = await Data.SaveData(sql, new { accountid = newSport.Account_Id, sportid = newSport.Sport_Id });
			return affectedRows != 0;
		}
		public async Task<bool> DeleteAccountDoesSport(Account_does_Sport oldSport)
		{
			string sql;
			int affectedRows;

			var workouts = await GetWorkouts(oldSport.Id, false);
			workouts.AddRange(await GetWorkouts(oldSport.Id, true));

			var routines = await GetRoutines(oldSport.Id);

			foreach (var routine in routines)
			{
				sql = "Delete from Routine where id = @postid;";
				affectedRows = await Data.SaveData(sql, new { postid = routine.Id });
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
			affectedRows = await Data.SaveData(sql, new { postid = oldSport.Id });
			return affectedRows != 0;
		}


		public async Task<bool> InsertRoutine(Routine newRoutine)
		{
			string sql = "Insert into Routine (status, Account_does_Sport_Id, ExampleWorkout_Id) values (@status, @aid, @eid);";
			int affectedRows = await Data.SaveData(sql, new { status = newRoutine.Status.ToString(), aid = newRoutine.Account_does_Sport_Id, eid = newRoutine.ExampleWorkout_Id });
			return affectedRows != 0;
		}
		public async Task<bool> UpdateRoutine(Routine oldRoutine)
		{
			string sql = "Update Routine set status = @status, Account_does_Sport_Id = @aid, ExampleWorkout_Id = @eid where id = @colid ;";
			int affectedRows = await Data.SaveData(sql, new { status = oldRoutine.Status.ToString(), aid = oldRoutine.Account_does_Sport_Id, eid = oldRoutine.ExampleWorkout_Id, colid = oldRoutine.Id });
			return affectedRows != 0;
		}
		public async Task<bool> DeleteRoutine(Routine oldRoutine)
		{
			oldRoutine.Status = SportStatus.Deleted;
			return await UpdateRoutine(oldRoutine);
		}


		public async Task<bool> InsertExercise(Exercise newExercise)
		{
			string sql = "Insert into Exercise (name, notes, type, status, creator_account_id, sport_id) values (@name, @notes, @type, @status, @accountid, @sid);";
			int affectedRows = await Data.SaveData(sql, new { name = newExercise.Name, notes = newExercise.Notes, type = newExercise.Type.ToString(), status = newExercise.Status.ToString(), accountid = newExercise.Creator_Account_Id, sid = newExercise.Sport_Id });
			return affectedRows != 0;
		}
		public async Task<bool> UpdateExercise(Exercise oldExercise)
		{
			string sql = "Update Exercise set name = @name, notes = @notes, type = @type, status = @status, creator_Account_Id = @userid, sport_id = @sid where id = @colid;";
			int affectedRows = await Data.SaveData(sql, new { name = oldExercise.Name, notes = oldExercise.Notes, type = oldExercise.Type.ToString(), status = oldExercise.Status.ToString(), userid = oldExercise.Creator_Account_Id, sid = oldExercise.Sport_Id, colid = oldExercise.Id });
			return affectedRows != 0;
		}
		public async Task<bool> DeleteExercise(Exercise oldExercise)
		{
			oldExercise.Status = SportStatus.Deleted;
			return await UpdateExercise(oldExercise);
		}


		public async Task<bool> InsertSet(Sets newSet)
		{
			TimeSpan len = new(newSet.LengthHours, newSet.LengthMinutes, newSet.LengthSeconds);
			string lenStr = len.ToString(@"hh\:mm\:ss");

			string sql = "Insert into Sets (workoutindex, isDone, type, reps, RPE, weight, length, distance, Exercise_id, Workout_id) values (@workoutindex, @isDone, @type, @reps, @rpe, @weight, @length, @distance, @exid, @workid);";
			int affectedRows = await Data.SaveData(sql, 
				new { 
					workoutindex = newSet.Workoutindex,
					isDone = newSet.IsDone, 
					type = newSet.Type.ToString(), 
					reps = newSet.Reps, 
					rpe = newSet.RPE, 
					weight = newSet.Weight, 
					length = lenStr, 
					distance = newSet.Distance, 
					exid = newSet.Exercise_Id, 
					workid = newSet.Workout_Id 
				});
			return affectedRows != 0;
		}
		public async Task<bool> UpdateSet(Sets oldSet)
		{
			TimeSpan len = new(oldSet.LengthHours, oldSet.LengthMinutes, oldSet.LengthSeconds);
			string lenStr = len.ToString(@"hh\:mm\:ss");
			string sql = "Update Sets set workoutindex = @workoutindex, isDone = @isDone, type = @type, reps = @reps, RPE = @rpe, weight = @weight, length = @length, distance = @distance, Exercise_id = @exid, Workout_id = @workid where id = @colid ;";
			int affectedRows = await Data.SaveData(sql,
			new
			{
					workoutindex = oldSet.Workoutindex,
					isDone = oldSet.IsDone,
					type = oldSet.Type.ToString(),
					reps = oldSet.Reps,
					rpe = oldSet.RPE,
					weight = oldSet.Weight,
					length = lenStr,
					distance = oldSet.Distance,
					exid = oldSet.Exercise_Id,
					workid = oldSet.Workout_Id,
					colid = oldSet.Id
				});
			return affectedRows != 0;
		}
		public async Task<bool> DeleteSet(int id)
		{
			string sql = "Delete from Sets where id = @postid;";
			int affectedRows = await Data.SaveData(sql, new { postid = id });
			return affectedRows != 0;
		}


		public async Task<bool> InsertWorkout(Workout newWorkout)
		{
			string sql = "Insert into Workout (name, isDone, starttime, duration, notes, isroutineexample, routine_id, account_does_sport_id) values (@name, @isDone, @starttime, @duration, @notes, @isroutine, @rid, @aid);";
			int affectedRows = await Data.SaveData(sql, new { name = newWorkout.Name, isdone = newWorkout.IsDone, starttime = newWorkout.Starttime.ToString("yyyy-MM-dd HH:mm:ss"), duration = newWorkout.Duration.ToString(@"hh\:mm\:ss"), notes = newWorkout.Notes, isroutine = newWorkout.IsRoutineExample, rid = newWorkout.Routine_Id, aid = newWorkout.Account_does_Sport_Id });
			return affectedRows != 0;
		}
		public async Task<bool> UpdateWorkout(Workout oldWorkout)
		{
			string sql = "Update Workout set name = @name, isDone = @isdone, starttime = @starttime, duration= @duration, notes = @notes, isroutineexample = @isroutine, routine_id = @rid, account_does_sport_id = @aid where id = @colid ;";
			int affectedRows = await Data.SaveData(sql, new { name = oldWorkout.Name, isdone = oldWorkout.IsDone, starttime = oldWorkout.Starttime.ToString("yyyy-MM-dd HH:mm:ss"), duration = oldWorkout.Duration.ToString(@"hh\:mm\:ss"), notes = oldWorkout.Notes, isroutine = oldWorkout.IsRoutineExample, rid = oldWorkout.Routine_Id, aid = oldWorkout.Account_does_Sport_Id, colid = oldWorkout.Id });
			return affectedRows != 0;
		}
		public async Task<bool> DeleteWorkout(Workout oldWorkout)
		{
			var sets = await GetSetsByWorkout(oldWorkout.Id);
			foreach (var set in sets)
			{
				bool isCorrect = await DeleteSet(set.Id);
				if (!isCorrect)
					return isCorrect;
			}

			string sql = "Delete from Workout where id = @postid;";
			int affectedRows = await Data.SaveData(sql, new { postid = oldWorkout.Id });
			return affectedRows != 0;
		}


		public async Task<List<Sport>> GetAllSports(int accountId)
		{
			string sql = $"Select * from Sport;";
			var allSport = await Data.LoadData<Sport, dynamic>(sql, new { });
			return allSport.Where(s => s.Status == SportStatus.Public || (s.Creator_Account_Id == accountId && s.Status == SportStatus.Private)).ToList();
		}
		public async Task<List<Sport>> GetAccountsSports(int accountId)
		{
			List<Sport> sports = [];

			var accountDoesSports = await GetAccountDoesSports(accountId);
			foreach (var a in accountDoesSports)
			{
				string sql = $"Select * from Sport where id = @id;";
				var list = await Data.LoadData<Sport, dynamic>(sql, new { id = a.Sport_Id });
				var sport = list.FirstOrDefault();
				if( sport != null )
					sports.Add(sport);
			}
			return sports;
		}

		public async Task<List<Account_does_Sport>> GetAccountDoesSport(int accountId, int sportId)
		{
			string sql = $"Select * from Account_does_Sport where (Account_Id = @aid AND  Sport_Id = @sid);";
			return await Data.LoadData<Account_does_Sport, dynamic>(sql, new { aid = accountId, sid = sportId });
		}
		public async Task<List<Account_does_Sport>> GetAccountDoesSport(int accountDoesSportId)
		{
			string sql = $"Select * from Account_does_Sport where Id = @id;";
			return await Data.LoadData<Account_does_Sport, dynamic>(sql, new { id = accountDoesSportId });
		}
		public async Task<List<Account_does_Sport>> GetAccountDoesSports(int accountId)
		{
			string sql = $"Select * from Account_does_Sport where Account_Id = @id;";
			return await Data.LoadData<Account_does_Sport, dynamic>(sql, new { id = accountId });
		}

		public async Task<List<Routine>> GetRoutine(int routineId)
		{
			string sql = $"Select * from Routine where Id = @id;";
			return await Data.LoadData<Routine, dynamic>(sql, new { id = routineId });
		}
		public async Task<List<Routine>> GetRoutines(int accountDoesSportId)
		{
			string sql = $"Select * from Routine where Account_does_Sport_Id = @id;";
			var allRoutines = await Data.LoadData<Routine, dynamic>(sql, new { id = accountDoesSportId });
			return allRoutines.Where(r => r.Status != SportStatus.Deleted).ToList();
		}

		public async Task<List<Workout>> GetRoutineExample(int accountDoesId, int RoutineId)
		{
			var list = await GetWorkouts(accountDoesId, true);
			return list.Where(w => w.Routine_Id == RoutineId).ToList();
		}

		public async Task<List<Exercise>> GetExercise(int exerciseId)
		{
			string sql = $"Select * from Exercise where id = @id;";
			return await Data.LoadData<Exercise, dynamic>(sql, new { id = exerciseId });
		}
		public async Task<List<Exercise>> GetExercises(int accountId, int sportId)
		{
			string sql = $"Select * from Exercise where Sport_id = @id;";
			var allExercise = await Data.LoadData<Exercise, dynamic>(sql, new { id = sportId });
			return allExercise.Where(e => e.Status == SportStatus.Public || (e.Creator_Account_Id == accountId && e.Status == SportStatus.Private)).ToList();
		}

		public async Task<List<Workout>> GetWorkout(int workoutId)
		{
			string sql = $"Select * from Workout where Id = @id;";
			return await Data.LoadData<Workout, dynamic>(sql, new { id = workoutId});
		}
		public async Task<List<Workout>> GetWorkouts(int accountDoesSportId, bool onlyExamples)
		{
			string sql = $"Select * from Workout where Account_does_Sport_Id = @id;";
			var allWorkouts = await Data.LoadData<Workout, dynamic>(sql, new { id = accountDoesSportId });
			return allWorkouts.Where(w => w.IsRoutineExample == onlyExamples).ToList();
		}

		public async Task<List<Sets>> GetSetsByExercise(int accountDoesSportId, int exerciseId)
		{
			string sql = $"Select * from Sets where Exercise_Id = @eid;";

			var sets = await Data.LoadData<Sets, dynamic>(sql, new { eid = exerciseId });
			List<int> workoutIds = sets.Select(s => s.Workout_Id).Distinct().ToList();

			List<Sets> retVal = [];
			foreach (int workoutId in workoutIds)
			{
				Workout workout = (await GetWorkout(workoutId)).First();
				if (workout.Account_does_Sport_Id == accountDoesSportId)
				{
					retVal.AddRange(sets.Where(s => s.Workout_Id == workoutId));
				}
			}
			return retVal;
		}

		public async Task<List<Sets>> GetSetsByWorkout(int workoutId)
		{
			string sql = $"Select * from Sets where Workout_Id = @wid;";
			var sets = await Data.LoadData<Sets, dynamic>(sql, new { wid = workoutId });
			return sets = sets.OrderBy(s => s.Workoutindex).ToList();
		}

		public async Task<List<Sets>> GetSetsByBoth(int exerciseId, int workoutId)
		{
			string sql = $"Select * from Sets where Workout_Id = @wid and  Exercise_Id = @eid ;";
			return await Data.LoadData<Sets, dynamic>(sql, new { wid = workoutId, eid = exerciseId });
		}
	}
}
